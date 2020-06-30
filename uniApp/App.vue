<script>
import { guid } from '@/common/utils.js'

export default {
  async onShow() {
    await this.init()
  },

  methods: {
    async init() {
      if (!this.$store.state.guid) {
        this.$store.commit('guid', guid())
      }
    }
  }
}
</script>

<style lang="less">
@import url('./components/learun-ui-wx/style/index.css');

page {
  background-color: #f3f3f3;
}

/* #ifdef H5 */
uni-page-head {
  .uni-page-head__title {
    font-weight: 400;
  }
}
/* #endif */
</style>
