<template>
  <view class="cu-form-group">
    <view class="title">
      <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
      {{ title }}
    </view>
    <picker
      mode="date"
      :class="[arrow ? 'picker-arrow' : '']"
      :fields="fields"
      :disabled="disabled"
      :value="value"
      :start="start"
      :end="end"
      @change="change"
    >
      <view class="picker">
        <slot v-if="$slot.default"></slot>
        <template v-else>
          {{ value || placeholder }}
        </template>
      </view>
    </picker>
  </view>
</template>

<script>
export default {
  name: 'l-date-picker',

  props: {
    title: { type: String },
    start: { type: String, default: '1900-01-01' },
    end: { type: String, default: '2100-01-01' },
    fields: { type: String, default: 'day' },
    disabled: { type: Boolean },
    placeholder: { type: String, default: '请选择日期...' },
    arrow: { type: Boolean, default: true },
    required: { type: Boolean },
    value: null
  },

  data() {
    return {}
  },

  methods: {
    change(e) {
      this.$emit('change', e.detail.value)
      this.$emit('input', e.detail.value)
    }
  }
}
</script>
