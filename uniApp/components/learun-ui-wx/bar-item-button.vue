<template>
  <view class="submit" :class="['bg-' + color]">{{ title }}</view>
</template>

<script>
export default {
  name: 'l-bar-item-button',

  props: {
    title: { type: String },
    color: { type: String, default: 'gray' }
  }
}
</script>
