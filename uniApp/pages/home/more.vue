<template>
  <view class="page" id="more">
    <l-banner v-model="searchText" ref="banner" noSearchButton placeholder="搜索应用名" type="search" fill fixed />

    <l-title v-if="searchText.length <= 0" class="solid-bottom">我的应用</l-title>
    <view
      class="function-list cu-list grid no-border"
      :class="['col-' + config('pageConfig.home.functionListColunm')]"
      v-if="searchText.length <= 0"
    >
      <view
        class="cu-item text-center flex flex-wrap justify-center align-center"
        v-for="(item, index) in myListDisplay"
        :key="index"
      >
        <view
          @click="funcListClick(item)"
          style="background-color: #fe955c;"
          class="app-item align-center flex flex-wrap justify-center align-center"
        >
          <l-icon :type="funcListIcon(item)" color="white" class="text-sl" />
        </view>
        <text>{{ item.F_Name }}</text>
        <l-button @click="removeClick(item.F_Id)" class="margin-top-sm" v-if="edit" line="red">移出</l-button>
      </view>
    </view>
    <view v-if="searchText.length <= 0" class="padding-lr padding-bottom bg-white">
      <l-button @click="editClick" block :line="edit ? 'green' : 'blue'">
        {{ edit ? '完成编辑' : '编辑“我的应用”列表' }}
      </l-button>
      <l-button class="margin-top block" v-if="edit" @click="cancelClick" block line="red">放弃编辑</l-button>
    </view>

    <template v-for="(group, title) in groupList">
      <view :key="title">
        <view class="margin-top" style="height: 1px;"></view>
        <l-title class="solid-bottom margin-top">{{ title }}</l-title>
        <view class="function-list cu-list grid no-border col-4">
          <view
            class="cu-item text-center flex flex-wrap justify-center align-center"
            v-for="(item, index) in group"
            :key="index"
          >
            <view
              @click="funcListClick(item)"
              :style="{ backgroundColor: funcListIconColor(item) }"
              class="app-item align-center flex flex-wrap justify-center align-center"
            >
              <l-icon :type="funcListIcon(item)" color="white" class="text-sl" />
            </view>
            <text>{{ item.F_Name }}</text>
            <l-button
              class="margin-top-sm"
              @click="itemClick(item.F_Id)"
              v-if="edit"
              :line="editList.includes(item.F_Id) ? 'red' : 'green'"
            >
              {{ editList.includes(item.F_Id) ? '移出' : '添加' }}
            </l-button>
          </view>
        </view>
      </view>
    </template>
  </view>
</template>

<script>
import _ from 'lodash'

export default {
  data() {
    return {
      allList: [],
      myList: [],
      editList: [],
      searchText: '',

      edit: false
    }
  },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      uni.showLoading({ title: '加载菜单中...', mask: true })
      // 同时发出请求，获取“所有功能按钮列表”和“我的功能按钮列表”
      await Promise.all([
        uni.request({ url: this.apiRoot`/function/list`, data: this.auth }).then(([err, result]) => {
          this.allList = result.data.data.data
        }),
        uni.request({ url: this.apiRoot`/function/mylist`, data: this.auth }).then(([err, result]) => {
          this.myList = result.data.data
        })
      ])

      this.myList = this.myList.filter(t => this.allList.find(li => li.F_Id === t))
      uni.hideLoading()
    },

    async editClick() {
      if (!this.edit) {
        this.editList = [...this.myList]
        this.edit = true

        return
      }
      const [err, result] = await uni.request({
        url: this.apiRoot`/function/mylist/update`,
        method: 'POST',
        data: { ...this.auth, data: this.editList.join(',') }
      })
      if (err || result.data.code !== 200) {
        uni.showModal({
          title: '更新失败',
          content: `“我的应用”列表更新失败。${result.data.info}`,
          showCancel: false
        })
        this.editList = [...this.myList]
        this.edit = false
        return
      }
      this.myList = [...this.editList]
      this.edit = false
      uni.$emit('home-list')
    },

    funcListIcon(item) {
      if (!item || !item.F_Icon) {
        return ''
      }

      return item.F_Icon.replace(`iconfont icon-`, ``)
    },

    funcListIconColor(item) {
      if (this.edit) {
        return this.editList.includes(item.F_Id) ? '#fe955c' : '#62bbff'
      }

      return this.myList.includes(item.F_Id) ? '#fe955c' : '#62bbff'
    },

    funcListClick(item) {
      if (item.F_IsSystem === 2) {
        this.setPageParam(item)
        uni.navigateTo({ url: `/pages/customapp/list?formId=${item.F_FormId}` })
        return
      }
      
      uni.navigateTo({ url: `/pages/${item.F_Url}/list` })
    },

    cancelClick() {
      this.edit = false
    },

    removeClick(id) {
      this.editList = _.without(this.editList, id)
    },

    itemClick(id) {
      if (this.editList.includes(id)) {
        this.editList = _.without(this.editList, id)
        return
      }

      this.editList = _.concat(this.editList, id)
    }
  },

  computed: {
    myListDisplay() {
      const list = this.edit ? this.editList : this.myList
      return list.reduce((list, id) => [...list, this.allList.find(t => t.F_Id === id)], [])
    },

    groupList() {
      const typeTable = _(this.$store.state.propTable.function)
        .keyBy('value')
        .mapValues('text')
        .value()

      return _(this.allList)
        .filter(item => item.F_Name.includes(this.searchText))
        .groupBy('F_Type')
        .mapKeys((v, k) => typeTable[k])
        .value()
    }
  }
}
</script>

<style lang="less" scoped>
.function-list {
  padding-bottom: 0;

  .cu-item {
    .app-item {
      border-radius: 50%;
      height: 45px;
      width: 45px;
    }
  }
}
</style>

<style lang="less">
#more {
  .function-list .cu-item text[class*='cuIcon'] {
    margin-top: 0 !important;
  }
}

page {
  padding-top: 100rpx;
}
</style>
