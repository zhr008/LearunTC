<template>
  <view>
    <l-title>进度条形状：</l-title>
    <view class="padding bg-white">
      <l-progress color="red" :percent="20" />
      <l-progress color="red" :percent="50" noText radius />
      <l-progress color="red" percent="87.5" round />
    </view>
    <wxdoc-desc>
      进度条使用 l-progress 标签，设置 percent 属性来表示进度百分比(无需百分号)； 
      添加 radius 属性则成为圆角进度条，添加 round 则成为胶囊型进度条，添加 noText 属性则不显示百分数
    </wxdoc-desc>

    <l-title>进度条尺寸/颜色：</l-title>
    <view class="padding bg-white">
      <l-progress color="red" percent="50" noText round />
      <l-progress size="sm" color="blue" percent="50" noText round />
      <l-progress size="xs" color="green" percent="50" noText round />
    </view>
    <wxdoc-desc>
      添加属性 size="*" 即可改变进度条尺寸，小尺寸为sm，特小尺寸为xs； 
      添加属性 color="*" 即可设置进度条颜色，把*替换成颜色英文名即可
    </wxdoc-desc>

    <l-title>进度条条纹：</l-title>
    <view class="padding bg-white">
      <l-progress color="red" percent="50" round striped />
      <l-progress color="blue" percent="50" round striped />
      <l-progress color="red" percent="70" round striped active />
      <l-progress color="blue" percent="70" round striped active />
    </view>
    <wxdoc-desc>
      添加 striped 属性即可显示条纹，再添加 active 属性即可显示动态条纹
    </wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>

<style>
l-progress {
  margin-top: 20rpx;
  display: block;
}
l-progress:first-of-type {
  margin-top: 0;
}
</style>
