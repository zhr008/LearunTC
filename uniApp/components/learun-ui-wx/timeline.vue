<template>
  <view class="cu-timeline">
    <view class="cu-time">
      <slot name="title">{{ title }}</slot>
    </view>
    <slot></slot>
  </view>
</template>

<script>
export default {
  name: 'l-timeline',

  props: {
    title: { type: null }
  }
}
</script>
