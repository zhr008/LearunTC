<template>
  <view class="cu-steps" :class="[arrow ? 'steps-arrow' : '']">
    <view
      class="cu-item"
      :class="index < step ? 'text-' + activeColor : ''"
      v-for="(item, index) in steps"
      :key="index"
    >
      <text v-if="number" class="num" :data-index="index + 1"></text>
      <text v-else :class="index < step || !point ? 'cuIcon-' + item.icon : 'cuIcon-title'"></text>
      {{ item.title }}
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-step',

  props: {
    step: { type: Number, default: 0 },
    steps: { type: Array, default: () => [] },
    activeColor: { type: String, default: 'blue' },
    point: { type: Boolean },
    arrow: { type: Boolean },
    number: { type: Boolean }
  }
}
</script>
