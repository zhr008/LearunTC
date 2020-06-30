<template>
  <view class="page">
    <l-nav
      v-model="tab"
      :items="['表单信息', `${type === 'child' ? '父' : ''}流程信息`]"
      type="flex"
      class="solid-bottom"
    />

    <view v-if="ready && tab === 0" class="form">
      <l-custom-form ref="form" :editMode="editMode" :scheme="scheme" :initFormValue="formValue" />

      <!-- 催办/撤销，适用于打开我的任务 -->
      <view v-if="taskType === 'my' && (canUrge || canRevoke)" class="form-action padding bg-white margin-top">
        <l-button v-if="canUrge" @click="urge" class="block margin-top" size="lg" color="orange" block>
          催办审核
        </l-button>
        <l-button v-if="canRevoke" @click="revoke" class="block margin-top" size="lg" color="red" block>
          撤销流程
        </l-button>
      </view>

      <!-- 按钮列表，适用于打开待办任务 -->
      <view v-if="taskType === 'pre' && buttonList.length > 0" class="form-action padding bg-white margin-top">
        <l-button
          v-for="button of buttonList"
          @click="taskAction(button)"
          :key="button.id"
          :color="getButtonColor(button)"
          class="block margin-top"
          size="lg"
          block
        >
          {{ button.name }}
        </l-button>
      </view>

      <!-- 子流程草稿/提交按钮 -->
      <view v-if="taskType === 'child'" class="form-action padding bg-white margin-top">
        <l-button @click="draft" class="block margin-top" size="lg" color="orange" block>保存草稿</l-button>
        <l-button @click="submit" class="block margin-top" size="lg" color="green" block>发起子流程</l-button>
      </view>
    </view>

    <!-- 流程图 -->
    <view v-if="ready && tab === 1" class="progress">
      <l-timeline title="当前" style="min-height: 100vh; background-color: #FFF">
        <l-timeline-item v-if="currentTask && currentTask.F_IsFinished" contentStyle="padding:10px" color="blue">
          <text class="text-blod">结束</text>
        </l-timeline-item>

        <l-timeline-item
          v-for="processItem of processList"
          :key="processItem.F_Id"
          contentStyle="padding:10px 13px"
          color="grey"
        >
          <view
            v-if="processItem.F_NodeName"
            style="font-size: 1.2em;padding-bottom:2px;margin-bottom: 6px;border-bottom: solid 1px #fff;"
          >
            {{ processItem.F_NodeName }}
          </view>
          <view>
            <text class="text-bold">{{ processItem.F_CreateUserName || '「系统」' }}</text>
            ： {{ processItem.F_OperationName }}
          </view>
          <view v-if="processItem.F_Des">
            <text class="text-bold">审批意见</text>
            ： {{ processItem.F_Des }}
          </view>
          <view style="font-size: 0.8em;margin-top:3px">{{ processItem.F_CreateDate }}</view>
        </l-timeline-item>

        <l-timeline-item contentStyle="padding:10px" color="green"><text class="text-blod">起步</text></l-timeline-item>
      </l-timeline>
    </view>
  </view>
</template>

<script>
import _ from 'lodash'
import customFormMixins from '@/common/custom-form.js'
import LCustomForm from '@/components/learun-app/custom-form.vue'

