<template>
  <view>
    <l-title>百分比宽度：</l-title>
    <view class="padding bg-white">
      <view class="flex flex-wrap">
        <view class="basis-xs bg-grey margin-xs padding-sm radius">xs(20%)</view>
        <view class="basis-df"></view>
        <view class="basis-sm bg-grey margin-xs padding-sm radius">sm(40%)</view>
        <view class="basis-df"></view>
        <view class="basis-df bg-grey margin-xs padding-sm radius">df(50%)</view>
        <view class="basis-lg bg-grey margin-xs padding-sm radius">lg(60%)</view>
        <view class="basis-xl bg-grey margin-xs padding-sm radius">xl(80%)</view>
      </view>
    </view>
    <wxdoc-desc>
      外部容器添加 class="flex flex-wrap"； 内部元素添加 class="basis-*"；
      把*换成上述的5个字符串 (xs/sm/df/lg/xl) 之一
    </wxdoc-desc>

    <l-title>比例宽度：</l-title>
    <view class="padding bg-white">
      <view class="flex">
        <view class="flex-sub bg-grey padding-sm margin-xs radius">sub (1)</view>
        <view class="flex-sub bg-grey padding-sm margin-xs radius">sub (1)</view>
      </view>
      <view class="flex">
        <view class="flex-sub bg-grey padding-sm margin-xs radius">sub (1)</view>
        <view class="flex-twice bg-grey padding-sm margin-xs radius">twice (2)</view>
      </view>
      <view class="flex">
        <view class="flex-sub bg-grey padding-sm margin-xs radius">sub</view>
        <view class="flex-twice bg-grey padding-sm margin-xs radius">twice (2)</view>
        <view class="flex-treble bg-grey padding-sm margin-xs radius">treble (3)</view>
      </view>
    </view>
    <wxdoc-desc>
      外部容器添加 class="flex"，内部元素添加 class="flex-*"；
      把*换成上述的3个字符串(sub/twice/treble) 之一
    </wxdoc-desc>

    <l-title>水平对齐：</l-title>
    <view class="bg-white padding">
      <view class="flex solid-bottom padding justify-start">
        <view class="bg-grey padding-sm margin-xs radius">start</view>
        <view class="bg-grey padding-sm margin-xs radius">start</view>
      </view>
      <view class="flex solid-bottom padding justify-end">
        <view class="bg-grey padding-sm margin-xs radius">end</view>
        <view class="bg-grey padding-sm margin-xs radius">end</view>
      </view>
      <view class="flex solid-bottom padding justify-center">
        <view class="bg-grey padding-sm margin-xs radius">center</view>
        <view class="bg-grey padding-sm margin-xs radius">center</view>
      </view>
      <view class="flex solid-bottom padding justify-between">
        <view class="bg-grey padding-sm margin-xs radius">between</view>
        <view class="bg-grey padding-sm margin-xs radius">between</view>
      </view>
      <view class="flex solid-bottom padding justify-around">
        <view class="bg-grey padding-sm margin-xs radius">around</view>
        <view class="bg-grey padding-sm margin-xs radius">around</view>
      </view>
    </view>
    <wxdoc-desc>
      外部容器添加 class="flex justify-*"；
      把*换成上述的字符串(start/end/center/between/around) 之一
    </wxdoc-desc>

    <l-title>垂直对齐：</l-title>
    <view class="bg-white padding">
      <view class="flex solid-bottom padding align-start">
        <view class="bg-grey padding-lg margin-xs radius" style="height: 160rpx;"></view>
        <view class="bg-grey padding-sm margin-xs radius">start (顶部)</view>
      </view>
      <view class="flex solid-bottom padding align-end">
        <view class="bg-grey padding-lg margin-xs radius" style="height: 160rpx;"></view>
        <view class="bg-grey padding-sm margin-xs radius">end (底部)</view>
      </view>
      <view class="flex solid-bottom padding align-center">
        <view class="bg-grey padding-lg margin-xs radius" style="height: 160rpx;"></view>
        <view class="bg-grey padding-sm margin-xs radius">center (居中)</view>
      </view>
    </view>
    <wxdoc-desc>
      外部容器添加 class="flex align-*"； 把*换成上述的字符串(start/end/center)之一
    </wxdoc-desc>

    <l-title>等分列网格：</l-title>
    <view class="bg-white padding">
      <view
        class="grid margin-bottom text-center"
        v-for="(item, index) in 5"
        :key="index"
        :class="'col-' + (index + 1)"
      >
        <view
          class="padding"
          :class="indexs % 2 == 0 ? 'bg-cyan' : 'bg-blue'"
          v-for="(item, indexs) in (index + 1) * 2"
          :key="indexs"
        >
          {{ indexs + 1 }}
        </view>
      </view>
    </view>
    <wxdoc-desc>外部容器添加 class="grid col-*"，把*换成列数数字</wxdoc-desc>
  </view>
</template>

