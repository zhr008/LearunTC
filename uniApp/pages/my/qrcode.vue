<template>
  <view class="page">
    <view class="bg">
      <view class="info">
        <img :src="avatar" class="avatar" />
        <view class="infotext">
          <view class="text-xl">{{ currentUser.realName }}</view>
          <l-tag line="green">{{ userTag }}</l-tag>
        </view>
      </view>

      <view class="qrcode"><tki-qrcode ref="qrcode" val="http://www.learun.cn/" :size="550" /></view>
    </view>
  </view>
</template>

<script>
import tkiQrcode from '@/components/tki-qrcode/tki-qrcode.vue'

export default {
  components: { tkiQrcode },

  onLoad() {
    this.$refs.qrcode._makeCode()
  },

  computed: {
    currentUser() {
      return this.$store.state.user
    },

    avatar() {
      return this.apiRoot`/user/img?data=${this.currentUser.userId}`
    },

    userTag() {
      const { companyId, departmentId } = this.currentUser
      if (!companyId) {
        return `总集团公司`
      }

      const { company, dep } = this.$store.state
      const companyName = company[companyId].name
      if (!dep) {
        return companyName
      }

      return `${companyName} / ${dep[departmentId].name}`
    }
  }
}
</script>

<style lang="less" scoped>
.page {
  background-color: #2f2d2d;
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  bottom: 0;
  top: 0;
  right: 0;
  left: 0;

  .bg {
    background: #ffffff;
    border-radius: 5px;
    padding: 20rpx;
    display: inline-block;

    .info {
      display: flex;
      align-items: center;
      margin-bottom: 10px;

      .avatar {
        width: 120rpx;
        height: 120rpx;
        margin-right: 15px;
        border-radius: 2px;
      }

      .infotext > view {
        margin-bottom: 10px;
      }
    }
  }
}
</style>
