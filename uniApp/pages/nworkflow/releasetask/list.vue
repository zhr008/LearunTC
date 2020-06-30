<template>
  <view class="page">
    <l-banner
      v-model="searchText"
      @change="searchChange"
      noSearchButton
      placeholder="搜索流程模板名"
      type="search"
      fill
      fixed
    />

    <l-list class="tasklist">
      <template v-for="item of displayList">
        <l-list-item v-if="item.F_Id" @click="taskClick(item)" :key="item.F_Id">
          <view class="item-img" style="background-color: #488aff;color:white;">
            <l-icon type="form" style="margin-right: 0;" />
          </view>
          <view class="item-main">
            <view class="text-black">{{ item.F_Name }}</view>
            <view class="item-desc">{{ item.F_Code }}</view>
          </view>
        </l-list-item>
        <l-list-item v-else @click="fetchList()" :key="item.tips">{{ item.tips }}</l-list-item>
      </template>
    </l-list>
  </view>
</template>

<script>
export default {
  data() {
    return {
      searchData: {},
      searchText: '',

      page: 1,
      total: 2,

      list: []
    }
  },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      await this.fetchList()
    },

    async fetchList() {
      if (this.page > this.total) {
        return
      }

      uni.showLoading({ title: '加载流程信息...', mask: true })
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/newwf/schemelist`,
        data: {
          ...this.auth,
          data: JSON.stringify({
            pagination: { rows: 10, page: this.page, sidx: 'F_Name', sord: 'DESC' },
            queryJson: JSON.stringify(this.searchData)
          })
        }
      })

      if (err || !result) {
        uni.hideLoading()
        uni.showToast({ title: '加载数据时出错', icon: 'none' })
        return
      }

      this.total = result.total
      this.page = this.page + 1
      this.list = this.list.concat(result.rows)

      uni.hideLoading()
      uni.showToast({ icon: 'none', title: `第 ${result.page} / ${result.total} 页，共 ${result.records} 项` })
    },

    async refresh() {
      this.page = 1
      this.total = 2
      this.list = []

      await this.fetchList()
    },

    taskClick(item) {
      this.setPageParam(item)
      uni.navigateTo({ url: './single?action=view' })
    },

    searchChange() {
      const searchData = {}
      if (this.searchText) {
        searchData.keyword = this.searchText
      }

      this.searchData = searchData
      this.refresh()
    }
  },

  computed: {
    displayList() {
      return [...this.list, { tips: this.page >= this.total ? '已展示全部任务模板' : '加载中...' }]
    }
  }
}
</script>

<style lang="less" scoped>
.tasklist {
  .item-img {
    display: inline-block;
    height: 40px;
    width: 40px;
    margin: 15px 0;
    line-height: 40px;
    text-align: center;
    color: #fff;
    border-radius: 4px;
    font-size: 18px;
  }

  .item-main {
    display: inline-block;
    margin-left: 15px;

    .item-desc {
      font-size: 13px;
      line-height: 1.2em;
    }
  }
}
</style>

<style>
page {
  padding-top: 100rpx;
}
</style>
