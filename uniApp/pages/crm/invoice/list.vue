<template>
  <view class="page">
    <l-banner
      v-model="searchText"
      @input="searchChange"
      noSearchButton
      placeholder="搜索票据名称"
      type="search"
      fill
      fixed
    />
    <l-list class="invoicelist">
      <template v-for="item of displayList">
        <l-list-item v-if="item.F_InvoiceId" @click="invoiceClick(item)" :key="item.F_InvoiceId">
          <view class="item-icon"><l-icon type="form" style="font-size: 30px; width: 1em;" /></view>
          <view class="item-main">
            <view class="item-desc text-cut">
              <text class="invoice-title text-black">客户名称：</text>
              {{ item.F_CustomerName }}
            </view>
            <view class="item-desc text-cut">
              <text class="text-black">开票信息：</text>
              {{ item.F_InvoiceContent }}
            </view>
          </view>
          <view class="time" slot="time">
            <view class="text-right margin-bottom-xs">{{ invoiceDateTime(item)[0] }}</view>
            <view class="text-right" v-if="invoiceDateTime(item)[1]">{{ invoiceDateTime(item)[1] }}</view>
          </view>
        </l-list-item>
        <l-list-item v-else @click="fetchList()" :key="item.tips">{{ item.tips }}</l-list-item>
      </template>
    </l-list>

    <l-custom-add @click="addInvoice" />
  </view>
</template>

<script>
import moment from 'moment'

export default {
  data() {
    return {
      searchData: {},
      searchText: '',

      page: 1,
      total: 2,
      list: []
    }
  },

  async onLoad() {
    await this.init()
  },

  onUnload() {
    uni.$off('invoice-list-change')
  },

  methods: {
    async init() {
      uni.$on('invoice-list-change', this.refersh)

      await this.fetchList()
    },

    async fetchList() {
      if (this.page > this.total) {
        return
      }

      uni.showLoading({ title: '加载开票信息...', mask: true })
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/crm/invoice/list`,
        data: {
          ...this.auth,
          data: JSON.stringify({
            pagination: { rows: 10, page: this.page, sidx: 'F_CreateDate', sord: 'DESC' },
            queryJson: JSON.stringify(this.searchData)
          })
        }
      })

      if (err || !result) {
        uni.hideLoading()
        uni.showToast({ title: '加载数据时出错', icon: 'none' })
        return
      }

      this.total = result.total
      this.page = this.page + 1
      this.list = this.list.concat(result.rows)

      uni.hideLoading()
      uni.showToast({ icon: 'none', title: `第 ${result.page} / ${result.total} 页，共 ${result.records} 项` })
    },

    async refersh() {
      this.page = 1
      this.total = 2
      this.list = []

      await this.fetchList()
    },

    invoiceClick(item) {
      this.setPageParam(item)
      uni.navigateTo({ url: './single?type=view' })
    },

    addInvoice() {
      uni.navigateTo({ url: './single' })
    },

    invoiceDateTime({ F_CreateDate }) {
      const dt = moment(F_CreateDate)
      if (!dt.isValid()) {
        return []
      }

      switch (this.config('pageConfig.invoice.invoiceDateDisplay')) {
        case 'date':
          return [dt.format('YYYY年 M月D日')]
        case 'datetime':
          return [dt.format('YYYY-M-D'), dt.format('HH:mm')]
        case 'before':
          return [dt.fromNow()]
        default:
          const now = moment()
          if (dt.isSame(now, 'day')) {
            return [`今天 ${dt.format('HH:mm')}`]
          } else if (dt.isSame(now, 'year')) {
            return [dt.format('M月D日'), dt.format('HH:mm')]
          }

          return [dt.format('YYYY-M-D')]
      }
    },

    searchChange() {
      const searchData = {}
      if (this.searchText) {
        searchData.keyword = this.searchText
      }

      this.searchData = searchData
      this.refresh()
    }
  },

  computed: {
    displayList() {
      return [...this.list, { tips: this.page >= this.total ? '已展示全部开票信息' : '加载中...' }]
    },

    showIcon() {
      return this.config('pageConfig.invoice.showIcon')
    }
  }
}
</script>

<style lang="less" scoped>
.invoicelist {
  .item-icon {
    display: inline-block;
    height: 40px;
    width: 40px;
    margin: 15px 0;
    line-height: 40px;
    text-align: center;
    color: #488aff;
    border-radius: 4px;
    font-size: 18px;
  }

  .item-main {
    display: inline-block;

    .item-desc {
      font-size: 13px;
      line-height: 1.8em;
      max-width: 500rpx;
    }
  }

  .time {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
  }
}
</style>

<style>
page {
  padding-top: 100rpx;
}
</style>
