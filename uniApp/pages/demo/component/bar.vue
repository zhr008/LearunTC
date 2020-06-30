<template>
  <view>
    <l-title>底部操作条：</l-title>
    <view class="bg-gray">
      <l-bar>
        <l-bar-item title="首页" icon="home" color="red" />
        <l-bar-item title="分类" icon="searchlist" />
        <l-bar-item title="消息" icon="comment">
          <l-tag slot="badge" badge round color="red">5</l-tag>
        </l-bar-item>
        <l-bar-item title="我的" icon="people" />
      </l-bar>

      <l-bar dark class="margin-top-sm">
        <l-bar-item title="首页" icon="home" />
        <l-bar-item title="分类" icon="searchlist" color="green">
          <l-tag slot="badge" badge round color="orange">最新!</l-tag>
        </l-bar-item>
        <l-bar-item title="消息" icon="comment" />
        <l-bar-item title="我的" icon="people" />
      </l-bar>
    </view>
    <wxdoc-desc>
      底部操作条使用 l-bar 标签，添加 dark 属性可以改为深色操作条；
      内部按钮使用 l-bar-item 标签，添加 title 属性设置标题文字，添加 icon 属性设置按钮图标，添加 color 属性设置颜色；
      按钮角标 l-tag 可以放置于 l-bar-item 标签中的默认插槽内；
      按钮标题文字可以放置于 l-bar-item 标签中的名为 title 的插槽内
    </wxdoc-desc>

    <l-title>带圆形突出按钮：</l-title>
    <view class="bg-gray">
      <l-bar>
        <l-bar-item title="首页" icon="home" />
        <l-bar-item title="分类" icon="searchlist" />
        <l-bar-item type="round" title="发布" icon="add" />
        <l-bar-item title="消息" icon="comment" />
        <l-bar-item title="我的" icon="people" />
      </l-bar>

      <l-bar class="margin-top-xl" dark>
        <l-bar-item title="首页" icon="home" />
        <l-bar-item title="分类" icon="searchlist" />
        <l-bar-item type="round" title="发布" icon="add" />
        <l-bar-item title="消息" icon="comment" />
        <l-bar-item title="我的" icon="people" />
      </l-bar>
    </view>
    <wxdoc-desc>
      在 l-bar-item 标签上添加 type="round" 属性即可适用圆形突出按钮
    </wxdoc-desc>

    <l-title>带大块按钮：</l-title>
    <view class="bg-gray">
      <l-bar>
        <l-bar-item title="客服" icon="service" />
        <l-bar-item title="收藏" icon="favor" />
        <l-bar-item title="购物车" icon="cart" />
        <l-bar-item-button title="立即购买" color="red" />
      </l-bar>

      <l-bar class="margin-top-xl">
        <l-bar-item title="收藏" icon="favor" />
        <l-bar-item title="购物车" icon="cart" />
        <l-bar-item-button title="加入购物车" color="orange" />
        <l-bar-item-button title="立即购买" color="red" />
      </l-bar>
    </view>
    <wxdoc-desc>
      大块按钮需要使用 l-bar-item-button 标签，可以设置 title、color 属性
    </wxdoc-desc>

    <l-title>标题栏：</l-title>
    <view class="bg-gray">
      <l-title>默认标题</l-title>
      <l-title long>长圆点标题</l-title>
      <l-title type="underline">下划线标题</l-title>
      <l-title type="colorfulUnderline" color="blue">变色下划线标题</l-title>
      <l-title type="blurUnderline" color="green">扩散下划线标题</l-title>
      <l-title type="nextSubtitle" color="blue" subtitle="subtitle">带副标题标题文字</l-title>
      <l-title type="blurSubtitle" subtitle="subtitle">带扩散副标题的标题文字</l-title>
    </view>
    <wxdoc-desc>
      标题栏使用 l-title 标签；添加 long 属性即设为长圆点标题；color 属性可以设置颜色；
      设置 type 属性为 underline 或 blurUnderline 可以设为带有下划线或扩散下划线的样式；
      设置 type 属性为 colorfulUnderline 可以设为带有变色下划线样式，注意变色下划线的颜色无法更改；
      设置 type 属性为 nextSubtitle 或 blurSubtitle 可以设为带有副标题或者扩散副标题的样式；
      副标题通过名为 subtitle 的属性或者插槽来设置
    </wxdoc-desc>

    <l-title>顶部操作栏：</l-title>
    <view class="bg-gray">
      <l-banner>基础操作条</l-banner>

      <l-banner color="black">
        返回操作条
        <template #left>
          <l-icon type="back" />
          返回
        </template>
      </l-banner>

      <l-banner color="blue">
        复杂操作条
        <template #left>
          <l-icon type="homefill" />
          首页
        </template>
        <template #right>
          <l-avatar src="https://img.learun.cn/wxcx/pic.jpg" round />
        </template>
      </l-banner>
    </view>
    <wxdoc-desc>
      顶部操作条使用 l-banner 标签；名为 left、right 的插槽内容将渲染到操作条左侧、右侧的位置；
      可以设置 color 属性来修改操作条的背景色；默认插槽中的内容将渲染到操作条的中间
    </wxdoc-desc>

    <l-title>搜索操作栏：</l-title>
    <view class="bg-gray">
      <l-banner v-model="searchText" type="search" />
      <l-banner type="search" color="cyan" fill />
      <view class="margin-top padding">当前搜索框:{{ searchText }}</view>
    </view>
    <wxdoc-desc>
      给 l-banner 添加属性 type="search" 即可设为搜索操作条；添加 fill 属性即可设为填充型样式；
      名为 searchInput、searchButton 的插槽将渲染到搜索输入框、搜索按钮上；搜索框输入内容将绑定 v-model 上
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return { searchText: '' }
  }
}
</script>

<style scoped>
l-title,
l-banner {
  margin-top: 20upx;
}
</style>
