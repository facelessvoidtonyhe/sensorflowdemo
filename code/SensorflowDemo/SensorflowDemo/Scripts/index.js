
var M = {

}
var needRefreshAsset = true;
$(function () {
    InitWs();
    InitCabinetLayer();
    RefreshAssetChart();
    RefreshCabinetInfo();
    GetCapacityInfo();
    RefreshAssetInfo();
    RefreshCabinetView();
    AutoRefreshCabinetView();
    RefreshChangeLog();
    var int = self.setInterval("AutoRefreshCabinetView()", 5000);
})

//刷新资产图表
function RefreshAssetChart() {
    var myChart = echarts.init(document.getElementById('assetChart'));
    $.ajax({
        contentType: "application/json",
        url: "/Home/GetAssetType",
        success: function (data) {
            var total = data.length;
            var legendData = [];
            var seriesData = [];
            for (var i = 0; i < total; i++) {
                legendData.push(data[i].ItemText);
                seriesData.push({ "value": data[i].ItemValue, "name": data[i].ItemText });
            }
            // 指定图表的配置项和数据
            option = {
                title: {
                    text: '所有资产-类型统计',
                    x: 'center',
                    textStyle: {
                        fontSize: 15,
                        fontWeight: 'bolder',
                        color: 'black'
                    }
                },
                legend: {
                    orient: 'vertical',
                    x: 'right',
                    data: legendData
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                series: [
                    {
                        name: '',
                        type: 'pie',
                        radius: ['50%', '70%'],
                        avoidLabelOverlap: false,
                        label: {
                            normal: {
                                show: false,
                                position: 'center'
                            },
                            emphasis: {
                                show: true,
                                textStyle: {
                                    fontSize: '30',
                                    fontWeight: 'bold'
                                }
                            }
                        },
                        labelLine: {
                            normal: {
                                show: false
                            }
                        },
                        data: seriesData
                    }
                ]
            };
            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        }
    });
}

//初始化机柜层位
function InitCabinetLayer() {
    $('#div_updown').empty();
    var template = $('<div class="divLayer"></div>');
    for (var i = 42; i > 0; i--) {
        $("#div_updown").append(template.clone().attr("id", i));
    }
}

//刷新预占资产列表
function assetList() {
    console.log('刷新资产list');
    $("#assetList").bootstrapTable('destroy');
    $("#assetList").bootstrapTable({ // 对应table标签的id
        url: "/Home/GetAssetList", // 获取表格数据的url
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        singleSelect: true,
        pageList: [5, 10], // 设置页面可以显示的数据条数
        pageSize: 5, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求
            return {
                pageSize: params.limit, // 每页要显示的数据条数
                offset: params.offset, // 每页显示数据的开始行号
                sort: params.sort, // 要排序的字段
                sortOrder: params.order, // 排序规则
                dataId: $("#dataId").val(), // 额外添加的参数
                time: Math.random()
            }
        },
        sortName: 'OrderNo', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        clickToSelect: true,
        columns: [
            {
                radio: true, // 显示一个勾选框
                align: 'center' // 居中显示
            },
            {
                field: 'BM', // 返回json数据中的name
                title: '资产编号', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'Type', // 返回json数据中的name
                title: '资产类型', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'UsedLayer', // 返回json数据中的name
                title: '高度(U)', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'UsedPower', // 返回json数据中的name
                title: '功率(W)', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'UsedWeight', // 返回json数据中的name
                title: '重量(Kg)', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
             {
                 field: 'UsedPowerPort', // 返回json数据中的name
                 title: '电口(个)', // 表格表头显示文字
                 align: 'center', // 左右居中
                 valign: 'middle' // 上下居中
             },
             {
                 field: 'UsedNetPort', // 返回json数据中的name
                 title: '网口(个)', // 表格表头显示文字
                 align: 'center', // 左右居中
                 valign: 'middle' // 上下居中
             }
        ],
        onLoadSuccess: function () {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        }
    })
}

function PreholdDialogShow() {
    $('.selectpicker').selectpicker({
        style: 'btn-info',
        size: 4
    });
    assetList();
    GetUsefulLayer();
}

//刷新机柜信息
function RefreshCabinetInfo() {
    $.ajax({
        type: "GET",
        url: "/home/GetCabinetModel",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('#cabinetName').html(data.Name);
            $('#cabinetName2').html(data.Name);
            $('#cabinetType').html(data.Type);
            $('#cabinetLocation').html(data.Location);
            $('#cabinetSN').html(data.SerialNumber);
            $('#cabinetUser').html(data.User);
            $('#coldtemp1').html(data.ColdTemp1);
            $('#coldtemp2').html(data.ColdTemp2);
            $('#coldtemp3').html(data.ColdTemp3);
            $('#hottemp1').html(data.HotTemp1);
            $('#hottemp2').html(data.HotTemp2);
            $('#hottemp3').html(data.HotTemp3);
        }
    });
}

