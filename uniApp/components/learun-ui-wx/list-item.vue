<template>
  <view @click="$emit('click', $event)" class="cu-item" :class="arrow ? 'arrow' : ''">
    <slot name="avatar"><l-avatar v-if="avatar" round size="lg" :src="avatar" /></slot>

    <view class="content">
      <view class="text-grey">
        <slot>{{ title }}</slot>
      </view>

      <view v-if="extra || $slots.extra" class="text-gray text-sm flex">
        <view class="text-cut text-grey">
          <slot name="extra">{{ extra }}</slot>
        </view>
      </view>
    </view>

    <view class="action">
      <slot v-if="$slots.action || $slots.time" name="action">
        <view v-if="time || $slots.time" class="text-grey text-sm">
          <slot name="time">{{ time }}</slot>
        </view>
        <slot name="tips"></slot>
      </slot>
      <template v-else>
        {{ action }}
      </template>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-list-item',

  props: {
    title: { type: String },
    action: { type: String },
    arrow: { type: Boolean },
    extra: { type: null },
    avatar: { type: null },
    time: { type: null }
  }
}
</script>
