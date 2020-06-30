<template>
  <view class="page">
    <template v-if="ready">
      <l-title border>订单详情</l-title>

      <l-select
        v-model="currentOrder.crmOrderJson.F_CustomerId"
        :disabled="!editMode"
        :arrow="editMode"
        :range="customerList"
        :placeholder="editMode ? '请选择订单客户' : ''"
        title="客户名称"
        required
      />
      <l-label
        @click="selectPage('user', 'currentOrder.crmOrderJson.F_SellerId')"
        :arrow="editMode"
        title="销售人员"
        required
      >
        {{ display('staff', 'currentOrder.crmOrderJson.F_SellerId') || (editMode ? '请选择销售人员' : '') }}
      </l-label>
      <l-date-picker
        v-model="currentOrder.crmOrderJson.F_OrderDate"
        :disabled="!editMode"
        :arrow="editMode"
        :placeholder="editMode ? '请选择订单日期' : ''"
        title="单据日期"
        required
      />
      <l-input title="单据编号" :value="currentOrder.crmOrderJson.F_OrderCode" right required disabled />

      <view v-for="(tableItem, tableIndex) of currentOrder.crmOrderProductJson" :key="tableIndex">
        <view class="table-item padding-lr">
          <view class="table-item-title">产品明细 (第{{ tableIndex + 1 }}项)</view>
          <view
            v-if="tableIndex !== 0 && editMode"
            class="table-item-delete text-blue"
            @click="tableDelete(tableIndex)"
          >
            删除
          </view>
        </view>

        <l-label
          @click="selectPage('layer', `currentOrder.crmOrderProductJson.${tableIndex}.F_ProductCode`)"
          :arrow="editMode"
          title="商品名称"
        >
          {{ tableItem.F_ProductName || (editMode ? '(请点击选择商品)' : '') }}
        </l-label>
        <l-label title="商品编号">{{ tableItem.F_ProductCode || (editMode ? '(未选择)' : '') }}</l-label>
        <l-input
          @input="setValue(`currentOrder.crmOrderProductJson.${tableIndex}.F_UnitId`, $event)"
          :value="tableItem.F_UnitId"
          :disabled="!editMode"
          :placeholder="editMode ? '请输入商品单位(例如:个)' : ''"
          title="单位"
          right
        />
        <l-input
          @input="setValue(`currentOrder.crmOrderProductJson.${tableIndex}.F_Qty`, $event, tableIndex)"
          :value="tableItem.F_Qty"
          :disabled="!editMode"
          :placeholder="editMode ? '请输入商品的数量' : ''"
          type="digit"
          title="数量"
          right
        />
        <l-input
          @input="setValue(`currentOrder.crmOrderProductJson.${tableIndex}.F_Price`, $event, tableIndex)"
          :value="tableItem.F_Price"
          :disabled="!editMode"
          :placeholder="editMode ? '请输入商品单价' : ''"
          type="digit"
          title="单价"
          right
        />
        <l-input
          @input="setValue(`currentOrder.crmOrderProductJson.${tableIndex}.F_TaxRate`, $event, tableIndex)"
          :value="tableItem.F_TaxRate"
          :disabled="!editMode"
          :placeholder="editMode ? '请输入商品税率百分比' : ''"
          type="digit"
          title="税率 (单位 %)"
          right
        />
        <l-label title="含税单价">{{ tableItem.F_Taxprice || '' }}</l-label>
        <l-label title="总税额">{{ tableItem.F_Tax || '' }}</l-label>
        <l-label title="不含税总金额">{{ tableItem.F_Amount || '' }}</l-label>
        <l-label title="含税总金额">{{ tableItem.F_TaxAmount || '' }}</l-label>
        <l-input
          @input="setValue(`currentOrder.crmOrderProductJson.${tableIndex}.F_Description`, $event)"
          :value="tableItem.F_Description"
          :disabled="!editMode"
          :placeholder="editMode ? '请输入商品相关说明' : ''"
          title="说明信息"
          right
        />
      </view>
      <view v-if="editMode" class="bg-white flex flex-wrap justify-center align-center solid-bottom" @click="tableAdd">
        <view class="add-btn text-blue padding">+ 添加一行表格</view>
      </view>

      <view class="cu-form-group" style="border-bottom: none"><view class="title">备注信息</view></view>
      <l-textarea
        v-model="currentOrder.crmOrderJson.F_Description"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入备注信息' : ''"
        textareaStyle="margin-top:0"
      />
      <l-input
        v-model="currentOrder.crmOrderJson.F_DiscountSum"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入优惠金额' : ''"
        title="优惠金额"
        type="digit"
        required
        right
      />
      <l-input
        v-model="currentOrder.crmOrderJson.F_Accounts"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入收款金额' : ''"
        title="收款金额"
        type="digit"
        required
        right
      />
      <l-date-picker
        v-model="currentOrder.crmOrderJson.F_PaymentDate"
        :disabled="!editMode"
        :arrow="editMode"
        :placeholder="editMode ? '请选择收款日期' : ''"
        title="收款日期"
        required
      />
      <l-select
        v-model="currentOrder.crmOrderJson.F_PaymentMode"
        :disabled="!editMode"
        :arrow="editMode"
        :range="paymentList"
        title="收款方式"
        placeholder="请选择收款方式"
        required
      />
      <l-input
        v-model="currentOrder.crmOrderJson.F_SaleCost"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入销售费用' : ''"
        title="销售费用"
        required
        right
      />
      <l-label title="制单人员" required>{{ currentOrder.crmOrderJson.F_CreateUserName || '' }}</l-label>
      <l-input
        v-model="currentOrder.crmOrderJson.F_ContractCode"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入关合同的编号' : ''"
        title="合同编号"
        right
      />
      <view class="cu-form-group" style="border-bottom: none"><view class="title">摘要信息</view></view>
      <l-textarea
        v-model="currentOrder.crmOrderJson.F_AbstractInfo"
        :disabled="!editMode"
        :placeholder="editMode ? '请输入摘要信息' : ''"
        textareaStyle="margin-top:0"
      />
    </template>

    <view class="fixbar">
      <view v-if="mode !== 'create' && !editMode" @click="action('delete')" class="btn line-red">
        <l-icon type="delete" />
        删除
      </view>
      <view v-if="mode !== 'create' && editMode" @click="action('reset')" class="btn line-orange">
        <l-icon type="refresh" />
        复原
      </view>
      <view v-if="editMode" @click="action('save')" class="btn line-green" style="min-width: 100px;text-align: center;">
        <l-icon type="check" />
        保存
      </view>
      <view v-else @click="action('edit')" class="btn line-blue" style="min-width: 100px;text-align: center;">
        <l-icon type="edit" />
        编辑
      </view>
    </view>
  </view>
