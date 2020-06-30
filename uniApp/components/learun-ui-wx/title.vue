<template>
  <view v-if="type !== 'default'" class="cu-bar bg-white">
    <!-- 下划线标题(普通/变色) -->
    <view v-if="type === 'underline' || type === 'colorfulUnderline'" class="action border-title">
      <text class="text-xl" :class="['text-' + color, blod ? 'text-blod' : '']">
        <slot>{{ title }}</slot>
      </text>
      <text v-if="type === 'underline'" :class="'bg-' + color" style="width:2rem"></text>
      <text v-else-if="type === 'colorfulUnderline'" class="bg-gradual-blue" style="width:3rem"></text>
    </view>

    <!-- 扩散型下划线标题 -->
    <view v-else-if="type === 'blurUnderline'" class="action sub-title">
      <text class="text-xl" :class="['text-' + color, blod ? 'text-blod' : '']">
        <slot>{{ title }}</slot>
      </text>
      <text :class="'bg-' + color"></text>
    </view>

    <!-- 扩散型副标题 -->
    <view v-else-if="type === 'blurSubtitle'" class="action sub-title">
      <text class="text-xl" :class="[' text-' + color, blod ? 'text-blod' : '']">
        <slot>{{ title }}</slot>
      </text>
      <text class="text-ABC" :class="[' text-' + color]">
        <slot name="subtitle">{{ subtitle }}</slot>
      </text>
    </view>

    <!-- 后缀型副标题 -->
    <view v-else-if="type === 'nextSubtitle'" class="action title-style-3">
      <text class="text-xl" :class="['text-' + color, blod ? 'text-blod' : '']">
        <slot>{{ title }}</slot>
      </text>
      <text class="text-Abc self-end margin-left-sm light" :class="'text-' + color">
        <slot name="subtitle">{{ subtitle }}</slot>
      </text>
    </view>
  </view>

  <!-- 普通标题(圆点/长圆点) -->
  <view v-else class="cu-bar bg-white" :class="[border ? 'solid-bottom' : '']">
    <view class="action" style="vertical-align:middle;">
      <text
        class="margin-right-xs"
        :class="[subColor ? 'text-' + subColor : '', long ? 'cuIcon-titles' : 'cuIcon-title']"
      ></text>
      <text class="text-xl" :class="[blod ? 'text-blod' : '']">
        <slot>{{ title }}</slot>
      </text>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-title',

  props: {
    type: { type: String, default: 'default' },
    color: { type: String, default: 'black' },
    subColor: { type: String, default: 'blue' },
    long: { type: Boolean },
    title: { type: null },
    subtitle: { type: null },
    blod: { type: Boolean },
    border: { type: Boolean }
  }
}
</script>
