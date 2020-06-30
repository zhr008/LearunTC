<template>
  <view>
    <view class="bg-grey padding margin text-center">当前页面索引号:{{ tab }}</view>

    <l-title>默认页签：</l-title>
    <l-nav :items="['首页', '收藏', '发现', '我的']" v-model="tab" />
    <wxdoc-desc>
      页签使用 l-nav 标签，给 item 属性绑定一个数组参数作为页签项；
      使用 v-model 属性绑定当前选中的页签索引
    </wxdoc-desc>

    <l-title>标签居中：</l-title>
    <l-nav type="center" :items="['首页', '收藏', '发现', '我的']" v-model="tab" />
    <wxdoc-desc>
      添加 type="center" 即可使用居中标签
    </wxdoc-desc>

    <l-title>标签平均排布：</l-title>
    <l-nav type="flex" :items="['首页', '收藏', '发现', '我的']" v-model="tab" />
    <wxdoc-desc>
      添加 type="flex" 即可使用平均排布的标签
    </wxdoc-desc>

    <l-title>标签背景色：</l-title>
    <l-nav color="blue" :items="['首页', '收藏', '发现', '我的']" v-model="tab" />
    <wxdoc-desc>
      添加 color 属性来设置背景色
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return {
      tab: 0
    }
  }
}
</script>