</template>

<script>
import _ from 'lodash'
import moment from 'moment'
import { copy } from '@/common/utils.js'

const orderProductTemplate = {
  F_ProductCode: '',
  F_ProductName: '',
  F_UnitId: '',
  F_Qty: '',
  F_Price: '',
  F_Amount: '',
  F_Taxprice: '',
  F_TaxRate: '',
  F_Tax: '',
  F_TaxAmount: ''
}

const orderTemplate = {
  crmOrderJson: {
    F_CustomerId: '',
    F_SellerId: '',
    F_OrderDate: '',
    F_OrderCode: '',
    F_Description: '',
    F_DiscountSum: '',
    F_Accounts: '',
    F_PaymentDate: '',
    F_PaymentMode: '',
    F_SaleCost: '',

    F_CreateUserId: '',
    F_CreateUserName: '',
    F_ContractCode: '',
    F_AbstractInfo: ''
  },
  crmOrderProductJson: []
}

export default {
  data() {
    return {
      mode: '',
      editMode: false,
      ready: false,

      originOrder: {},
      currentOrder: {},

      customerList: [],
      paymentList: []
    }
  },

  async onLoad({ type, id }) {
    await this.init(type, id)
  },

  methods: {
    async init(type = 'view', id) {
      this.mode = type
      this.editMode = ['create', 'edit'].includes(this.mode)
      uni.showLoading({ title: `加载数据中...`, mask: true })

      this.paymentList = Object.values(this.$store.state.propTable.Client_PaymentMode)
      await uni
        .request({
          url: this.apiRoot`/crm/customer/list`,
          data: { ...this.auth }
        })
        .then(([err, { data: { data: result } } = {}]) => {
          this.customerList = result.map(t => ({ text: t.F_FullName, value: t.F_CustomerId }))
        })

      if (this.mode === 'create') {
        this.originOrder = copy(orderTemplate)
        this.originOrder.crmOrderProductJson.push(copy(orderProductTemplate))

        const code = await this.fetchCode()
        this.originOrder.crmOrderJson.F_OrderCode = code
        this.originOrder.crmOrderJson.F_CreateUserName = this.$store.state.user.realName
      } else {
        this.originOrder = await this.fetchOrderInfo(id)
      }

      this.currentOrder = copy(this.originOrder)

      uni.hideLoading()
      this.ready = true
    },

    async action(type) {
      switch (type) {
        case 'delete':
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
                  data: { ...this.auth, data: this.originOrder.crmOrderJson.F_OrderId }
                })
                .then(([err, { data }]) => {
                  if (err || !data || data.code !== 200) {
                    uni.showToast({ title: '删除失败', icon: 'none' })
                    return
                  }

                  uni.$emit('order-list-change')
                  uni.navigateBack()
                  uni.showToast({ title: '删除成功', icon: 'success' })
                })
                .catch(() => {
                  uni.showToast({ title: '删除失败', icon: 'none' })
                })
            }
          })
          return

        case 'edit':
          this.editMode = true
          return

        case 'reset':
          this.editMode = false
          this.currentOrder = copy(this.originOrder)

          return

        case 'save':
          const errList = this.verifyOrder()
          if (errList.length > 0) {
            uni.showModal({
              title: '存在错误输入项',
              content: errList.join('\n'),
              showCancel: false
            })

            return
          }

          uni.showModal({
            title: '保存订单',
            content: `确定要提交保存该订单吗？`,
            success: ({ confirm }) => {
              if (!confirm) {
                return
              }

              uni
                .request({
                  url: this.apiRoot`/crm/order/save`,
                  method: 'POST',
                  header: { 'content-type': 'application/x-www-form-urlencoded' },
                  data: { ...this.auth, data: this.getPostData() }
                })
                .then(([err, { data }]) => {
                  if (err || !data || data.code !== 200) {
                    uni.showToast({ title: '保存失败', icon: 'none' })
                    return
                  }

                  uni.$emit('order-list-change')
                  if (this.mode === 'create') {
                    uni.navigateBack()
                    uni.showToast({ title: '已成功创建订单', icon: 'success' })

                    return
                  }

                  this.editMode = false
                  this.originOrder = copy(this.currentOrder)
                  uni.showToast({ title: '已成功修改订单', icon: 'success' })
                })
                .catch(() => {
                  uni.showToast({ title: '保存失败', icon: 'none' })
                })
            }
          })
          return

        default:
          return
      }
    },

    getPostData() {
      const crmOrderJson = copy(this.currentOrder.crmOrderJson)
      const crmOrderProductJson = copy(this.currentOrder.crmOrderProductJson)

      const setEmptyValue = obj => {
        Object.entries(obj).forEach(([k, v]) => {
          if (v === undefined || v === null) {
            obj[k] = ''
          }
        })
      }

      setEmptyValue(crmOrderJson)
      crmOrderProductJson.forEach(t => setEmptyValue(t))
      const result = {
        crmOrderJson: JSON.stringify(crmOrderJson),
        crmOrderProductJson: JSON.stringify(crmOrderProductJson)
      }

      if (this.mode !== 'create') {
        result.keyValue = this.originOrder.F_OrderId
      }

      return JSON.stringify(result)
    },

    setValue(path, value, tableIndex) {
      _.set(this, path, value)

      if (tableIndex !== undefined) {
        this.calcPrice(tableIndex)
      }
    },

    selectPage(type, path) {
      if (!this.editMode) {
        return
      }

      if (type === 'layer') {
        uni.$once(`select-layer`, data => {
          _.set(this, path.replace('F_ProductCode', 'F_ProductName'), data.text)
          _.set(this, path, data.id)
        })
        uni.navigateTo({
          url: `/pages/common/select-layer?source=Client_ProductInfo&keyName=商品名称&valueName=商品编号`
        })

        return
      }

      uni.$once(`select-${type}`, data => {
        _.set(this, path, data.id)
      })
      uni.navigateTo({ url: `/pages/common/select-${type}` })
    },

    display(type, path) {
      const val = _.get(this, path, '-')

      if (type === 'layer') {
        const result = Object.values(this.$store.state.propTable.Client_ProductInfo).find(t => t.value === val)
        return result ? result.text : ''
      }

      return _.get(this.$store.state, `${type}.${val}.name`, '')
    },

    async fetchOrderInfo(id) {
      const [orderErr, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/crm/order/form`,
        data: { ...this.auth, data: id }
      })

      const val = { crmOrderJson: result.orderData, crmOrderProductJson: result.orderProductData }
      val.crmOrderJson.F_CreateDate = moment(val.crmOrderJson.F_CreateDate).format('YYYY-MM-DD')
      val.crmOrderJson.F_OrderDate = moment(val.crmOrderJson.F_OrderDate).format('YYYY-MM-DD')
      val.crmOrderJson.F_PaymentDate = moment(val.crmOrderJson.F_PaymentDate).format('YYYY-MM-DD')

      return val
    },

    async fetchCode() {
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/coderule/code`,
        data: { ...this.auth, data: 10000 }
      })

      return result
    },

    calcPrice(index) {
      const listItem = copy(this.currentOrder.crmOrderProductJson[index])

      const singlePrice = Number(listItem.F_Price) || 0
      const num = Number(listItem.F_Qty) || 0
      const taxRate = Number(listItem.F_TaxRate) || 0

      listItem.F_Amount = (singlePrice * num).toFixed(2)
      listItem.F_Taxprice = (singlePrice * (1 + taxRate / 100)).toFixed(2)
      listItem.F_Tax = ((singlePrice * num * taxRate) / 100).toFixed(2)
      listItem.F_TaxAmount = (singlePrice * num * (1 + taxRate / 100)).toFixed(2)

      this.$set(this.currentOrder.crmOrderProductJson, index, listItem)
    },

    tableAdd() {
      this.currentOrder.crmOrderProductJson.push(copy(orderProductTemplate))
    },

    tableDelete(tableIndex) {
      this.currentOrder.crmOrderProductJson.splice(tableIndex, 1)
    },

    verifyOrder() {
      const errList = []
      const { crmOrderJson } = this.currentOrder

      if (!crmOrderJson.F_CustomerId) {
        errList.push('必须选择一个客户')
      }
      if (!crmOrderJson.F_SellerId) {
        errList.push('必须选择销售人员')
      }
      if (!crmOrderJson.F_OrderDate) {
        errList.push('必须选择单据日期')
      }
      if (!crmOrderJson.F_DiscountSum || isNaN(crmOrderJson.F_DiscountSum)) {
        errList.push('请正确输入优惠金额')
      }
      if (!crmOrderJson.F_Accounts || isNaN(crmOrderJson.F_Accounts)) {
        errList.push('请正确输入收款金额')
      }
      if (!crmOrderJson.F_PaymentDate) {
        errList.push('必须选择收款日期')
      }
      if (!crmOrderJson.F_PaymentDate) {
        errList.push('必须选择收款方式')
      }
      if (!crmOrderJson.F_SaleCost || isNaN(crmOrderJson.F_SaleCost)) {
        errList.push('请正确输入销售费用')
      }

      return errList
    }
  }
}
</script>

<style lang="less" scoped>
.table-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  height: 30px;

  .table-item-action {
    cursor: pointer;
  }
}

.add-btn {
  text-align: center;
  line-height: 1em;
}

.fixbar {
  position: fixed;
  bottom: 10px;
  bottom: calc(10px + constant(safe-area-inset-bottom));
  bottom: calc(10px + env(safe-area-inset-bottom));
  right: 5px;
  z-index: 1000;
  font-size: 16px;

  .btn {
    display: inline-block;
    padding: 4px 6px;
    margin: 0 3px;
    border-radius: 3px;
    background-color: #fff;
    border: currentColor 1px solid;
  }
}

.page {
  margin-bottom: 100rpx;
  margin-bottom: calc(100rpx + constant(safe-area-inset-bottom));
  margin-bottom: calc(100rpx + env(safe-area-inset-bottom));
}
</style>