//保存预占资产
function SavePrehold() {
    console.log('调用SavePrehold方法');
    var selectContent = $("#assetList").bootstrapTable('getSelections')[0];
    if (typeof (selectContent) == 'undefined') {
        alert('请至少选择一个资产！');
        return;
    }
    var CabinetLayer = $('#LayerSelect option:selected').val();
    $.ajax({
        type: "POST",
        url: "/home/PreholdAsset?OrderNo=" + selectContent.OrderNo + "&CabinetNo=1&CabinetLayer=" + CabinetLayer,
        dataType: "json",
        success: function (data) {
            $('#prehold').modal('hide');
            needRefreshAsset = true;
            GetCapacityInfo();
            RefreshAssetInfo();
            RefreshAssetChart();
            RefreshCabinetView();
            RefreshChangeLog();
        }
    });
}

//获取未占用的层位
function GetUsefulLayer() {
    $.ajax({
        type: "GET",
        url: "/home/GetUsefulLayer?CabinetNo=1",
        dataType: "json",
        success: function (data) {
            $('#LayerSelect').empty();
            for (var i = 0; i < data.length; i++) {
                $('#LayerSelect').append('<option value="' + data[i] + '">' + data[i] + '</option>');
            }
            $("#LayerSelect").selectpicker('refresh');
        }
    });
}

//下架选择
function OffLineDialogShow() {
    assetOfCabinetList();
}

function assetOfCabinetList() {
    $("#assetList2").bootstrapTable('destroy');
    $("#assetList2").bootstrapTable({ // 对应table标签的id
        url: "/Home/GetAssetOnLine", // 获取表格数据的url
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        singleSelect: true,
        pageList: [5, 10], // 设置页面可以显示的数据条数
        pageSize: 5, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求
            return {
                pageSize: params.limit, // 每页要显示的数据条数
                offset: params.offset, // 每页显示数据的开始行号
                sort: params.sort, // 要排序的字段
                sortOrder: params.order, // 排序规则
                dataId: $("#dataId").val(), // 额外添加的参数
                time: Math.random()
            }
        },
        sortName: 'OrderNo', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        clickToSelect: true,
        columns: [
            {
                radio: true, // 显示一个勾选框
                align: 'center' // 居中显示
            },
            {
                field: 'BM', // 返回json数据中的name
                title: '资产编号', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'Type', // 返回json数据中的name
                title: '资产类型', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'StartLayer', // 返回json数据中的name
                title: '层位', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            }
        ],
        onLoadSuccess: function () {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        }
    })
}

//保存下架资产
function SaveOffLine() {
    var selectContent = $("#assetList2").bootstrapTable('getSelections')[0];
    if (typeof (selectContent) == 'undefined') {
        alert('请至少选择一个资产！');
        return;
    }
    $.ajax({
        type: "POST",
        url: "/home/OffLineAsset?OrderNo=" + selectContent.OrderNo,
        dataType: "json",
        success: function (data) {
            $('#offline').modal('hide');
            needRefreshAsset = true;
            GetCapacityInfo();
            RefreshAssetInfo();
            RefreshAssetChart();
            RefreshCabinetView();
            RefreshChangeLog();
        }
    });
}

//获取机柜容量信息
function GetCapacityInfo() {
    $.ajax({
        type: "POST",
        url: "/home/GetCapacityInfo?CabinetNo=1",
        dataType: "json",
        success: function (data) {
            $('#progress1').css("width", data.progress1);
            $('#progress2').css("width", data.progress2);
            $('#progress3').css("width", data.progress3);
            $('#progress4').css("width", data.progress4);
            $('#progress5').css("width", data.progress5);
            $('#progress1Content').html(data.progress1Content);
            $('#progress2Content').html(data.progress2Content);
            $('#progress3Content').html(data.progress3Content);
            $('#progress4Content').html(data.progress4Content);
            $('#progress5Content').html(data.progress5Content);
            $('#progress1').attr("class", data.ColorStyle1);
            $('#progress2').attr("class", data.ColorStyle2);
            $('#progress3').attr("class", data.ColorStyle3);
            $('#progress4').attr("class", data.ColorStyle4);
            $('#progress5').attr("class", data.ColorStyle5);
        }
    });
}

