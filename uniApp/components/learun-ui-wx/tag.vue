<template>
  <view
    :class="[
      round ? 'round' : null,
      radius ? 'radius' : null,
      size && !capsule ? size : null,
      color && !capsule ? 'bg-' + color : null,
      line && !capsule ? 'line-' + line : null,
      light && !capsule ? 'light' : null,
      capsule ? 'cu-capsule' : 'cu-tag',
      badge ? 'badge' : null
    ]"
    @click="click"
  >
    <template v-if="capsule">
      <view class="cu-tag tag-icon" :class="[size, color ? 'bg-' + color : null]"><slot name="icon"></slot></view>
      <view class="cu-tag" :class="[size, color ? 'line-' + color : null]"><slot></slot></view>
    </template>

    <template v-else>
      <slot></slot>
    </template>
  </view>
</template>

<script>
export default {
  name: 'l-tag',

  props: {
    round: { type: Boolean },
    radius: { type: Boolean },
    size: { type: String, default: 'df' },
    color: { type: String },
    line: { type: [String, Boolean] },
    light: { type: Boolean },
    capsule: { type: Boolean },
    badge: { type: Boolean }
  },

  methods: {
    click(e) {
      this.$emit('click', e)
    }
  }
}
</script>
