<template>
  <view :class="[type === 'left' ? '' : 'self']" class="cu-item">
    <view
      v-if="type === 'left'"
      class="cu-avatar radius"
      :style="{ backgroundImage: 'url(' + avatar + ')', borderRadius: roundAvatar ? '50%' : '3px' }"
    ></view>
    <view class="main">
      <view
        :class="[type === 'left' ? (leftColor ? 'bg-' + leftColor : '') : rightColor ? 'bg-' + rightColor : '']"
        class="content shadow"
      >
        <text style="word-break: break-all;"><slot></slot></text>
      </view>
    </view>
    <view
      v-if="type !== 'left'"
      class="cu-avatar radius"
      :style="{ backgroundImage: 'url(' + avatar + ')', borderRadius: roundAvatar ? '50%' : '3px' }"
    ></view>

    <view class="date">
      <slot name="date">{{ date }}</slot>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-chat-msg',

  props: {
    type: { type: String, default: 'left' },
    avatar: { type: String },
    roundAvatar: { type: Boolean },
    date: { type: String },
    leftColor: { type: String },
    rightColor: { type: String, default: 'green' }
  }
}
</script>
