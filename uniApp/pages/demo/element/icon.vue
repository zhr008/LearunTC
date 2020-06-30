<template>
  <view>
    <l-title>使用图标：</l-title>
    <view class="flex flex-wrap justify-around">
      <view class="align-center text-center padding-sm">
        <l-icon class="text-xxl" type="settings" style="display: block;" />
        <text style="display: block;">普通图标</text>
      </view>

      <view class="align-center text-center padding-sm">
        <l-icon class="text-xxl" color="red" type="settings" style="display: block;" />
        <text style="display: block;">彩色图标</text>
      </view>

      <view class="align-center text-center padding-sm">
        <l-icon
          class="text-xxl"
          shadow
          color="black"
          type="settings"
          style="display: block;"
        />
        <text style="display: block;">带阴影图标</text>
      </view>
    </view>
    <wxdoc-desc>
      图标使用 l-icon 标签，设置 type="*" ，将*替换成图标英文名即可(见下方图标名列表)；
      设置 color 属性可以指定图标颜色，添加 shadow 属性可以加上阴影
    </wxdoc-desc>

    <l-title>所有图标：</l-title>
    <view class="grid col-3 padding">
      <view
        class="align-center text-center padding-sm"
        v-for="(item, index) of icons"
        :key="index"
      >
        <l-icon class="text-xxl" :type="item" style="display: block;" />
        <text style="display: block;">{{ item }}</text>
      </view>
    </view>
  </view>
</template>

<script>
import { icons } from '@/components/learun-ui-wx/icon.vue'

export default {
  data() {
    return { icons }
  }
}
</script>
