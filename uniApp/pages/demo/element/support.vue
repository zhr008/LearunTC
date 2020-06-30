<template>
  <view>
    <l-title>浮动：</l-title>
    <view class="padding bg-white" style="height:120rpx">
      <view class="bg-grey radius fl padding-xs">fl · 左侧浮动</view>
      <view class="bg-grey radius fr padding-xs">fr · 右侧浮动</view>
    </view>
    <wxdoc-desc>给容器添加 fl/fr 的类名，即可实现左/右浮动</wxdoc-desc>

    <l-title>外边距：</l-title>
    <view class="padding bg-white">
      <view class="example">四周外边距：margin-*</view>
      <view class="example">垂直方向外边距：margin-tb-*</view>
      <view class="example">水平方向外边距：margin-lr-*</view>
      <view class="margin-tb-xl"></view>
      <view class="example">上外边距：margin-top-*</view>
      <view class="example">下外边距：margin-bottom-*</view>
      <view class="example">左外边距：margin-left-*</view>
      <view class="example">右外边距：margin-right-*</view>
    </view>
    <wxdoc-desc>
      给容器添加上方文档中右侧的类名，把其中的*替换成尺寸，尺寸有 xs、sm、df、lg、xl 五种
    </wxdoc-desc>

    <l-title>内边距：</l-title>
    <view class="padding bg-white">
      <view class="example">四周内边距：padding-*</view>
      <view class="example">垂直方向内边距：padding-tb-*</view>
      <view class="example">水平方向内边距：padding-lr-*</view>
      <view class="margin-tb-xl"></view>
      <view class="example">上内边距：padding-top-*</view>
      <view class="example">下内边距：padding-bottom-*</view>
      <view class="example">左内边距：padding-left-*</view>
      <view class="example">右内边距：padding-right-*</view>
    </view>
    <wxdoc-desc>
      给容器添加上方文档中右侧的类名，把其中的*替换成尺寸，尺寸有 xs、sm、df、lg、xl 五种
    </wxdoc-desc>
  </view>
</template>

<style scoped>
.example {
  margin-top: 20rpx;
  margin-bottom: 20rpx;
}
</style>
