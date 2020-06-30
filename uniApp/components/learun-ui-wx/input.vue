<template>
  <view class="cu-form-group">
    <view class="title">
      <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
      <slot name="title">{{ title }}</slot>
    </view>
    <input
      @input="input"
      :type="type"
      :disabled="disabled"
      :password="password"
      :maxlength="maxlength"
      :placeholder="placeholder"
      :value="value"
      :style="{ textAlign: right ? 'right' : 'left' }"
    />
    <slot name="suffix"></slot>
  </view>
</template>

<script>
export default {
  name: 'l-input',

  props: {
    type: { type: String, default: 'text' },
    placeholder: { type: String },
    maxlength: { type: Number, default: -1 },
    password: { type: Boolean },
    right: { type: Boolean },
    title: { type: String },
    disabled: { type: Boolean },
    value: { type: null },
    required: { type: Boolean }
  },

  methods: {
    input(e) {
      this.$emit('input', e.detail.value)
      this.$emit('change', e.detail.value)
    }
  }
}
</script>
