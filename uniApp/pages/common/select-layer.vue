<template>
  <view class="page">
    <l-banner v-model="searchText" noSearchButton placeholder="搜索项目名/项目值" type="search" fill fixed />

    <view class="layer-list">
      <view @click="itemClick(item)" v-for="(item, index) of list" :key="index" class="layer-item">
        <view v-for="fieldItem of fieldList" :key="fieldItem.name">
          <text class="text-black label">{{ fieldItem.label || '(未命名标题)' }}：</text>
          <text>{{ item[fieldItem.name] }}</text>
        </view>
      </view>
    </view>
  </view>
</template>

<script>
export default {
  data() {
    return {
      searchText: '',

      sourceList: [],
      fieldList: []
    }
  },

  onBackPress() {
    uni.$off('select-layer')
  },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      const param = this.getPageParam()
      this.sourceList = param.source
      this.fieldList = param.layerData
    },

    itemClick(item) {
      uni.$emit('select-layer', item)
      uni.navigateBack()
    }
  },

  computed: {
    list() {
      if (!this.searchText) {
        return this.sourceList
      }

      return this.sourceList.filter(item => this.fieldList.some(t => (item[t.name] || '').includes(this.searchText)))
    }
  }
}
</script>

<style lang="less" scoped>
.layer-item {
  padding: 20rpx 25rpx;
  border-bottom: 1rpx solid #ddd;
  background: #ffffff;
  color: #8f8f94;

  .label {
    white-space: nowrap;
  }

  &:first-child {
    border-top: 1rpx solid #ddd;
  }
}
</style>

<style>
page {
  padding-top: 100rpx;
}
</style>
