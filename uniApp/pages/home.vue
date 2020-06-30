<template>
  <view id="home" class="page">
    <l-banner
      @inputFocus="moreClick"
      placeholder="搜索应用名"
      inputStyle="background-color:#3d9ee0;color:#fff"
      placeholderStyle="color:#fff"
      hexColor="#0C86D8"
      type="search"
      fill
    >
      <!-- #ifndef H5 -->
      <l-icon @click="scanClick" class="text-xxl margin-left-sm" slot="left" type="scan" color="white" />
      <!-- #endif -->
      <l-icon @click="msgClick" class="text-xxl" slot="searchButton" type="mail" color="white" />
    </l-banner>

    <!-- 轮播图片 -->
    <swiper style="height:120px;">
      <swiper-item v-for="(item, index) of imgData" :key="index">
        <image style="height:100%; width:100%;" mode="aspectFill" :src="apiRoot(`/desktop/img?data=${item}`)"></image>
      </swiper-item>
    </swiper>

    <!-- 功能宫格列表 -->
    <view class="function-list cu-list grid no-border" :class="['col-' + config('pageConfig.home.functionListColunm')]">
      <view
        @click="funcListClick(item)"
        class="cu-item text-center flex flex-wrap justify-center align-center"
        v-for="(item, index) in funcListDisplay"
        :key="index"
      >
        <view class="app-item align-center flex flex-wrap justify-center align-center">
          <l-icon :type="funcListIcon(item)" color="white" class="text-sl" />
        </view>
        <text>{{ item.F_Name }}</text>
      </view>
    </view>
    <view class="text-center bg-white margin-bottom">
      <text @click="moreClick" class="function-more-btn margin-tb text-gray">更多应用</text>
    </view>

    <!-- 统计数据宫格 -->
    <l-title class="solid-bottom">统计数据</l-title>
    <view class="count-list cu-list grid col-3 solid-top">
      <view class="cu-item text-center" v-for="(item, index) in countData" :key="index">
        <text class="margin-bottom-xs">{{ item.title }}</text>
        <text class="count-item-value">{{ item.value }}</text>
      </view>
    </view>

    <!-- 通知列表区块 -->
    <view :key="blockIndex" v-for="(block, blockIndex) of noticeData">
      <view class="margin-top"></view>
      <l-title class="solid-bottom">{{ block.title }}</l-title>
      <l-list class="solid-top">
        <l-list-item @click="noticeClick(item)" arrow v-for="(item, i) of block.content" :key="i">
          {{ item.f_title }}
          <l-tag slot="action" line="gray">{{ postDateTime(item.f_time) }}</l-tag>
        </l-list-item>
      </l-list>
    </view>
    <view class="margin-top" style="height: 1px;"></view>

    <!-- 图表区块 -->
    <view :key="item.id" v-for="(item, index) of chartData">
      <l-title class="solid-bottom">{{ item.title }}</l-title>
      <view class="chart-list">
        <canvas
          @tap="chartTap(index, $event)"
          @touchstart="touchStart(index, $event)"
          @touchmove="touchMove(index, $event)"
          @touchend="touchEnd(index, $event)"
          :style="{ width: cWidth + 'px', height: cHeight + 'px' }"
          :canvas-id="item.id"
          :id="item.id"
          disable-scroll="true"
          class="charts"
        ></canvas>
      </view>
      <view class="margin-top" style="height: 1px;"></view>
    </view>
  </view>
</template>

<script>
import moment from 'moment'
import uCharts from '@/components/u-charts/u-charts.js'

const chartsObject = {}

