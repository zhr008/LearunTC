<template>
  <view
    v-if="type === 'default'"
    class="cu-bar"
    :class="[hexColor ? '' : 'bg-' + color, fixed ? 'banner-fixed' : '']"
    :style="{ backgroundColor: hexColor, boxShadow: noshadow ? 'none' : '0 1rpx 6rpx rgba(0, 0, 0, 0.1)' }"
  >
    <view class="action" @click="clickLeft"><slot name="left"></slot></view>

    <view class="content" @click="clickCenter">
      <slot>{{ title }}</slot>
    </view>

    <view class="action" @click="clickRight"><slot name="right"></slot></view>
  </view>

  <view
    v-else-if="type === 'search'"
    class="cu-bar search"
    :class="[hexColor ? '' : 'bg-' + color, fixed ? 'banner-fixed' : '']"
    :style="{ backgroundColor: hexColor, boxShadow: noshadow ? 'none' : '0 1rpx 6rpx rgba(0, 0, 0, 0.1)' }"
  >
    <slot name="left"></slot>
    <view :style="inputStyle" class="search-form" :class="[fill ? 'radius' : 'round']">
      <slot name="searchInput">
        <text class="cuIcon-search"></text>
        <input
          confirm-type="search"
          type="text"
          ref="bannerInput"
          @change="searchTextChange"
          @focus="$emit('inputFocus', $event)"
          :placeholder-style="placeholderStyle"
          :adjust-position="false"
          :placeholder="placeholder"
          :value="value"
        />
      </slot>
    </view>
    <view v-if="!noSearchButton" class="action">
      <slot name="searchButton">
        <template v-if="fill">
          <view @click="searchClick">搜索</view>
        </template>
        <button v-else class="cu-btn bg-green shadow-blur round" @click="searchClick">搜索</button>
      </slot>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-banner',

  props: {
    type: { type: String, default: 'default' },
    color: { type: String, default: 'white' },
    hexColor: { type: String },
    inputStyle: { type: String },
    fill: { type: Boolean },
    title: { type: null },
    value: { type: null },
    placeholder: { type: String, default: '搜索图片、文章、视频' },
    noSearchButton: { type: Boolean },
    placeholderStyle: { type: String },
    fixed: { type: Boolean },
    noshadow: { type: Boolean }
  },

  methods: {
    searchTextChange(e) {
      this.$emit('input', e.detail.value)
      this.$emit('change', e.detail.value)
    },

    clickLeft(e) {
      this.$emit('clickLeft', e)
    },

    clickRight(e) {
      this.$emit('clickRight', e)
    },

    clickCenter(e) {
      this.$emit('clickCenter', e)
    },

    searchClick(F) {
      this.$emit('searchClick')
    },

    inputFocus() {
      this.$refs.bannerInput.focus()
    }
  }
}
</script>

<style scoped lang="less">
.banner-fixed {
  position: fixed;
  width: 100%;
  top: 0;
  z-index: 1024;
}
</style>
