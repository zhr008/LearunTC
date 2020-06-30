<template>
  <view class="page">
    <l-title>修改密码</l-title>
    <l-input title="旧密码" v-model="oldPwd" placeholder="请输入旧密码" password></l-input>
    <l-input title="新的密码" v-model="newPwd" placeholder="请输入新密码" password></l-input>
    <l-input title="确认输入" v-model="confirmPwd" placeholder="请输入旧密码" password></l-input>

    <view class="padding"><l-button @click="ok" size="lg" block color="blue" class="block">确认修改</l-button></view>
  </view>
</template>

<script>
import md5 from 'js-md5'

export default {
  data() {
    return {
      oldPwd: '',
      newPwd: '',
      confirmPwd: ''
    }
  },

  methods: {
    async ok() {
      const { auth, oldPwd, newPwd, confirmPwd } = this
      if (oldPwd.length < 6) {
        uni.showModal({ title: '操作失败', content: '旧密码输入不正确，请重新确认。', showCancel: false })
      }
      if (newPwd.length < 6 || newPwd.length > 16) {
        uni.showModal({ title: '操作失败', content: '新密码不符合要求，请修改后重试。', showCancel: false })
        return
      }
      if (newPwd !== confirmPwd) {
        uni.showModal({ title: '操作失败', content: '新密码和确认密码输入不一致，请修改。', showCancel: false })
        return
      }

      const [err, { data: result }] = await uni.request({
        url: this.apiRoot`/user/modifypw`,
        method: 'POST',
        data: { ...auth, data: JSON.stringify({ newpassword: md5(newPwd), oldpassword: md5(oldPwd) }) }
      })

      const { code, info } = result || {}
      if (err || code !== 200) {
        uni.showModal({ title: '操作失败', content: err || info, showCancel: false })
        return
      }
      
      uni.navigateBack()
      uni.showToast({ title: '密码修改成功' })
    }
  }
}
</script>
