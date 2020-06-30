// 本页是框架的默认配置页，请不要修改本页的任何内容
// 如需定制相关配置项，请修改项目根目录下的 config.js 文件内的相应条目

export default {
  // 登录页显示的公司名称
  "company": "未设置公司名",
  // 软件版本号
  "version": "<版本号>",
  // 版权信息显示的年份，设为 true 则自动使用当前年份，设为 false 则关闭
  "year": true,
  // 全局设置是否使用圆形头像
  "roundAvatar": false,
  // 请求数据的接口地址
  "apiRoot": "localhost",

  // 页面相关配置
  "pageConfig": {
    // 「首页」
    "home": {
      // 首页功能入口显示的列数
      "functionListColunm": 4,
      // 首页功能入口最多显示的入口数量，设为 false 或 -1 则不限制
      "functionListLimit": 8,
      // 新闻、公告发表时间显示方式，可选 before、date、datetime
      "noticeDateDisplay": "before"
    },

    // 「消息」页
    "msg": {
      // 是否使用圆形头像，覆盖全局设置
      "roundAvatar": null,
      // 消息列表中时间显示方式，可选 auto、before、date、datetime
      "noticeDateDisplay": "auto",
      // 是否开启未读标记
      "unread": true,
      // 周期轮询消息的时间间隔，单位是毫秒
      "fetchMsg": 3000
    },

    // 「通讯录」页
    "contact": {
      // 是否使用圆形头像，覆盖全局设置
      "roundAvatar": null,
      // 是否显示(分)公司、部门、职员标签
      "tag": true,
      // 是否在职员这一级也显示标签
      "staffTag": false,
      // 自定义标签
      "costumeTag": ['公司', '分公司', '部门', '职员'],
      // 是否把职员排列到部门的前面显示
      "staffFirst": false,
      // 默认展开几级列表，设为 true 则全部展开
      "expand": 1
    },

    // 「我的」页
    "my": {
      // 是否使用圆形头像，覆盖全局设置
      "roundAvatar": null
    },

    // 「聊天消息」页
    "chat": {
      // 是否使用圆形头像，覆盖全局设置
      "roundAvatar": null,
      // 聊天记录时间显示方式，可选 auto、before、date、datetime
      "msgDateDisplay": "auto",
      // 周期轮询消息的时间间隔，单位是毫秒
      "fetchMsg": 1500
    },

    // 「我的任务」页
    "mytask": {
      // 流程信息的时间显示方式，可选 auto、before、date、datetime
      "taskDateDisplay": "auto"
    },

    // 「开票管理」页
    "invoice": {
      // 开票信息的时间显示方式，可选 auto、before、date、datetime
      "invoiceDateDisplay": "auto",
      // 开票信息是否前置显示小图标
      "showIcon": true
    }
  }
}
