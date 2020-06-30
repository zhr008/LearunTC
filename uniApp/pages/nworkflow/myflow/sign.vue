<template>
  <view>
    <l-label v-if="type === 'sign'" @click="selectStaff" arrow>
      <template #title>
        <text style="color:red;font-size: 1.2em;">*</text>
        加签人员
      </template>
      {{ displayStaff }}
    </l-label>

    <view class="cu-form-group" style="border-bottom: none"><view class="title">备注</view></view>
    <l-textarea v-model="remark" textareaStyle="margin-top:0" :placeholder="`输入${typeText}备注`" />

    <view class="padding-lr margin-tb">
      <l-button class="block" @click="submit" block size="lg" color="green">提交流程{{ typeText }}</l-button>
    </view>
  </view>
</template>

<script>
import _ from 'lodash'

export default {
  data() {
    return {
      type: 'sign',
      typeText: '',

      staff: '',
      remark: '',
      taskParam: {}
    }
  },

  async onLoad({ type }) {
    await this.init(type)
  },

  methods: {
    async init(type = 'sign') {
      this.type = type
      this.typeText = type === 'sign' ? '加签' : '审核'

      this.taskParam = this.getPageParam()
    },

    selectStaff() {
      uni.$once(`select-user`, data => {
        this.staff = data.id
      })
      uni.navigateTo({ url: `/pages/common/select-user` })
    },

    async submit() {
      if (this.type === 'sign' && !this.staff) {
        uni.showModal({ title: '请补全必填项', content: '必须指定一个加签用户。', showCancel: false })

        return
      }

      const postData =
        this.type === 'sign'
          ? { ...this.taskParam, des: this.remark, userId: this.staff }
          : { ...this.taskParam, des: this.remark }

      const [err, { data: result }] = await uni.request({
        url: this.type === 'sign' ? this.apiRoot`/newwf/sign` : this.apiRoot`/newwf/audit`,
        method: 'POST',
        header: { 'content-type': 'application/x-www-form-urlencoded' },
        data: { ...this.auth, data: JSON.stringify(postData) }
      })

      if (err || result.code !== 200) {
        uni.showToast({ title: `${this.typeText}请求失败`, icon: 'none' })
      }

      uni.$emit('task-list-change')
      uni.navigateBack({ delta: 2 })
      uni.showToast({ title: `已成功提交${this.typeText}`, icon: 'success' })
    }
  },

  computed: {
    displayStaff() {
      return _.get(this.$store.state.staff, `${this.staff}.name`, '请选择加签人员')
    }
  }
}
</script>
