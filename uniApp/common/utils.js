export const convertHtml = str => str
  .replace(/{@zuojian@}|{@youjian@}|{@and@}/g, tag => ({
    '{@zuojian@}': '<',
    '{@youjian@}': '>',
    '{@and@}': '&'
  })[tag] || tag)
  .replace(/&amp;|&lt;|&gt;|&#39;|&quot;/g, tag => ({
    '&amp;': '&',
    '&lt;': '<',
    '&gt;': '>',
    '&#39;': "'",
    '&quot;': '"'
  })[tag] || tag)

export const guid = (joinChar = '_') =>
  `xxxxxxxx${joinChar}xxxx${joinChar}4xxx${joinChar}yxxx${joinChar}xxxxxxxxxxxx`.replace(/[xy]/g, c => {
    const r = Math.random() * 16 | 0;
    const v = c === 'x' ? r : (r & 0x3 | 0x8);

    return v.toString(16);
  })

export const displayCash = (num, placeholder = '-') => {
  if (!num || isNaN(num)) {
    return placeholder
  }

  return Number(num)
    .toFixed(2)
    .replace(/(\d{1,2})(?=(\d{3})+\.)/g, '$1,')
}

export const copy = t => JSON.parse(JSON.stringify(t))

export default { convertHtml, guid, displayCash, copy }
