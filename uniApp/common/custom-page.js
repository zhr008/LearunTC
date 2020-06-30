import _ from 'lodash'
import moment from 'moment'
import { guid, copy } from '@/common/utils.js'

// 用于代码生成器页面
export default {
  methods: {
    // 获取表单默认值
    async getDefaultForm() {
      const result = {}
      for (const [tableName, tableItem] of Object.entries(this.scheme)) {
        const itemData = {}
        for (const [fieldName, scheme] of Object.entries(tableItem)) {
          if (fieldName !== '__GIRDTABLE__') {
            itemData[fieldName] = await this.getDefaultValue(`${tableName}.${fieldName}`, scheme)
          }
        }
        result[tableName] = '__GIRDTABLE__' in tableItem ? [itemData] : itemData
      }

      return result
    },

    async getDefaultValue(path, scheme) {
      switch (scheme.type) {
        case 'currentInfo':
          switch (scheme.dataType) {
            case 'user':
              return this.$store.state.user.userId
            case 'department':
              return this.$store.state.user.departmentId
            case 'company':
              return this.$store.state.user.companyId
            case 'time':
              return moment().format('YYYY-MM-DD HH:mm:ss')
            default:
              return ''
          }

        case 'datetime':
          const datetimeFormat = Number(scheme.dateformat) === 0 ? 'YYYY-MM-DD' : 'YYYY-MM-DD HH:mm:ss'
          const today = moment()
          const dfDatetime = [today.subtract(1, 'day'), today, today.add(1, 'day')][Number(scheme.dfvalue)] || today

          return dfDatetime.format(datetimeFormat) || ''

        case 'radio':
        case 'select':
          const radioItem = _.get(this.dataSource, path).find(t => t.value === scheme.dfvalue) ||
            _.get(this.dataSource, path)[0]
          return scheme.type === 'radio' ? radioItem.value : ''

        case 'checkbox':
          if (!scheme.dfvalue) { return [] }
          return scheme.dfvalue.split(',').filter(t => _.get(this.dataSource, path).find(s => s.value === t))

        case 'encode':
          const [err, { data: { data: result } } = {}] = await uni.request({
            url: this.apiRoot + `/coderule/code`,
            data: { ...this.auth, data: scheme.rulecode || 10000 }
          })
          return result

        case 'upload':
          return []

        case 'guid':
          return guid('-')

        default:
          return scheme.dfvalue || ''
      }
    },

    // 获取时间差、当前信息等表单项的显示文本
    displayItem(path) {
      const scheme = _.get(this.scheme, path)
      const value = this.getValue(path)

      switch (scheme.type) {
        case 'datetimerange':
          if (value) { return value }

          const startTime = this.getValue(scheme.startTime)
          const endTime = this.getValue(scheme.endTime)
          if (!startTime || !endTime || moment(endTime).isBefore(startTime)) { return '-' }

          return moment
            .duration(moment(endTime).diff(moment(startTime)))
            .asDays()
            .toFixed(0)

        case 'currentInfo':
          switch (scheme.dataType) {
            case 'user':
              return _.get(this.$store.state.staff, `${value}.name`, '')
            case 'department':
              return _.get(this.$store.state.dep, `${value}.name`, '')
            case 'company':
              return _.get(this.$store.state.company, `${value}.name`, '')
            default:
              return value || ''
          }

        case 'encode':
        default:
          return value || ''
      }
    },

    // 验证表单项输入是否正确，返回一个包含所有错误信息的数组
    verifyForm() {
      const result = []
      Object.entries(this.scheme).forEach(([tableName, tableItem]) => {
        if ('__GIRDTABLE__' in tableItem) {
          this.getValue(tableName).forEach((tableValue, index) => {
            Object.entries(tableItem).forEach(([fieldName, scheme]) => {
              if (fieldName === '__GIRDTABLE__' || !scheme.verify) { return }

              const val = tableValue[fieldName]
              const verifyResult = this.verify[scheme.verify](val)

              if (verifyResult !== true) {
                result.push(`[表格${tableItem.__GIRDTABLE__}第${index}行${scheme.title}列]: ${verifyResult}`)
              }
            })
          })
        } else {
          Object.entries(tableItem).forEach(([fieldName, scheme]) => {
            if (!scheme.verify) { return }

            const val = this.getValue(`${tableName}.${fieldName}`)
            const verifyResult = this.verify[scheme.verify](val)

            if (verifyResult !== true) {
              result.push(`[${scheme.title}]: ${verifyResult}`)
            }
          })
        }
      })

      return result
    },

    // 获取要提交的表单数据
    async getPostData() {
      const result = {}

      for (const [tableName, tableItem] of Object.entries(this.scheme)) {
        if ('__GIRDTABLE__' in tableItem) {
          // 从表
          const tableArray = []
          const tableData = this.current[tableName]
          for (let index = 0; index < tableData.length; ++index) {
            const tableValue = tableData[index]
            const tableObj = {}
            for (const [fieldName, scheme] of Object.entries(tableItem)) {
              if (fieldName === '__GIRDTABLE__') { return }
              tableObj[fieldName] = await this.convertToPostData(scheme, tableValue[fieldName])
            }
            tableArray.push(tableObj)
          }
          result[`str${tableName}Entity`] = JSON.stringify(tableArray)

        } else {
          // 主表
          const strEntity = {}
          for (const [fieldName, scheme] of Object.entries(tableItem)) {
            strEntity[fieldName] = await this.convertToPostData(scheme, this.current[tableName][fieldName])
          }
          result.strEntity = JSON.stringify(strEntity)
        }
      }

      if (this.mode !== 'create') {
        result.keyValue = this.id
      }

      return JSON.stringify(result)
    },

    // 将单项表单数据转为 post 数据
    async convertToPostData(scheme, val) {
      switch (scheme.type) {
        case 'checkbox':
          return val ? val.join(',') : ''

        case 'datetimerange':
          const startTime = this.getValue(scheme.startTime)
          const endTime = this.getValue(scheme.endTime)
          if (!startTime || !endTime || moment(endTime).isBefore(startTime)) {
            return ''
          } else {
            return moment.duration(moment(endTime).diff(moment(startTime))).asDays().toFixed(0)
          }

        case 'datetime':
          return val ? moment(val).format('YYYY-MM-DD HH:mm:ss') : ''

        case 'upload':
          const uploadUid = []
          await Promise.all(
            val.map(t => {
              const id = guid('-')
              uploadUid.push(id)
              return uni.uploadFile({
                url: this.apiRoot + `/annexes/upload`,
                name: 'file',
                filePath: t,
                formData: { ...this.auth, id }
              })
            })
          )
          return uploadUid.join(',')

        default:
          return val || ''
      }
    }

    // 格式化处理表单数据
    formatFormData(formData) {
      const data = _.omit(formData, 'keyValue')
      for (const [tableName, tableItem] of Object.entries(this.scheme)) {
        if ('__GIRDTABLE__' in tableItem) {
          const tableData = data[tableName]
          for (let index = 0; index < tableData.length; ++index) {
            const tableValue = tableData[index]

            for (const [fieldName, scheme] of Object.entries(tableItem)) {
              if (fieldName === '__GIRDTABLE__') { return }
              const dataSource = _.get(this.dataSource, `${tableName}.${fieldName}`)
              tableValue[fieldName] = this.convertToFormValue(scheme, tableValue[fieldName], dataSource)
            }
          }
        } else {
          for (const [fieldName, scheme] of Object.entries(tableItem)) {
            const dataSource = _.get(this.dataSource, `${tableName}.${fieldName}`)
            data[tableName][fieldName] = this.convertToFormValue(scheme, data[tableName][fieldName], dataSource)
          }
        }
      }

      return data
    },

    // 将单项表单数据格式化
    convertToFormValue(scheme, val, dataSource) {
      switch (scheme.type) {
        case 'upload':
          if (!val) { return '' }
          return val.split(',').map(t => this.apiRoot + `/annexes/down?data=${t}`)

        case 'radio':
          if (!val || !dataSource.map(t => t.value).includes(val)) { return '' }
          return val

        case 'checkbox':
          const validValue = dataSource.map(t => t.value)
          const checkboxVal = val.split(',') || []
          return checkboxVal.filter(t => validValue.includes(t))

        case 'datetime':
          if (!val) { return '' }
          return moment(val).format(
            Number(scheme.dateformat) === 0 || scheme.datetime === 'date' ?
            'YYYY-MM-DD' :
            'YYYY-MM-DD HH:mm:ss'
          )

        default:
          return val || ''
      }
    }
  },

  computed: {
    // 验证函数
    verify() {
      return {
        NotNull: t => t.length > 0 || '不能为空',
        Num: t => !isNaN(t) || '须输入数值',
        NumOrNull: t => t.length <= 0 || !isNaN(t) || '须留空或输入数值',
        Email: t => /^[a-zA-Z0-9-_.]+@[a-zA-Z0-9-_]+.[a-zA-Z0-9]+$/.test(t) || '须符合Email格式',
        EmailOrNull: t => t.length <= 0 || /^[a-zA-Z0-9-_.]+@[a-zA-Z0-9-_]+.[a-zA-Z0-9]+$/.test(t) ||
          '须留空或符合Email格式',
        EnglishStr: t => /^[a-zA-Z]*$/.test(t) || '须由英文字母组成',
        EnglishStrOrNull: t => t.length <= 0 || /^[a-zA-Z]*$/.test(t) || '须留空或由英文字母组成',
        Phone: t => /^[+0-9- ]*$/.test(t) || '须符合电话号码格式',
        PhoneOrNull: t => t.length <= 0 || /^[+0-9- ]*$/.test(t) || '须留空或符合电话号码格式',
        Fax: t => /^[+0-9- ]*$/.test(t) || '须符合传真号码格式',
        Mobile: t => /^1[0-9]{10}$/.test(t) || '须符合手机号码格式',
        MobileOrPhone: t => /^[+0-9- ]*$/.test(t) || /^1[0-9]{10}$/.test(t) || '须符合电话或手机号码格式',
        MobileOrNull: t => t.length <= 0 || /^1[0-9]{10}$/.test(t) || '须留空或符合手机号码格式',
        MobileOrPhoneOrNull: t => t.length <= 0 || /^1[0-9]{10}$/.test(t) || /^[+0-9- ]*$/.test(t) ||
          '须留空或符合手机/电话号码格式',
        Uri: t => /^[a-zA-z]+:\/\/[^\s]*$/.test(t) || '须符合网址Url格式',
        UriOrNull: t => t.length <= 0 || /^[a-zA-z]+:\/\/[^\s]*$/.test(t) || '须留空或符合网址Url格式'
      }
    }
  }
}
