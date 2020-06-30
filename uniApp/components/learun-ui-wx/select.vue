<template>
  <view class="cu-form-group">
    <view class="title">
      <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
      {{ title }}
    </view>
    <picker
      @change="change"
      :mode="multiple ? 'multiSelector' : 'selector'"
      :range="innerRange"
      :value="index"
      :disabled="disabled"
      :class="[arrow ? 'picker-arrow' : '']"
    >
      <view class="picker">
        <slot>{{ display }}</slot>
      </view>
    </picker>
  </view>
</template>

<script>
export default {
  name: 'l-select',

  props: {
    title: { type: String },
    disabled: { type: Boolean },
    range: { type: Array },
    placeholder: { type: String, default: '请选择...' },
    multiple: { type: Boolean },
    value: { type: null },
    emptyText: { type: String, default: '(未选择)' },
    splitText: { type: String, default: ' ' },
    required: { type: Boolean },
    arrow: { type: Boolean, default: true }
  },

  data() {
    return {
      index: -1
    }
  },

  model: {
    prop: 'value',
    event: 'input'
  },

  mounted() {
    this.calcIndex()
  },

  methods: {
    change(e) {
      this.index = e.detail.value
      this.$emit('change', this.currentModel)
      this.$emit('input', this.currentModel)
    },

    calcIndex() {
      if (this.multiple) {
        if (this.objectMode) {
          this.index = this.range.map((group, idx) => group.findIndex(t => t.value === this.value[idx]))
        } else {
          this.index = this.range.map((group, idx) => group.indexOf(this.value[idx]))
        }
      } else {
        if (this.objectMode) {
          this.index = this.range.findIndex(t => t.value === this.value)
        } else {
          this.index = this.range.indexOf(this.value)
        }
      }
    }
  },

  computed: {
    objectMode() {
      return typeof (this.multiple ? this.range[0][0] : this.range[0]) === 'object'
    },

    innerRange() {
      if (!this.objectMode) {
        return this.range
      }

      if (this.multiple) {
        return this.range.map(item => item.map(t => t.text))
      }

      return this.range.map(t => t.text)
    },

    currentModel() {
      const { multiple, range, index, objectMode } = this
      if (!multiple) {
        return index === -1 ? undefined : objectMode ? range[index].value : range[index]
      }

      if (!objectMode) {
        return range.map((group, idx) => group[index[idx]])
      }

      range.map((group, idx) => (index[idx] === -1 ? undefined : group[index[idx]].value))
    },

    display() {
      const { multiple, range, value, index, objectMode, emptyText, placeholder, splitText } = this
      if (!multiple) {
        return index === -1 ? placeholder : objectMode ? range[index].text : range[index]
      }

      if (!Array.isArray(index) || index.every(t => t === -1)) {
        return placeholder
      }

      if (!objectMode) {
        return range.map((group, idx) => group[index[idx]] || emptyText).join(splitText)
      }

      return range.map((group, idx) => (index[idx] === -1 ? emptyText : group[index[idx]].text)).join(splitText)
    }
  },

  watch: {
    value() {
      this.calcIndex()
    }
  }
}
</script>
