import _ from 'lodash'
import customCommon from './custom-common.js'

// 用于 custom-app 自定义应用的表单页
// 注意: 不是代码生成器
export default {
  mixins: [customCommon],

  methods: {
    // 生成自定义表单相关数据结构
    // 参数: schemeData (必填), formData, useDefault
    // 返回: scheme, formValue
    async getCustomAppForm(prop) {
      const { schemeData, formData, keyValue, useDefault } = prop
      const schemeInfoId = schemeData.schemeInfoId

      const scheme = []
      const formValue = { schemeInfoId, formData: {} }
      if (keyValue) {
        formValue.keyValue = keyValue
      }

      for (let dataIndex = 0; dataIndex < schemeData.F_Scheme.data.length; ++dataIndex) {
        const { componts } = schemeData.F_Scheme.data[dataIndex]
        for (const t of componts) {
          // 之后的 t 即表示每个 scheme 项
          t.__valuePath__ = `formData.${t.id}`

          if (t.type === 'girdtable' && t.table) {
            // 数据项是表格的情况
            // 先设置源数据,不然无法获取默认值
            for (const fieldItem of t.fieldsData) {
              fieldItem.__sourceData__ = await this.getSourceData(fieldItem)
            }
            t.__defaultItem__ = await this.getDefaultData(t, prop)

            if (formData) {
              // 有表单值的情况,从表单值中获取数据
              const val = _.get(formData, `${schemeInfoId}.${t.table}`, [])
                .map((valueItem, valueIndex) => {
                  const tableItemValue = {}
                  for (const fieldItem of t.fieldsData.filter(t => t.field)) {
                    const formDataValue = _.get(valueItem, fieldItem.field.toLowerCase())
                    tableItemValue[fieldItem.field] = this.convertToFormValue(fieldItem, formDataValue)
                  }

                  return tableItemValue
                })

              // useDefault 表示在从 formData 取不到值的时候使用默认值
              if ((!val || val.length <= 0) && useDefault) {
                _.set(formValue, t.__valuePath__, [_.clone(t.__defaultItem__)])
              } else {
                _.set(formValue, t.__valuePath__, val)
              }

            } else {
              // 无表单值的情况,默认值
              _.set(formValue, t.__valuePath__, [_.clone(t.__defaultItem__)])
            }

          } else if (t.field) {
            // 数据项不是表格的情况
            // 先设置源数据,不然无法获取默认值
            t.__sourceData__ = await this.getSourceData(t)
            if (formData) {
              // 有表单值的情况,从表单值中获取数据
              const path = `${schemeInfoId}.${t.table}.${dataIndex}.${t.field.toLowerCase()}`
              const formDataValue = _.get(formData, path)

              // useDefault 表示在从 formData 取不到值的时候使用默认值
              if (!formDataValue && useDefault) {
                _.set(formValue, t.__valuePath__, await this.getDefaultData(t, prop))
              } else {
                _.set(formValue, t.__valuePath__, this.convertToFormValue(t, formDataValue))
              }

            } else {
              // 无表单值的情况,默认值
              _.set(formValue, t.__valuePath__, await this.getDefaultData(t, prop))
            }
          }

          scheme.push(t)
        }
      }

      return { formValue, scheme }
    },

    // 获取最终需要POST的数据
    // 参数: formValue , scheme
    async getPostData(originFormValue, scheme) {
      const formValue = JSON.parse(JSON.stringify(originFormValue))

      // 依次按照 scheme 项目遍历
      for (const item of scheme) {
        if (item.field) {
          // 不是表格的情况
          const path = item.__valuePath__
          const val = _.get(formValue, path)
          const result = await this.convertToPostData(item, val, originFormValue, scheme)
          _.set(formValue, path, result)

        } else if (item.table && item.fieldsData) {
          // 是表格的情况
          const tableValue = _.get(formValue, item.__valuePath__, [])
          for (let valueIndex = 0; valueIndex < tableValue.length; ++valueIndex) {
            for (const schemeItem of item.fieldsData) {
              const path = `${item.__valuePath__}.${valueIndex}.${schemeItem.field}`
              const val = _.get(formValue, path)
              const result = await this.convertToPostData(schemeItem, val, originFormValue, scheme)
              _.set(formValue, path, result)
            }
          }
        }
      }

      formValue.formData = JSON.stringify(formValue.formData)

      return [formValue]
    },

    // 获取表单的 formData
    // 参数: schemeInfoId , keyValue
    async fetchFormData(schemeInfoId, keyValue) {
      const [err, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot + `/form/data`,
        data: { ...this.auth, data: JSON.stringify([{ schemeInfoId, keyValue }]) }
      })

      return result
    }
  }
}
