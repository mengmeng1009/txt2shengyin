var zm = {
    alert: function (neirong) {
        let al = $("#zmalert")
        if (al.length == 0) {
            al = $("<div id='zmalert'>").hide().appendTo($("body"));//将提示框添加到文档//初始隐藏状态
            let zz = $("<div class='zm-zhezhao' style='z-index:999;' >");//提示框遮罩层
            let ts = $("<div class='zm-alertarea'>");//提示区域
            let tstitle = $("<div class='zm-title'>").text("提示");//提示标题
            let tsneirong = $("<div class='zm-neirong'>");//提示内容存放位置
            let tsbtn = $("<div class='zm-btn'>").text("确定").on("click", function () {
                $("#zmalert").hide();
            });//提示按钮//点击时隐藏提示框
            ts.append(tstitle);
            ts.append(tsneirong);
            ts.append(tsbtn);
            zz.append(ts)
            al.append(zz)
        }
        $("#zmalert>.zm-zhezhao>.zm-alertarea>.zm-neirong").html(neirong);
        al.show();
    }
}