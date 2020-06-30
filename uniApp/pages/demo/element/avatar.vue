<template>
  <view>
    <l-title>图片头像：</l-title>
    <view class="padding flex flex-wrap justify-around">
      <l-avatar src="https://img.learun.cn/wxcx/pic.jpg" />
      <l-avatar round src="https://img.learun.cn/wxcx/pic.jpg" />
      <l-avatar radius src="https://img.learun.cn/wxcx/pic.jpg" />
    </view>
    <wxdoc-desc>
      头像使用 l-avatar 标签，属性 src="*" 可以设置头像图片的资源路径；
      添加 round 属性将使头像变成圆形，添加 radius 属性可以使头像变成圆角
    </wxdoc-desc>

    <l-title>文字/图标头像：</l-title>
    <view class="padding flex flex-wrap justify-around">
      <l-avatar round color="red">A</l-avatar>
      <l-avatar round color="blue">sn</l-avatar>
      <l-avatar radius color="green">魏</l-avatar>
      <l-avatar radius color="brown"><l-icon type="likefill" /></l-avatar>
    </view>
    <wxdoc-desc>
      也可以使用文字或图标作为头像的内容，将文字或图标放置于头像标签内即可；
      添加 color="*" 属性可以设置头像的背景颜色
    </wxdoc-desc>

    <l-title>头像尺寸：</l-title>
    <view class="padding flex flex-wrap justify-around">
      <l-avatar radius size="sm">sm</l-avatar>
      <l-avatar radius size="df">df</l-avatar>
      <l-avatar radius size="lg">lg</l-avatar>
      <l-avatar radius size="xl">xl</l-avatar>
    </view>
    <wxdoc-desc>
      添加 size="*" 属性即可设置头像大小，把*替换成上面的尺寸字符串之一(改成小写)
    </wxdoc-desc>

    <l-title>头像背景色：</l-title>
    <view
      class="padding flex flex-wrap"
      style="background-color: #DDDDDD;"
    >
      <l-avatar
        radius
        size="lg"
        class="margin-xs"
        :color="item.name"
        v-for="(item, index) in colors"
        :key="index"
      >
        <text style="font-size: 0.4em;">{{ item.name }}</text>
      </l-avatar>
    </view>
    <wxdoc-desc>
      添加 color="*" 属性可以设置头像的背景颜色，把*替换成上面的颜色英文名即可
    </wxdoc-desc>

    <l-title>头像角标：</l-title>
    <view class="padding flex flex-wrap justify-around">
      <l-avatar src="https://img.learun.cn/wxcx/pic.jpg">
        <l-tag slot="badge" badge round color="red">5</l-tag>
      </l-avatar>

      <l-avatar size="lg" src="https://img.learun.cn/wxcx/pic.jpg">
        <l-tag slot="badge" badge round color="red"><l-icon type="like" /></l-tag>
      </l-avatar>

      <l-avatar size="lg" src="https://img.learun.cn/wxcx/pic.jpg">
        <l-tag slot="badge" badge round color="blue">完成</l-tag>
      </l-avatar>
    </view>
    <wxdoc-desc>
      在头像标签内放置 l-tag 标签，并给内部的这个标签添加 badge 属性，即可设置头像角标
    </wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>