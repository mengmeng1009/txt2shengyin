
//初始化fileinput上传附件
var FileInput = function () {
    var oFile = new Object();
    //初始化fileinput控件（第一次初始化）
    oFile.Init = function (ctrlID, uploadUrl, data, callbackfun) {
        oFile.uploadUrl = uploadUrl;
        oFile.data = data;
        oFile.callbackfun = callbackfun
        if (oFile.isInit) {
            return;
        }
        oFile.isInit = true;
        var control = $('#' + ctrlID);
        //初始化上传控件的样式
        control.fileinput({
            language: 'zh', //设置语言
            uploadUrl: oFile.uploadUrl, //上传的地址
            allowedFileExtensions: ['jpg', 'gif', 'png'],//接收的文件后缀
            showUpload: false, //是否显示上传按钮
            showClose: false,//是否显示关闭按钮
            showRemove: false,//是否显示移除按钮
            showUpload: true,//是否显示上传按钮
            autoReplace: true,//是否自动替换当前图片，设置为true时，再次选择文件会将当前的文件替换掉。
            showCaption: false,//是否显示标题
            browseClass: "btn btn-primary zm-upbtn", //按钮样式 
            uploadClass: "btn btn-default btn-secondary zm-upbtn", //按钮样式 
            browseLabel: "选择头像",//选择文件按钮
            browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>&nbsp;',
            dropZoneEnabled: true,//是否显示拖拽区域
            uploadExtraData: function () {
                return oFile.data
            },//上传文件时额外传递的参数设置
            dropZoneTitle: '预览区域',//预览区提示文本
            //minImageWidth: 50, //图片的最小宽度
            //minImageHeight: 50,//图片的最小高度
            //maxImageWidth: 1000,//图片的最大宽度
            //maxImageHeight: 1000,//图片的最大高度
            //maxFileSize: 0,//单位为kb，如果为0表示不限制文件大小
            //minFileCount: 0,
            maxFileCount: 1, //表示允许同时上传的最大文件个数
            enctype: 'multipart/form-data',
            validateInitialCount: true,
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
        });
        control.on("fileuploaded", function (event, data, previewId, index) {
            oFile.callbackfun(event, data, previewId, index)
        })
        control.on('fileerror', function (event, data, msg) {  //一个文件上传失败
            console.log('文件上传失败！' + msg);
        });
        //导入文件上传完成之后的事件
        //control.on("fileuploaded", function (event, data, previewId, index) {
        //    $("#myModal").modal("hide");
        //    var data = data.response.lstOrderImport;
        //    if (data == undefined) {
        //        toastr.error('文件格式类型不正确');
        //        return;
        //    }
        //    //1.初始化表格
        //    var oTable = new TableInit();
        //    oTable.Init(data);
        //    $("#div_startimport").show();
        //});
    }
    return oFile;
};


//配置微信js-sdk//jsApiList需要使用的JS接口列表
var isWkwebwk = false//判断是否ios
var peizhiweixin = function (jsApiList) {
    $.getJSON('/EngineApi/weixinhp/getSign', {
        url: window.location.href.split("#")[0]
    },
        function (r) {
            let jg = r.data.jg;
            wx.config({
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: jg.AppID, // 必填，企业号的唯一标识，此处填写企业号corpid
                timestamp: jg.timestamp, // 必填，生成签名的时间戳
                nonceStr: jg.noncestr, // 必填，生成签名的随机串
                signature: jg.signature, // 必填，签名，见附录1
                jsApiList: jsApiList // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });
            wx.ready(function (rc) {
                // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
                //wx.checkJsApi({
                //    jsApiList: ['getLocalImgData'], // 需要检测的JS接口列表，所有JS接口列表见附录2,
                //    success: function (res) {
                //        // 以键值对的形式返回，可用的api值true，不可用为false
                //        // 如：{"checkResult":{"chooseImage":true},"errMsg":"checkJsApi:ok"}
                //        if (res.checkResult.getLocalImgData) {
                //            isWkwebwk = true
                //        } else {
                //            isWkwebwk = false
                //        }
                //    }
                //});
            });
            wx.error(function (res) {
                // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
                console.log("微信调用失败了", res);
            });
        }
    );
}
//获取guid
function guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

//递增数据
var dizengshuju = function (obj, zengliang, end, jiange) {
    console.log(obj)
    setTimeout(function () {
        if (obj < end) {
            obj = obj + zengliang
            dizengshuju(obj, zengliang, end, jiange)
        }
    }, jiange)
}
//get请求，获取json
var zmGetJson = function (url, chenggong) {
    $.ajax({
        url: url,
        type: "get",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (chenggong) {
                chenggong(result);
            } else {
                console.log(result);
            }

        },
        error(xhr, status, error) {
            if (shibai) {
                shibai(xhr, status, error)
            }
            else {
                console.log(result);
            }
        }
    });
}
//封装post//$.post 会报415错误
var zmPost = function (url, postdata, chenggong, shibai) {
    postdata.uid = zmAppConfig.uid;
    $.ajax({
        url: url,
        type: "post",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postdata),
        success: function (result) {
            if (chenggong) {
                chenggong(result);
            } else {
                console.log(result);
            }

        },
        error(xhr, status, error) {
            if (shibai) {
                shibai(xhr, status, error)
            }
            else {
                console.log(result);
            }
        }
    });
}



var getUid = function () {
    if (localStorage.getItem("zmUid")) {
        return localStorage.getItem("zmUid");
    } else {
        let uid = guid();
        localStorage.setItem("zmUid", uid);
    }
}
var zmAppConfig = {
    uid: getUid()
}