export default {
  data() {
    return {
      imgData: [],
      settingData: null,
      listData: [],
      myList: [],

      countData: [],
      noticeData: [],
      chartData: [],

      pixelRatio: 1,
      cWidth: '',
      cHeight: ''
    }
  },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      uni.$on('home-list', () => {
        uni.request({ url: this.apiRoot`/function/mylist`, data: this.auth }).then(([err, r]) => {
          this.myList = r.data.data
        })
      })
      this.pixelRatio = uni.getSystemInfoSync().pixelRatio
      this.cWidth = uni.upx2px(750)
      this.cHeight = uni.upx2px(500)

      if (!this.$store.state.user) {
        uni.reLaunch({ url: '/pages/login' })
        return
      }

      // 同时发出请求，获取轮播图、所有功能列表、我的功能列表、商机通知提醒图表数据
      // 商机、通知提醒图表数据，获取的是数据ID，所以还需要进一步请求
      await Promise.all([
        uni
          .request({ url: this.apiRoot`/desktop/imgid`, data: this.auth })
          .then(([err, result]) => (this.imgData = result.data.data)),

        uni
          .request({ url: this.apiRoot`/function/list`, data: this.auth })
          .then(([err, result]) => (this.listData = result.data.data.data)),

        uni
          .request({ url: this.apiRoot`/function/mylist`, data: this.auth })
          .then(([err, result]) => (this.myList = result.data.data)),

        uni
          .request({ url: this.apiRoot`/desktop/setting`, data: this.auth })
          .then(([err, result]) => (this.settingData = result.data.data.data))
      ])

      this.myList = this.myList.filter(t => this.listData.find(li => li.F_Id === t))

      // 发出请求，获取商机信息、消息通知信息、图表信息；三类数据全部同时请求
      await Promise.all([
        ...this.settingData.target.map(item =>
          uni
            .request({
              url: this.apiRoot`/desktop/data`,
              data: { ...this.auth, data: JSON.stringify({ type: 'Target', id: item.F_Id }) }
            })
            .then(([err, result]) => {
              const { value } = result.data.data
              if (!value) {
                return
              }
              this.countData.push({ title: item.F_Name, value })
            })
        ),

        ...this.settingData.list.map(item =>
          uni
            .request({
              url: this.apiRoot`/desktop/data`,
              data: { ...this.auth, data: JSON.stringify({ type: 'list', id: item.F_Id }) }
            })
            .then(([err, result]) => {
              const { value } = result.data.data
              if (!value) {
                return
              }
              this.noticeData.push({ title: item.F_Name, content: value })
            })
        ),

        ...this.settingData.chart.map(item =>
          uni
            .request({
              url: this.apiRoot`/desktop/data`,
              data: { ...this.auth, data: JSON.stringify({ type: 'chart', id: item.F_Id }) }
            })
            .then(([err, result]) => {
              const { value } = result.data.data
              if (!value) {
                return
              }
              this.chartData.push({
                title: item.F_Name,
                value,
                id: item.F_Id,
                type: item.F_Type
              })
            })
        )
      ])

      // 渲染图表
      const _self = this
      this.chartData.forEach(item => {
        const chartConfig = {
          $this: _self,
          pixelRatio: 1,
          width: this.cWidth * 1,
          height: this.cHeight * 1,
          canvasId: item.id,
          background: '#FFFFFF',
          dataLabel: true,
          enableScroll: true,
          padding: [20, 15, 5, 15]
        }

        // 根据 item.type 的值选用对应的图表初始化配置项
        // 0=环形图；1=折线图；2=柱状图
        const charts = new uCharts(
          [
            {
              ...chartConfig,
              type: 'ring',
              series: item.value.map(t => ({ name: t.name, data: t.value })),
              extra: { pie: { offsetAngle: -45, ringWidth: 20, labelWidth: 15 } },
              legend: { lineHeight: 20 }
            },
            {
              ...chartConfig,
              type: 'line',
              series: [{ name: item.title, data: item.value.map(t => t.value) }],
              categories: item.value.map(t => t.name),
              extra: { line: { type: 'straight' } },
              xAxis: { rotateLabel: true, fontSize: 10, itemCount: 8 }
            },
            {
              ...chartConfig,
              type: 'column',
              series: [{ name: item.title, data: item.value.map(t => t.value) }],
              categories: item.value.map(t => t.name),
              xAxis: { rotateLabel: true, fontSize: 10 }
            }
          ][item.type]
        )
        chartsObject[item.id] = charts
      })
    },

    chartTap(index, e) {
      const item = this.chartData[index]
      const format = [
        ({ name, data }) => `${name}: ${data}`,
        ({ name, data }, category) => `${category} ${name}: ${data}`
      ][item.type]
      chartsObject[item.id].showToolTip(e, { format })
    },

    touchStart(index, e) {
      const item = this.chartData[index]
      chartsObject[item.id].scrollStart(e)
    },

    touchMove(index, e) {
      const item = this.chartData[index]
      chartsObject[item.id].scroll(e)
    },

    touchEnd(index, e) {
      const item = this.chartData[index]
      chartsObject[item.id].scrollEnd(e)
    },

    postDateTime(timeStr) {
      switch (this.config('pageConfig.home.noticeDateDisplay')) {
        case 'date':
          return moment(timeStr).format('YYYY年 M月D日')
        case 'datetime':
          return moment(timeStr).format('YYYY-M-D H:mm')
        default:
          return moment(timeStr).fromNow()
      }
    },

    // 获取功能区按钮的图标的 type
    funcListIcon(item) {
      if (!item || !item.F_Icon) {
        return ''
      }

      return item.F_Icon.replace(`iconfont icon-`, ``)
    },

    // 点击功能按钮
    funcListClick(item) {
      if (item.F_IsSystem === 2) {
        this.setPageParam(item)
        uni.navigateTo({ url: `/pages/customapp/list?formId=${item.F_FormId}` })
        return
      }

      uni.navigateTo({ url: `/pages/${item.F_Url}/list` })
    },

    // 点击通知公告的标题
    noticeClick(item) {
      this.setPageParam(item)
      uni.navigateTo({ url: '/pages/home/notice' })
    },

    // #ifndef H5
    // 点击左上角扫码图标，H5 无此功能
    scanClick() {
      uni.scanCode({
        scanType: ['qrCode', 'barCode'],
        success({ result, charSet }) {}
      })
    },
    // #endif

    // 点击更多功能按钮
    moreClick() {
      uni.navigateTo({ url: '/pages/home/more' })
    },

    // 点击右上角的消息按钮
    msgClick() {
      uni.switchTab({ url: '/pages/msg' })
    }
  },

  computed: {
    funcListDisplay() {
      const { myList, listData } = this
      const displayCount = this.config('pageConfig.home.functionListLimit')
      const myFuncList = myList.reduce((list, id) => {
        if (listData.find(t => t.F_Id === id)) {
          return [...list, listData.find(t => t.F_Id === id)]
        }
        return list
      }, [])

      if (!displayCount || displayCount === -1) {
        return myFuncList
      }

      return myFuncList.slice(0, displayCount)
    }
  }
}
</script>

