<template>
  <view class="cu-list" :class="[card ? 'card-menu margin-tb' : '', message ? 'menu-avatar' : 'menu']">
    <slot></slot>
  </view>
</template>

<script>
export default {
  name: 'l-list',

  props: {
    card: { type: Boolean },
    message: { type: Boolean }
  }
}
</script>
