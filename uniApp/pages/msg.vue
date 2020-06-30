<template>
  <view id="msg" class="page">
    <l-list message>
      <l-list-item v-for="item of msgList" :key="item.F_Id" @click="goTo(item)" :extra="item.F_Content">
        <image
          class="avatar"
          :style="{ borderRadius: roundAvatar ? '50%' : '3px' }"
          :src="avatarSrc(item)"
          slot="avatar"
          mode="aspectFill"
        ></image>
        <text class="text-black">{{ msgTitle(item) }}</text>
        <l-tag
          v-if="config('pageConfig.msg.unread') && item.F_IsRead === 1"
          class="margin-left-sm"
          color="red"
          light
          round
        >
          新消息
        </l-tag>
        <view class="time" slot="time">
          <view class="text-right">{{ msgDateTime(item.F_Time)[0] }}</view>
          <view class="text-right" v-if="msgDateTime(item.F_Time)[1]">{{ msgDateTime(item.F_Time)[1] }}</view>
        </view>
      </l-list-item>
      <view class="padding text-gray text-center" v-if="msgList.length <= 0">消息列表为空</view>
    </l-list>
  </view>
</template>

<script>
import moment from 'moment'

export default {
  data() {
    return {
      msgList: [],
      userTable: {},
      timer: null,
      nextTime: '1888-10-10 10:10:10'
    }
  },

  async onLoad() {
    await this.init()
  },

  onShow() {
    this.timer = setInterval(() => {
      this.fetchMsg()
    }, this.config('pageConfig.msg.fetchMsg'))
  },

  onHide() {
    clearInterval(this.timer)
  },

  methods: {
    async init() {
      uni.showLoading({ title: '读取消息列表', mask: true })
      await Promise.all([
        uni
          .request({
            url: this.apiRoot`/user/map`,
            data: { ...this.auth }
          })
          .then(([err, result]) => {
            this.userTable = result.data.data.data
          }),
        this.fetchMsg()
      ])
      uni.hideLoading()
    },

    async fetchMsg() {
      const date = moment(this.nextTime).format('YYYY-MM-DD HH:mm:ss')
      const [err, result] = await uni.request({
        url: this.apiRoot`/im/contacts`,
        data: { ...this.auth, data: date }
      })

      this.nextTime = result.data.data.time
      const newMsg = result.data.data.data
      const allMsg = [...this.msgList]
      newMsg.forEach(item => {
        const idx = allMsg.findIndex(t => t.F_Id === item.F_Id)
        if (idx === -1) {
          allMsg.push(item)
          return
        }
        allMsg[idx] = item
      })

      this.msgList = allMsg.sort((a, b) => moment(b.F_Time).valueOf() - moment(a.F_Time).valueOf())
    },

    goTo(item) {
      item.F_IsRead = 0
      uni.navigateTo({ url: `/pages/msg/chat?chatid=${item.F_Id}&userid=${item.F_OtherUserId}` })
    },

    msgTitle(item) {
      if (item.F_OtherUserId === 'IMSystem') {
        return '系统消息'
      }

      return this.userTable[item.F_OtherUserId] ? this.userTable[item.F_OtherUserId].name : '未知用户'
    },

    msgDateTime(date) {
      const dt = moment(date)
      switch (this.config('pageConfig.msg.noticeDateDisplay')) {
        case 'date':
          return [dt.format('YYYY年 M月D日')]
        case 'datetime':
          return [dt.format('YYYY-M-D'), dt.format('HH:mm')]
        case 'before':
          return [dt.fromNow()]
        default:
          const now = moment()
          if (dt.isSame(now, 'day')) {
            return [`今天 ${dt.format('H:mm')}`]
          } else if (dt.isSame(now, 'year')) {
            return [dt.format('M月D日'), dt.format('HH:mm')]
          }

          return [dt.format('YYYY-M-D')]
      }
    },

    avatarSrc(item) {
      if (!this.userTable[item.F_OtherUserId]) {
        return '/static/img-avatar/chat-boy.jpg'
      }

      return Number(this.userTable[item.F_OtherUserId].img) === 1
        ? '/static/img-avatar/chat-boy.jpg'
        : '/static/img-avatar/chat-girl.jpg'
    }
  },

  computed: {
    roundAvatar() {
      const page = this.config('pageConfig.msg.roundAvatar')
      const global = this.config('roundAvatar')

      return page === null || page === undefined ? global : page
    }
  }
}
</script>

<style scoped lang="less">
.avatar {
  width: 96rpx;
  height: 96rpx;
}

.time {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
}
</style>
