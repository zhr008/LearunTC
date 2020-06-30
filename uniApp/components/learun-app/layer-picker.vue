<template>
  <l-label @click="itemClick" :arrow="arrow" :required="required" :title="title">{{ value || placeholder }}</l-label>
</template>

<script>
export default {
  name: 'l-layer-picker',

  props: {
    value: { type: String, default: null },
    title: { type: String, required: true },
    arrow: { type: Boolean },
    required: { type: Boolean },
    placeholder: { type: String, default: '点击以选择' },
    readonly: { type: Boolean },
    source: { type: Object, required: true }
  },

  methods: {
    itemClick(e) {
      if (this.readonly) {
        return
      }

      uni.$once(`select-layer`, data => {
        const val = data[this.source.selfField]
        this.$emit('input', val)
        this.$emit('change', val)

        this.$emit('layerchange', data)
      })

      this.setPageParam(this.source)
      uni.navigateTo({
        url: `/pages/common/select-layer`
      })
    }
  }
}
</script>
