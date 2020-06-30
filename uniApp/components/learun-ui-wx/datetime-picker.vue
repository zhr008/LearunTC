<template>
  <l-label @click.self="click" :arrow="arrow" :required="required" :title="title">
    {{ value || placeholder }}
    <l-datetime-panel
      v-if="datetimeModal"
      ref="datetime"
      @confirm="confirm"
      @cancel="cancel"
      :val="value"
      :startYear="1900"
      isAll
    />
  </l-label>
</template>

<script>
export default {
  name: 'l-datetime-picker',

  props: {
    title: { type: String },
    disabled: { type: Boolean },
    placeholder: { type: String, default: '请选择日期...' },
    arrow: { type: Boolean, default: true },
    required: { type: Boolean },
    value: null
  },

  data() {
    return {
      datetimeModal: false
    }
  },

  methods: {
    click(e) {
      if (this.disabled || this.datetimeModal) {
        return
      }

      this.datetimeModal = true
      this.$nextTick(() => {
        this.$refs.datetime.setDate(this.value)
        this.$refs.datetime.show()
        this.$emit('open')
      })
    },

    confirm({ selectRes }) {
      setTimeout(() => {
        this.datetimeModal = false
        this.$emit('input', selectRes)
        this.$emit('change', selectRes)
        this.$emit('close')
      }, 300)
    },

    cancel() {
      setTimeout(() => {
        this.datetimeModal = false
      }, 300)
      this.$emit('close')
    }
  }
}
</script>
