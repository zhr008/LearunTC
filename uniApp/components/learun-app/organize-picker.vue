<template>
  <l-label @click="itemClick" :arrow="arrow" :required="required" :title="title">{{ display || placeholder }}</l-label>
</template>

<script>
export default {
  name: 'l-organize-picker',

  props: {
    value: { type: String, default: null },
    type: { type: String, required: true },
    title: { type: String, required: true },
    arrow: { type: Boolean },
    required: { type: Boolean },
    placeholder: { type: String, default: '点击以选择' },
    readonly: { type: Boolean }
  },

  methods: {
    itemClick() {
      if (this.readonly) {
        return
      }

      uni.$once(`select-${this.type}`, data => {
        this.$emit('input', data.id)
        this.$emit('change', data.id)
      })
      uni.navigateTo({ url: `/pages/common/select-${this.type}` })
    }
  },

  computed: {
    display() {
      if (!this.value) {
        return null
      }
      const key = { user: 'staff', department: 'dep', company: 'company' }[this.type]
      const orgItem = this.$store.state[key][this.value]

      return orgItem ? orgItem.name : null
    }
  }
}
</script>