///刷新柜内资产
function RefreshAssetInfo() {
    if (!needRefreshAsset) {
        return;
    }
    needRefreshAsset = false;
    $.ajax({
        type: "GET",
        url: "/home/GetAssetInfo?CabinetNo=1",
        dataType: "json",
        success: function (data) {
            $('#accordion2').empty();
            for (var i = 0; i < data.length; i++) {
                var model = data[i];
                var stateStr = '';
                if (model.State == '0') {
                    stateStr = '新增';
                } else if (model.State == '1') {
                    stateStr = '预占';
                } else if (model.State == '2') {
                    stateStr = '已上架';
                }
                var assetStr = '<div class="panel panel-default"><div class="panel-heading assetHead"><h4 class="panel-title"><a  data-id="' + model.OrderNo + '" data-toggle="collapse" data-parent="#accordion2" href="#collapse' + model.OrderNo + '">' + model.Name + ',' + model.BM + '</a></h4></div><div id="collapse' + model.OrderNo + '" class="panel-collapse collapse in"><div class="panel-body"><table style="width:100%"><tr class="assetTableRow"><td class="assetTableCol">资产类别</td><td class="assetTableCol2">' + model.Type + '</td><td class="assetTableCol">品牌</td><td class="assetTableCol2">' + model.AssetTM + '</td></tr><tr class="assetTableRow"><td class="assetTableCol">规格型号</td><td class="assetTableCol2">' + model.AssetModel + '</td><td class="assetTableCol">标签ID</td><td class="assetTableCol2">' + model.LabelId + '</td></tr><tr class="assetTableRow"><td class="assetTableCol">RFID标签</td><td class="assetTableCol2">' + model.RfidId + '</td><td class="assetTableCol">状态</td><td class="assetTableCol2">' + stateStr + '</td></tr><tr class="assetTableRow"><td class="assetTableCol">使用单位</td><td class="assetTableCol2">' + model.UseUnit + '</td><td class="assetTableCol">用途</td><td class="assetTableCol2">' + model.Purpose + '</td></tr><tr class="assetTableRow"><td class="assetTableCol">维保状态</td><td class="assetTableCol2">' + model.MaintenanceState + '</td><td class="assetTableCol">维保单位</td><td class="assetTableCol2">' + model.MaintenanceUnit + '</td></tr><tr class="assetTableRow"><td class="assetTableCol">维保期限</td><td class="assetTableCol2">' + model.MaintenanceDeadlineStr + '</td><td class="assetTableCol"></td><td class="assetTableCol2"></td></tr></table></div></div></div>';
                $('#accordion2').append(assetStr);
            }
            if (data.length > 0) {
                $('a[data-id=' + data[0].OrderNo + ']').click();
            }
        }
    });
}

function RefreshChangeLog() {
    $.ajax({
        type: "GET",
        url: "/home/GetChangeLog",
        dataType: "json",
        success: function (data) {
            $('#changeLogDiv').empty();
            for (var i = 0; i < data.length; i++) {
                var model = data[i];
                var stateStr = '';
                var imgStr = '';
                if (model.OperationType == '0') {
                    stateStr = '';
                } else if (model.OperationType == '1') {
                    stateStr = '预占';
                    imgStr = '../Content/Images/log1.png'
                } else if (model.OperationType == '2') {
                    stateStr = '上架';
                    imgStr = '../Content/Images/log2.png'
                } else if (model.OperationType == '3') {
                    stateStr = '下架';
                    imgStr = '../Content/Images/log3.png'
                }
                var assetStr = '<div class="logRow"><div class="logCol1">' + model.CreateTimeStr + '</div><div class="logCol2"><img src="' + imgStr + '" style="vertical-align:top"/><img src="/Content/Images/line.gif" style="vertical-align:bottom;"/></div><div class="logCol3"><h5>' + stateStr + '</h5><p>' + model.OperationDetail + '</p></div></div>';
                $('#changeLogDiv').append(assetStr);
            }
        }
    });
}

String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({[" + i + "]})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}

//自动刷新机柜列表
function AutoRefreshCabinetView() {
    RefreshCabinetView();
    RefreshAssetInfo();
    RefreshCabinetView();
    RefreshChangeLog();
    RefreshAssetChart();
}

