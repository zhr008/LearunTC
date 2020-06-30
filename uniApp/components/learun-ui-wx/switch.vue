<template>
  <view class="cu-form-group">
    <view class="title">{{ title }}</view>
    <switch
      :class="[value ? 'checked' : '', icon ? 'switch-' + icon : null, color ? color : null, radius ? 'radius' : null]"
      :checked="value"
      :disabled="disabled"
      @change="change"
    ></switch>
  </view>
</template>

<script>
export default {
  name: 'l-switch',

  props: {
    title: { type: String },
    color: { type: String },
    icon: { type: String },
    radius: { type: Boolean },
    disabled: { type: Boolean }
  },

  data() {
    return {
      value: false
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
