<template>
  <view v-if="!info" @click="click('item', $event)" class="custom-item">
    <slot name="maincontent" :content="mainContent" :title="mainTitle">
      <view v-for="(mt, i) of mainTitle" :key="mt" class="custom-item-main">
        <text class="custom-item-title">{{ mt || '' }}：</text>
        <slot name="main" :content="mainContent[i]" :idx="i">{{ mainContent[i] || '' }}</slot>
      </view>
    </slot>

    <slot name="subcontent" :content="subContent" :title="subTitle">
      <view v-for="(st, i) of subTitle" :key="st" class="custom-item-sub">
        <text class="custom-item-title">{{ st || '' }}：</text>
        <slot name="sub" :content="subContent[i]" :idx="i">{{ subContent[i] || '' }}</slot>
      </view>
    </slot>

    <view class="custom-action">
      <slot name="action">
        <view
          @click="click('delete', $event)"
          class="custom-action-btn line-red text-sm"
          style="border: currentColor 1px solid;"
        >
          <l-icon type="delete" />
          删除
        </view>
        <view
          @click="click('edit', $event)"
          class="custom-action-btn line-blue text-sm"
          style="border: currentColor 1px solid;"
        >
          <l-icon type="edit" />
          编辑
        </view>
        <view
          @click="click('view', $event)"
          class="custom-action-btn line-blue text-sm"
          style="border: currentColor 1px solid;min-width: 160rpx;"
        >
          查看
          <l-icon type="right" />
        </view>
      </slot>
    </view>
  </view>
  <view v-else class="custom-item"><slot></slot></view>
</template>

<script>
export default {
  name: 'l-custom-item',

  props: {
    mainTitle: { type: Array, default: () => [] },
    subTitle: { type: Array, default: () => [] },
    mainContent: { type: Array, default: () => [] },
    subContent: { type: Array, default: () => [] },
    info: { type: Boolean }
  },

  methods: {
    click(type, e) {
      this.$emit(type, e)
      this.$emit('click', type)
    }
  }
}
</script>

<style lang="less">
.custom-item {
  padding: 20rpx 20rpx;
  border-bottom: 1rpx solid #ddd;
  background: #ffffff;
  position: relative;
  color: #8f8f94;

  &:first-child {
    border-top: 1rpx solid #ddd;
  }

  .custom-action {
    margin-top: 20rpx;

    .custom-action-btn {
      display: inline-block;
      padding: 4px 6px;
      margin: 0 3px;
      border-radius: 3px;
      text-align: center;
    }
  }

  .custom-item-main {
    padding-top: 4px;

    &:first-child {
      padding-top: 0;
    }
  }

  .custom-item-sub {
    padding-top: 3px;
    font-size: 0.9em;

    &:first-child {
      padding-top: 0;
      margin-top: 20rpx;
    }
  }

  .custom-item-title {
    white-space: nowrap;
    color: #333333;
  }
}
</style>
