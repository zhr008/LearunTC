<template>
  <view class="page">
    <l-custom-form v-if="ready" ref="form" :editMode="editMode" :scheme="scheme" :initFormValue="current" />

    <view v-if="ready" class="padding bg-white margin-tb">
      <l-button v-if="editMode" @click="action('save')" class="block" size="lg" color="green" block>提交保存</l-button>
      <l-button v-else @click="action('edit')" class="block" size="lg" line="orange" block>编辑本页</l-button>
      <l-button
        v-if="mode !== 'create' && editMode"
        @click="action('reset')"
        class="block margin-top"
        size="lg"
        line="red"
        block
      >
        取消编辑
      </l-button>
      <l-button
        v-if="mode !== 'create' && !editMode"
        @click="action('delete')"
        class="block margin-top"
        size="lg"
        line="red"
        block
      >
        删除
      </l-button>
    </view>
  </view>
</template>

<script>
import { copy } from '@/common/utils.js'
import customAppFormMixins from '@/common/custom-app-form.js'
import LCustomForm from '@/components/learun-app/custom-form.vue'

export default {
  data() {
    return {
      id: '',
      schemeId: '',
      mode: '',
      editMode: false,
      ready: false,

      scheme: [],
      current: {},
      origin: {}
    }
  },

  components: { LCustomForm },

  mixins: [customAppFormMixins],

  async onLoad({ type, id }) {
    await this.init(type, id)
  },

  methods: {
    async init(type, id) {
      uni.showLoading({ title: `加载数据中...`, mask: true })

      const schemeData = this.getPageParam()
      this.schemeId = schemeData.F_SchemeInfoId
      this.id = id
      this.mode = type
      this.editMode = ['create', 'edit'].includes(this.mode)

      const formData = this.mode !== 'create' ? await this.fetchFormData(this.schemeId, this.id) : null
      const keyValue = this.mode !== 'create' ? this.id : null
      const { formValue, scheme } = await this.getCustomAppForm({ schemeData, formData, keyValue })
      this.scheme = scheme
      this.origin = formValue
      this.current = copy(this.origin)

      this.ready = true
      uni.hideLoading()
    },

    async action(type) {
      switch (type) {
        case 'edit':
          this.editMode = true
          return

        case 'reset':
          this.editMode = false
          this.current = copy(this.origin)
          this.$refs.form.resetFormValue()
          return

        case 'save':
          const verifyResult = this.$refs.form.verifyValue()
          if (verifyResult.length > 0) {
            uni.showModal({ title: '表单验证失败', content: verifyResult.join('\n'), showCancel: false })
            return
          }

          uni.showModal({
            title: '提交确认',
            content: `确定要提交本页表单内容吗？`,
            success: async ({ confirm }) => {
              if (confirm) {
                uni.showLoading({ title: '正在提交...', mask: true })
                const formValue = this.$refs.form.getFormValue()
                const postData = JSON.stringify(await this.getPostData(formValue, this.scheme))

                uni
                  .request({
                    url: this.apiRoot + `/form/save`,
                    method: 'POST',
                    header: { 'content-type': 'application/x-www-form-urlencoded' },
                    data: { ...this.auth, data: postData }
                  })
                  .then(([err, result]) => {
                    uni.hideLoading()
                    if (err || result.data.code !== 200) {
                      uni.showToast({ title: `表单提交保存失败`, icon: 'none' })
                      return
                    }

                    uni.$emit('custom-list-change')
                    this.origin = copy(this.current)
                    this.$refs.form.resetFormValue()
                    this.mode = 'view'
                    this.editMode = false
                    uni.showToast({ title: `提交保存成功`, icon: 'success' })
                  })
              }
            }
          })

          return

        case 'delete':
          uni.showModal({
            title: '删除项目',
            content: `确定要删除本项吗？`,
            success: ({ confirm }) => {
              if (!confirm) {
                return
              }

              uni
                .request({
                  url: this.apiRoot + `/form/delete`,
                  method: 'POST',
                  header: { 'content-type': 'application/x-www-form-urlencoded' },
                  data: { ...this.auth, data: JSON.stringify({ schemeInfoId: this.schemeId, keyValue: this.id }) }
                })
                .then(([err, { data }]) => {
                  if (err || !data || data.code !== 200) {
                    uni.showToast({ title: '删除失败', icon: 'none' })
                    return
                  }

                  uni.$emit('custom-list-change')
                  uni.navigateBack()
                  uni.showToast({ title: '删除成功', icon: 'success' })
                })
            }
          })
          return

        default:
          return
      }
    }
  }
}
</script>
