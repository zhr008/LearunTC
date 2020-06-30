<template>
  <view>
    <l-title>标签形状：</l-title>
    <view class="padding bg-white flex flex-wrap justify-around">
      <l-tag>默认</l-tag>
      <l-tag round>椭圆 (round)</l-tag>
      <l-tag radius>圆角 (radius)</l-tag>
    </view>
    <wxdoc-desc>
      标签使用 l-tag 标签；添加 round 属性为椭圆标签；添加 radius 属性为圆角标签
    </wxdoc-desc>

    <l-title>标签尺寸：</l-title>
    <view class="padding bg-white flex flex-wrap justify-around">
      <l-tag size="sm">小尺寸 (sm)</l-tag>
      <l-tag>普通尺寸</l-tag>
    </view>
    <wxdoc-desc>标签只有标准尺寸和小尺寸，添加 size="sm" 属性即为小尺寸</wxdoc-desc>

    <l-title>标签颜色：</l-title>
    <view class="padding-sm flex flex-wrap">
      <view
        class="padding-xs"
        v-for="(item, index) in colors"
        :key="index"
        :style="{ backgroundColor: item.name === 'white' ? '#EEE' : null }"
      >
        <l-tag :color="item.name">{{ item.title }} · {{ item.name }}</l-tag>
      </view>
    </view>
    <wxdoc-desc>
      添加 color="*" 属性，把*替换成上面的英文颜色名，即可设置标签颜色
    </wxdoc-desc>

    <l-title>亮色标签：</l-title>
    <view class="padding-sm flex flex-wrap">
      <view
        class="padding-xs"
        v-for="(item, index) in colors"
        :key="index"
        :style="{ backgroundColor: item.name === 'white' ? '#EEE' : null }"
      >
        <l-tag :color="item.name" light>{{ item.title }} · {{ item.name }}</l-tag>
      </view>
    </view>
    <wxdoc-desc>在彩色标签的基础上，添加 light 属性，即是亮色标签</wxdoc-desc>

    <l-title>镂空标签：</l-title>
    <view class="padding-sm flex flex-wrap">
      <view
        class="padding-xs"
        v-for="(item, index) in colors"
        :key="index"
        :style="{ backgroundColor: item.name === 'white' ? '#333' : null }"
      >
        <l-tag :line="item.name">{{ item.title }} · {{ item.name }}</l-tag>
      </view>
    </view>
    <wxdoc-desc>
      添加 line="*" 属性，把*替换成上面的英文颜色名，即可设置为镂空标签
    </wxdoc-desc>

    <l-title>胶囊样式：</l-title>
    <view class="padding flex flex-wrap justify-around">
      <l-tag capsule color="red">
        <l-icon slot="icon" type="likefill" />
        12 喜欢
      </l-tag>

      <l-tag capsule round color="blue">
        <template #icon>
          喜欢
        </template>
        24
      </l-tag>

      <l-tag capsule radius size="sm" color="brown">
        <l-icon slot="icon" type="likefill" />
        100
      </l-tag>
    </view>
    <wxdoc-desc>
      添加 capsule 属性即成为胶囊标签；
      注意，胶囊标签左侧的内容，需要放置在 slot="icon" 的插槽内
    </wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>
