import _ from 'lodash'
import customCommon from './custom-common.js'

// 用于工作流相关的表单和流程处理
export default {
  mixins: [customCommon],

  methods: {
    // 生成自定义表单相关数据结构
    // 参数: schemeData (必填), processId, currentNode, formData, code, useDefault
    // 返回: scheme, formValue
    async getCustomForm(prop) {
      const { schemeData, formData, currentNode, code, processId, useDefault } = prop

      const scheme = []
      const formValue = { processId, formreq: [] }
      if (code) {
        formValue.code = code
      }

      const schemeList = Array.isArray(schemeData) ? schemeData : Object.values(schemeData)
      for (let schemeIndex = 0; schemeIndex < schemeList.length; ++schemeIndex) {
        const schemeItem = schemeList[schemeIndex]
        schemeItem.F_Scheme = JSON.parse(schemeItem.F_Scheme)
        // 已有表单值的时候,舍弃掉不存在表单值中的 scheme
        if (formData && !formData[schemeItem.F_SchemeInfoId]) {
          continue
        }

        // 设置 formreq 的内容,非新建模式下需要设置 keyValue
        const { formId, field } = _.get(currentNode, `wfForms.${schemeIndex}`, {})
        const formreqObj = { schemeInfoId: formId, processIdName: field, formData: {} }
        if (formData) {
          if (Object.values(_.get(formData, `${schemeItem.F_SchemeInfoId}`, {})).some(t => t && t.length > 0)) {
            formreqObj.keyValue = processId
          }
        }
        formValue.formreq[schemeIndex] = formreqObj

        for (let dataIndex = 0; dataIndex < schemeItem.F_Scheme.data.length; ++dataIndex) {
          const { componts } = schemeItem.F_Scheme.data[dataIndex]
          for (const t of componts) {
            // 之后的 t 即表示每个 scheme 项
            t.__valuePath__ = `formreq.${schemeIndex}.formData.${t.id}`
            t.__schemeIndex__ = schemeIndex
            t.__dataIndex__ = dataIndex

            if (t.type === 'girdtable' && t.table) {
              // 数据项是表格的情况
              // 先设置源数据,不然无法获取默认值
              for (const fieldItem of t.fieldsData) {
                fieldItem.__sourceData__ = await this.getSourceData(fieldItem)
              }
              t.__defaultItem__ = await this.getDefaultData(t, prop)
              if (formData) {
                // 有表单值的情况,从表单值中获取数据
                const val = _.get(formData, `${schemeItem.F_SchemeInfoId}.${t.table}`, [])
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
                const path = `${schemeItem.F_SchemeInfoId}.${t.table}.${dataIndex}.${t.field.toLowerCase()}`
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

            // 权限控制
            const authObj = _.get(currentNode, `wfForms.${schemeIndex}.authorize.${t.id}`, {})
            t.edit = authObj.isEdit
            if (Number(t.isHide) !== 1 && authObj.isLook !== 0) {
              // 加入 scheme
              scheme.push(t)
            }
          }
        }
      }

      return { scheme, formValue }
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

      formValue.formreq.forEach(t => { t.formData = JSON.stringify(t.formData) })
      formValue.formreq = JSON.stringify(formValue.formreq)

      return formValue
    },

    // 获取流程信息信息
    // 参数: {code , processId , taskId}
    async fetchProcessInfo({ code, processId, taskId }) {
      const url = this.apiRoot + `${processId ? '/newwf/processinfo' : '/newwf/scheme'}`
      const reqObj = { processId }
      if (taskId) {
        reqObj.taskId = taskId
      }
      const data = processId ? JSON.stringify(reqObj) : code
      const [piErr, { data: { data: result } } = {}] = await uni.request({ url, data: { ...this.auth, data } })

      if (result.info) {
        result.info.Scheme = JSON.parse(result.info.Scheme)
      } else if (result.F_Content) {
        result.F_Content = JSON.parse(result.F_Content)
      }

      return result
    },

    // 从 processInfo 流程信息中,提取出 currentNode
    // 参数: processInfo
    getCurrentNode(processInfo) {
      if (processInfo.info) {
        return processInfo.info.Scheme.nodes.find(t => t.id === processInfo.info.CurrentNodeId)
      } else if (processInfo.F_Content) {
        return processInfo.F_Content.nodes.find(t => t.type === 'startround')
      }

      return {}
    },

    // 获取表单的 schemeData
    // 参数: currentNode
    async fetchSchemeData({ wfForms }) {
      if (!wfForms || wfForms.every(t => !t.formId)) {
        return {}
      }
      const data = JSON.stringify(wfForms.filter(t => t.formId).map(t => ({ id: t.formId, ver: '' })))
      const [err, { data: { data: schemeData } } = {}] = await uni.request({
        url: this.apiRoot + `/form/scheme`,
        data: { ...this.auth, data }
      })

      return schemeData
    },

    // 获取表单的 formData
    // 参数: currentNode , keyValue
    async fetchFormData({ wfForms }, keyValue) {
      const reqData = JSON.stringify(
        wfForms
        .filter(t => t.formId)
        .map(t => ({
          schemeInfoId: t.formId,
          processIdName: t.field,
          keyValue
        }))
      )

      const [fdErr, { data: { data: result } } = {}] = await uni.request({
        url: this.apiRoot + `/form/data`,
        data: { ...this.auth, data: reqData }
      })

      return result
    }
  }
}
