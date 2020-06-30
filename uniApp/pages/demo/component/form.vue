<template>
  <view>
    <l-title>输入框：</l-title>
    <view class="bg-white">
      <form>
        <l-input title="标题" placeholder="请输入内容" />
        <l-input title="禁用" disabled placeholder="请输入内容(禁用)" />
        <l-input title="标题" placeholder="请输入内容(带后缀图标)">
          <l-icon slot="suffix" type="copy" color="red" />
        </l-input>
        <l-input title="标题" placeholder="请输入内容(带后缀按钮)">
          <l-button slot="suffix" color="green" shadow>按钮</l-button>
        </l-input>
        <l-input title="标题" placeholder="请输入内容(带后缀标签)">
          <l-tag slot="suffix" capsule radius color="blue">
            <template #icon>
              中国大陆
            </template>
            +86
          </l-tag>
        </l-input>
        <l-input title="请输入" v-model="text" placeholder="双向绑定测试" />
        <view class="text-center">当前输入内容: {{ text }}</view>
      </form>
    </view>
    <wxdoc-desc>
      输入框使用 l-input 标签；添加 title属性可以设置左侧的标题文字；添加 placeholder 属性可以设置无内容的占位文本； 
      标签内的 slot="suffix" 插槽可以放置后缀按钮、标签等
    </wxdoc-desc>

    <l-title>选择器：</l-title>
    <view class="bg-white">
      <l-select
        :range="['男生', '女生']"
        v-model="select1"
        title="选择器"
        placeholder="请选择性别..."
      />
      <view class="text-center">当前选择: {{ select1 }}</view>
      
      <l-select
        multiple
        :range="[['安卓', 'iOS', 'WP'], ['QQ登录', '微信登录']]"
        v-model="select2"
        title="双列选择器"
      />
      <view class="text-center">当前选择: {{ select2.join('，') }}</view>

      <l-select
        multiple
        :range="[['红色', '绿色', '蓝色'], ['纯棉', '化纤', '混纺'], ['M', 'L', 'XL']]"
        v-model="select3"
        title="三列选择器"
      />
      <view class="text-center">当前选择: {{ select3.join('，') }}</view>
    </view>
    <wxdoc-desc>
      选择器使用 l-select 标签；如果是两列、三列选择器，还需要额外添加 multiple 属性；title 属性是标题文字； 
      可选项请绑定为 range 属性，当前选定的值通过 v-model 绑定；
      注意多列选择器的 range 是一个双层嵌套数组，而 v-model 是一个数组 
    </wxdoc-desc>

    <l-title>日期/时间选择：</l-title>
    <view class="bg-white">
      <l-date-picker v-model="datePicker" title="日期" placeholder="请选择日期..." />
      <view class="text-center">当前已选日期: {{ datePicker }}</view>

      <l-time-picker v-model="timePicker" title="时间" placeholder="请选择时间..." />
      <view class="text-center">当前已选时间: {{ timePicker }}</view>
    </view>
    <wxdoc-desc>
      日期选择器使用 l-date-picker 标签；时间选择器使用 l-time-picker 标签；
      两者都可以使用 title、placeholder 属性来设置标题、占位文字； 两者都有 start、end
      属性来设置选择的起点和终点，注意是字符串格式
    </wxdoc-desc>

    <l-title>地区选择：</l-title>
    <view class="bg-white">
      <l-region-picker v-model="regionPicker" title="家乡" placeholder="请选择地区..." />
      <view class="text-center">
        当前已选地区: {{ regionPicker.join('，') }}
      </view>
    </view>
    <wxdoc-desc>地区选择器使用 l-region-picker 标签</wxdoc-desc>

    <l-title>开关：</l-title>
    <view class="bg-white">
      <l-switch title="普通开关" />
      <l-switch title="自定义颜色" color="red" />
      <l-switch title="自定义图标" icon="sex" />
      <l-switch title="方形开关" radius v-model="switchValue" />
      <view class="text-center">当前开关状态: {{ switchValue }}</view>
    </view>
    <wxdoc-desc>
      开关使用 l-switch 标签；添加 title 属性可以设置标题文本；添加 color 属性可以设置颜色； 
      添加 radius 属性将变成圆角方形开关；添加 icon="sex" 属性将变为性别选择开关
    </wxdoc-desc>

    <l-title>单选框：</l-title>
    <view class="bg-white">
      <radio-group>
        <l-radio title="单选框1" v-model="radioValue" radioValue="1" />
        <l-radio title="单选框2" v-model="radioValue" radioValue="2" />
        <l-radio title="点状单选框3" v-model="radioValue" point radioValue="3" />
        <l-radio
          title="自定义颜色单选框4"
          v-model="radioValue"
          color="blue"
          radioValue="4"
        />
        <view class="text-center">当前单选框状态: {{ radioValue }}</view>
      </radio-group>
    </view>
    <wxdoc-desc>
      单选框使用 l-radio 标签，通过 title 属性绑定标题文字，通过 radioValue 属性绑定值；
      同一组单选框，请绑定同一个变量作为它们的 v-model，因此无需使用 radio-group；
      使用 color 属性设置颜色；添加 point 属性将单选框转为点状风格
    </wxdoc-desc>

    <l-title>复选框：</l-title>
    <view class="bg-white">
      <checkbox-group>
        <l-checkbox title="复选框1" v-model="checkboxValue" checkboxValue="A" />
        <l-checkbox title="复选框2" v-model="checkboxValue" checkboxValue="B" />
        <l-checkbox title="圆形复选框3" v-model="checkboxValue" round checkboxValue="C" />
        <l-checkbox
          title="自定义颜色复选框3"
          v-model="checkboxValue"
          color="blue"
          checkboxValue="D"
        />
        <view class="text-center">
          当前复选框状态: [
          <text :key="index" v-for="(item, index) of checkboxValue">{{ item }}</text>
          ]
        </view>
      </checkbox-group>
    </view>
    <wxdoc-desc>
      复选框使用 l-checkbox 标签，同单选框，它使用 checkboxValue 来绑定值，请将同一组复选框绑定相同的变量作为 v-model； 
      使用 title 属性来设置标题文本；使用 color 属性设置复选框颜色；添加 round 属性转为圆形复选框
    </wxdoc-desc>

    <l-title>图片上传：</l-title>
    <view class="bg-white">
      <l-upload v-model="imgList" number="3" />
      <view class="text-center">当前图片数量: {{ imgList.length }}</view>
    </view>
    <wxdoc-desc>图片上传使用 l-upload 标签；属性 number 表示图片张数上限</wxdoc-desc>

    <l-title>多行文本：</l-title>
    <view class="bg-white">
      <l-textarea placeholder="输入点什么..." v-model="textareaValue" />
      <view class="text-center">当前输入内容: {{ textareaValue }}</view>
    </view>
    <wxdoc-desc>
      多行文本使用 l-textarea 标签；使用 placeholder 属性来设置占位文本
    </wxdoc-desc>
  </view>
</template>

<script>
export default {
  data() {
    return {
      text: '',
      select1: '男生',
      select2: ['安卓', 'QQ登录'],
      select3: ['红色', '纯棉', 'M'],

      datePicker: null,
      timePicker: null,
      regionPicker: [],

      switchValue: false,

      radioValue: '1',
      checkboxValue: [],
      imgList: [],
      textareaValue: ''
    }
  }
}
</script>
