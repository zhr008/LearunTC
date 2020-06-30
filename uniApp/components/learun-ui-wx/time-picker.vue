<template>
  <view class="cu-form-group">
    <view class="title">{{ title }}</view>
    <picker
      mode="time"
      :class="[arrow ? 'picker-arrow' : '']"
      :value="value"
      :start="start"
      :end="end"
      :disabled="disabled"
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
  name: 'l-time-picker',

  props: {
    title: { type: String },
    start: { type: String, default: '00:00' },
    end: { type: String, default: '23:59' },
    disabled: { type: Boolean },
    placeholder: { type: String, default: '请选择时间...' },
    arrow: { type: Boolean, default: true }
  },

  data() {
    return {
      value: null
    }
  },

  methods: {
    change(e) {
      this.value = e.detail.value
      this.$emit('change', this.value)
      this.$emit('input', this.value)
    }
  }
}
</script>
