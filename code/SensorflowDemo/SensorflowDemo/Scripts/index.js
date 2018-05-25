$(function () {
    InitCabinetLayer();
    RefreshAssetChart();
})

//刷新资产图表
function RefreshAssetChart() {
    console.log('开始');
    var myChart = echarts.init(document.getElementById('assetChart'));
    console.log(myChart);
    option = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'right',
            data: ['服务器', '交换机', '路由器', '存储', '防火墙']
        },
        series: [
            {
                name: '访问来源',
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
                data: [
                    { value: 335, name: '服务器' },
                    { value: 310, name: '交换机' },
                    { value: 234, name: '路由器' },
                    { value: 135, name: '存储' },
                    { value: 1548, name: '防火墙' }
                ]
            }
        ]
    };

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
}

//初始化机柜层位
function InitCabinetLayer() {
    console.log('调用InitCabinetLayer');
    $('#div_updown').empty();
    var template = $('<div class="divLayer"></div>');
    for (var i = 42; i > 0; i--) {
        $("#div_updown").append(template.clone().attr("id", i));
    }
}

//刷新预占资产列表
function assetList() {
    console.log('刷新资产list');
    $("#assetList").bootstrapTable({ // 对应table标签的id
        url: "/Home/Test", // 获取表格数据的url
        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true
        striped: true,  //表格显示条纹，默认为false
        pagination: true, // 在表格底部显示分页组件，默认false
        pageList: [10, 20], // 设置页面可以显示的数据条数
        pageSize: 10, // 页面数据条数
        pageNumber: 1, // 首页页码
        sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求
            return {
                pageSize: params.limit, // 每页要显示的数据条数
                offset: params.offset, // 每页显示数据的开始行号
                sort: params.sort, // 要排序的字段
                sortOrder: params.order, // 排序规则
                dataId: $("#dataId").val() // 额外添加的参数
            }
        },
        sortName: 'OrderNo', // 要排序的字段
        sortOrder: 'desc', // 排序规则
        columns: [
            {
                checkbox: true, // 显示一个勾选框
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
            console.log($('#table').bootstrapTable('getData', true));
            var rowCount = $('#table').bootstrapTable('getData', true).length;
            if (rowCount == 0) {
                $('#assetList').append("<tr id='emptyTr'><td style='text-align:center;vertical-align: middle;' colspan='8'><p>没有查询到相关数据</p></td></tr>");
            }
            else {
                $('#emptyTr').remove();
            }
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
}
