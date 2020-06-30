import Vue from 'vue'
import App from '@/App.vue'
import store from '@/common/store.js'
import mixins from '@/common/mixins.js'
import moment from 'moment'

Vue.mixin(mixins)
moment.locale('zh-cn')

import WxdocDesc from '@/components/learun-app/desc.vue'
Vue.component('wxdoc-desc', WxdocDesc)

import LButton from '@/components/learun-ui-wx/button.vue'
import LIcon from '@/components/learun-ui-wx/icon.vue'
import LTag from '@/components/learun-ui-wx/tag.vue'
import LAvatar from '@/components/learun-ui-wx/avatar.vue'
import LProgress from '@/components/learun-ui-wx/progress.vue'
import LLoading from '@/components/learun-ui-wx/loading.vue'
import LInput from '@/components/learun-ui-wx/input.vue'
import LSelect from '@/components/learun-ui-wx/select.vue'
import LDatePicker from '@/components/learun-ui-wx/date-picker.vue'
import LTimePicker from '@/components/learun-ui-wx/time-picker.vue'
import LDatetimePanel from '@/components/learun-ui-wx/datetime-panel.vue'
import LDateTimePicker from '@/components/learun-ui-wx/datetime-picker.vue'
import LRegionPicker from '@/components/learun-ui-wx/region-picker.vue'
import LSwitch from '@/components/learun-ui-wx/switch.vue'
import LRadio from '@/components/learun-ui-wx/radio.vue'
import LCheckbox from '@/components/learun-ui-wx/checkbox.vue'
import LCheckboxPicker from '@/components/learun-ui-wx/checkbox-picker.vue'
import LUpload from '@/components/learun-ui-wx/upload.vue'
import LTextarea from '@/components/learun-ui-wx/textarea.vue'
import LCard from '@/components/learun-ui-wx/card.vue'
import LList from '@/components/learun-ui-wx/list.vue'
import LListItem from '@/components/learun-ui-wx/list-item.vue'
import LModal from '@/components/learun-ui-wx/modal.vue'
import LBar from '@/components/learun-ui-wx/bar.vue'
import LBarItem from '@/components/learun-ui-wx/bar-item.vue'
import LBarItemButton from '@/components/learun-ui-wx/bar-item-button.vue'
import LTitle from '@/components/learun-ui-wx/title.vue'
import LBanner from '@/components/learun-ui-wx/banner.vue'
import LTimeline from '@/components/learun-ui-wx/timeline.vue'
import LTimelineItem from '@/components/learun-ui-wx/timeline-item.vue'
import LStep from '@/components/learun-ui-wx/step.vue'
import LNav from '@/components/learun-ui-wx/nav.vue'
import LLabel from '@/components/learun-ui-wx/label.vue'
import LChat from '@/components/learun-ui-wx/chat.vue'
import LChatMsg from '@/components/learun-ui-wx/chat-msg.vue'
import LChatInput from '@/components/learun-ui-wx/chat-input.vue'
import LCustomItem from '@/components/learun-ui-wx/custom-item.vue'
import LCustomAdd from '@/components/learun-ui-wx/custom-add.vue'

Vue.component('l-button', LButton)
Vue.component('l-icon', LIcon)
Vue.component('l-tag', LTag)
Vue.component('l-avatar', LAvatar)
Vue.component('l-progress', LProgress)
Vue.component('l-loading', LLoading)
Vue.component('l-input', LInput)
Vue.component('l-select', LSelect)
Vue.component('l-date-picker', LDatePicker)
Vue.component('l-time-picker', LTimePicker)
Vue.component('l-datetime-panel', LDatetimePanel)
Vue.component('l-datetime-picker', LDateTimePicker)
Vue.component('l-region-picker', LRegionPicker)
Vue.component('l-switch', LSwitch)
Vue.component('l-radio', LRadio)
Vue.component('l-checkbox', LCheckbox)
Vue.component('l-checkbox-picker', LCheckboxPicker)
Vue.component('l-upload', LUpload)
Vue.component('l-textarea', LTextarea)
Vue.component('l-card', LCard)
Vue.component('l-list', LList)
Vue.component('l-list-item', LListItem)
Vue.component('l-modal', LModal)
Vue.component('l-bar', LBar)
Vue.component('l-bar-item', LBarItem)
Vue.component('l-bar-item-button', LBarItemButton)
Vue.component('l-title', LTitle)
Vue.component('l-banner', LBanner)
Vue.component('l-timeline', LTimeline)
Vue.component('l-timeline-item', LTimelineItem)
Vue.component('l-step', LStep)
Vue.component('l-nav', LNav)
Vue.component('l-label', LLabel)
Vue.component('l-chat', LChat)
Vue.component('l-chat-msg', LChatMsg)
Vue.component('l-chat-input', LChatInput)
Vue.component('l-custom-item', LCustomItem)
Vue.component('l-custom-add', LCustomAdd)

Vue.config.productionTip = process.env.NODE_ENV === 'development'
Vue.prototype.$store = store

new Vue({ ...App, mpType: 'app', store }).$mount()
