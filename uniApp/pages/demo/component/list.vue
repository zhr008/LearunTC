<template>
  <view>
    <l-title>宫格列表：</l-title>
    <l-select title="选择宫格的列数" :range="[3, 4, 5]" v-model="gridCol" />
    <l-switch title="是否显示边框" v-model="gridBorder" />
    <view class="cu-list grid" :class="['col-' + gridCol, gridBorder ? '' : 'no-border']">
      <view class="cu-item" v-for="(item, index) in gridList" :key="index">
        <l-icon :type="item.icon" :color="item.color" />
        <l-tag badge v-if="item.badge">{{ item.badge }}</l-tag>
        <text>{{ item.name }}</text>
      </view>
    </view>
    <wxdoc-desc>
      外部容器添加 class="cu-list grid col-*" 并将*替换为列数(一般为3~5)，内部容器添加 class="cu-item"；
      外部容器添加类名 no-border 即可设为无边框宫格
    </wxdoc-desc>

    <l-title>菜单列表：</l-title>
    <view class="bg-gray">
      <l-switch title="卡片轮廓型列表" v-model="menuCard" />
      <l-list :border="menuBorder" :card="menuCard">
        <l-list-item title="基础型列表项" />

        <l-list-item title="带有箭头的列表项" arrow />

        <l-list-item>
          <l-icon type="emoji" color="red" />
          定制内容的列表项
          <template #action>
            我是右侧的文字~
          </template>
        </l-list-item>

        <l-list-item title="带按钮的列表项">
          <l-button slot="action" color="green" shadow>我是按钮</l-button>
        </l-list-item>

        <l-list-item title="带标签的列表项">
          <template #action>
            <l-tag color="red" round>标签1</l-tag>
            <l-tag color="olive" round>标签2</l-tag>
            <l-tag color="blue" round>标签3</l-tag>
          </template>
        </l-list-item>

        <l-list-item title="多行列表项">
          <view>多行列表项</view>
          <view class="text-gray text-sm" slot="extra">
            <l-icon type="infofill" />
            我是Tips文字
          </view>
        </l-list-item>
      </l-list>
    </view>
    <wxdoc-desc>
      列表外侧使用 l-list 标签，添加 card 属性转为卡片轮廓列表；列表项使用 l-list-item 标签；
      列表项添加 arrow 属性可以设置箭头；
      列表项可以添加以下属性：title、action，分别表示列表项左侧标题、列表项右侧内容；
      默认插槽的内容将渲染为列表项的 title 标题内容；名为 action 的插槽内容渲染到列表项右侧；
      名为 extra 的插槽内容将渲染到列表项的标题下方
    </wxdoc-desc>

    <l-title>消息列表：</l-title>
    <l-list message>
      <l-list-item
        avatar="https://img.learun.cn/wxcx/pic.jpg"
        title="王小明"
        extra="消息内容消息内容消息内容消息内容消息内容"
        time="22:04"
      />

      <l-list-item
        title="王小明"
        extra="消息内容消息内容消息内容消息内容消息内容"
        time="22:04"
      >
        <l-avatar slot="avatar" src="https://img.learun.cn/wxcx/pic.jpg" size="lg">
          <l-tag slot="badge" badge round color="red">5</l-tag>
        </l-avatar>
      </l-list-item>

      <l-list-item
        avatar="https://img.learun.cn/wxcx/pic.jpg"
        extra="消息内容消息内容消息内容消息内容消息内容"
        time="22:04"
      >
        <text class="text-red">王小明</text>
        <l-tag class="margin-left-sm" color="red" light round>新消息</l-tag>
      </l-list-item>

      <l-list-item
        title="王小明"
        avatar="https://img.learun.cn/wxcx/pic.jpg"
        extra="消息内容消息内容消息内容消息内容消息内容"
        time="22:04"
      >
        <template #extra>
          我在
          <l-icon type="locationfill" color="orange" />
          苏州市
        </template>
      </l-list-item>

      <l-list-item
        title="王小明"
        avatar="https://img.learun.cn/wxcx/pic.jpg"
        extra="消息内容消息内容消息内容消息内容消息内容"
        time="22:04"
      >
        <template #tips>
          <l-icon type="notice_forbid_fill" color="gray" size="lg" />
        </template>
      </l-list-item>
    </l-list>
    <wxdoc-desc>
      在 l-list 列表标签上添加 message 属性即可将列表设为消息列表；列表项仍是 l-list-item 标签；
      标签可以设置以下属性：avatar、title、extra、time、tips；
      这些属性依次表示：头像地址、消息标题、消息内容、发送时间、消息额外提示(显示在时间下面)；
      其中 avatar、title、extra、time、tips 属性的内容均可使用同名的插槽来渲染
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return {
      gridCol: 3,
      gridBorder: false,

      menuArrow: false,
      menuCard: false,
      gridList: [
        {
          icon: 'cardboardfill',
          color: 'red',
          badge: 120,
          name: 'VR'
        },
        {
          icon: 'recordfill',
          color: 'orange',
          badge: 1,
          name: '录像'
        },
        {
          icon: 'picfill',
          color: 'yellow',
          name: '图像'
        },
        {
          icon: 'noticefill',
          color: 'olive',
          badge: 22,
          name: '通知'
        },
        {
          icon: 'upstagefill',
          color: 'cyan',
          name: '排行榜'
        },
        {
          icon: 'clothesfill',
          color: 'blue',
          name: '皮肤'
        },
        {
          icon: 'discoverfill',
          color: 'purple',
          name: '发现'
        },
        {
          icon: 'questionfill',
          color: 'mauve',
          name: '帮助'
        },
        {
          icon: 'commandfill',
          color: 'purple',
          name: '问答'
        },
        {
          icon: 'brandfill',
          color: 'mauve',
          name: '版权'
        }
      ]
    }
  }
}
</script>