<template>
  <view class="cu-form-group">
    <view class="title">{{ title }}</view>
    <checkbox
      :class="[computed ? 'checked' : '', color ? color : null, round ? 'round' : null]"
      :checked="computed"
      :value="checkboxValue"
      :disabled="disabled"
      @click="change"
    ></checkbox>
  </view>
</template>

<script>
export default {
  name: 'l-checkbox',

  props: {
    title: { type: String },
    disabled: { type: Boolean },
    round: { type: Boolean },
    color: { type: String },
    checkboxValue: null,
    value: { type: Array, default: () => [] }
  },

  computed: {
    isCheck() {
      return this.value.includes(this.checkboxValue)
    }
  },

  methods: {
    change(e) {
      let arr = this.value
      const isCurrentCheck = this.value.includes(this.checkboxValue)

      if (isCurrentCheck) {
        arr = arr.filter(t => t !== this.checkboxValue)
      } else {
        arr.push(this.checkboxValue)
      }

      this.$emit('change', arr)
      this.$emit('input', arr)
    }
  }
}
</script>
