<template>
  <view class="page">
    <l-input :value="currentUser.mobile" disabled title="手机"></l-input>
    <l-input :value="currentUser.telephone" disabled title="电话"></l-input>
    <l-input :value="currentUser.email" disabled title="邮箱"></l-input>
    <l-input :value="currentUser.weChat" disabled title="微信"></l-input>
    <l-input :value="currentUser.oICQ" disabled title="QQ"></l-input>
  </view>
</template>

<script>
export default {
  computed: {
    currentUser() {
      return this.$store.state.user
    }
  }
}
</script>
