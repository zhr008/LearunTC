<template>
  <view>
    <l-title>普通聊天记录：</l-title>
    <l-chat>
      <l-chat-msg avatar="https://img.learun.cn/wxcx/pic.jpg" date="2019-12-12">我是一条对方发来的消息</l-chat-msg>
      <wxdoc-desc>
        聊天记录需要使用 l-chat 标签包裹，聊天记录使用 l-chat-msg 标签，标签内容即为聊天文字 属性 avatar 设置头像；
        属性 date 或同名插槽设置日期时间；属性 type 可以取值 left/right 表示聊天记录是左侧还是右侧，默认在左
      </wxdoc-desc>
      <l-chat-msg type="right" date="6月1日" avatar="https://img.learun.cn/wxcx/pic.jpg">
        我是一条自己发出的消息
      </l-chat-msg>
      <l-chat-msg date="18:00" leftColor="yellow" avatar="https://img.learun.cn/wxcx/pic.jpg">
        我是特殊颜色的消息
      </l-chat-msg>
      <l-chat-msg date="19:25" roundAvatar avatar="https://img.learun.cn/wxcx/pic.jpg">我的头像是圆的</l-chat-msg>
      <wxdoc-desc>
        属性leftColor/rightColor 设置在左/右侧显示时的聊天记录的背景色；属性 roundAvatar 可以设置为圆形头像
      </wxdoc-desc>
    </l-chat>

    <wxdoc-desc>
      使用 l-chat-input 作为输入框，输入框的内容使用 v-model 绑定，属性 placeholder 设置占位符；
      点击发送按钮后，会触发 sendMsg 事件，可以监听该事件以获取输入内容；
      使用输入框时请为页面添加一条样式： page { padding-bottom: 100rpx; } 
    </wxdoc-desc>
    <l-chat-input placeholder="这里输入消息内容" v-model="msgInput" />
  </view>
</template>

<script>
export default {
  data() {
    return {
      msgInput: ''
    }
  }
}
</script>

<style lang="less">
page {
  padding-bottom: 100rpx;
}
</style>
