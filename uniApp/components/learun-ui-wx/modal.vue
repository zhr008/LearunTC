<template>
  <view class="cu-modal" :class="[value ? 'show' : '', type === 'bottom' ? 'bottom-modal' : '']" @click="modalClick">
    <view v-if="type === 'img'" class="cu-dialog">
      <view class="bg-img" :style="{ backgroundImage: 'url(' + img + ')', height: '200px' }">
        <view class="cu-bar justify-end text-white">
          <view class="action" @tap="close"><l-icon type="close" /></view>
        </view>
      </view>
      <view class="cu-bar bg-white"><view class="action margin-0 flex-sub solid-left" @tap="close">关闭</view></view>
    </view>

    <view v-else-if="type === 'radio'" class="cu-dialog" @tap.stop="">
      <radio-group class="block" @change="radioChange">
        <view class="cu-list menu text-left">
          <view class="cu-item" v-for="(item, index) of range" :key="index">
            <label class="flex justify-between align-center flex-sub">
              <view class="flex-sub">{{ objectMode ? item.text : item }}</view>
              <radio
                class="round"
                :class="radioIndex === index ? 'checked' : ''"
                :checked="radioIndex === index ? true : false"
                :value="objectMode ? item.value : item"
              ></radio>
            </label>
          </view>
        </view>
      </radio-group>
    </view>

    <view v-else-if="type === 'checkbox'" class="cu-dialog" @tap.stop="">
      <view class="cu-bar bg-white">
        <view class="action text-blue" @tap="close(0)">取消</view>
        <view v-if="!readonly" class="action text-green" @tap="close(1)">确定</view>
      </view>
      <view class="grid col-3 padding-sm">
        <view v-for="(item, index) in range" class="padding-xs" :key="index">
          <button
            class="cu-btn orange lg block"
            :class="checkboxIndex.includes(index) ? 'bg-orange' : 'line-orange'"
            @tap="chooseCheckbox(index)"
          >
            {{ objectMode ? item.text : item }}
          </button>
        </view>
      </view>
    </view>

    <view v-else class="cu-dialog">
      <view class="cu-bar bg-white" :class="[type === 'bottom' ? '' : 'justify-end']">
        <template v-if="type === 'default' || type === 'confirm'">
          <view class="content">
            <slot name="title">{{ title }}</slot>
          </view>
          <view class="action" @tap="close"><l-icon type="close" color="red" /></view>
        </template>

        <template v-if="type === 'bottom'">
          <slot name="action">
            <l-button class="action" line="red" @click="close">取消</l-button>
            <view class="action padding-lr">{{ title }}</view>
          </slot>
        </template>
      </view>

      <view class="padding">
        <slot>{{ content }}</slot>
      </view>

      <template v-if="type === 'confirm'">
        <view class="cu-bar bg-white justify-end">
          <view class="action">
            <button class="cu-btn line-green text-green" @tap="close(0)">取消</button>
            <button class="cu-btn bg-green margin-left" @tap="close(1)">确定</button>
          </view>
        </view>
      </template>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-modal',

  props: {
    title: { type: [String, Number] },
    content: { type: [String, Number] },
    value: { type: null },
    type: { type: String, default: 'default' },
    img: { type: String },
    range: { type: Array },
    radio: { type: null },
    checkbox: { type: null },
    readonly: { type: Boolean }
  },

  data() {
    return {
      radioIndex: undefined,
      checkboxIndex: []
    }
  },

  created() {
    this.init()
  },

  methods: {
    init() {
      if (this.type === 'radio') {
        this.radioIndex = this.objectMode
          ? this.range.findIndex(t => t.value === this.radio)
          : this.range.indexOf(this.radio)
      } else if (this.type === 'checkbox' && Array.isArray(this.checkbox)) {
        this.checkboxIndex = this.checkbox.reduce(
          (a, b) => a.concat(this.objectMode ? this.range.findIndex(t => t.value === b) : this.range.indexOf(b)),
          []
        )
      }
    },

    close(arg) {
      this.$emit('input', false)
      this.$emit('close', false)

      if (typeof arg === 'number') {
        this.$emit(['cancel', 'ok'][arg])
      }
    },

    modalClick() {
      if (this.type === 'radio' || this.type === 'checkbox') {
        this.close()
      }
    },

    radioChange(e) {
      if (this.readonly) {
        return
      }

      this.radioIndex = this.objectMode
        ? this.range.findIndex(t => t.value === e.detail.value)
        : this.range.indexOf(e.detail.value)
      this.$emit('update:radio', this.objectMode ? this.range[this.radioIndex].value : this.range[this.radioIndex])
      this.$emit('radioChange', this.objectMode ? this.range[this.radioIndex].value : this.range[this.radioIndex])
    },

    chooseCheckbox(index) {
      if (this.readonly) {
        return
      }

      if (this.checkboxIndex.includes(index)) {
        this.checkboxIndex = this.checkboxIndex.filter(t => t !== index)
      } else {
        this.checkboxIndex.push(index)
      }

      const checkboxValue = this.checkboxIndex.reduce(
        (a, b) => a.concat(this.objectMode ? this.range[b].value : this.range[b]),
        []
      )

      this.$emit('update:checkbox', checkboxValue)
      this.$emit('checkboxChange', checkboxValue)
    }
  },

  watch: {
    value(newVal) {
      if (newVal) {
        this.init()
      }
    }
  },

  computed: {
    objectMode() {
      return typeof this.range[0] === 'object'
    }
  }
}
</script>
