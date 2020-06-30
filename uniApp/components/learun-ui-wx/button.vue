<template>
  <button
    class="cu-btn"
    hover-class="button-hover"
    :class="[size, round ? 'round' : null, shadow ? 'shadow' : null, buttonColor, buttonBorder, block ? 'block' : null]"
    :disabled="disabled ? 'disabled' : null"
    :type="disabled && line ? 'default' : null"
    @click="click"
  >
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: 'l-button',

  props: {
    size: { type: String, default: 'df' },
    color: { type: String },
    round: { type: Boolean },
    shadow: { type: Boolean },
    line: { type: [String, Boolean] },
    blod: { type: Boolean },
    icon: { type: String },
    disabled: { type: Boolean },
    block: { type: Boolean }
  },

  computed: {
    buttonBorder() {
      return this.line && `line${this.blod ? 's' : ''}-${this.line || 'gray'}`
    },

    buttonColor() {
      if (this.color) {
        return `bg-${this.color}`
      } else if (!this.line) {
        return `bg-gray`
      }

      return null
    }
  },

  methods: {
    click(e) {
      this.$emit('click', e)
    }
  }
}
</script>
