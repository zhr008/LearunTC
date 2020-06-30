<template>
  <view class="cu-bar foot input" :style="[{ bottom: inputBottom + 'px' }]">
    <input
      :adjust-position="false"
      :focus="false"
      :value="value"
      :placeholder="placeholder"
      :disabled="inputDisabled"
      class="solid-bottom"
      cursor-spacing="10"
      @focus="inputFocus"
      @blur="inputBlur"
      @input="input"
    />
    <slot name="action">
      <button @click="sendClick" :disabled="buttonDisabled" class="cu-btn bg-green shadow">发送</button>
    </slot>
  </view>
</template>

<script>
export default {
  name: 'l-chat-input',

  props: {
    value: null,
    placeholder: null,
    inputDisabled: { type: Boolean },
    buttonDisabled: { type: Boolean }
  },

  data() {
    return { inputBottom: 0 }
  },

  methods: {
    input(e) {
      this.$emit('input', e.detail.value)
      this.$emit('change', e.detail.value)
    },

    inputFocus(e) {
      this.inputBottom = e.detail.height
      this.$emit('focus')
    },

    inputBlur(e) {
      this.inputBottom = 0
      this.$emit('blur')
    },

    sendClick(e) {
      this.$emit('sendMsg', this.value)
    }
  }
}
</script>
