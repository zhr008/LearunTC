<template>
  <view>
    <l-title>基本步骤条：</l-title>
    <view class="bg-white padding">
      <l-step
        :steps="[
          { title: '步骤1', icon: 'emoji' },
          { title: '步骤2', icon: 'home' },
          { title: '步骤3', icon: 'search' },
          { title: '步骤4', icon: 'check' }
        ]"
        :step="1"
      />

      <l-step
        class="margin-top-sm"
        activeColor="red"
        point
        :steps="[
          { title: '步骤1', icon: 'emoji' },
          { title: '步骤2', icon: 'home' },
          { title: '步骤3', icon: 'search' },
          { title: '步骤4', icon: 'check' }
        ]"
        :step="2"
      />
    </view>
    <wxdoc-desc>
      步骤条使用 l-step 标签；绑定 steps 属性作为步骤条项的显示内容，这是一个数组；
      steps 使用以下数据： [{title:'步骤标题',icon:'步骤图标'},...] ；
      属性 step 表示当前所处步骤号；属性 activeColor 表示激活时的颜色；添加属性 point 将把未激活的步骤显示为圆点
    </wxdoc-desc>

    <l-title>箭头步骤条：</l-title>
    <view class="bg-white padding">
      <l-step
        arrow
        :steps="[
          { title: '步骤1', icon: 'emoji' },
          { title: '步骤2', icon: 'home' },
          { title: '步骤3', icon: 'search' },
          { title: '步骤4', icon: 'check' }
        ]"
        :step="1"
      />
    </view>
    <wxdoc-desc>
      给 l-step 标签添加 arrow 属性即可使用箭头步骤条
    </wxdoc-desc>

    <l-title>数字步骤条：</l-title>
    <view class="bg-white padding">
      <l-step
        number
        arrow
        :steps="[
          { title: '步骤1', icon: 'emoji' },
          { title: '步骤2', icon: 'home' },
          { title: '步骤3', icon: 'search' },
          { title: '步骤4', icon: 'check' }
        ]"
        :step="1"
      />
    </view>
    <wxdoc-desc>
      给 l-step 标签添加 number 即可使用数字编号图标
    </wxdoc-desc>
  </view>
</template>
