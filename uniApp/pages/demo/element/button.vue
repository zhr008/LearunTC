<template>
  <view>
    <l-title>按钮类型：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap justify-around">
        <l-button>默认</l-button>
        <l-button round>圆角 (round)</l-button>
        <l-button round>
          <l-icon type="likefill" class="margin-right-xs" />
          图标
        </l-button>
      </view>
    </view>
    <wxdoc-desc>按钮使用 l-button 标签，添加 round 属性即为圆角</wxdoc-desc>

    <l-title>禁用按钮：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap justify-around margin-top-sm">
        <l-button color="red" disabled>禁用按钮 (disabled)</l-button>
        <l-button color="red" disabled round>禁用圆角按钮</l-button>
      </view>
    </view>
    <wxdoc-desc>添加 disabled 属性即可禁用按钮</wxdoc-desc>

    <l-title>按钮尺寸：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap justify-around">
        <l-button size="sm">小尺寸 (sm)</l-button>
        <l-button>默认尺寸 (df)</l-button>
        <l-button size="lg">大尺寸 (lg)</l-button>
      </view>
    </view>
    <wxdoc-desc>
      添加 size="*" 属性，把*换成尺寸字符串(sm/df/lg)即可设置按钮大小
    </wxdoc-desc>

    <l-title>按钮颜色：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap">
        <l-button
          class="margin-sm"
          v-for="(item, index) of colors"
          :key="index"
          :color="item.name"
        >
          {{ item.title }} ({{ item.name }} | {{ item.color }})
        </l-button>
      </view>
    </view>
    <wxdoc-desc>添加 color="*" 属性，把*换成上述的颜色单词即可设置按钮颜色</wxdoc-desc>

    <l-title>镂空按钮：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap justify-around">
        <l-button color="red">普通按钮</l-button>
        <l-button line="red">镂空按钮</l-button>
        <l-button line="red" blod>加粗线条按钮</l-button>
      </view>
    </view>
    <wxdoc-desc>
      添加 line="*" 属性，把*换成颜色单词即可设置镂空按钮；
      添加blod属性即可使用粗线条型镂空按钮
    </wxdoc-desc>

    <l-title>按钮阴影：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap justify-around">
        <l-button color="red">普通按钮</l-button>
        <l-button color="red" shadow>带阴影按钮 (shadow)</l-button>
      </view>
    </view>
    <wxdoc-desc>添加 shadow 属性即可显示按钮阴影</wxdoc-desc>

    <l-title>块状按钮：</l-title>
    <view class="padding flex flex-direction">
      <l-button block size="lg" color="blue">普通块状按钮 (block)</l-button>
      <l-button block size="lg" color="blue" class="margin-top-sm">
        <l-icon type="likefill" color="white" class="margin-right-xs" />
        带图标
      </l-button>
      <l-button block size="lg" line="red" class="margin-top-sm">镂空块状按钮</l-button>
      <l-button block size="lg" color="blue" disabled class="margin-top-sm">
        禁用块状按钮
      </l-button>
      <l-button block size="lg" line="red" disabled class="margin-top-sm">
        禁用镂空块状按钮
      </l-button>
    </view>
    <wxdoc-desc>添加 block 属性即可使用块状按钮</wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>