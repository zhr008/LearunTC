<template>
  <view>
    <l-title>时间轴示例：</l-title>
    <l-timeline title="昨天">
      <l-timeline-item time="22:15" color="green" icon="noticefill">
        快递已到达您的收件地址，您已完成签收，本次递送结束，感谢您的使用，祝您生活愉快！
      </l-timeline-item>
      <l-timeline-item time="17:59" color="blue" icon="attentionforbidfill">
        快递已经到达集散中心，正在又快递人员给您上门配送，请稍作等待。
      </l-timeline-item>
      <l-timeline-item time="09:46" color="red" icon="evaluate_fill">
        快递已出库，正在发往您所在城市的集散中心。
      </l-timeline-item>
    </l-timeline>
    <wxdoc-desc>
      时间轴使用 l-timeline 标签；添加 title 属性可以设置时间线标题
      时间轴中的项使用 l-timeline-item 标签；设置 color 属性可以设置项目的背景色；设置 icon 属性可以设置图标
      设置 time 属性可以设置项目发生时间；标签内的内容将渲染为时间轴子项的内容
    </wxdoc-desc>

    <l-title>定制内容时间轴：</l-title>
    <l-timeline>
      <template #title>
        <l-icon type="time" />
        前天
      </template>
      <l-timeline-item color="olive" icon="noticefill">
        <l-tag slot="time" radius capsule color="blue" class="margin-right-sm">
          <template #icon>
            晚上
          </template>
          10:25
        </l-tag>
        快递已到达您的收件地址，您已完成签收，本次递送结束，感谢您的使用，祝您生活愉快！
      </l-timeline-item>
      <l-timeline-item color="blue">
        <image src="https://img.learun.cn/wxcx/pic.jpg" mode="aspectFill"></image>
      </l-timeline-item>
    </l-timeline>
    <wxdoc-desc>
      时间轴子项内的 time 属性也可以用名为 time 的插槽代替；时间轴子项内容也可以不是纯文本
    </wxdoc-desc>
  </view>
</template>

