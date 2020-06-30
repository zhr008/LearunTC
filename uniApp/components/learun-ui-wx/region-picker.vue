<template>
  <view class="cu-form-group">
    <view class="title">{{ title }}</view>
    <picker
      mode="region"
      @change="change"
      :class="[arrow ? 'picker-arrow' : '']"
      :customitem="customitem"
      :value="value"
      :disabled="disabled"
    >
      <view class="picker">
        <slot v-if="$slot.default"></slot>
        <template v-else>
          {{ display || placeholder }}
        </template>
      </view>
    </picker>
  </view>
</template>

<script>
export default {
  name: 'l-region-picker',

  props: {
    title: { type: String },
    disabled: { type: Boolean },
    placeholder: { type: String, default: '请选择地区...' },
    multiple: { type: Boolean },
    customitem: { type: String },
    arrow: { type: Boolean, default: true }
  },

  data() {
    return {
      value: []
    }
  },

  methods: {
    change(e) {
      this.value = e.detail.value
      this.$emit('change', this.value)
      this.$emit('input', this.value)
    }
  },

  computed: {
    display() {
      const { value } = this
      if (!value || !value.length) {
        return null
      }

      return value.join(' ')
    }
  }
}
</script>
