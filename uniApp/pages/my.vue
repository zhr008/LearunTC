<template>
  <view class="page" id="my" v-if="currentUser">
    <view class="mybanner" @click="goTo('info')">
      <view class="avatarslot">
        <image
          class="avatar"
          :style="{ borderRadius: roundAvatar ? '50%' : '3px' }"
          :src="avatarSrc()"
          mode="aspectFill"
        ></image>
      </view>
      <view class="info">
        <view class="username text-xl text-white">{{ currentUser.realName }}</view>
        <view class="usertag">
          <l-tag color="green">{{ userTag }}</l-tag>
        </view>
      </view>
      <view class="badge text-white text-lg"><l-icon round type="right" /></view>
    </view>

    <l-list border card>
      <l-list-item @click="goTo('contact')" arrow>
        <l-icon type="phone" color="blue" />
        联系方式
      </l-list-item>
      <l-list-item @click="goTo('qrcode')" arrow>
        <l-icon type="qrcode" color="blue" />
        我的二维码
      </l-list-item>
      <l-list-item @click="goTo('password')" arrow>
        <l-icon type="edit" color="blue" />
        修改密码
      </l-list-item>
    </l-list>

    <l-list border card>
      <l-list-item @click="goTo('learun')" arrow>
        <l-icon type="home" color="blue" />
        关于力软
      </l-list-item>
      <l-list-item @click="goTo('framework')" arrow>
        <l-icon type="info" color="blue" />
        力软敏捷框架
      </l-list-item>
    </l-list>

    <view class="padding-lr"><l-button @click="logout" block size="lg" color="red">退出登录</l-button></view>
    <view class="footer">{{ copyRightDisplay }}</view>
  </view>
</template>

<script>
export default {
  methods: {
    async logout() {
      uni.showModal({
        title: '注销确认',
        content: '确定要注销登录吗？',
        success: ({ confirm }) => {
          if (confirm) {
            this.$store.commit('logout')
            uni.reLaunch({ url: '/pages/login' })
          }
        }
      })
    },

    goTo(urlPath) {
      uni.navigateTo({ url: `/pages/my/${urlPath}` })
    },

    avatarSrc() {
      if (!this.currentUser) {
        return ''
      }

      return this.apiRoot`/user/img?data=${this.currentUser.userId}`
    }
  },

  computed: {
    currentUser() {
      return this.$store.state.user
    },

    userTag() {
      if (!this.currentUser) {
        return ''
      }

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
    },

    roundAvatar() {
      const page = this.config('pageConfig.my.roundAvatar')
      const global = this.config('roundAvatar')

      return page === null || page === undefined ? global : page
    },

    copyRightDisplay() {
      const year = this.config('year') === true ? new Date().getFullYear() : this.config('year')
      const companyName = this.config('company')

      return `Copyright © ${year} ${companyName}`
    }
  }
}
</script>

<style scoped lang="less">
.mybanner {
  background: #0c86d8;
  height: 120px;
  padding: 25px 15px;
  display: flex;
  align-items: center;

  .avatarslot {
    .avatar {
      height: 60px;
      width: 60px;
    }
  }

  .info {
    padding-left: 20px;

    .username {
      margin-bottom: 5px;
    }
  }

  .badge {
    flex-grow: 1;
    display: flex;
    justify-content: flex-end;
  }
}

.footer {
  margin-top: 15rpx;
  text-align: center;
  font-size: 14px;
  color: #ccc;
}
</style>
