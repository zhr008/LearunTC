<template>
  <view>
    <l-title>本页所有卡片样式切换开关：</l-title>
    <view class="bg-gray"><l-switch title="是否是轮廓卡片" v-model="outline" /></view>
    <wxdoc-desc>卡片使用 l-card 标签；属性 outline 设置是否显示轮廓</wxdoc-desc>

    <l-title>案例类卡片：</l-title>
    <view class="bg-gray">
      <l-card
        type="case"
        :outline="outline"
        :info="{
          title: '我是卡片标题',
          badge: '最新',
          badgeColor: 'blue',
          img: 'https://img.learun.cn/wxcx/pic.jpg',
          avatar: 'https://img.learun.cn/wxcx/pic.jpg',
          user: '我是发布用户',
          tips: '10天前'
        }"
      >
        <template #footer>
          <view class="text-gray text-sm">
            <l-icon type="attentionfill" class="margin-lr-xs" />
            10
            <l-icon type="appreciatefill" class="margin-lr-xs" />
            20
            <l-icon type="messagefill" class="margin-lr-xs" />
            30
          </view>
        </template>
      </l-card>
    </view>
    <wxdoc-desc>
      添加属性 type="case" 来使用案例类卡片；卡片绑定一个 info 对象属性，通过该对象提供显示参数；
      info对象的属性：title、badge、badgeColor、img、avatar、user、tips；
      这些属性依次对应：标题、角标、角标颜色、卡片大图、头像、用户名栏、日期或提示栏；
      其中 img、badge、title、avatar、user、tips 的内容可以使用同名插槽来代替；
      还有一个 footer 插槽用于渲染卡片右下角的内容
    </wxdoc-desc>

    <l-title>动态类卡片：</l-title>
    <view class="bg-gray">
      <l-card
        type="dynamic"
        :outline="outline"
        :info="{
          avatar: 'https://img.learun.cn/wxcx/pic.jpg',
          user: '我是发布用户',
          tips: '10天前'
        }"
      >
        <view class="text-content">我是卡片内容正文</view>
        <view class="grid flex-sub padding-lr col-1">
          <view
            class="bg-img only-img"
            style="background-image:url(https://img.learun.cn/wxcx/pic.jpg);"
          ></view>
        </view>
        <template #footer>
          <view class="text-gray text-sm text-right padding">
            <l-icon type="attentionfill" class="margin-lr-xs" />
            10
            <l-icon type="appreciatefill" class="margin-lr-xs" />
            20
            <l-icon type="messagefill" class="margin-lr-xs" />
            30
          </view>
        </template>
      </l-card>
    </view>
    <wxdoc-desc>
      添加属性 type="dynamic" 来使用案例类卡片；卡片绑定一个 info 对象属性，通过该对象提供显示参数；
      info对象的属性：avatar、user、tips；这些属性依次对应：头像、用户名栏、日期或提示栏；
      其中 avatar、user、tips 的内容可以使用同名插槽来代替；
      默认插槽中的内容将渲染为卡片主体内容；footer 插槽中的内容渲染到卡片最底部
    </wxdoc-desc>

    <l-title>文章类卡片：</l-title>
    <view class="bg-gray">
      <l-card
        type="article"
        :outline="outline"
        :info="{
          title: '我是卡片标题',
          img: 'https://img.learun.cn/wxcx/pic.jpg'
        }"
      >
        卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文，卡片内容正文
        <template #footer>
          <view>
            <l-tag color="red" light round size="sm">标签1</l-tag>
            <l-tag color="green" light round size="sm" class="margin-left-xs">
              标签2
            </l-tag>
          </view>
        </template>
      </l-card>
    </view>
    <wxdoc-desc>
      添加属性 type="article" 来使用文章类卡片；卡片绑定一个 info 对象属性，通过该对象提供显示参数；
      info对象的属性：title、img；这些属性依次对应：标题、卡片大图；
      其中 title、img 的内容可以使用同名插槽来代替；
      默认插槽的内容将渲染到卡片内容文字区域；还有一个 footer 插槽用于渲染卡片右下角的内容
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return {
      outline: false
    }
  }
}
</script>
