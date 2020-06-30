<template>
  <view>
    <!-- 循环按照 formValue 里的行来渲染表格块 -->
    <view v-for="(valueItem, valueIndex) in value" :key="valueIndex">
      <!-- 表格内部的组件 只有几种 (layer 类似 select，label 类似 title) -->
      <!-- 表格标题 除了第一个，后面的都有删除按钮 -->
      <view class="table-item padding-lr">
        <view class="table-item-title">{{ item.title }} (第{{ valueIndex + 1 }}行)</view>
        <view v-if="valueIndex !== 0 && edit" class="table-item-delete text-blue" @click="tableDelete(valueIndex)">
          删除
        </view>
      </view>

      <!-- 循环按照 fieldsData 里面定义的列来渲染组件 -->
      <view v-for="tableItem of item.fieldsData" :key="tableItem.id">
        <!-- 标题文字 label -->
        <l-label v-if="tableItem.type === 'label'" :title="tableItem.name"></l-label>
        
        <!-- 文字输入框 input (text) -->
        <l-input
          v-else-if="tableItem.type === 'input'"
          right
          @input="setTableValue(`${valueIndex}.${tableItem.field}`, $event)"
          :value="getTableValue(`${valueIndex}.${tableItem.field}`)"
          :disabled="!edit"
          :required="tableItem.verify"
          :title="tableItem.name"
          :placeholder="displayPlaceHolder(tableItem)"
        />
        
        <!-- 单选和选择 radio select -->
        <l-select
          v-else-if="tableItem.type === 'radio' || tableItem.type === 'select'"
          @input="setTableValue(`${valueIndex}.${tableItem.field}`, $event)"
          :value="getTableValue(`${valueIndex}.${tableItem.field}`)"
          :required="tableItem.verify"
          :range="tableItem.__sourceData__"
          :title="tableItem.name"
          :disabled="!edit"
          :arrow="edit"
          :placeholder="displayPlaceHolder(tableItem)"
        />
        
        <!-- 弹层选择器 layer -->
        <l-layer-picker
          v-else-if="tableItem.type === 'layer'"
          @input="setTableValue(`${valueIndex}.${tableItem.field}`, $event)"
          :value="getTableValue(`${valueIndex}.${tableItem.field}`)"
          :arrow="edit"
          :required="tableItem.verify"
          :readonly="!edit"
          :title="tableItem.name"
          :placeholder="displayPlaceHolder(tableItem)"
          :source="tableItem.__sourceData__"
        />
        
        <!-- 时间日期 datetime -->
        <l-date-picker
          v-else-if="tableItem.type === 'datetime'"
          @input="setTableValue(`${valueIndex}.${tableItem.field}`, $event)"
          :value="getTableValue(`${valueIndex}.${tableItem.field}`)"
          :required="tableItem.verify"
          :disabled="!edit"
          :arrow="edit"
          :title="tableItem.name"
          :placeholder="displayPlaceHolder(tableItem)"
        />
        
        <!-- 多选 checkbox -->
        <l-checkbox-picker
          v-else-if="tableItem.type === 'checkbox'"
          @input="setTableValue(`${valueIndex}.${tableItem.field}`, $event)"
          :value="getTableValue(`${valueIndex}.${tableItem.field}`)"
          :readonly="!edit"
          :range="tableItem.__sourceData__"
          :arrow="edit"
          :required="tableItem.verify"
          :title="tableItem.name"
          :placeholder="displayPlaceHolder(tableItem)"
        />
      </view>
    </view>

    <!-- 添加表格按钮 -->
    <view v-if="edit" @click="tableAdd()" class="bg-white flex flex-wrap justify-center align-center solid-bottom">
      <view class="add-btn text-blue padding">+ 添加一行表格</view>
    </view>
  </view>
</template>

<script>
import _ from 'lodash'
import LLayerPicker from '@/components/learun-app/layer-picker.vue'

export default {
  name: 'l-custom-form-table',

  components: { LLayerPicker },

  props: {
    item: { type: Object, required: true },
    value: { type: Array, required: true },
    edit: { type: Boolean, default: true }
  },

  methods: {
    // 向外输出值
    output(val) {
      this.$emit('input', val)
    },

    // 设置表单数据的方法
    setTableValue(path, value) {
      const newVal = JSON.parse(JSON.stringify(this.value))
      _.set(newVal, path, value)
      this.output(newVal)
    },

    // 获取表单数据的方法
    getTableValue(path) {
      return _.get(this.value, path)
    },

    // 删除表格行
    tableDelete(tableIndex) {
      this.output(this.value.filter((t, i) => i !== tableIndex))
    },

    // 添加表格行
    tableAdd() {
      this.output([...this.value, JSON.parse(JSON.stringify(this.item.__defaultItem__))])
    },

    // 显示 placeholder
    displayPlaceHolder(item) {
      switch (item.type) {
        case 'text':
        case 'input':
        case 'textarea':
        case 'texteditor':
          return this.edit ? `请输入${item.name}` : ''

        case 'checkbox':
        case 'radio':
        case 'select':
        case 'datetime':
        case 'organize':
        case 'layer':
          return this.edit ? `请选择${item.name}` : ''

        default:
          return ''
      }
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
</style>
