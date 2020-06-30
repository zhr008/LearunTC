<template>
  <view class="cu-card" :class="[outline ? '' : 'no-card', type !== 'base' ? type : '']">
    <view class="cu-item shadow">
      <template v-if="type === 'case'">
        <view class="image">
          <slot name="img"><image :src="info.img" mode="widthFix"></image></slot>
          <slot name="badge">
            <l-tag :color="info.badgeColor">{{ info.badge }}</l-tag>
          </slot>
          <view class="cu-bar bg-shadeBottom">
            <slot name="title">
              <text class="text-cut">{{ info.title }}</text>
            </slot>
          </view>
        </view>
        <view class="cu-list menu-avatar">
          <view class="cu-item">
            <slot name="avatar"><l-avatar style="margin: 0 30rpx;" size="lg" round :src="info.avatar" /></slot>
            <view class="content flex-sub">
              <slot name="user">
                <view class="text-grey">{{ info.user }}</view>
              </slot>
              <view class="text-gray text-sm flex justify-between">
                <slot name="tips">{{ info.tips }}</slot>
                <slot name="footer"></slot>
              </view>
            </view>
          </view>
        </view>
      </template>

      <template v-else-if="type === 'dynamic'">
        <view class="cu-list menu-avatar">
          <view class="cu-item">
            <slot name="avatar"><l-avatar style="margin: 0 30rpx;" size="lg" round :src="info.avatar" /></slot>
            <view class="content flex-sub">
              <slot name="user">
                <view>{{ info.user }}</view>
              </slot>
              <slot name="tips">
                <view class="text-gray text-sm flex justify-between">{{ info.tips }}</view>
              </slot>
            </view>
          </view>
        </view>
        <slot></slot>
        <slot name="footer"></slot>
      </template>

      <template v-else-if="type === 'article'">
        <view class="title">
          <slot name="title">
            <view class="text-cut">{{ info.title }}</view>
          </slot>
        </view>
        <view class="content">
          <slot name="img"><image :src="info.img" mode="aspectFill"></image></slot>
          <view class="desc">
            <view class="text-content"><slot></slot></view>
            <slot name="footer"></slot>
          </view>
        </view>
      </template>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-card',

  props: {
    type: { type: String, default: 'case' },
    outline: { type: Boolean },
    info: { type: Object, default: () => {} }
  }
}
</script>
