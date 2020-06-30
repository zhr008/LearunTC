<template>
  <view id="notice" class="page">
    <view class="padding text-lg">
      <u-parse v-if="ready" :imageProp="{ domain: 'www.learun.cn/admsapi/learun/adms' }" :content="content"></u-parse>
    </view>
    <view class="padding-sm text-grey notice-info">
      <view class="text-right">发布于 {{ time }}</view>
      <view class="text-right">{{ date }}</view>
    </view>
  </view>
</template>

<script>
import moment from 'moment'
import uParse from '@/components/u-parse/u-parse.vue'
import { convertHtml } from '@/common/utils.js'

export default {
  data() {
    return {
      ready: false,
      content: '',
      time: '',
      date: ''
    }
  },

  components: { uParse },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      uni.showLoading({ title: '加载中...', mask: true })
      const noticeItem = this.getPageParam()

      this.content = noticeItem.f_content
      this.content = convertHtml(this.content)

      this.time = moment(noticeItem.f_time).format('HH : mm')
      this.date = moment(noticeItem.f_time).format('YYYY-M-D')
      uni.setNavigationBarTitle({ title: noticeItem.f_title })

      this.ready = true
      uni.hideLoading()
    }
  }
}
</script>
