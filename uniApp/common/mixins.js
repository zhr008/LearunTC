import { config, apiRoot } from '@/common/env.js'

export default {
  methods: {
    getPageParam() {
      const result = this.$store.state.pageParam
      this.$store.commit('pageParam', null)

      return result
    },

    setPageParam(val) {
      this.$store.commit('pageParam', val)
    }
  },

  computed: {
    auth() {
      return { loginMark: this.$store.state.guid, token: this.$store.state.token }
    },

    config() {
      return config
    },

    apiRoot() {
      return apiRoot
    }
  }
}
