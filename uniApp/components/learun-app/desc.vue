<template>
  <view class="wxdoc-desc margin-sm padding-sm radius" :class="displayDesc ? 'wxdoc-desc-display' : ''">
    <view v-if="displayDesc" class="wxdoc-desc-text text-center">
      <text class="wxdoc-desc-content text-left"><slot></slot></text>
    </view>
    <view v-else class="wxdoc-desc-tip text-center" @click="displayDesc = true">显示说明</view>
  </view>
</template>

<script>
export default {
  name: 'wxdoc-desc',

  props: {
    expand: { type: Boolean, default: false }
  },

  data() {
    return {
      displayDesc: this.expand
    }
  }
}
</script>

<style scoped>
.wxdoc-desc-display {
  background: #eeeeee;
}

.wxdoc-desc-content {
  display: inline-block;
  font-size: 1em;
}

.wxdoc-desc-tip {
  font-size: 1em;
}
</style>
