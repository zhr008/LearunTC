<template>
  <view>
    <template v-for="(group, i) of components">
      <l-title :key="i" :color="group.color">{{ group.title }}类组件：</l-title>

      <view class="cu-list grid col-4" :key="i + 1000">
        <view
          @click="toPage(`${group.url}/${item.url}`)"
          v-for="(item, index) in group.list"
          :key="index"
          class="cu-item"
        >
          <view :class="['cuIcon-' + item.icon, 'text-' + group.color]"></view>
          <text>{{ item.name }}</text>
        </view>
      </view>
    </template>
  </view>
</template>

<script>
import components from './components.json'

export default {
  data() {
    return { components }
  },

  methods: {
    toPage(pageUrl) {
      uni.navigateTo({ url: `/pages/demo/${pageUrl}` })
    }
  }
}
</script>
