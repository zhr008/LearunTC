<template>
  <view>
    <view :key="item.id" v-for="(item, index) of scheme">
      <!-- 标题文字 label -->
      <template v-if="item.type === 'label'">
        <view v-if="index !== 0" style="height: 15px;"></view>
        <l-title border>{{ item.title }}</l-title>
      </template>

      <!-- 文字输入框 text -->
      <l-input
        v-else-if="item.type === 'text'"
        right
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :disabled="!isEdit(item)"
        :required="item.verify"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 单选和选择 radio和select -->
      <l-select
        v-else-if="item.type === 'radio' || item.type === 'select'"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :disabled="!isEdit(item)"
        :arrow="isEdit(item)"
        :required="item.verify"
        :range="item.__sourceData__"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 多选 checkbox -->
      <l-checkbox-picker
        v-else-if="item.type === 'checkbox'"
        @open="modal = 'checkbox'"
        @close="modal = null"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :readonly="!isEdit(item)"
        :required="item.verify"
        :range="item.__sourceData__"
        :arrow="isEdit(item)"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 多行文本 textarea -->
      <l-textarea
        v-else-if="item.type === 'textarea' || item.type === 'texteditor'"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :readonly="!isEdit(item)"
        :required="item.verify"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
        :hide="modal"
        formMode
      />

      <!-- 时间日期 datetime -->
      <l-datetime-picker
        v-else-if="item.type === 'datetime' && Number(item.dateformat) === 1"
        @open="modal = 'datetime'"
        @close="modal = null"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :disabled="!isEdit(item)"
        :required="item.verify"
        :arrow="isEdit(item)"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 日期 date -->
      <l-date-picker
        v-else-if="item.type === 'datetime' && Number(item.dateformat) !== 1"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :required="item.verify"
        :disabled="!isEdit(item)"
        :arrow="isEdit(item)"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 当前状态信息 currentInfo / 编码 encode / 时间区间 datetimerange -->
      <l-label
        v-else-if="item.type === 'currentInfo' || item.type === 'encode' || item.type === 'datetimerange'"
        :required="item.verify"
        :title="item.title"
      >
        {{ displayItem(item) || '' }}
      </l-label>

      <!-- 公司人员结构选单 organize -->
      <l-organize-picker
        v-else-if="item.type === 'organize'"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :type="item.dataType"
        :arrow="isEdit(item)"
        :required="item.verify"
        :readonly="!isEdit(item)"
        :title="item.title"
        :placeholder="displayPlaceHolder(item)"
      />

      <!-- 文件上传 upload -->
      <template v-else-if="item.type === 'upload'">
        <view class="cu-form-group" style="border-bottom: none">
          <view class="title">
            <text v-if="item.verify" style="color:red;font-size: 1.2em;">*</text>
            {{ item.title }}
          </view>
        </view>
        <l-upload
          @input="setValue(item.__valuePath__, $event)"
          :value="getValue(item.__valuePath__)"
          :readonly="!isEdit(item)"
          :number="1"
        />
      </template>

      <!-- HTML内容 html -->
      <view v-else-if="item.type === 'html'" class="cu-form-group">
        <view class="bg-white"><view v-html="displayItem(item)"></view></view>
      </view>

      <!-- 表格 girdtable -->
      <l-custom-form-table
        v-else-if="item.type === 'girdtable'"
        @input="setValue(item.__valuePath__, $event)"
        :value="getValue(item.__valuePath__)"
        :item="item"
        :edit="isEdit(item)"
      />
    </view>
  </view>
</template>

<script>
import _ from 'lodash'
import moment from 'moment'
import { guid, convertHtml } from '@/common/utils.js'
import LCustomFormTable from './custom-form-table.vue'
import LOrganizePicker from '@/components/learun-app/organize-picker.vue'

