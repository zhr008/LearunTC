<template>
  <view v-if="formMode">
    <view class="cu-form-group" style="border-bottom: none">
      <view class="title">
        <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
        {{ title }}
      </view>
    </view>
    <view class="cu-form-group" style="position: relative;border-top: none">
      <textarea
        @input="textareaInput"
        @focus.stop.prevent
        :maxlength="maxlength"
        :placeholder="displayHolder"
        :fixed="fixed"
        :value="display"
        :auto-height="autoHeight"
        style="margin-top:0"
      ></textarea>
      <cover-view v-if="readonly" @click.stop.prevent class="textarea-mask"></cover-view>
    </view>
  </view>

  <view v-else class="cu-form-group" style="position: relative;">
    <textarea
      @input="textareaInput"
      @focus.stop.prevent
      :maxlength="maxlength"
      :placeholder="displayHolder"
      :fixed="fixed"
      :value="display"
      :style="textareaStyle"
      :auto-height="autoHeight"
    ></textarea>
    <cover-view v-if="readonly" @click.stop.prevent class="textarea-mask"></cover-view>
  </view>
</template>

<script>
export default {
  name: 'l-textarea',

  props: {
    maxlength: { type: Number, default: -1 },
    readonly: { type: Boolean },
    placeholder: { type: String, default: '请输入...' },
    fixed: { type: Boolean },
    value: { type: String },
    textareaStyle: { type: String },
    autoHeight: { type: Boolean },
    hide: { type: Boolean },
    formMode: { type: Boolean },
    required: { type: Boolean },
    title: { type: String }
  },

  methods: {
    textareaInput(e) {
      this.$emit('change', e.detail.value)
      this.$emit('input', e.detail.value)
    }
  },

  computed: {
    display() {
      return this.hide ? '' : this.value
    },

    displayHolder() {
      return this.hide ? '' : this.placeholder
    }
  }
}
</script>

<style scoped lang="less">
.textarea-mask {
  z-index: 999;
  position: absolute;
  bottom: 0;
  top: 0;
  left: 0;
  right: 0;
}
</style>
