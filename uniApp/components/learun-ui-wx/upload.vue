<template>
  <view class="cu-form-group">
    <view class="grid col-4 grid-square flex-sub">
      <view class="bg-img" v-for="(item, index) in imgList" :key="index" :data-url="imgList[index]" @tap="ViewImage">
        <image :src="imgList[index]" mode="aspectFill"></image>
        <view v-if="!readonly" class="cu-tag bg-red" @tap.stop="DelImg(index)" :data-index="index">
          <l-icon type="close" style="width: 18px;height: 24px;font-size: 24px;" />
        </view>
      </view>

      <view class="solids" @tap="ChooseImage" v-if="!readonly && imgList.length < Number(number)">
        <l-icon type="cameraadd" />
      </view>
    </view>
  </view>
</template>

<script>
export default {
  name: 'l-upload',

  props: {
    number: { type: Number, default: 1 },
    readonly: { type: Boolean }
  },

  data() {
    return {
      imgList: []
    }
  },

  methods: {
    DelImg(index) {
      this.imgList.splice(index, 1)
      this.$emit('del')
      this.$emit('input', this.imgList)
    },

    ChooseImage() {
      uni.chooseImage({
        count: Number(this.number),
        sizeType: ['original', 'compressed'],
        sourceType: ['album', 'camera'],
        success: res => {
          if (this.imgList.length !== 0) {
            this.imgList = this.imgList.concat(res.tempFilePaths)
          } else {
            this.imgList = res.tempFilePaths
          }

          this.$emit('add')
          this.$emit('input', this.imgList)
        }
      })
    },

    ViewImage(e) {
      uni.previewImage({
        urls: this.imgList,
        current: e.currentTarget.dataset.url
      })
    }
  }
}
</script>
