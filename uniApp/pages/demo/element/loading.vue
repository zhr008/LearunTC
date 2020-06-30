<template>
  <view>
    <l-title>加载状态：</l-title>
    <view class="padding bg-white">
      <l-loading />
      <l-loading completed />
      <l-loading error />
    </view>
    <wxdoc-desc>
      使用 l-loading 标签来放置加载状态组件；默认是加载中，添加 completed 属性则为完成，添加 error 属性则为失败
    </wxdoc-desc>

    <l-title>背景色：</l-title>
    <view class="padding bg-white">
      <l-loading color="blue" />
      <l-loading color="green" completed />
      <l-loading color="red" error />
    </view>
    <wxdoc-desc>
      添加 color="*" 属性即可设置组件的背景色，把*替换成颜色的英文名
    </wxdoc-desc>
  </view>
</template>
