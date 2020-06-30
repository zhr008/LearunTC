<template>
  <view>
    <scroll-view
      @scrolltolower="fetchList"
      :class="sideOpen ? 'show' : ''"
      scroll-y
      style="padding-top: 190rpx"
      class="mainpage"
    >
      <l-banner
        v-model="searchText"
        @input="searchChange"
        placeholder="搜索任务名/关键字"
        type="search"
        fixed
        noshadow
        fill
      >
        <l-icon @click="sideOpen = true" class="text-xxl" slot="searchButton" type="time" color="blue" />
      </l-banner>
      <view class="header">
        <l-nav @change="fetchList" type="flex" :items="tabList" v-model="tab" class="solid-bottom" />
      </view>

      <l-list class="tasklist">
        <template v-for="item of displayList">
          <l-list-item v-if="item.F_Id" @click="taskClick(item)" :key="item.F_Id" :extra="item.F_Content">
            <view
              class="item-img"
              :style="{ backgroundColor: ['#62bbff', '#ffd761', '#ff6283'][item.F_Level] || '#555' }"
            >
              {{ ['普通', '重要', '紧急'][item.F_Level] || '其它' }}
            </view>
            <view class="item-main">
              <view :class="item.F_Title ? 'text-black' : ''">
                {{ item.F_Title || '(草稿 · 待编辑)' }}
                <l-icon v-if="item.F_EnabledMark === 2" type="edit" />
              </view>
              <view class="item-desc">{{ item.F_SchemeName }}</view>
              <view class="tag-bar">
                <view
                  v-for="(tag, i) in item.tags"
                  :key="i"
                  class="tag"
                  :class="[tag.type === 'color' ? 'bg-' + tag.color : 'text-' + tag.color + ' line-' + tag.color]"
                >
                  {{ tag.text }}
                </view>
              </view>
            </view>

            <view class="time" slot="time">
              <view class="text-right margin-bottom-xs">{{ taskDateTime(item)[0] }}</view>
              <view class="text-right" v-if="taskDateTime(item)[1]">{{ taskDateTime(item)[1] }}</view>
            </view>
          </l-list-item>
          <l-list-item v-else @click="fetchList()" :key="item.tips">{{ item.tips }}</l-list-item>
        </template>
      </l-list>

      <l-modal type="bottom" @close="selectItem = null" v-model="modal">
        <view class="text-center" style="width: 100%;" slot="action">
          选中了「{{ selectItem.F_Title || '未命名' }}」草稿
        </view>
        <l-button @click="editDraft" class="block" color="blue" block>编辑草稿</l-button>
        <l-button @click="deleteDraft" class="block margin-top-sm" color="red" block>删除草稿</l-button>
        <l-button @click="cancelSelect" class="block margin-top-sm" line="blue" block>取消</l-button>
      </l-modal>
    </scroll-view>

    <view class="sideclose" :class="sideOpen ? 'show' : ''" @click="sideOpen = false">
      <l-icon type="pullright" color="blue" />
    </view>

    <scroll-view scroll-y class="sidepage" :class="sideOpen ? 'show' : ''">
      <view class="padding">
        <view class="side-title">按任务发布时间筛选：</view>
        <l-button
          @click="changeDateRange('all')"
          :line="dateRange !== 'all' ? 'green' : ''"
          :color="dateRange === 'all' ? 'green' : ''"
          class="block margin-top-sm"
          block
        >
          全部
        </l-button>
        <l-button
          @click="changeDateRange('today')"
          :line="dateRange !== 'today' ? 'blue' : ''"
          :color="dateRange === 'today' ? 'blue' : ''"
          class="block margin-top-sm"
          block
        >
          今天
        </l-button>
        <l-button
          @click="changeDateRange('7d')"
          :line="dateRange !== '7d' ? 'blue' : ''"
          :color="dateRange === '7d' ? 'blue' : ''"
          class="block margin-top-sm"
          block
        >
          最近7天内
        </l-button>
        <l-button
          @click="changeDateRange('1m')"
          :line="dateRange !== '1m' ? 'blue' : ''"
          :color="dateRange === '1m' ? 'blue' : ''"
          class="block margin-top-sm"
          block
        >
          最近1个月内
        </l-button>
        <l-button
          @click="changeDateRange('3m')"
          :line="dateRange !== '3m' ? 'blue' : ''"
          :color="dateRange === '3m' ? 'blue' : ''"
          class="block margin-top-sm"
          block
        >
          最近3个月内
        </l-button>
        <l-button
          @click="changeDateRange('custom')"
          :line="dateRange !== 'custom' ? 'cyan' : ''"
          :color="dateRange === 'custom' ? 'cyan' : ''"
          class="block margin-top-sm"
          block
        >
          自定义
        </l-button>

        <view v-if="dateRange === 'custom'" class="side-title">自定义时间区间：</view>
        <l-date-picker
          @change="searchChange"
          v-if="dateRange === 'custom'"
          v-model="startDate"
          title="起始时间"
          placeholder="点击来选取"
        />
        <l-date-picker
          @change="searchChange"
          v-if="dateRange === 'custom'"
          v-model="endDate"
          title="结束时间"
          placeholder="点击来选取"
        />
      </view>
    </scroll-view>
  </view>