//刷新机柜列表
function RefreshCabinetView() {
    var template = $([
      '<div class="divLayer" >',
      '    <div class="assetName"></div>',
      '</div>'].join(""));
    $.ajax({
        url: '/Home/GetCabinetAssetInfo?timer=' + Math.random() + "&CabinetNo=1",
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        success: function (ret) {
            console.log('RefreshCabinetView');
            console.log(ret);
            if (ret.length == 0)
            {
                InitCabinetLayer();
                return;
            }
            if (!ret || ret.length == -1 || ret == -1 || ret.length == 0) return;
            InitCabinetLayer();
            $.each(ret, function (i, item) {
                //机柜资产
                var startLayer = parseInt(item.StartLayer);//起始层位
                var layerCount = parseInt(item.UsedLayer);//占用层位
                var newStartLayer = layerCount + startLayer;
                var e = template.clone();
                e.height(19.8 * layerCount);
                e.attr("title", item.Name);
                if (item.CabinetState == "预占") {
                    e.css("background-color", "#2099F2");
                }
                else if (item.CabinetState == "在架") {
                    e.css("background-color", "#21CC9B");
                }
                else if (item.CabinetState == "遗失") {
                    e.css("background-color", "White");
                }
                else if (item.CabinetState == '非法') {
                    e.css("background-color", "#FC3224");
                }
                e.css("vertical-align", "middle");
                e.css("line-height", 19.8 * layerCount + "px");
                e.css("margin-left", "54px");
                e.css("width", "283px");
                e.on("click", function () {
                    //点击查看资产信息
                    showAssetInfo(item.OrderNo);
                });
                e.find(".assetName").html(item.Name);
                e.attr("id", "asset_" + item.OrderNo);
                $('#div_updown div[id="{0}"]'.format(startLayer)).before(e);
                for (var j = 0; j < layerCount; j++) {
                    $("#div_updown div[id='{0}']".format(startLayer + j)).remove();
                }
            });
        }
    })
}

function showAssetInfo(assetId) {
    $('a[href=#profile]').click();
    $('a[data-id=' + assetId + ']').click();
}

//定位选择资产
function LocationDialogShow() {
    $("#assetList3").bootstrapTable('destroy');
    $("#assetList3").bootstrapTable({ // 对应table标签的id
        url: "/Home/GetAssetLocation", // 获取表格数据的url
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        singleSelect: true,
        pageList: [5, 10], // 设置页面可以显示的数据条数
        pageSize: 5, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求
            return {
                pageSize: params.limit, // 每页要显示的数据条数
                offset: params.offset, // 每页显示数据的开始行号
                sort: params.sort, // 要排序的字段
                sortOrder: params.order, // 排序规则
                dataId: $("#dataId").val(), // 额外添加的参数
                time: Math.random()
            }
        },
        sortName: 'OrderNo', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        clickToSelect: true,
        columns: [
            {
                radio: true, // 显示一个勾选框
                align: 'center' // 居中显示
            },
            {
                field: 'BM', // 返回json数据中的name
                title: '资产编号', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'Type', // 返回json数据中的name
                title: '资产类型', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'StartLayer', // 返回json数据中的name
                title: '层位', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            }
        ],
        onLoadSuccess: function () {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        }
    })
}

function InitWs() {
    var start = function () {
        $.post("/Home/GetServerIpPort", function (data) {
            var wsImpl = window.WebSocket || window.MozWebSocket;
            window.ws = new wsImpl('ws://' + data + '/');
            ws.onmessage = function (evt) {
                var input = document.getElementById('txtBarcode');
                input.value = evt.data;
                $('#txtBarcode').textbox('setValue', evt.data);
            };
            ws.onopen = function () {
            };
            ws.onclose = function () {
            }
        });
    }
    window.onload = start;
}


//发送定位命令
function SaveLocation() {
    var selectContent = $("#assetList3").bootstrapTable('getSelections')[0];
    if (typeof (selectContent) == 'undefined') {
        alert('请至少选择一个资产！');
        return;
    }
    ws.send(selectContent.OrderNo);
    $('#location').modal('hide');
}

//盘点窗口
function InventoryDialogShow() {
    $("#assetList4").bootstrapTable('destroy');
    $("#assetList4").bootstrapTable({ // 对应table标签的id
        url: "/Home/GetAssetInventory", // 获取表格数据的url
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        singleSelect: true,
        pageList: [5, 10], // 设置页面可以显示的数据条数
        pageSize: 5, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求
            return {
                pageSize: params.limit, // 每页要显示的数据条数
                offset: params.offset, // 每页显示数据的开始行号
                sort: params.sort, // 要排序的字段
                sortOrder: params.order, // 排序规则
                dataId: $("#dataId").val(), // 额外添加的参数
                time: Math.random()
            }
        },
        sortName: 'OrderNo', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        clickToSelect: true,
        columns: [
            {
                field: 'BM', // 返回json数据中的name
                title: '资产编号', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'Type', // 返回json数据中的name
                title: '资产类型', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'StartLayer', // 返回json数据中的name
                title: '层位', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle' // 上下居中
            },
            {
                field: 'CabinetState', // 返回json数据中的name
                title: '状态', // 表格表头显示文字
                align: 'center', // 左右居中
                valign: 'middle',
                formatter: function (value, row, index) {
                    if (value == '盘点到') {
                        return "<image src='/Content/Images/yes.png' title='盘点到'/>";
                    } else {
                        return "<image src='/Content/Images/no.png' title='未盘点到'/>";
                    }
                }// 上下居中
            }
        ],
        onLoadSuccess: function () {  //加载成功时执行
            console.info("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.info("加载数据失败");
        }
    })
}
