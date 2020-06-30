<template>
  <view class="page">
    <l-list border>
      <l-list-item title="头像"><l-avatar slot="action" :src="avatarSrc" /></l-list-item>
      <l-list-item title="账号" :action="currentUser.account" />
      <l-list-item title="工号" :action="currentUser.enCode" />
      <l-list-item title="姓名" :action="currentUser.realName" />
      <l-list-item title="性别" :action="Number(currentUser.gender) === 1 ? '男' : '女'" />
      <l-list-item title="公司" :action="info.company" />
      <l-list-item title="部门" :action="info.dep" />
      <l-list-item title="岗位" :action="info.job" />
      <l-list-item title="角色" :action="info.role" />
    </l-list>
  </view>
</template>

<script>
export default {
  computed: {
    currentUser() {
      return this.$store.state.user
    },

    info() {
      const company = this.currentUser.companyId ? this.$store.state.company[this.currentUser.companyId].name : ''
      const dep = this.currentUser.departmentId ? this.$store.state.dep[this.currentUser.departmentId].name : ''
      const role = (this.currentUser.role || []).join(' · ')
      const job = (this.currentUser.post || []).join(' · ')

      return { company, dep, role, job }
    },

    avatarSrc() {
      return this.apiRoot`/user/img?data=${this.currentUser.userId}`
    }
  }
}
</script>