</template>

<script>
import moment from 'moment'

export default {
  data() {
    return {
      tab: 0,
      tabList: ['由我发起', '待办任务', '已办任务'],
      sideOpen: false,

      pageInfo: [{ page: 1, total: 2 }, { page: 1, total: 2 }, { page: 1, total: 2 }],
      list: [[], [], []],
      
      searchData: {},
      searchText: '',
      dateRange: 'all',
      startDate: null,
      endDate: null,

      modal: false,
      selectItem: null
    }
  },

  async onLoad() {
    await this.init()
  },

  onUnload() {
    uni.$off('task-list-change')
  },

  methods: {
    async init() {
      uni.$on('task-list-change', this.refreshList)

      await this.fetchList()
    },

    async fetchList(e) {
      if (e && e.preventDefault) {
        e.preventDefault()
      }

      const tab = this.tab
      const pageInfo = this.pageInfo[tab]
      uni.setNavigationBarTitle({ title: this.tabList[tab] })

      if (pageInfo.page > pageInfo.total) {
        return
      }

      uni.showLoading({ title: '加载任务中...', mask: true })
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot(['/newwf/mylist', '/newwf/mytask', '/newwf/mytaskmaked'][tab]),
        data: {
          ...this.auth,
          data: JSON.stringify({
            pagination: { rows: 10, page: pageInfo.page, sidx: 'F_CreateDate', sord: 'DESC' },
            queryJson: JSON.stringify(this.searchData)
          })
        }
      })

      if (err || !result) {
        return
      }

      result.rows.forEach(item => {
        item.tags = this.getTag(item)
        item.mark = ['my', 'pre', 'maked'][tab]
      })

      pageInfo.page = pageInfo.page + 1
      pageInfo.total = result.total
      this.$set(this.list, tab, this.list[tab].concat(result.rows))

      uni.hideLoading()
      uni.showToast({ icon: 'none', title: `第 ${result.page} / ${result.total} 页，共 ${result.records} 项` })
    },

    async refreshList() {
      this.$set(this.pageInfo, this.tab, { page: 1, total: 2 })
      this.$set(this.list, this.tab, [])

      await this.fetchList()
    },

    taskDateTime({ F_CreateDate }) {
      const dt = moment(F_CreateDate)
      if (!dt.isValid()) {
        return []
      }

      switch (this.config('pageConfig.mytask.taskDateDisplay')) {
        case 'date':
          return [dt.format('YYYY年 M月D日')]
        case 'datetime':
          return [dt.format('YYYY-M-D'), dt.format('HH:mm')]
        case 'before':
          return [dt.fromNow()]
        default:
          const now = moment()
          if (dt.isSame(now, 'day')) {
            return [`今天 ${dt.format('HH:mm')}`]
          } else if (dt.isSame(now, 'year')) {
            return [dt.format('M月D日'), dt.format('HH:mm')]
          }

          return [dt.format('YYYY-M-D')]
      }
    },

    taskClick(item) {
      if (item.F_EnabledMark === 2) {
        this.selectItem = item
        this.modal = true

        return
      }
      if (item.F_IsAgain === 1 && this.tab !== 2) {
        this.setPageParam({ ...item, mark: 'my' })
        uni.navigateTo({ url: '/pages/nworkflow/releasetask/single?type=again' })

        return
      }
      if (item.F_TaskType === 4 || item.F_TaskType === 6) {
        this.setPageParam({ ...item })
        uni.navigateTo({ url: './single?type=child' })

        return
      }

      this.setPageParam(item)
      uni.navigateTo({ url: './single' })
    },

    editDraft() {
      this.setPageParam(this.selectItem)
      this.cancelSelect()
      uni.navigateTo({ url: '/pages/nworkflow/releasetask/single?type=draft' })
    },

    deleteDraft() {
      const that = this
      uni.showModal({
        title: '删除草稿',
        content: `确定要删除草稿「${this.selectItem.F_Title || '(未命名)'}」吗？`,
        success({ confirm }) {
          if (!confirm) {
            return
          }

          uni.request({
            url: this.apiRoot`/newwf/deldraft`,
            method: 'POST',
            data: {
              ...that.auth,
              data: that.selectItem.F_Id
            },
            success() {
              const list = that.list[that.tab]
              const index = list.findIndex(item => item.F_Id === that.selectItem.F_Id)
              list.splice(index, 1)
              that.cancelSelect()
            }
          })
        }
      })
    },

    cancelSelect() {
      this.modal = false
      this.selectItem = null
    },

    changeDateRange(type) {
      this.dateRange = type
      if (type === 'custom') {
        this.startDate = null
        this.endDate = null
      }
      this.searchChange()
    },

    getTag(item) {
      const { F_IsFinished, F_IsAgain, F_TaskName, F_EnabledMark, F_IsUrge, F_TaskType } = item
      const { tab } = this
      let result = []

      if (tab !== 2) {
        result.push({ text: '待审批', color: 'orange', type: 'line' })
      }

      if (F_TaskName) {
        result.push({ text: F_TaskName, color: 'blue', type: 'line' })
      }

      if (F_IsFinished === 1) {
        result = [{ text: '已结束', color: 'black', type: 'color' }]
      } else if (F_EnabledMark === 2) {
        result = [{ text: '草稿', color: 'orange', type: 'color' }]
      } else if (F_EnabledMark === 3) {
        result = [{ text: '作废', color: 'red', type: 'line' }]
      }

      if (F_IsAgain === 1 && tab !== 2) {
        result = [{ text: '重新发起', color: 'red', type: 'color' }]
      }

      if (F_TaskType === 3 && tab === 1) {
        result.unshift({ text: '加签', color: 'blue', type: 'line' })
      }

      if (Number(F_IsUrge) === 1 && tab === 1) {
        result.unshift({ text: '催办加急', color: 'red', type: 'color' })
      }

      return result
    },

    searchChange() {
      const { searchText, dateRange, startDate, endDate } = this
      const result = {}

      if (searchText) {
        result.keyword = searchText
      }

      const todayEnd = moment().format('YYYY-MM-DD 23:59:59')
      if (dateRange === 'today') {
        result.StartTime = moment()
          .subtract(1, 'day')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '7d') {
        result.StartTime = moment()
          .subtract(7, 'days')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '1m') {
        result.StartTime = moment()
          .subtract(1, 'month')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === '3m') {
        result.StartTime = moment()
          .subtract(3, 'months')
          .format('YYYY-MM-DD 00:00:00')
        result.EndTime = todayEnd
      } else if (dateRange === 'custom' && (startDate || startDate)) {
        if (!(startDate && startDate && moment(startDate).isAfter(endDate))) {
          result.StartTime = startDate ? moment(startDate).format('YYYY-MM-DD 00:00:00') : '1900-01-01 00:00:00'
          result.EndTime = endDate ? moment(endDate).format('YYYY-MM-DD 23:59:59') : todayEnd
        }
      }

      this.searchData = result
      this.refreshList()
    }
  },

  computed: {
    displayList() {
      const isAllLoaded = this.pageInfo[this.tab].page >= this.pageInfo[this.tab].total

      return [...this.list[this.tab], { tips: isAllLoaded ? '已展示全部任务' : '加载中...' }]
    }
  }
}
</script>

<style scoped lang="less">
@import '~@/common/css/sidepage.less';

.header {
  position: fixed;
  width: 100%;
  top: 100rpx;
  z-index: 1024;
  border-bottom: 1px #ddd;
}

.tasklist {
  .item-img {
    display: inline-block;
    height: 55px;
    width: 55px;
    margin: 15px 0;
    line-height: 55px;
    text-align: center;
    color: #fff;
    border-radius: 50%;
    font-size: 18px;
  }

  .item-main {
    display: inline-block;
    margin-left: 15px;

    .item-desc {
      font-size: 13px;
      line-height: 1.2em;
    }

    .tag-bar {
      min-height: 18px;

      .tag {
        display: inline-block;
        margin-right: 5px;
        padding: 2px 5px;
        font-size: 12px;
        line-height: 1em;

        &[class*='line-'] {
          border: 1rpx solid currentColor;
          padding: 1px 4px;
        }
      }
    }
  }

  .time {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
  }
}
</style>

<style>
page {
  width: 100vw;
  overflow: hidden;
}
</style>