export default {
  data() {
    return {
      tab: 0,
      editMode: false,
      type: 'view',
      ready: false,

      currentTask: null,
      processList: [],

      processId: null,
      processInfo: null,
      currentNode: null,

      parentProcessId: null,
      parentProcessInfo: null,
      parentCurrentNode: null,

      scheme: [],
      formValue: {}
    }
  },

  components: { LCustomForm },

  mixins: [customFormMixins],

  async onLoad({ type }) {
    await this.init(type)
  },

  methods: {
    async init(type = 'view') {
      this.type = type
      this.currentTask = this.getPageParam()

      uni.showLoading({ title: `加载表单中...`, mask: true })
      uni.setNavigationBarTitle({ title: this.currentTask.F_Title })

      if (this.type === 'child' && this.taskType !== 'maked') {
        this.editMode = true
      }

      // 获得流程信息
      this.processInfo = await this.fetchProcessInfo({
        processId: this.currentTask.F_Id,
        taskId: this.currentTask.F_TaskId
      })
      this.processId = this.currentTask.F_Id
      this.currentNode = this.getCurrentNode(this.processInfo)
      this.processList = _.get(this.processInfo, `info.TaskLogList`, [])

      if (this.type === 'child') {
        // 子流程的场合，获取父流程信息
        this.parentProcessInfo = await this.fetchProcessInfo({ processId: this.processId })
        this.parentProcessId = this.processInfo.info.childProcessId
        this.parentCurrentNode = this.getCurrentNode(this.parentProcessInfo)
        this.processList = _.get(this.parentProcessInfo, `info.TaskLogList`, [])

        const childFlowCode = this.currentNode.childFlow
        const childFlowNode = this.getCurrentNode(await this.fetchProcessInfo({ code: childFlowCode }))

        const schemeData = await this.fetchSchemeData(childFlowNode)
        const formData = await this.fetchFormData(childFlowNode, this.parentProcessId)
        const { formValue, scheme } = await this.getCustomForm({
          formData,
          schemeData,
          currentNode: childFlowNode,
          processId: this.parentProcessId,
          code: childFlowCode,
          useDefault: true
        })
        this.scheme = scheme
        this.formValue = formValue
      } else {
        // 不是子流程，可以直接渲染
        const schemeData = await this.fetchSchemeData(this.currentNode)
        const formData = await this.fetchFormData(this.currentNode, this.processId)

        const { formValue, scheme } = await this.getCustomForm({
          formData,
          schemeData,
          currentNode: this.currentNode,
          processId: this.processId,
          code: null
        })

        this.scheme = scheme
        this.formValue = formValue
      }

      this.ready = true
      uni.hideLoading()
    },

    // 催办
    async urge() {
      uni.showModal({
        title: '确认催办',
        content: '确定要催办审核吗？',
        success: ({ confirm }) => {
          if (confirm) {
            uni.showLoading({ title: '提交催办中...', mask: true })
            uni
              .request({
                url: this.apiRoot`/newwf/urge`,
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' },
                data: { ...this.auth, data: this.currentTask.F_Id }
              })
              .then(([err, result]) => {
                uni.hideLoading()
                if (err || result.data.code !== 200) {
                  uni.showToast({ title: `催办请求失败`, icon: 'none' })
                  return
                }

                uni.$emit('task-list-change')
                uni.showToast({ title: `已提交催办`, icon: 'success' })
              })
          }
        }
      })
    },

    // 撤销
    async revoke() {
      uni.showModal({
        title: '确认撤销',
        content: '确定要撤销流程吗？',
        success: ({ confirm }) => {
          if (confirm) {
            uni.showLoading({ title: '提交撤销中...', mask: true })
            uni
              .request({
                url: this.apiRoot`/newwf/revoke`,
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' },
                data: { ...this.auth, data: this.currentTask.F_Id }
              })
              .then(([err, result]) => {
                uni.hideLoading()
                if (err || result.data.code !== 200) {
                  uni.showToast({ title: `撤销请求失败`, icon: 'none' })
                  return
                }

                uni.navigateBack()
                uni.$emit('task-list-change')
                uni.showToast({ title: `已撤销流程`, icon: 'success' })
              })
          }
        }
      })
    },

    // 提交草稿按钮
    async draft() {
      uni.showModal({
        title: '提交确认',
        content: '确定要提交草稿吗？',
        success: async ({ confirm }) => {
          if (confirm) {
            uni.showLoading({ title: '正在提交...', mask: true })

            const formValue = this.$refs.form.getFormValue()
            const postData = await this.getPostData(formValue, this.scheme)

            uni
              .request({
                url: this.apiRoot`/newwf/draft`,
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' },
                data: { ...this.auth, data: JSON.stringify(postData) }
              })
              .then(([err, result]) => {
                uni.hideLoading()
                if (err || result.data.code !== 200) {
                  uni.showToast({ title: `保存失败`, icon: 'none' })
                  return
                }

                uni.navigateBack()
                uni.$emit('task-list-change')
                uni.showToast({ title: `草稿已保存`, icon: 'success' })
              })
          }
        }
      })
    },

    // 发起流程
    async submit() {
      const isAgain = this.type === 'again'

      const verifyResult = this.$refs.form.verifyValue()
      if (verifyResult.length > 0) {
        uni.showModal({
          title: '表单验证失败',
          content: verifyResult.join('\n'),
          showCancel: false
        })
        return
      }

      uni.showModal({
        title: '提交确认',
        content: `确定要发起子流程吗？`,
        success: async ({ confirm }) => {
          if (confirm) {
            uni.showLoading({ title: '正在提交...', mask: true })

            const formValue = this.$refs.form.getFormValue()
            const postData = await this.getPostData(formValue, this.scheme)
            postData.auditors = JSON.stringify({})

            uni
              .request({
                url: this.apiRoot(isAgain ? '/newwf/againcreate' : '/newwf/create'),
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' },
                data: { ...this.auth, data: JSON.stringify(postData) }
              })
              .then(([err, result]) => {
                uni.hideLoading()
                if (err || result.data.code !== 200) {
                  uni.showToast({ title: `流程${isAgain ? '重新' : ''}发起失败`, icon: 'none' })
                  return
                }

                uni.$emit('task-list-change')
                uni.navigateBack()
                uni.showToast({ title: `流程${isAgain ? '重新' : ''}发起成功`, icon: 'success' })
              })
          }
        }
      })
    },

    // 表单操作按钮
    taskAction(action) {
      const currentTask = this.processInfo.task.find(t => t.F_NodeId === this.currentNode.id)

      if (action.code === '__sign__') {
        this.setPageParam({
          processId: currentTask.F_ProcessId,
          taskId: currentTask.F_Id,
          formreq: JSON.stringify([])
        })
        uni.navigateTo({ url: `./sign?type=sign` })

        return
      }

      this.setPageParam({
        operationCode: action.code,
        operationName: action.name,
        processId: currentTask.F_ProcessId,
        taskId: currentTask.F_Id,
        formreq: JSON.stringify([]),
        auditors: JSON.stringify({})
      })
      uni.navigateTo({ url: `./sign?type=verify` })
    },

    // 获取按钮颜色
    getButtonColor({ code }) {
      return { agree: 'green', disagree: 'red', end: 'red' }[code] || 'blue'
    }
  },

  computed: {
    // 按钮渲染列表
    buttonList() {
      const btnList = _.get(this.currentNode, `btnList`, [])
      const isSign = _.get(this.currentNode, `isSign`, 0)
      if (this.taskType === 'pre' && Number(isSign) === 1) {
        btnList.push({
          id: '__sign__',
          code: '__sign__',
          name: '加签'
        })
      }

      return btnList
    },

    // 获取当前任务类别
    taskType() {
      if (this.type === 'child') {
        return 'child'
      }

      return _.get(this.currentTask, 'mark', 'unknow')
    },

    // 是否显示催办（已开始、未结束、未作废）
    canUrge() {
      if (!this.currentTask) {
        return false
      }

      return !this.currentTask.F_IsFinished && this.currentTask.F_EnabledMark !== 3
    },

    // 是否显示撤销（未开始）
    canRevoke() {
      if (!this.currentTask) {
        return false
      }

      return !this.currentTask.F_IsStart
    }
  }
}
</script>

<style lang="less" scoped>
.form-action {
  l-button {
    &:first-child {
      margin-top: 0;
    }
  }
}
</style>
