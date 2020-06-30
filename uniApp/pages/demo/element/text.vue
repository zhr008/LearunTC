<template>
  <view>
    <l-title>文字大小：</l-title>
    <view class="bg-white padding-lr">
      <view
        class="solids-bottom padding-xs flex flex-direction justify-start align-center"
      >
        <view class="flex-sub text-center">
          <view class="solid-bottom text-xsl padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-xsl (60)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-sl padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-sl (40)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-xxl padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-xxl (22)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-xl padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-xl (18)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-lg padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-lg (16)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-df padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-df (14)</view>
        </view>
      </view>

      <view class="solids-bottom padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-sm padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-sm (12)</view>
        </view>
      </view>

      <view class="padding-xs flex align-center">
        <view class="flex-sub text-center">
          <view class="solid-bottom text-xs padding">
            <text class=" cuIcon-roundcheckfill text-green"></text>
            文字大小
          </view>
          <view class="padding-xs">text-xs (10)</view>
        </view>
      </view>
    </view>
    <wxdoc-desc>给文本所在的容器添加上面的类名(text-开头)即可实现设置字号</wxdoc-desc>

    <l-title>文字颜色：</l-title>
    <view class="grid col-5 padding-sm">
      <view class="padding-sm" v-for="(item, index) in colors" :key="index">
        <view
          class="text-center"
          :class="'text-' + item.name"
          :style="{ backgroundColor: index === colors.length - 1 ? '#555' : null }"
        >
          {{ item.title }} {{ item.name }}
        </view>
      </view>
    </view>
    <wxdoc-desc>
      给文本所在的容器添加 class="text-*"；
      把*替换成上面的颜色的英文名，即可适用这些字体颜色
    </wxdoc-desc>

    <l-title>文字阴影：</l-title>
    <view class="padding">
      <view class="text-xxl text-center text-shadow text-black">文字带阴影</view>
    </view>
    <wxdoc-desc>
      给文本所在的容器添加 class="text-shadow" 即可适用文字阴影；
      注意，只有在文本已设置了颜色时阴影才会生效，例如已经设置了 class="text-black"
    </wxdoc-desc>

    <l-title>文字截断：</l-title>
    <view class="padding bg-white flex flex-wrap justify-around">
      <view class="text-cut padding bg-grey radius" style="width:220px">
        床前明月光，疑是地上霜，举头望明月，低头思故乡
      </view>
    </view>
    <wxdoc-desc>
      给文本所在的容器添加 class="text-cut"；
      并且设置一个宽度值，即可自动截断文字，超出时显示成省略号
    </wxdoc-desc>

    <l-title>文字对齐：</l-title>
    <view class="padding bg-white">
      <view class="padding text-left">(left) 床前明月光，疑是地上霜</view>
      <view class="padding text-center">(center) 床前明月光，疑是地上霜</view>
      <view class="padding text-right">(right) 床前明月光，疑是地上霜</view>
    </view>
    <wxdoc-desc>
      给文本所在的容器添加 class="text-*"；
      把*替换成上述的对齐字符串(left/center/right)，即可设置文本的对齐方式
    </wxdoc-desc>

    <l-title>特殊文字：</l-title>
    <view class="padding text-center">
      <view class="padding-lr bg-white">
        <view class="solid-bottom padding"><text class="text-price">100.00</text></view>
        <view class="padding">(text-price) 价格文本，自动添加"¥"符号</view>
      </view>
      <view class="padding-lr bg-white margin-top">
        <view class="solid-bottom padding">
          <text class="text-Abc">Taylor Swift</text>
        </view>
        <view class="padding">(text-Abc) 英文单词首字母大写</view>
      </view>
      <view class="padding-lr bg-white margin-top">
        <view class="solid-bottom padding">
          <text class="text-ABC">Taylor Swift</text>
        </view>
        <view class="padding">(text-ABC) 全部字母大写</view>
      </view>
      <view class="padding-lr bg-white margin-top">
        <view class="solid-bottom padding">
          <text class="text-abc">Taylor Swift</text>
        </view>
        <view class="padding">(text-abc) 全部字母小写</view>
      </view>
    </view>
    <wxdoc-desc>给文本所在的元素添加上面的类名(text-开头)即可实现文字特殊字体</wxdoc-desc>
  </view>
</template>

<script>
import mixins from '@/components/learun-ui-wx/mixins.js'

export default {
  mixins: [mixins]
}
</script>
