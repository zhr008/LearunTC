import Vue from 'vue'
import Vuex, { Store } from 'vuex'

Vue.use(Vuex)

export default new Store({
  state: {
    user: null,
    guid: null,
    token: null,

    company: null,
    dep: null,
    staff: null,

    propTable: null,

    pageParam: null
  },

  mutations: {
    logout(state) {
      state.user = null
      state.token = null
      state.company = null
      state.dep = null
      state.staff = null
      state.propTable = null
      state.pageParam = null
    },

    user(state, val) { state.user = val },
    guid(state, val) { state.guid = val },
    token(state, val) { state.token = val },

    company(state, val) { state.company = val },
    dep(state, val) { state.dep = val },
    staff(state, val) { state.staff = val },

    propTable(state, val) { state.propTable = val },

    pageParam(state, val) { state.pageParam = val }
  },

  actions: {}
})
