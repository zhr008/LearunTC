<template>
  <view
    class="cu-progress"
    :class="[
      size,
      radius ? 'radius' : null,
      round ? 'round' : null,
      striped ? 'striped' : null,
      active && striped ? 'active' : null
    ]"
  >
    <view :class="[color ? 'bg-' + color : null]" :style="{ width: percent ? Number(percent) + '%' : '' }">
      {{ noText ? '' : percent + '%' }}
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-progress',

  props: {
    percent: { type: Number },
    noText: { type: Boolean },
    color: { type: String },
    radius: { type: Boolean },
    round: { type: Boolean },
    size: { type: String, default: 'df' },
    striped: { type: Boolean },
    active: { type: Boolean }
  }
}
</script>
