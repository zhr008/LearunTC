<template>
  <view class="page">
    <scroll-view @scrolltolower="fetchList" scroll-y>
      <view class="list">
        <l-custom-item
          v-for="(item, index) of displayList"
          :key="item[primaryKey]"
          :mainTitle="mainTitle"
          :mainContent="item.mainContent"
          :subTitle="subTitle"
          :subContent="item.subContent"
          @view="action('view', `${item[primaryKey]}`)"
          @edit="action('edit', `${item[primaryKey]}`)"
          @delete="deleteItem(`${item[primaryKey]}`, index)"
        />

        <l-custom-item info @click="fetchList">
          {{ page >= total ? `已加载全部条目` : `加载中...` }}
        </l-custom-item>
      </view>
    </scroll-view>

    <l-custom-add @click="action('create')" />
  </view>
</template>

<script>
import _ from 'lodash'
import moment from 'moment'

export default {
  data() {
    return {
      ready: false,
      formId: '',
      listScheme: {},
      itemScheme: {},
      list: [],

      primaryKey: '',
      mainTitle: [],
      subTitle: [],
      mainSchemes: [],
      subSchemes: [],

      page: 1,
      total: 2,

      searchText: ''
    }
  },

  async onLoad({ formId }) {
    await this.init(formId)
  },

  onUnload() {
    uni.$off('custom-list-change')
  },

  onReachBottom() {
    this.fetchList()
  },

  methods: {
    async init(formId) {
      uni.$on('custom-list-change', this.refresh)

      this.formId = formId
      const pageInfo = this.getPageParam()
      uni.setNavigationBarTitle({ title: pageInfo.F_Name })
      this.listScheme = JSON.parse(pageInfo.F_Scheme)

      await this.fetchItemScheme()
      const mainTitleIds = this.listScheme.title.split(',')
      const subTitleIds = this.listScheme.content.filter(Boolean)

      const scheme = this.itemScheme.F_Scheme
      const mainTable = scheme.dbTable.find(t => !t.relationName)
      const tableName = mainTable.name
      this.primaryKey = `${mainTable.field.toLowerCase()}${scheme.dbTable.findIndex(t => !t.relationName)}`

      await this.fetchList()

      for (let tableIndex = 0; tableIndex < scheme.data.length; ++tableIndex) {
        const { componts } = scheme.data[tableIndex]
        componts.forEach(t => {
          if (t.table === tableName) {
            if (mainTitleIds.includes(t.id)) {
              this.mainTitle.push(t.title)
              this.mainSchemes.push({ ...t, __id__: `${t.field.toLowerCase()}${tableIndex}` })
            } else if (subTitleIds.includes(t.id)) {
              this.subTitle.push(t.title)
              this.subSchemes.push({ ...t, __id__: `${t.field.toLowerCase()}${tableIndex}` })
            }
          }
        })
      }
    },

    async fetchList(e) {
      if (e && e.preventDefault) {
        e.preventDefault()
      }

      if (this.page > this.total) {
        return
      }

      uni.showLoading({ title: '加载列表中...', mask: true })
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/custmer/pagelist`,
        data: {
          ...this.auth,
          data: JSON.stringify({
            pagination: { rows: 10, page: this.page, sidx: this.primaryKey, sord: 'ASC' },
            queryJson: JSON.stringify({}),
            formId: this.formId
          })
        }
      })

      if (err || !result) {
        uni.hideLoading()
        uni.showToast({ title: '加载数据时出错', icon: 'none' })
        this.loading = false
        return
      }

      this.total = result.total
      this.page = result.page + 1
      this.list = this.list.concat(result.rows)

      uni.hideLoading()
    },

    async refresh() {
      this.page = 1
      this.total = 2
      this.list = []

      await this.fetchList()
    },

    async fetchItemScheme() {
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot`/form/scheme`,
        data: { ...this.auth, data: JSON.stringify([{ id: this.formId, ver: '' }]) }
      })

      this.itemScheme = result[this.formId]
      this.itemScheme.F_Scheme = JSON.parse(this.itemScheme.F_Scheme)
      this.ready = true
    },

    async action(type, id = 'no') {
      this.setPageParam(this.itemScheme)
      uni.navigateTo({ url: `./single?type=${type}&id=${id}` })
    },

    async deleteItem(id, index) {
      uni.showModal({
        title: '删除项目',
        content: `确定要删除该项吗？`,
        success: ({ confirm }) => {
          if (!confirm) {
            return
          }

          uni
            .request({
              url: this.apiRoot`/form/delete`,
              method: 'POST',
              header: { 'content-type': 'application/x-www-form-urlencoded' },
              data: { ...this.auth, data: JSON.stringify({ schemeInfoId: this.formId, keyValue: id }) }
            })
            .then(([err, { data }]) => {
              if (err || !data || data.code !== 200) {
                uni.showToast({ title: '删除失败', icon: 'none' })
                return
              }

              this.list.splice(index, 1)
              uni.showToast({ title: '删除成功', icon: 'success' })
            })
        }
      })
    },

    displayItem(scheme, value) {
      switch (scheme.type) {
        case 'currentInfo':
        case 'organize':
          switch (scheme.dataType) {
            case 'user':
              return _.get(this.$store.state.staff, `${value}.name`, '')

            case 'department':
              return _.get(this.$store.state.dep, `${value}.name`, '')

            case 'company':
              return _.get(this.$store.state.company, `${value}.name`, '')

            default:
              return value || ''
          }

        case 'radio':
        case 'select':
        case 'layer':
          const selectItem = Object.values(this.$store.state.propTable[scheme.itemCode]).find(t => t.value === value)
          return _.get(selectItem, 'text', '')

        case 'checkbox':
          if (!value || value.split(',').length <= 0) {
            return ''
          }
          const checkboxItems = value.split(',')
          return Object.values(this.$store.state.propTable[scheme.itemCode])
            .filter(t => checkboxItems.includes(t.value))
            .map(t => t.text)
            .join('，')

        case 'datetime':
          if (!value) {
            return ''
          }
          return moment(value).format(Number(scheme.dateformat) === 0 ? 'YYYY-MM-DD' : 'YYYY-MM-DD HH:mm:ss')

        default:
          return value || ''
      }
    }
  },

  computed: {
    displayList() {
      return this.list.map(item => {
        const mainContent = this.mainSchemes.map(scheme => this.displayItem(scheme, item[scheme.__id__]))
        const subContent = this.subSchemes.map(scheme => this.displayItem(scheme, item[scheme.__id__]))

        return { ...item, mainContent, subContent }
      })
    }
  }
}
</script>
