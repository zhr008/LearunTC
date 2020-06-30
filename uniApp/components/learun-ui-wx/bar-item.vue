<template>
  <view v-if="type === 'default'" class="action" @click="click">
    <slot>
      <l-icon class="cuIcon-cu-image" :type="icon" :color="color" size="lg" />
      <slot name="badge"></slot>
      <view :class="'text-' + color">
        <slot name="title">{{ title }}</slot>
      </view>
    </slot>
  </view>

  <view v-else-if="type === 'round'" class="action text-gray add-action">
    <slot>
      <button class="cu-btn shadow" :class="['bg-' + color, 'cuIcon-' + icon]"></button>
      <slot name="title">{{ title }}</slot>
    </slot>
  </view>
</template>

<script>
export default {
  name: 'l-bar-item',

  props: {
    type: { type: String, default: 'default' },
    title: { type: String },
    icon: { type: String },
    color: { type: String, default: 'gray' }
  },

  methods: {
    click(e) {
      this.$emit('click', e)
    }
  }
}
</script>
