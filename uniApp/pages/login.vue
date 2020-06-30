<template>
  <view class="page">
    <view class="content">
      <view class="logo-banner">
        <view class="logo-bg"><image class="logo" mode="aspectFit" src="/static/logo.png"></image></view>
        <view class="logo-text">
          <view class="main-title">力软敏捷开发框架</view>
          <!-- #ifdef  MP-WEIXIN -->
          <view class="sub-title">微信小程序版</view>
          <!-- #endif -->
        </view>
      </view>
      <l-input v-model="username" placeholder="手机号 / 账号"><l-icon type="people" slot="title" /></l-input>
      <l-input v-model="password" placeholder="密码" password><l-icon type="lock" slot="title" /></l-input>
      <l-button @click="clickLogin" size="lg" block color="blue" class="margin-top block">登 录</l-button>
      <view class="more"><view @click="signupTips" class="signup">如何注册</view></view>
    </view>
    <view class="footer">{{ copyRightDisplay }}</view>
  </view>
</template>

<script>
import md5 from 'js-md5'

export default {
  data() {
    return {
      username: '',
      password: ''
    }
  },

  onShow() {
    const isDev = process.env.NODE_ENV === 'development'
    if (isDev) {
      this.username = 'system'
      this.password = '0000'
    }
  },

  methods: {
    signupTips() {
      uni.showToast({ title: '请前往力软框架PC端官网(learun.cn)注册账号。已注册的账号可直接登录。', icon: 'none' })
    },

    async clickLogin() {
      const { username, password, check } = this
      if (!check()) {
        return
      }

      const data = {
        token: '',
        loginMark: this.$store.state.guid,
        data: JSON.stringify({ username, password: md5(password) })
      }

      uni.showLoading({ title: '登录中...', mask: true })
      const [error, { data: resultData }] = await uni.request({
        url: this.apiRoot + '/user/login',
        method: 'POST',
        data
      })
      const { data: loginData, code, info } = resultData || {}

      if (error || code !== 200) {
        uni.hideLoading()
        uni.showModal({ title: '登录失败', content: info || error, showCancel: false })
        return
      }

      const { baseinfo } = loginData
      this.$store.commit('user', baseinfo)
      this.$store.commit('token', baseinfo.token)

      await Promise.all([
        uni
          .request({ url: this.apiRoot`/company/map`, data: this.auth })
          .then(([err, result]) => this.$store.commit('company', result.data.data.data)),
        uni
          .request({ url: this.apiRoot`/department/map`, data: this.auth })
          .then(([err, result]) => this.$store.commit('dep', result.data.data.data)),
        uni
          .request({ url: this.apiRoot`/user/map`, data: this.auth })
          .then(([err, result]) => this.$store.commit('staff', result.data.data.data)),
        uni
          .request({ url: this.apiRoot`/dataitem/map`, data: this.auth })
          .then(([err, result]) => this.$store.commit('propTable', result.data.data.data))
      ])

      uni.reLaunch({ url: '/pages/home' })
      uni.hideLoading()
    },

    check() {
      const { username, password } = this
      if (username.length <= 0 || password.length <= 0) {
        uni.showModal({
          title: '输入错误',
          content: '账号或密码不能为空，请重新输入。',
          showCancel: false
        })
        return false
      }

      return true
    }
  },

  computed: {
    copyRightDisplay() {
      const year = this.config('year') === true ? new Date().getFullYear() : this.config('year')
      const companyName = this.config('company')

      return `Copyright © ${year} ${companyName}`
    }
  }
}
</script>

<style lang="less">
page {
  height: 100%;
}
</style>

<style scoped lang="less">
.page {
  height: 100%;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;

  .content {
    text-align: center;
    width: 100%;
    padding: 0 38rpx;
    padding-bottom: 80rpx;

    l-input {
      text-align: left;
    }
  }

  .logo-banner {
    margin-bottom: 80rpx;

    .logo-bg {
      padding: 10rpx 15rpx;
      background-color: #2782d7;
      text-align: center;
      display: inline-block;
      text-align: left;
      border-radius: 8px;

      .logo {
        height: 100rpx;
        width: 100rpx;
      }
    }

    .logo-text {
      display: block;
      margin: 20rpx 0;
      color: #555;

      .main-title {
        font-size: 1.4em;
        margin-bottom: 10rpx;
      }
    }
  }

  .more {
    margin-top: 30rpx;

    .signup {
      color: #555;
    }
  }

  .footer {
    position: absolute;
    width: 100%;
    bottom: 10px;
    bottom: calc(10px + env(safe-area-inset-bottom));
    text-align: center;
    font-size: 14px;
    color: #555;
  }
}
</style>
