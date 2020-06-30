import _ from 'lodash'
import moment from 'moment'
import { guid } from '@/common/utils.js'

// 自定义表单通用处理部分
export default {
  methods: {
    // 获取一个 scheme 表单项的源数据
    // 这是一个内部方法
    // 参数: 单个 schemeItem
    async getSourceData(item) {
      if (['radio', 'select', 'checkbox'].includes(item.type)) {
        // radio select checkbox 三种情况
        if (!item.dataSource || Number(item.dataSource) === 0) {
          // dataSource 为 0,使用 clientData
          return Object.values(this.$store.state.propTable[item.itemCode]).map(t => ({ value: t.value, text: t.text }))
        } else {
          // dataSource 不为 0,使用数据源,需要请求接口,并且提取出显示字段和实际字段
          const [code, displayField = item.showField, valueField = item.saveField] = item.dataSourceId.split(',')
          const [err, { data: { data: sourceData } } = {}] = await uni.request({
            url: this.apiRoot + '/datasource/map',
            data: { ...this.auth, data: JSON.stringify({ code, ver: '' }) }
          })
          if (err || !sourceData || !sourceData.data) {
            return []
          }

          return sourceData.data.map(t => ({ text: t[displayField], value: t[valueField] }))
        }

      } else if (['layer'].includes(item.type)) {
        // layer 需要更多属性
        if (!item.dataSource || Number(item.dataSource) === 0) {
          // dataSource 为 0,使用 clientData
          // clientData 对象转数组后,隐含 key:item.text 和 value:item.value 的关系
          const [keyItem, valueItem] = item.layerData
          const source = Object
            .values(this.$store.state.propTable[item.itemCode])
            .map(t => ({ value: t.value, text: t.text }))

          return {
            source,
            selfField: 'value',
            layerData: [{ name: 'text', label: keyItem.label }, { name: 'value', label: valueItem.label }]
          }
        } else {
          // dataSource 不为 0,使用数据源,需要请求接口,并且提取出显示字段和实际字段
          const [code] = item.dataSourceId.split(',')
          const [err, { data: { data: sourceData } } = {}] = await uni.request({
            url: this.apiRoot + '/datasource/map',
            data: { ...this.auth, data: JSON.stringify({ code, ver: '' }) }
          })
          if (err || !sourceData || !sourceData.data) {
            return []
          }
          const selfField = _.get(item.layerData.find(t => t.value === item.field), 'name')
          const source = sourceData.data
          return { source, selfField, layerData: item.layerData.filter(t => (!t.hide) && (t.value || t.label)) }
        }
      }

      return []
    },

    // 获取一个 scheme 表单项的默认值
    // 这是一个内部方法
    // 参数: 单个 schemeItem , {processId}
    async getDefaultData(item, prop) {
      const { processId } = prop
      switch (item.type) {
        case 'currentInfo':
          switch (item.dataType) {
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
          const datetimeFormat = Number(item.dateformat) === 0 ? 'YYYY-MM-DD' : 'YYYY-MM-DD HH:mm:ss'
          const today = moment()
          const dfDatetime = [
            today.subtract(1, 'day'),
            today,
            today.add(1, 'day')
          ][Number(item.dfvalue)] || today

          return dfDatetime.format(datetimeFormat) || ''

        case 'radio':
        case 'select':
          const radioItem = item.__sourceData__.find(t => t.value === item.dfvalue) || item.__sourceData__[0]
          return item.type === 'radio' ? radioItem.value : ''

        case 'checkbox':
          if (!item.dfvalue) {
            return []
          }
          return item.dfvalue.split(',').filter(t => item.__sourceData__.find(s => s.value === t))

        case 'encode':
          const [err, { data: { data: result } } = {}] = await uni.request({
            url: this.apiRoot + `/coderule/code`,
            data: { ...this.auth, data: item.rulecode || 10000 }
          })
          return result

        case 'upload':
          return []

        case 'guid':
          return item.table ? processId : ''

        case 'girdtable':
          console.log(`表格开始获取默认值了！`)
          const tableItemObj = {}
          for (const fieldItem of item.fieldsData) {
            tableItemObj[fieldItem.field] = await this.getDefaultData(fieldItem, prop)
          }
          return _.clone(tableItemObj)

        default:
          return item.dfvalue || ''
      }
    },

    // 将一个 formData 值转化为 formValue 值
    // 这是一个内部方法
    // 参数: 单个 schemeItem , 数据值
    convertToFormValue(item, val) {
      switch (item.type) {
        case 'upload':
          if (!val) {
            return ''
          }
          return val.split(',').map(t => this.apiRoot + `/annexes/down?data=${t}`)

        case 'radio':
          if (!val || !item.__sourceData__.map(t => t.value).includes(val)) {
            return ''
          }
          return val

        case 'checkbox':
          const validValue = item.__sourceData__.map(t => t.value)
          const checkboxVal = val.split(',') || []
          return checkboxVal.filter(t => validValue.includes(t))

        case 'datetime':
          if (!val) {
            return ''
          }
          return moment(val).format(
            Number(item.dateformat) === 0 || item.datetime === 'date' ?
            'YYYY-MM-DD' :
            'YYYY-MM-DD HH:mm:ss'
          )

        default:
          return val || ''
      }
    },

    // 将一个 formValue 值转化为 post 提交值
    // 这是一个内部方法
    // 参数: 单个 schemeItem , 表单项值 , formValue , scheme
    async convertToPostData(item, val, formValue, scheme) {
      switch (item.type) {
        case 'checkbox':
          return val ? val.join(',') : ''

        case 'datetimerange':
          const startTime = _.get(formValue, scheme.find(t => t.id === item.startTime).__valuePath__, null)
          const endTime = _.get(formValue, scheme.find(t => t.id === item.endTime).__valuePath__, null)
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
  }
}
