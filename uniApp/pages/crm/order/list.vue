<template>
  <view class="page">
    <scroll-view
      @scrolltolower="fetchList"
      style="padding-top: 100rpx"
      :class="sideOpen ? 'show' : ''"
      class="mainpage solid-top"
      scroll-y
    >
      <view class="custom-list-header">
        <view class="custom-list-banner">
          {{ pageInfo }}
          <view class="custom-list-action">
            <l-icon @click="sideOpen = true" class="text-xxl" type="searchlist" color="blue" />
          </view>
        </view>
      </view>

      <view v-if="ready" class="list">
        <view v-for="(item, index) of displayList" v-if="item.F_OrderId" :key="item.F_OrderId" class="custom-item">
          <view class="custom-item-main">
            <text class="custom-item-title">客户名称：</text>
            {{ item.__customer__ }}
          </view>
          <view class="custom-item-main">
            <text class="custom-item-title">销售人员：</text>
            {{ item.__seller__ }}
          </view>

          <view class="custom-item-main margin-top">
            <text class="custom-item-title">收款金额：</text>
            <text class="text-black" style="font-size: 1.8em;">{{ displayCash(item.F_Accounts) }}</text>
          </view>
          <view class="custom-item-main" style="padding-top: 2px;">
            <text class="custom-item-title">优惠金额：</text>
            {{ displayCash(item.F_DiscountSum) }}
          </view>
          <view class="custom-item-main">
            <text class="custom-item-title">收款信息：</text>
            <view v-if="Number(item.F_PaymentState) === 2" class="payment bg-orange">
              部分收款： {{ displayCash(item.F_ReceivedAmount) }}
            </view>
            <view v-else-if="Number(item.F_PaymentState) === 3" class="payment bg-olive">全部收款</view>
            <view v-else class="payment bg-red">未收款</view>
            <view class="margin-left-xs" style="display: inline-block;">({{ item.__payment__ }}方式)</view>
          </view>

          <view class="custom-item-sub  margin-top">
            <text class="custom-item-title">编号：</text>
            {{ item.F_OrderCode || '-' }}
          </view>
          <view class="custom-item-sub">
            <text class="custom-item-title">制单：</text>
            {{ item.F_CreateUserName || '-' }}
          </view>
          <view class="custom-item-sub">
            <text class="custom-item-title">日期：</text>
            {{ item.__date__ }}
          </view>

          <view class="custom-action">
            <view
              @click="deleteItem(item, index)"
              class="custom-action-btn line-red"
              style="border: currentColor 1px solid;"
            >
              <view class="flex flex-wrap text-center align-center justify-center"><l-icon type="delete" /></view>
              <view class="text-center text-xs">删除</view>
            </view>
            <view
              @click="action('edit', item.F_OrderId)"
              class="custom-action-btn line-blue"
              style="border: currentColor 1px solid;"
            >
              <view class="flex flex-wrap text-center align-center justify-center"><l-icon type="edit" /></view>
              <view class="text-center text-xs">编辑</view>
            </view>
            <view
              @click="action('view', item.F_OrderId)"
              class="custom-action-btn line-blue"
              style="border: currentColor 1px solid;min-width: 140rpx;"
            >
              <view class="flex flex-wrap text-center align-center justify-center"><l-icon type="right" /></view>
              <view class="text-center text-xs">查看</view>
            </view>
          </view>
        </view>

        <view class="custom-item">
          <view @click="fetchList" class="custom-item-tips">{{ page >= total ? `已加载全部` : `加载中...` }}</view>
        </view>
      </view>
    </scroll-view>

    <view class="sideclose" :class="sideOpen ? 'show' : ''" @click="sideOpen = false">
      <l-icon type="pullright" color="blue" />
    </view>

    <scroll-view scroll-y class="sidepage" :class="sideOpen ? 'show' : ''">
      <view v-if="ready" class="padding">
        <view class="side-title">按详细条件筛选：</view>

        <l-select
          v-model="selectedCustomer"
          @change="searchChange"
          :range="selectCustomer"
          title="客户名称"
          placeholder="按客户名筛选"
          arrow
        />
        <l-select
          v-model="selectedPayment"
          @change="searchChange"
          :range="payment"
          title="收款状态"
          placeholder="按收款状态筛选"
          arrow
        />
        <l-label arrow @click="selectSeller" title="销售人员">{{ displaySelectedSeller || '按销售人筛选' }}</l-label>

        <view class="side-title">按任务发布时间筛选：</view>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('all')"
          :line="dateRange !== 'all' ? 'green' : ''"
          :color="dateRange === 'all' ? 'green' : ''"
        >
          全部
        </l-button>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('today')"
          :line="dateRange !== 'today' ? 'blue' : ''"
          :color="dateRange === 'today' ? 'blue' : ''"
        >
          今天
        </l-button>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('7d')"
          :line="dateRange !== '7d' ? 'blue' : ''"
          :color="dateRange === '7d' ? 'blue' : ''"
        >
          最近7天内
        </l-button>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('1m')"
          :line="dateRange !== '1m' ? 'blue' : ''"
          :color="dateRange === '1m' ? 'blue' : ''"
        >
          最近1个月内
        </l-button>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('3m')"
          :line="dateRange !== '3m' ? 'blue' : ''"
          :color="dateRange === '3m' ? 'blue' : ''"
        >
          最近3个月内
        </l-button>
        <l-button
          class="block margin-top-sm"
          block
          @click="dateRangeChange('custom')"
          :line="dateRange !== 'custom' ? 'cyan' : ''"
          :color="dateRange === 'custom' ? 'cyan' : ''"
        >
          自定义
        </l-button>

        <view v-if="dateRange === 'custom'" class="side-title">自定义时间区间：</view>
        <l-date-picker
          v-if="dateRange === 'custom'"
          @change="searchChange"
          v-model="startDate"
          title="起始时间"
          placeholder="点击来选取"
        />
        <l-date-picker
          v-if="dateRange === 'custom'"
          @change="searchChange"
          v-model="endDate"
          title="结束时间"
          placeholder="点击来选取"
        />

        <view class="padding-tb">
          <l-button class="block" block line="orange" @click="reset">重置筛选条件</l-button>
        </view>
      </view>
    </scroll-view>

    <l-custom-add v-if="!sideOpen" @click="action('create')" />
  </view>
