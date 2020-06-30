<template>
  <view>
    <l-title>边框：</l-title>
    <view class="padding bg-white flex flex-wrap">
      <view class="margin-sm example solid">四周</view>
      <view class="margin-sm example solid-top">上</view>
      <view class="margin-sm example solid-bottom">下</view>
      <view class="margin-sm example solid-left">左</view>
      <view class="margin-sm example solid-right">右</view>
    </view>
    <wxdoc-desc>
      给需要添加边框的元素设置 class="solid" 即可添加四周边框；
      如设置 class="solid-*"，并把*替换成 top、bottom、left、right，则分别是上、下、左、右边框
    </wxdoc-desc>

    <l-title>加粗边框：</l-title>
    <view class="padding bg-white flex flex-wrap">
      <view class="margin-sm example solids">四周</view>
      <view class="margin-sm example solids-top">上</view>
      <view class="margin-sm example solids-bottom">下</view>
      <view class="margin-sm example solids-left">左</view>
      <view class="margin-sm example solids-right">右</view>
    </view>
    <wxdoc-desc>
      给需要添加边框的元素设置 class="solids" 即可添加四周粗边框；
      如设置 class="solids-*"，并把*替换成 top、bottom、left、right，则分别是上、下、左、右粗边框
    </wxdoc-desc>

    <l-title>阴影：</l-title>
    <view class="padding bg-gray text-center">
      <view class="padding-xl radius shadow bg-white">默认阴影</view>
      <view class="padding-xl radius shadow bg-blue margin-top">
        根据背景颜色而改变的阴影
      </view>
      <view class="padding-xl radius shadow shadow-lg bg-white margin-top">长阴影</view>
      <view class="padding-xl radius shadow shadow-lg bg-blue margin-top">长阴影</view>
      <view class="padding-xl radius shadow-warp bg-white margin-top">翘边阴影</view>
      <view
        class="padding-xl radius shadow-blur bg-red margin-top bg-img"
        style="background-image:url('https://img.learun.cn/wxcx/pic.jpg');"
      >
        <view>根据背景图而改变的阴影</view>
      </view>
    </view>
    <wxdoc-desc>
      给元素添加一个 class ，取值：shadow 是普通阴影，shadow-lg 是长阴影，shadow-warp 是翘边阴影；
      如果容器有背景色，则阴影也会有背景色；如果容器是图片背景，可以使用 shadow-blur
    </wxdoc-desc>
  </view>
</template>

<style scoped>
.example {
  height: 130rpx;
  width: 130rpx;
  text-align: center;
  line-height: 130rpx;
  font-size: 40rpx;
}
</style>
