/* 以下是一些公共方法 */
function dialogAlert(content) {
    dialog({
        title: '系统提示',
        content: content
    }).showModal();
}

function dialogAlertNoModal(content) {
	dialog({
		title: '系统提示',
		content: content
	}).show();
}

function dialogConfirm(content, okfunction, cancelfunction) {
	var d = dialog({
		title: '系统提示',
		content: content,
		okValue: '确定',
		ok: okfunction,
		cancelValue: '取消',
		cancel: (cancelfunction === '' ? 'function () {}' : cancelfunction)
	}).showModal();

	return d;
}

function trim(str) {   //删除左右两端的空格
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

function ltrim(str) { //删除左边的空格
    return str.replace(/(^\s*)/g, "");
}

function rtrim(str) { //删除右边的空格
    return str.replace(/(\s*$)/g, "");
}

//Div居中
function letDivCenter(divName) {
    var top = ($(window).height() - $(divName).height()) / 2;
    var left = ($(window).width() - $(divName).width()) / 2;
    var scrollTop = $(document).scrollTop();
    var scrollLeft = $(document).scrollLeft();
    $(divName).css({ position: 'absolute', 'top': top + scrollTop, left: left + scrollLeft }).show();
}

function isNumber(val) {
    var reg = /^[1-9]\d*$/;
    return reg.test(val);

}

/*验证用户名，长度5-16，仅支持字母、数字和下划线*/
function isUserName(obj) {
    var r = new RegExp("^[0-9a-zA-Z_]{5,16}$");
    return obj.match(r);
}

/*验证密码，长度6~18，仅支持字母、数字和下划线*/
function isPassword(obj) {
    var r = new RegExp("^[0-9a-zA-Z_]{6,18}$");
    return obj.match(r);
}