<style scoped lang="less">
.page {
  background-color: #f3f3f3;

  .content {
    background-color: #fff;
  }

  .function-list {
    padding-bottom: 0;

    .cu-item {
      .app-item {
        border-radius: 50%;
        height: 45px;
        width: 45px;
      }

      &:nth-child(7n + 1) > .app-item {
        background-color: #62bbff;
      }
      &:nth-child(7n + 2) > .app-item {
        background-color: #7bd2ff;
      }
      &:nth-child(7n + 3) > .app-item {
        background-color: #ffd761;
      }
      &:nth-child(7n + 4) > .app-item {
        background-color: #fe955c;
      }
      &:nth-child(7n + 5) > .app-item {
        background-color: #ff6283;
      }
      &:nth-child(7n + 6) > .app-item {
        background-color: #60e3f3;
      }
      &:nth-child(7n) > .app-item {
        background-color: #acc8fe;
      }
    }
  }

  .function-more-btn {
    display: inline-block;
    border: currentColor 1px solid;
    border-radius: 2px;
    padding: 10rpx 30rpx;
  }

  .count-list {
    &:after {
      content: '';
      clear: both;
      display: table;
    }

    .count-item-value {
      color: #0188d2;
      font-size: 24px;
    }
  }

  .chart-list {
    background-color: #fff;

    .charts {
    }
  }
}
</style>

<style lang="less">
#home {
  .function-list .cu-item text[class*='cuIcon'] {
    margin-top: 0 !important;
  }
}
</style>
