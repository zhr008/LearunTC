<template>
  <view id="contact" class="page">
    <l-banner
      v-model="searchText"
      placeholder="搜索(分)公司名/部门名/职员姓名"
      type="search"
      noSearchButton
      fixed
      fill
    />

    <view class="tree">
      <template v-for="(item, index) of treeList">
        <view
          class="tree-item"
          :key="item.id"
          v-if="isSearch ? item.show && item.searchShow : item.show"
          :style="{ paddingLeft: item.rank * 25 + 'rpx' }"
          @click="clickTreeItem(item, index)"
        >
          <l-icon class="tree-item-icon" v-if="item.type !== 'staff'" :type="item.open ? 'unfold' : 'right'" />
          <image
            class="tree-item-avatar"
            v-else
            mode="aspectFill"
            :style="{ borderRadius: roundAvatar ? '50%' : '3px' }"
            :src="avatarSrc(item)"
          ></image>
          <text class="tree-item-title">{{ item.name }}</text>
          <l-tag v-if="displayTag(item)" class="margin-left-sm" size="sm" :line="tagColor(item)">
            {{ typeName(item) }}
          </l-tag>
        </view>
      </template>
    </view>
  </view>
</template>

<script>
import moment from 'moment'

export default {
  data() {
    return {
      contactList: [],
      searchText: ''
    }
  },

  async onLoad() {
    await this.init()
  },

  methods: {
    async init() {
      uni.showLoading({ title: '加载联系人...', mask: true })
      const { company: companyTable, dep: departmentTable, staff: staffTable } = this.$store.state
      const { find } = this

      const staffFirst = this.config('pageConfig.contact.staffFirst')
      const expandRank = this.config('pageConfig.contact.expand')

      // 查找所有根级公司，存入数组。根级公司必须显示， show 必须为 true
      const contactList = Object.entries(companyTable)
        .filter(([id, company]) => Number(company.parentId) === 0)
        .map(([id, company]) => ({
          id,
          name: company.name,
          type: 'company',
          rank: 0,
          show: true,
          open: expandRank === true || 0 < expandRank,
          children: [],
          searchShow: false
        }))

      // 插入剩余非根级公司，完成公司部分数据的填充
      Object.entries(companyTable)
        .filter(([id, company]) => Number(company.parentId) !== 0)
        .forEach(([id, company]) => {
          const parent = find(contactList, t => t.id === company.parentId)
          if (!parent) {
            return
          }

          const currentCompany = {
            id,
            name: company.name,
            type: 'company',
            parent: company.parentId,
            rank: parent.rank + 1,
            show: parent.open,
            open: expandRank === true || parent.rank + 1 < expandRank,
            children: [],
            searchShow: false
          }
          parent.children.push(currentCompany)
        })

      // 插入部门信息，完成部门信息填充
      Object.entries(departmentTable).forEach(([id, department]) => {
        if (Number(department.parentId) === -1) {
          return
        }

        const parentId = Number(department.parentId) !== 0 ? department.parentId : department.companyId
        const parent = find(contactList, t => t.id === parentId)

        if (!parent) {
          return
        }

        const currentDep = {
          id,
          name: department.name,
          type: 'dep',
          parent: parentId,
          rank: parent.rank + 1,
          show: parent.open,
          open: expandRank === true || parent.rank + 1 < expandRank,
          children: [],
          searchShow: false
        }
        parent.children.push(currentDep)
      })

      // 插入职员信息，完成通讯录整体结构
      Object.entries(staffTable).forEach(([id, staff]) => {
        if (id === 'System' || staff.companyId === '') {
          return null
        }

        const parentId = staff.departmentId && Number(staff.departmentId) !== 0 ? staff.departmentId : staff.companyId
        const parent = find(contactList, t => t.id === parentId)

        if (!parent) {
          return
        }

        const currentStaff = {
          id,
          name: staff.name,
          type: 'staff',
          parent: parentId,
          img: staff.img,
          rank: parent.rank + 1,
          show: parent.open,
          open: false,
          searchShow: false
        }
        parent.children[staffFirst ? 'unshift' : 'push'](currentStaff)
      })

      this.contactList = contactList
      uni.hideLoading()
    },

    clickTreeItem(item, index) {
      if (item.type === 'staff') {
        uni.navigateTo({ url: `/pages/msg/chat?userid=${item.id}` })
      }

      const { find } = this
      const currentNode = find(this.contactList, t => t.id === item.id)
      const result = !item.open
      currentNode.open = result

      const atcion = (t, parent) => {
        t.show = result && parent.open
      }

      find(currentNode.children, atcion, currentNode)
    },

    avatarSrc(item) {
      if (!Number.isNaN(item.img)) {
        return Number(item.img) === 1 ? '/static/img-avatar/chat-boy.jpg' : '/static/img-avatar/chat-girl.jpg'
      }

      return item.img
    },

    // 工具方法，依次递归遍历 list 树形结构直到找出满足 compare 回调的项为止
    find(list, compare, parent = null) {
      for (const item of list || []) {
        if (compare(item, parent)) {
          return item
        }

        const result = this.find(item.children, compare, item)

        if (result) {
          return result
        }
      }
    },

    typeName(item) {
      const tagNames = this.config('pageConfig.contact.costumeTag')
      const index = {
        staff: 3,
        dep: 2,
        company: item.rank <= 0 ? 0 : 1
      }[item.type]

      return tagNames[index]
    },

    displayTag(item) {
      if (!this.config('pageConfig.contact.tag')) {
        return false
      }

      return item.type !== 'staff' || this.config('pageConfig.contact.staffTag')
    },

    tagColor(item) {
      return { company: 'red', dep: 'blue', staff: 'green' }[item.type]
    }
  },
  computed: {
    // 将树形结构拍平
    treeList() {
      const { contactList, searchText, isSearch } = this
      const treeList = []

      const genTreeList = (list, parent = []) => {
        list.forEach(item => {
          if (isSearch) {
            item.searchShow = item.name.includes(searchText)
            if (item.searchShow) {
              parent.forEach(parentItem => {
                parentItem.searchShow = true
              })
            }
          }
          treeList.push(item)
          if (item.children && item.children.length > 0) {
            genTreeList(item.children, [...parent, item])
          }
        })
      }

      genTreeList(contactList)

      return treeList
    },

    isSearch() {
      return this.searchText && this.searchText.length > 0
    },

    roundAvatar() {
      const page = this.config('pageConfig.contact.roundAvatar')
      const global = this.config('roundAvatar')

      return page === null || page === undefined ? global : page
    }
  }
}
</script>

<style scoped lang="less">
.tree {
  .tree-item {
    padding: 15rpx;
    display: flex;
    align-items: center;
    background-color: #fff;

    .tree-item-icon {
      margin: 0 15px;
    }

    .tree-item-avatar {
      width: 30px;
      height: 30px;
      margin-left: 15px;
      margin-right: 8px;
    }
  }
}
</style>

<style lang="less">
page {
  padding-top: 100rpx;
}
</style>
