<template>
  <view class="custom-add" @click="$emit('click', $event)"><l-icon type="add" /></view>
</template>

<script>
export default {
  name: 'l-custom-add'
}
</script>

<style lang="less">
.custom-add {
  position: fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 30px;
  bottom: 20px;
  bottom: calc(20rpx + constant(safe-area-inset-bottom));
  bottom: calc(20rpx + env(safe-area-inset-bottom));
  right: 20px;
  height: 40px;
  width: 40px;
  border-radius: 50%;
  background-color: #488aff;
  color: #fff;
}
</style>
