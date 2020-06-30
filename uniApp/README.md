# 力软框架微信小程序端 · 二次开发手册
修订日期：2020年2月17日  
版本号：1.1.3  
  
-----
  
# 开发前的准备
力软框架微信小程序端使用 uni-app 技术来开发。  
因为微信小程序原本不支持 Vue ，而 uni-app 为小程序开发提供了 Vue 支持，简化了开发难度。  
  
需要下载安装以下工具：
- [HBuilderX](https://www.dcloud.io/hbuilderx.html) ，uni-app 的官方开发工具，请下载App开发版
- [微信小程序开发工具](https://developers.weixin.qq.com/miniprogram/dev/devtools/download.html) ，用于调试和发布微信小程序
  
HBuilderX 下载后是个压缩包，将它解压到硬盘上，运行 HBuilderX.exe 即可启动；  
微信小程序则按照指引，依次点击下一步即可完成安装。 
  
以上工具的安装、使用过程中遇到问题，或是对开发方式不熟悉，可以参考以下文档：
- [uni-app 介绍与文档](https://uniapp.dcloud.io/) ，uni-app 的代码写法与常规 Vue 开发有区别，查阅文档获取帮助  
- [微信小程序官方开发指南](https://developers.weixin.qq.com/miniprogram/dev/framework/) ，微信小程序与常规网页开发有区别，查阅文档获取帮助  
  
-----
# 起步
请按照上述步骤，先将所需工具完成安装。  
启动 HBuilderX，点击左上角菜单：「文件」>「打开目录」，然后选择本项目的文件夹，即可打开本项目。  
  
安装后首次运行 HBuilderX 时，必做的操作：
- 点击左上角菜单>「工具」>「设置」>点击「运行配置」页，找到「微信开发者工具路径」，在这里请正确设置微信开发者工具的安装路径。
- 点击左上角菜单>「工具」>「插件安装」，在打开的插件列表中安装「less 编译」。
- 选择左侧 /config.js 文件，修改公司名称（company）、后台接口地址（apiRoot）。
（仅安装后初次运行需要做，后续使用无需重复）  
  
如果您已申请了自己的微信小程序 AppID，打开项目根目录下的 manifest.json，请在其中「微信小程序配置」页面填入 AppID。  
  
打开本项目后，可以在左侧的浏览项目目录，点击文件即可进行编辑。  
项目根目录下的文件夹：
- common： 存放多个页面通用的 CSS、JS 代码
- components： 存放小程序中用到的组件
- node_modules： 存放小程序中使用到的 npm 库
- pages： 重要，小程序内所有页面均放在此目录下
- static： 重要，小程序内的图片、音视频等静态资源目录
- unpackage (从未运行过的项目没有此文件夹)： 运行项目时会自动把编译生成的代码放在此目录下
  
项目根目录下的文件：
- .gitignore： 该文件列出了在使用 Git 提交代码时所忽略的目录和文件
- App.vue： 重要，小程序运行时的全局 Vue 对象，可以用来设置应用生命周期钩子、导入全局样式等
- config.js： 重要，小程序多个页面的配置文件，可以控制页面显示的内容和方式
- main.js： 重要，项目启动时的入口 JS 文件
- manifest.json： uni-app 相关的设置，在这里设置应用名称、微信 AppID 等
- package.json： 记录小程序所用到的 npm 库的名称和版本
- pages.json： 重要，这是小程序所有页面的路径、样式的配置文件
- uni.scss： uni-app 内置的样式
  
uni-app 中，每一个小程序页面都是一个 .vue 文件，它包含了模板、JS、CSS 代码；  
文件中可以引入其他 JS、CSS，引入安装好的 npm 库，也可以引入图片视频音乐等媒体。  
  
需要注意的是，小程序所有页面必须放置于 /pages 目录中， 且每个页面都必须在 /pages.json 中添加一条对应的记录。这是微信官方的强制规定。  
例如：  
某个小程序页面，它的目录为：  
`/pages/mypage/index.vue`  
那么在 /pages.json 里，需要在 pages 列表中添加一条记录：  
`{ "path": "pages/mypage/index", "style": { "navigationBarTitleText": "我是页面的标题" } }`  
  
-----
  
# 使用力软代码生成器
以下是使用力软代码生成器创建页面的流程：  
1. 第 6 步会输出「移动页面主页面」和「移动表单页」两个页面的代码；  
2. 在微信小程序的 /pages 目录下， 创建一个与代码生成器输出目录名同名的文件夹，然后在其中创建两个文件：list.vue、single.vue ；  
3. 在微信小程序的 /pages.json 文件中，添加新添的2个页面的记录。
  
例如：  
假设用户输出目录名为 LR_Codedemo，功能类名 wxtest，那么需要创建在微信小程序项目中新建以下两个文件：  
`/pages/LR_Codedemo/wxtest/list.vue` （将「移动页面主页面」里的代码复制到该文件内）；  
`/pages/LR_Codedemo/wxtest/index.vue` （将「移动表单页」里的代码复制到该文件内）。

然后，需要配置 /pages.json 文件：  
在 pages 数组里添加以下两条（标题可以自行更改）：  
```
{
  "pages": [
    { "path": "/pages/LR_Codedemo/list", "style": { "navigationStyle": "移动页面主页面标题" } },
    { "path": "/pages/LR_Codedemo/index", "style": { "navigationStyle": "移动表单页标题" } },
    
    // ...其他页面
  ]
}  
```
这样就完成了新自定义页面的生成。list.vue、single.vue 内的代码可以根据业务进行修改定制。  