</template>

<script>
import moment from 'moment'
import { displayCash } from '@/common/utils.js'

export default {
  data() {
    return {
      sideOpen: false,
      pageInfo: '(请等待页面加载完成...)',
      searchData: {},
      ready: false,

      page: 1,
      total: 2,
      list: [],

      selectedCustomer: null,
      selectedPayment: null,
      selectedSeller: null,

      customers: [],
      selectCustomer: [],
      payment: [],

      dateRange: 'all',
      startDate: null,
      endDate: null
    }
  },

  async onLoad() {
    await this.init()
  },

  onUnload() {
    uni.$off('order-list-change')
  },

  methods: {
    async init() {
      uni.$on('order-list-change', this.refresh)

      this.payment = Object.values(this.$store.state.propTable.Client_PaymentMode)
      await this.getCustomerList()
      await this.fetchList()
      this.ready = true
    },

    async fetchList(e) {
      if (e && e.preventDefault) {
        e.preventDefault()
      }

      if (this.page > this.total) {
        return
      }

      uni.showLoading({ title: '加载订单信息...', mask: true })
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/crm/order/pagelist`,
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
      this.pageInfo = `已加载 ${result.page} / ${result.total} 页，合计共 ${result.records} 条记录`
    },

    async refresh() {
      this.page = 1
      this.total = 2
      this.list = []

      await this.fetchList()
    },

    async getCustomerList() {
      await uni
        .request({
          url: this.apiRoot`/crm/customer/list`,
          data: { ...this.auth }
        })
        .then(([err, { data: { data: result } } = {}]) => {
          this.customers = result
          this.selectCustomer = result.map(t => ({ text: t.F_FullName, value: t.F_CustomerId }))
        })
    },

    selectSeller() {
      uni.$once(`select-user`, data => {
        this.selectedSeller = data.id
        this.searchChange()
      })
      uni.navigateTo({ url: `/pages/common/select-user` })
    },

    reset() {
      this.selectedCustomer = null
      this.selectedPayment = null
      this.selectedSeller = null
    },

    getOrderSeller(item) {
      const seller = this.$store.state.staff[item.F_SellerId]
      return seller ? seller.name : '(未知人员)'
    },

    getOrderCustomer(item) {
      const customer = this.customers.find(t => t.F_CustomerId === item.F_CustomerId)
      return customer ? customer.F_FullName : '(未知客户)'
    },

    getOrderPayment(item) {
      const payment = this.payment.find(t => t.value === item.F_PaymentMode)
      return payment ? payment.text : '未知'
    },

    getOrderDate(item) {
      if (!item.F_OrderDate) {
        return '未知'
      }

      return moment(item.F_OrderDate).format('YYYY-MM-DD')
    },

    action(type, id = 'no') {
      uni.navigateTo({ url: `./single?type=${type}&id=${id}` })
    },

    deleteItem(item, index) {
      uni.showModal({
        title: '删除订单',
        content: `确定要删除该订单吗？`,
        success: ({ confirm }) => {
          if (!confirm) {
            return
          }

          uni
            .request({
              url: this.apiRoot`/crm/order/delete`,
              method: 'POST',
              data: { ...this.auth, data: item.F_OrderId }
            })
            .then(([err, { data }]) => {
              if (err || !data || data.code !== 200) {
                uni.showToast({ title: '删除失败', icon: 'none' })
                return
              }

              this.list.splice(index, 1)
              uni.showToast({ title: '删除成功', icon: 'success' })
            })
        }
      })
    },

    dateRangeChange(type) {
      this.dateRange = type
      if (type === 'custom') {
        this.startDate = null
        this.endDate = null
      }
      this.searchChange()
    },

    async searchChange() {
      const result = {}

      const todayEnd = moment().format('YYYY-MM-DD 23:59:59')
      if (dateRange === 'today') {
        result.StartTime = moment()
          .subtract(1, 'day')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '7d') {
        result.StartTime = moment()
          .subtract(7, 'days')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '7d') {
        result.StartTime = moment()
          .subtract(7, 'days')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '1m') {
        result.StartTime = moment()
          .subtract(1, 'month')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '3m') {
        result.StartTime = moment()
          .subtract(3, 'months')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === 'custom' && (startDate || startDate)) {
        if (!(startDate && startDate && moment(startDate).isAfter(endDate))) {
          result.StartTime = startDate ? moment(startDate).format('YYYY-MM-DD 00:00:00') : '1900-01-01 00:00:00'
          result.EndTime = endDate ? moment(endDate).format('YYYY-MM-DD 23:59:59') : todayEnd
        }
      }

      Object.assign(result, _.mapValues(_.pickBy(this.queryData), t => (Array.isArray(t) ? t.join(',') : t)))

      this.searchData = result
      await this.refresh()
    },

    displayCash
  },

  computed: {
    displaySelectedSeller() {
      if (!this.selectSeller) {
        return ''
      }

      const seller = this.$store.state.staff[this.selectedSeller]
      return seller ? seller.name : ''
    },

    displayList() {
      const {
        list,
        getOrderSeller,
        getOrderCustomer,
        getOrderPayment,
        getOrderDate,
        selectedCustomer,
        selectedSeller,
        selectedPayment,
        dateRange,
        startDate,
        endDate
      } = this

      let result = list.map(t => ({
        ...t,
        __date__: getOrderDate(t),
        __customer__: getOrderCustomer(t),
        __payment__: getOrderPayment(t),
        __seller__: getOrderSeller(t)
      }))

      if (selectedCustomer) {
        result = result.filter(t => t.F_CustomerId === selectedCustomer)
      }

      if (selectedSeller) {
        result = result.filter(t => t.F_SellerId === selectedSeller)
      }

      if (selectedPayment) {
        result = result.filter(t => t.F_PaymentMode === selectedPayment)
      }

      return result
    }
  }
}
</script>

<style lang="less" scoped>
@import '~@/common/css/sidepage.less';
@import '~@/common/css/custom-item.less';

.payment {
  display: inline-block;
  padding: 5rpx 10rpx;
  margin: 0 5px;
  margin-left: 0;
  border-radius: 3px;
  font-size: 0.8rem;
}

.custom-action {
  position: absolute;
  bottom: 20rpx;
  right: 10rpx;

  .custom-action-btn {
    display: inline-block;
    padding: 4px 6px;
    margin: 0 3px;
    border-radius: 3px;
  }
}
</style>