export default {
  name: 'l-custom-form',

  props: {
    scheme: { type: Array, default: () => [] },
    editMode: { type: Boolean, default: true },
    initFormValue: { type: Object, default: () => ({}) }
  },

  components: { LCustomFormTable, LOrganizePicker },

  data() {
    return {
      formValue: this.initFormValue,
      modal: false
    }
  },

  methods: {
    // 本方法由调用方使用 $refs 的方式调用
    // 获取 formValue 表单数据值
    getFormValue() {
      return this.formValue
    },

    // 本方法由调用方使用 $refs 的方式调用
    // 重置 formValue 表单数据值
    resetFormValue(newVal) {
      this.formValue = newVal || this.initFormValue
    },

    // 本方法由调用方使用 $refs 的方式调用
    // 依次验证表单项，返回一个所有错误提示的数组，如果为空数组则表示无错误
    verifyValue() {
      const errorList = []

      this.scheme
        .filter(t => t.verify)
        .forEach(schemeItem => {
          if (schemeItem.table && schemeItem.field) {
            const verifyFunc = this.verify[schemeItem.verify]
            const verifyResult = verifyFunc(this.getValue(schemeItem.__valuePath__))
            if (verifyResult !== true) {
              errorList.push(`[${schemeItem.title}]: ${verifyResult}`)
            }
          } else if (schemeItem.fieldsData) {
            this.getValue(schemeItem.__valuePath__).forEach((valueItem, valueIndex) => {
              schemeItem.fieldsData.forEach(fieldItem => {
                const verifyFunc = this.verify[fieldItem.verify]
                const verifyResult = verifyFunc(
                  this.getValue(`${schemeItem.__valuePath__}.${valueIndex}.${fieldItem.field}`)
                )
                if (verifyResult !== true) {
                  errorList.push(`[表格${schemeItem.title}第${tableIndex}行${fieldItem.name}列]: ${verifyResult}`)
                }
              })
            })
          }
        })

      return errorList
    },

    // 设置表单数据的方法
    setValue(path, value) {
      _.set(this.formValue, path, value)
    },

    // 获取表单数据的方法
    getValue(path) {
      return _.get(this.formValue, path)
    },

    // 获取表单项的显示内容
    // 例如组织结构选单、多选框、HTML内容，需要格式化后显示
    displayItem(item) {
      const path = item.__valuePath__

      switch (item.type) {
        case 'currentInfo':
        case 'organize':
          switch (item.dataType) {
            case 'user':
              return _.get(this.$store.state.staff, `${this.getValue(path)}.name`, '')
            case 'department':
              return _.get(this.$store.state.dep, `${this.getValue(path)}.name`, '')
            case 'company':
              return _.get(this.$store.state.company, `${this.getValue(path)}.name`, '')
            default:
              return this.getValue(path)
          }

        case 'datetimerange':
          const startTime = this.getValue(this.scheme.find(t => t.id === item.startTime).__valuePath__)
          const endTime = this.getValue(this.scheme.find(t => t.id === item.endTime).__valuePath__)
          if (!startTime || !endTime || moment(endTime).isBefore(startTime)) {
            return '-'
          }
          return moment
            .duration(moment(endTime).diff(moment(startTime)))
            .asDays()
            .toFixed(0)

        case 'html':
          return convertHtml(item.title || '')

        default:
          return this.getValue(path) || ''
      }
    },

    // 获取表单项的 placeholder ，在无法编辑时隐藏
    displayPlaceHolder(item) {
      const edit = this.isEdit(item)
      switch (item.type) {
        case 'text':
        case 'textarea':
        case 'texteditor':
          return edit ? `请输入${item.title || item.name}` : ''

        case 'checkbox':
        case 'radio':
        case 'select':
        case 'datetime':
        case 'organize':
        case 'layer':
          return edit ? `请选择${item.title || item.name}` : ''

        default:
          return ''
      }
    },

    // 获取表单项可否编辑，控制 placeholder 显示与否，以及是否显示箭头
    isEdit(item) {
      return this.editMode && item.edit !== 0
    }
  },

  computed: {
    verify() {
      return {
        NotNull: t => t.length > 0 || '不能为空',
        Num: t => !isNaN(t) || '须输入数值',
        NumOrNull: t => t.length <= 0 || !isNaN(t) || '须留空或输入数值',
        Email: t => /^[a-zA-Z0-9-_.]+@[a-zA-Z0-9-_]+.[a-zA-Z0-9]+$/.test(t) || '须符合Email格式',
        EmailOrNull: t =>
          t.length <= 0 || /^[a-zA-Z0-9-_.]+@[a-zA-Z0-9-_]+.[a-zA-Z0-9]+$/.test(t) || '须留空或符合Email格式',
        EnglishStr: t => /^[a-zA-Z]*$/.test(t) || '须由英文字母组成',
        EnglishStrOrNull: t => t.length <= 0 || /^[a-zA-Z]*$/.test(t) || '须留空或由英文字母组成',
        Phone: t => /^[+0-9- ]*$/.test(t) || '须符合电话号码格式',
        PhoneOrNull: t => t.length <= 0 || /^[+0-9- ]*$/.test(t) || '须留空或符合电话号码格式',
        Fax: t => /^[+0-9- ]*$/.test(t) || '须符合传真号码格式',
        Mobile: t => /^1[0-9]{10}$/.test(t) || '须符合手机号码格式',
        MobileOrPhone: t => /^[+0-9- ]*$/.test(t) || /^1[0-9]{10}$/.test(t) || '须符合电话或手机号码格式',
        MobileOrNull: t => t.length <= 0 || /^1[0-9]{10}$/.test(t) || '须留空或符合手机号码格式',
        MobileOrPhoneOrNull: t =>
          t.length <= 0 || /^1[0-9]{10}$/.test(t) || /^[+0-9- ]*$/.test(t) || '须留空或符合手机/电话号码格式',
        Uri: t => /^[a-zA-z]+:\/\/[^\s]*$/.test(t) || '须符合网址Url格式',
        UriOrNull: t => t.length <= 0 || /^[a-zA-z]+:\/\/[^\s]*$/.test(t) || '须留空或符合网址Url格式'
      }
    }
  }
}
</script>
