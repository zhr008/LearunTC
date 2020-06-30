<template>
  <view id="chat" class="page">
    <l-chat>
      <l-chat-msg
        v-for="msgItem of msgList"
        :type="msgItem.F_SendUserId === chatUserId ? 'left' : 'right'"
        :key="msgItem.F_MsgId"
        :avatar="avatar(msgItem.F_SendUserId)"
        :roundAvatar="roundAvatar"
        :date="dateDisplay(msgItem.F_CreateDate)"
      >
        {{ msgItem.F_Content }}
      </l-chat-msg>
    </l-chat>

    <l-chat-input
      v-model="msgInput"
      :buttonDisabled="buttonDisabled"
      @sendMsg="sendMsg"
      placeholder="输入要发送的内容"
    />
  </view>
</template>

<script>
import moment from 'moment'

export default {
  data() {
    return {
      chatUser: null,
      chatUserId: null,

      msgList: [],
      msgInput: '',
      timer: null,
      isFetchReady: false,
      buttonDisabled: false
    }
  },

  async onLoad({ chatid, userid }) {
    await this.init(userid)
  },

  onShow() {
    this.timer = setInterval(() => {
      this.fetchMsg()
    }, this.config('pageConfig.chat.fetchMsg'))
  },

  onHide() {
    clearInterval(this.timer)
  },

  onUnload() {
    clearInterval(this.timer)
  },

  methods: {
    async init(userid) {
      uni.showLoading({ title: '加载消息...', mask: true })
      this.chatUserId = userid
      this.chatUser = this.$store.state.staff[userid] || { name: '系统通知', img: '1' }
      uni.setNavigationBarTitle({ title: this.chatUser.name })

      const [err, { data: result }] = await uni.request({
        url: this.apiRoot`/im/msg/lastlist`,
        data: { ...this.auth, data: userid }
      })

      const { data: msg } = result
      msg.reverse()

      this.msgList = msg
      this.isFetchReady = true

      setTimeout(() => {
        uni.pageScrollTo({
          scrollTop: 9999,
          duration: 50
        })
      }, 10)

      uni.hideLoading()
    },

    async fetchMsg(date = Date.now()) {
      const { isFetchReady, auth, chatUser, chatUserId } = this
      if (!isFetchReady) {
        return
      }

      const [err, { data: result }] = await uni.request({
        url: this.apiRoot`/im/msg/list2`,
        data: {
          ...auth,
          data: JSON.stringify({
            otherUserId: chatUserId,
            time: moment(date).format('YYYY-MM-DD HH:mm:ss')
          })
        }
      })

      this.msgList = this.msgList.concat(result.data)
    },

    dateDisplay(date) {
      const dt = moment(date)
      switch (this.config('pageConfig.chat.msgDateDisplay')) {
        case 'date':
          return dt.format('YYYY年 M月D日')
        case 'datetime':
          return dt.format('YYYY-MM-DD HH:mm')
        case 'before':
          return dt.fromNow()
        default:
          const now = moment()
          if (dt.isSame(now, 'day')) {
            return `今天 ${dt.format('HH:mm')}`
          } else if (dt.isSame(now, 'year')) {
            return dt.format('M月D日 HH:mm')
          }

          return dt.format('YYYY-MM-DD HH:mm')
      }
    },

    avatar(id) {
      if (id === 'IMSystem') {
        return this.apiRoot`/user/img?data=System`
      }
      return this.apiRoot`/user/img?data=${id}`
    },

    async sendMsg() {
      this.isFetchReady = false
      this.buttonDisabled = true

      const [err, { data: result }] = await uni.request({
        url: this.apiRoot`/im/send`,
        method: 'POST',
        data: {
          ...this.auth,
          data: JSON.stringify({
            userId: this.chatUserId,
            content: this.msgInput
          })
        }
      })

      this.msgList.push({
        F_Content: this.msgInput,
        F_CreateDate: result.time,
        F_IsSystem: null,
        F_MsgId: result.msgId,
        F_RecvUserId: this.chatUserId,
        F_SendUserId: this.$store.state.user.userId
      })

      this.msgInput = ''
      setTimeout(() => {
        uni.pageScrollTo({
          scrollTop: 9999,
          duration: 100
        })
      }, 10)

      this.buttonDisabled = true
      this.isFetchReady = true
    }
  },

  computed: {
    roundAvatar() {
      const page = this.config('pageConfig.chat.roundAvatar')
      const global = this.config('roundAvatar')

      return page === null || page === undefined ? global : page
    }
  }
}
</script>

<style lang="less">
page {
  padding-bottom: 100rpx;
  padding-bottom: calc(100rpx + constant(safe-area-inset-bottom));
  padding-bottom: calc(100rpx + env(safe-area-inset-bottom));
}
</style>
