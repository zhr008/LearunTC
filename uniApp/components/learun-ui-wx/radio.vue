<template>
  <view class="cu-form-group">
    <view class="title">
      <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
      {{ title }}
    </view>
    <radio
      :class="[isCheck ? 'checked' : '', color ? color : null, point ? 'radio' : null]"
      :checked="isCheck"
      :value="radioValue"
      :disabled="disabled"
      @click="change"
    ></radio>
  </view>
</template>

<script>
export default {
  name: 'l-radio',

  props: {
    title: { type: String },
    disabled: { type: Boolean },
    point: { type: Boolean },
    color: { type: String },
    radioValue: null,
    value: null,
    required: { type: Boolean }
  },

  data() {
    return {}
  },

  computed: {
    isCheck() {
      return this.value && this.value == this.radioValue
    }
  },

  methods: {
    change(e) {
      this.$emit('change', this.radioValue)
      this.$emit('input', this.radioValue)
    }
  }
}
</script>
