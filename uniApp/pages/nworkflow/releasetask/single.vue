<template>
  <view class="page">
    <l-custom-form v-if="ready" ref="form" :scheme="scheme" :initFormValue="formValue" />

    <view v-if="ready && type !== 'again'">
      <view style="height:15px"></view>
      <l-input v-model="title" v-if="needTitle" required right title="流程标题" placeholder="请为流程设置一个标题" />
      <l-select v-model="level" :range="levelRange" title="流程优先级" required placeholder="请选择一个优先级" />
    </view>

    <view v-if="ready" class="padding-lr margin-tb">
      <l-button v-if="type !== 'again'" class="block" @click="draft" block size="lg" color="orange">保存草稿</l-button>
      <l-button class="block margin-top" @click="submit" block size="lg" color="green">
        {{ type === 'again' ? `重新发起流程` : `发起流程` }}
      </l-button>
    </view>
  </view>
</template>

<script>
import { guid } from '@/common/utils.js'
import customFormMixins from '@/common/custom-form.js'
import LCustomForm from '@/components/learun-app/custom-form.vue'

export default {
  data() {
    return {
      ready: false,
      type: 'create',

      processId: '',
      processInfo: null,
      currentNode: null,

      formValue: {},
      scheme: [],
      formData: null,

      level: '0',
      levelRange: [{ value: '0', text: '普通' }, { value: '1', text: '重要' }, { value: '2', text: '紧急' }],
      title: '',
      needTitle: false
    }
  },

  components: { LCustomForm },

  mixins: [customFormMixins],

  async onLoad({ type }) {
    await this.init(type)
  },

  methods: {
    async init(type = 'create') {
      uni.showLoading({ title: `加载表单中...`, mask: true })
      this.type = type

      const currentTask = this.getPageParam()
      const title = {
        create: `创建[${currentTask.F_Name}]流程`,
        draft: `编辑[${currentTask.F_Title || currentTask.F_SchemeName}]流程草稿`,
        again: `重新发起[${currentTask.F_Title}]流程`
      }[this.type]
      uni.setNavigationBarTitle({ title })

      const { F_Code: code, F_Id: processId } = currentTask
      this.processId = this.type === 'create' ? guid('-') : processId
      this.processInfo = await this.fetchProcessInfo({ code, processId: this.type === 'create' ? null : processId })
      this.currentNode = this.getCurrentNode(this.processInfo)

      this.needTitle = this.type !== 'again' && Number(this.currentNode.isTitle) === 1

      if (this.type !== 'create') {
        this.formData = await this.fetchFormData(this.currentNode, this.processId)
      }

      const schemeData = await this.fetchSchemeData(this.currentNode)
      const { formValue, scheme } = await this.getCustomForm({
        schemeData,
        processId: this.processId,
        formData: this.formData,
        currentNode: this.currentNode,
        code: this.type === 'again' ? null : code,
        useDefault: true
      })
      this.scheme = scheme
      this.formValue = formValue
      
      this.ready = true
      uni.hideLoading()
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

                uni.$emit('task-list-change')
                uni.navigateBack()
                uni.showToast({ title: `草稿已保存`, icon: 'success' })
              })
          }
        }
      })
    },

    // 发起流程按钮
    async submit() {
      const isAgain = this.type === 'again'

      const verifyResult = this.verifyValue()
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
        content: `确定要${isAgain ? '重新' : ''}发起流程吗？`,
        success: async ({ confirm }) => {
          if (confirm) {
            uni.showLoading({ title: '正在提交...', mask: true })

            const formValue = this.$refs.form.getFormValue()
            const postData = await this.getPostData(formValue, this.scheme)
            postData.title = this.title
            postData.level = this.level
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

    // 获取表单验证结果，是一个包含错误信息的数组，长度为 0 则没有错误
    verifyValue() {
      const errList = this.$refs.form.verifyValue()
      if (this.needTitle && !this.title) {
        errList.push(`流程的标题不能为空`)
      }

      return errList
    }
  }
}
</script>
