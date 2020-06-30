<template>
  <view
    class="cu-avatar"
    :class="[size, round ? 'round' : null, radius ? 'radius' : null, color ? 'bg-' + color : null]"
    :style="{
      backgroundImage: src ? 'url(' + src + ')' : null,
      backgroundColor: color || src ? null : '#ccc'
    }"
  >
    <slot v-if="!src"></slot>
    <slot name="badge"></slot>
  </view>
</template>

<script>
export default {
  name: 'l-avatar',

  props: {
    src: { type: String },
    round: { type: Boolean },
    radius: { type: Boolean },
    size: { type: String, default: 'df' },
    color: { type: String }
  }
}
</script>
