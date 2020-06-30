<template>
  <view class="cu-form-group">
    <view class="title">
      <slot name="title">
        <text v-if="required" style="color:red;font-size: 1.2em;">*</text>
        {{ title }}
      </slot>
    </view>
    <view @click="itemClick" class="action form-item-action" :class="[arrow ? 'form-item-arrow' : 'form-item-noarrow']">
      <view class="picker"><slot></slot></view>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-label',

  props: {
    title: { type: null },
    arrow: { type: Boolean },
    required: { type: Boolean }
  },

  methods: {
    itemClick(e) {
      this.$emit('click', e)
    }
  }
}
</script>

<style>
.form-item-action {
  flex: 1;
  padding-right: 40rpx;
  overflow: hidden;
  position: relative;
  text-align: right;
}

.form-item-noarrow {
  padding-right: 0;
}
</style>
