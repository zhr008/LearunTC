<template>
  <view class="cu-bar tabbar" :class="[dark ? 'bg-black' : 'bg-white']"><slot></slot></view>
</template>

<script>
export default {
  name: 'l-bar',

  props: {
    msginput: { type: Boolean },
    dark: { type: Boolean }
  }
}
</script>
