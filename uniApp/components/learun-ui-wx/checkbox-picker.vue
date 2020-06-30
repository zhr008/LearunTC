<template>
  <l-label @click="click" :arrow="arrow" :required="required" :title="title">
    {{ display || placeholder }}
    <l-modal
      v-if="modal"
      @ok="confirm"
      @cancel="cancel"
      :value="modalOpen"
      :checkbox.sync="checked"
      :range="range"
      :readonly="readonly"
      type="checkbox"
    />
  </l-label>
</template>

<script>
export default {
  name: 'l-checkbox-picker',

  props: {
    value: { type: Array, required: true },
    title: { type: String, required: true },
    arrow: { type: Boolean },
    required: { type: Boolean },
    placeholder: { type: String, default: '点击以选择' },
    readonly: { type: Boolean },
    range: { type: Array, required: true }
  },

  data() {
    return {
      modal: false,
      modalOpen: false,
      checked: this.value
    }
  },

  methods: {
    click() {
      if (this.readonly || this.modal || this.modalOpen) {
        return
      }

      this.modal = true
      setTimeout(() => {
        this.modalOpen = true
        this.$emit('open')
      }, 300)
    },

    confirm() {
      this.modalOpen = false
      setTimeout(() => {
        this.modal = false
        this.$emit('input', this.checked)
        this.$emit('change', this.checked)
        this.$emit('close')
      }, 300)
    },

    cancel() {
      this.modalOpen = false
      setTimeout(() => {
        this.modal = false
        this.$emit('close')
      }, 300)
    }
  },

  computed: {
    display() {
      if (this.value.length <= 0) {
        return null
      }

      if (!this.objMode) {
        return this.value.join('，')
      }

      return this.value
        .reduce((a, b) => {
          const selected = this.range.find(t => t.value === b)
          return selected ? a.concat(selected.text) : a
        }, [])
        .join('，')
    },

    objMode() {
      return typeof this.range[0] === 'object'
    }
  }
}
</script>
