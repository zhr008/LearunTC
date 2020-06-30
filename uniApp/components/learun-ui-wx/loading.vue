<template>
  <view class="cu-load" :class="['bg-' + color, error ? 'erro' : completed ? 'over' : 'loading']">
    {{ error ? errorText : completed ? completeText : loadingText }}
  </view>
</template>

<script>
export default {
  name: 'l-loading',

  props: {
    completed: { type: Boolean },
    error: { type: Boolean },
    color: { type: String, default: 'gray' },
    loadingText: { type: String, default: '加载中...' },
    completeText: { type: String, default: '加载完成' },
    errorText: { type: String, default: '加载失败' }
  }
}
</script>
