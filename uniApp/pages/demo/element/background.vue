<template>
  <view>
    <l-title>深色背景：</l-title>
    <view class="grid col-3 padding-xs">
      <view
        class="padding-sm"
        v-for="(item, index) in colors"
        :key="index"
        :style="{ backgroundColor: item.name === 'white' ? '#EEE' : null }"
      >
        <view
          class="padding-xs radius text-center shadow-blur"
          :class="'bg-' + item.name"
        >
          <view class="text-df">{{ item.title }} · {{ item.name }}</view>
          <view class="margin-top-xs">{{ item.color }}</view>
        </view>
      </view>
    </view>
    <wxdoc-desc>
      给某个元素添加 class="bg-*" 即可实现背景色，把*替换成上述颜色英文名即可
    </wxdoc-desc>

    <l-title>淡色背景：</l-title>
    <view class="grid col-3 bg-white padding-xs">
      <view
        class="padding-sm"
        v-for="(item, index) in colors"
        :key="index"
        :style="{ backgroundColor: item.name === 'white' ? '#EEE' : null }"
      >
        <view class="padding-xs radius text-center light" :class="'bg-' + item.name">
          <view class="text-df">{{ item.title }} · {{ item.name }}</view>
        </view>
      </view>
    </view>
    <wxdoc-desc>添加 class="light" 即可将背景色变为浅色</wxdoc-desc>

    <l-title>渐变背景：</l-title>
    <view class="grid col-2 padding-sm">
      <view class="padding-sm">
        <view class="bg-gradual-red padding-xs radius text-center shadow-blur">
          <view class="text-df">魅红 · gradual-orange</view>
          <view class="margin-top-xs">#f43f3b - #ec008c</view>
        </view>
      </view>
      <view class="padding-sm">
        <view class="bg-gradual-orange padding-xs radius text-center shadow-blur">
          <view class="text-df">鎏金 · gradual-orange</view>
          <view class="margin-top-xs">#ff9700 - #ed1c24</view>
        </view>
      </view>
      <view class="padding-sm">
        <view class="bg-gradual-green padding-xs radius text-center shadow-blur">
          <view class="text-df">翠柳 · gradual-green</view>
          <view class="margin-top-xs">#39b54a - #8dc63f</view>
        </view>
      </view>
      <view class="padding-sm">
        <view class="bg-gradual-blue padding-xs radius text-center shadow-blur">
          <view class="text-df">靛青 · gradual-blue</view>
          <view class="margin-top-xs">#0081ff - #1cbbb4</view>
        </view>
      </view>
      <view class="padding-sm">
        <view class="bg-gradual-purple padding-xs radius text-center shadow-blur">
          <view class="text-df">惑紫 · gradual-purple</view>
          <view class="margin-top-xs">#9000ff - #5e00ff</view>
        </view>
      </view>
      <view class="padding-sm">
        <view class="bg-gradual-pink padding-xs radius text-center shadow-blur">
          <view class="text-df">霞彩 · gradual-pink</view>
          <view class="margin-top-xs">#ec008c - #6739b6</view>
        </view>
      </view>
    </view>
    <wxdoc-desc>
      添加 class="bg-*" 即可将背景色变为渐变色，把*替换成上述的英文名即可
    </wxdoc-desc>

    <l-title>图片背景：</l-title>
    <view
      class="bg-img bg-mask flex align-center"
      style="background-image: url('https://img.learun.cn/wxcx/pic.jpg'); height: 414upx;"
    >
      <view class="padding-xl text-white">
        <view class="padding-xs text-xxl">背景文字标题</view>
        <view class="padding-xs text-lg">
          背景文字内容，背景文字内容，背景文字内容，背景文字内容
        </view>
      </view>
    </view>
    <wxdoc-desc>
      给某一容器添加图片背景(background-image)后，可以添加一个 class="bg-img bg-mask"；
      这样会实现如上图般的图片遮盖效果，背景图会变暗，然后便可以在容器中添加其他文字等内容
    </wxdoc-desc>

    <l-title>图片背景(文字渐变底色)：</l-title>
    <view class="grid col-2">
      <view
        class="bg-img padding-bottom-xl"
        style="background-image: url('https://img.learun.cn/wxcx/pic.jpg');height: 207upx;"
      >
        <view class="bg-shadeTop padding padding-bottom-xl">上方文字</view>
      </view>

      <view
        class="bg-img padding-top-xl flex align-end"
        style="background-image: url('https://img.learun.cn/wxcx/pic.jpg');height: 207upx;"
      >
        <view class="bg-shadeBottom padding padding-top-xl flex-sub">下方文字</view>
      </view>
    </view>
    <wxdoc-desc>
      给某一容器添加图片背景(background-image)后，再添加一个 class="bg-img"；
      内部文字需要放入 view 中，并给这个容器添加一个 class="bg-shade*"，*换成 Top 或
      Bottom； 这样便可以实现如上图所示的图片背景，背景前面的文字底下有个渐变效果
    </wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>
