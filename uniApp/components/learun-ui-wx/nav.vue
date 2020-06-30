<template>
  <scroll-view
    scroll-x
    class="nav"
    :class="['bg-' + color, type === 'center' ? 'text-center' : '']"
    scroll-with-animation
    :scroll-left="scrollLeft"
  >
    <template v-if="type !== 'flex'">
      <view
        class="cu-item"
        v-for="(item, idx) in items"
        :class="value === idx ? 'text-green cur' : ''"
        :key="idx"
        @tap="tabSelect(idx)"
      >
        {{ item }}
      </view>
    </template>

    <view v-else class="flex text-center">
      <view
        class="cu-item flex-sub"
        v-for="(item, idx) in items"
        :class="[value === idx ? (color ? 'bg-white cur' : 'bg-green cur') : '']"
        :key="idx"
        @tap="tabSelect(idx)"
      >
        {{ item }}
      </view>
    </view>
  </scroll-view>
</template>

<script>
export default {
  name: 'l-nav',

  props: {
    color: { type: String, default: 'white' },
    items: { type: Array, default: () => [] },
    value: { type: Number },
    type: { type: String, default: 'default' }
  },

  data() {
    return {
      scrollLeft: 0
    }
  },

  methods: {
    tabSelect(idx) {
      this.scrollLeft = (idx - 1) * 60

      this.$emit('input', idx)
      this.$emit('change', idx)
    }
  }
}
</script>
