<template>
  <view>
    <l-title>普通弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="normal = true">显示</l-button>
      <l-modal v-model="normal" title="模态框标题">我是模态框内容</l-modal>
    </view>
    <wxdoc-desc>
      弹出窗口使用 l-modal 标签，将弹出窗口显示与否绑定为 v-model ，属性 title 可以设置弹出窗口的标题；
      标签内的默认插槽内容成为弹出窗口的内容，在名为 title 的插槽中的内容将渲染为标题；
      弹出窗口关闭时会触发 close 事件
    </wxdoc-desc>

    <l-title>底部弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="bottom = true">显示</l-button>
      <l-modal type="bottom" v-model="bottom" title="底部模态框标题">
        我是底部模态框内容
      </l-modal>
    </view>
    <wxdoc-desc>
      给 l-modal 标签添加属性 type="bottom" 即设为底部弹出窗口；
      底部窗口有一个名为 action 的插槽，该插槽的内容将渲染到底部弹出窗口的按钮区
    </wxdoc-desc>

    <l-title>对话弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="confirm = true">显示</l-button>
      <l-tag v-if="confirmValue" class="margin-left-sm" capsule round color="blue">
        <template #icon>
          选择了
        </template>
        {{ confirmValue }}
      </l-tag>
      <l-modal
        @ok="confirmValue = '确认'"
        @cancel="confirmValue = '取消'"
        type="confirm"
        v-model="confirm"
        title="对话窗口标题"
      >
        我是对话窗口的内容
      </l-modal>
    </view>
    <wxdoc-desc>
      给 l-modal 标签添加属性 type="confirm" 即可设为对话弹出窗口，对话弹出窗口有确认和取消按钮；
      点击确认按钮将触发 ok 事件，点击取消按钮将触发 cancel 事件
    </wxdoc-desc>

    <l-title>图片弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="img = true">显示</l-button>
      <l-modal type="img" v-model="img" img="https://img.learun.cn/wxcx/pic.jpg" />
    </view>
    <wxdoc-desc>
      给 l-modal 标签添加属性 type="img" 即可设为图片弹出窗口，设置属性 img 来设置图片的地址
    </wxdoc-desc>

    <l-title>单选弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="radio = true">显示</l-button>
      <l-tag class="margin-left-sm" capsule round color="blue">
        <template #icon>
          选择了
        </template>
        {{ radioValue }}
      </l-tag>
      <l-modal
        type="radio"
        v-model="radio"
        :radio.sync="radioValue"
        :range="['选项1', '选项2', '选项3']"
      />
    </view>
    <wxdoc-desc>
      给 l-modal 标签添加属性 type="radio" 即可设为单选弹出窗口；
      单选弹出窗口需要设置属性 range 表示单选项，设置属性 radio (请使用 .sync 双向绑定) 表示当前选中的项；
      单选弹出窗口有事件 radioChange 表示单选项发生改变
    </wxdoc-desc>

    <l-title>多选弹出窗口：</l-title>
    <view class="padding">
      <l-button slot="action" color="green" shadow @click="checkbox = true">
        显示
      </l-button>
      <l-tag class="margin-left-sm" capsule color="blue">
        <template #icon>
          选择了
        </template>
        {{ checkboxValue.join('，') }}
      </l-tag>
      <l-modal
        type="checkbox"
        v-model="checkbox"
        :checkbox.sync="checkboxValue"
        :range="['选项1', '选项2', '选项3', '选项4']"
        @checkboxChange="log"
      />
    </view>
    <wxdoc-desc>
      给 l-modal 标签设置属性 type="checkbox" 即可设为多选弹出窗口；
      多选弹出窗口需要设置属性 range 表示多选项，设置属性 checkbox (请使用 .sync 双向绑定) 表示当前选中的项；
      多选弹出窗口有事件 checkboxChange 表示选项发生改变，另有 ok、cancel 事件表示点击了确定、取消
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return {
      normal: false,
      bottom: false,
      confirm: false,
      img: false,
      radio: false,
      checkbox: false,

      confirmValue: null,
      radioValue: '选项1',
      checkboxValue: ['选项1']
    }
  },

  methods: {
    log(e) {
      console.log(e)
    }
  }
}
</script>
