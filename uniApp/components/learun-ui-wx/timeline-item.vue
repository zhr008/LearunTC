<template>
  <view class="cu-item" :class="[icon ? 'cuIcon-' + icon : '']">
    <view class="content shadow-blur" :class="['bg-' + color]" :style="contentStyle">
      <slot name="time">
        <text class="margin-right-sm" v-if="time">{{ time }}</text>
      </slot>
      <slot></slot>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-timeline-item',

  props: {
    time: { type: null },
    icon: { type: String },
    color: { type: String, default: 'blue' },
    contentStyle: { type: String }
  }
}
</script>
