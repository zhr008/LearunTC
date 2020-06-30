<template>
  <view v-if="ready" class="page">
    <l-select
      v-model="currentInvoice.F_CustomerId"
      :disabled="mode === 'view'"
      :arrow="mode !== 'view'"
      :range="customers"
      required
      title="客户名称"
      placeholder="请选择开票客户"
    />

    <view class="cu-form-group" style="border-bottom: none">
      <view class="title">
        <text style="color:red;font-size: 1.2em;">*</text>
        开票信息
      </view>
    </view>
    <l-textarea
      v-model="currentInvoice.F_InvoiceContent"
      textareaStyle="margin-top:0"
      autoHeight
      :placeholder="`输入开票信息`"
      :disabled="mode === 'view'"
    />

    <view v-if="ready" class="padding-lr margin-tb">
      <l-button v-if="mode === 'view'" class="block" @click="edit" block size="lg" color="orange">编辑内容</l-button>
      <l-button v-else class="block" @click="submit" block size="lg" color="green">提交开票信息</l-button>
      <l-button v-if="mode === 'edit'" class="block margin-top" @click="cancel" block size="lg" color="red">
        取消编辑
      </l-button>
    </view>
  </view>
</template>

<script>
export default {
  data() {
    return {
      mode: 'new',
      ready: false,
      customerList: [],
      originInvoice: {},
      currentInvoice: {}
    }
  },

  async onLoad({ type }) {
    await this.init(type)
  },

  methods: {
    async init(type = 'new') {
      uni.showLoading({ title: '加载开票信息...' })
      this.currentInvoice = { F_CustomerId: '', F_InvoiceContent: '' }

      if (type !== 'new') {
        this.mode = type
        const pageInfo = this.getPageParam()
        this.originInvoice = pageInfo
        this.currentInvoice = pageInfo
      }

      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/crm/customer/list`,
        data: this.auth
      })

      this.customerList = result
      this.ready = true
      uni.hideLoading()
    },

    async edit() {
      this.mode = 'edit'
    },

    async submit() {
      if (!this.currentInvoice.F_CustomerId || !this.currentInvoice.F_InvoiceContent) {
        uni.showModal({ title: '请补全必填项', content: '开票客户、开票信息均须填写内容。', showCancel: false })

        return
      }

      uni.showLoading({ title: '提交开票信息...' })
      const F_CustomerName = this.customers.find(t => t.value === this.currentInvoice.F_CustomerId).text
      const postData = {
        keyValue: this.currentInvoice.F_InvoiceId || '',
        entity: {
          F_CustomerId: this.currentInvoice.F_CustomerId,
          F_InvoiceContent: this.currentInvoice.F_InvoiceContent,
          F_CustomerName
        }
      }

      const [err, { data: result }] = await uni.request({
        url: this.apiRoot`/crm/invoice/save`,
        method: 'POST',
        header: { 'content-type': 'application/x-www-form-urlencoded' },
        data: { ...this.auth, data: JSON.stringify(postData) }
      })

      if (err || result.code !== 200) {
        uni.hideLoading()
        uni.showToast({ title: `开票信息提交失败`, icon: 'none' })

        return
      }

      this.originInvoice = Object.assign({}, this.currentInvoice)
      this.mode = 'view'
      uni.hideLoading()
      uni.showToast({ title: `开票信息提交成功`, icon: 'success' })
      uni.$emit('invoice-list-change')
    },

    cancel() {
      this.ready = false
      this.currentInvoice = Object.assign({}, this.originInvoice)
      this.mode = 'view'
      this.ready = true
    }
  },

  computed: {
    customers() {
      return this.customerList.map(item => ({ text: item.F_FullName, value: item.F_CustomerId }))
    }
  }
}
</script>

<style lang="less" scoped></style>
