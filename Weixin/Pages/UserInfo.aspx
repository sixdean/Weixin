<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Weixin.Pages.UserInfo" %>

<body>
    <script type="text/javascript">
        $(function () {
            CreateGroupDataGrid();
            CreateUsersDataGrid("0");
        });

        function CreateGroupDataGrid() {
            $("#groupDataGrid").datagrid({
                url: "Ashx/GetData.ashx?type=GetGroupDataGrid",
                method: 'get',
                title: '用户分组',
                iconCls: 'icon-save',
                fitColumns: true,
                singleSelect: true,
                checkbox: true,
                height: 580,
                border: false,
                loadMsg: '在使劲加载中...',
                frozenColumns: [[
                    { field: 'groupId', title: 'Id' }
                ]],
                columns: [
                [
                    { field: 'id', hidden: true },
                    { field: 'name', title: '名称', width: 100 },
                    { field: 'count', title: '数量' },
                    {
                        field: 'IsAdd', title: '是否新增', formatter: function (value, row, index) {
                            var result = "否";
                            if (value == 1) {
                                result = '<span style="color:red">是</span>';
                            }
                            return result;
                        }
                    },
                    { field: 'IsUpdate', title: '是否修改', formatter: function (value, row, index) {
                        var result = "否";
                        if (value == 1) {
                            result = '<span style="color:red">是</span>';
                        }
                        return result;
                    }
                    },
                    { field: 'IsDelete', title: '是否删除', formatter: function (value, row, index) {
                        var result = "否";
                        if (value == 1) {
                            result = '<span style="color:red">是</span>';
                        }
                        return result;
                    }
                    },
                    { field: 'IsSync', title: '是否同步', formatter: function (value, row, index) {
                        var result = '<span style="color:red">否</span>';
                        if (value == 1) {
                            result = "是";
                        }
                        return result;
                    }
                    },
                    {
                        field: "Action",
                        title: 'Action',
                        align: 'center',
                        formatter: function (value, row, index) {
                            var u = "<a href='#' onclick='UpdateGroup(" + JSON.stringify(row) + " )' >修改</a>&nbsp&nbsp";
                            var d = "<a href='#' onclick='DeleteGroup(" + JSON.stringify(row) + " )' >删除</a>&nbsp&nbsp";
                            var s = "<a href='#' onclick='SelectGroup(" + JSON.stringify(row) + " )' >查询</a>&nbsp&nbsp";
                            return u + d + s;
                        }
                    }
                ]
            ],
                toolbar: [
                {
                    iconCls: 'icon-add',
                    text: '新增',
                    handler: function () {
                        AddGroup();
                    }
                }, '-', {
                    text: '刷新',
                    iconCls: "icon-reload",
                    handler: function () {
                        $('#groupDataGrid').datagrid('reload', {});
                    }
                }, '-', {
                    text: '<a href="#" title="从微信服务器上获取数据更新到本地,并删除本地原来数据" class="easyui-tooltip">同步数据</a>',
                    iconCls: "icon-hamburg-down",
                    handler: function () {
                        GetWeixinGroupData();
                    }
                }, '-', {
                    text: '<a href="#" title="将本地的数据更新到微信服务器,并修改本地数据状态" class="easyui-tooltip">更新数据</a>',
                    iconCls: "icon-hamburg-publish",
                    handler: function () {
                        UpdateWeixinGroupData();

                    }
                }, '-', {
                    text: '测试3',
                    iconCls: "icon-cancel",
                    handler: function () {
                        $.messager.show({ icon: "error", msg: "点击发送开了房间爱师傅的骄傲是代理费", timeout: 444444444, position: "topCenter", showType: 'fade', title: 'ceshifalfjalskf' });
                    }
                }, '-'
            ]

            });
        }

        function UpdateGroup(row) {
            console.info(row);
            if (row.groupId == 0 || row.groupId == 1 || row.groupId == 2) {
                messagerShowTop('提示', '系统分组，不允许修改');
            } else {
                $("#groupDialog").css('display', 'block');
                $("#groupDialog").dialog({
                    title: '修改用户分组信息',
                    width: 400,
                    height: 200,
                    closed: false,
                    cache: false,
                    modal: true,
                    buttons: [
                {
                    text: '保存',
                    iconCls: "icon-ok",
                    handler: function () {
                        if ($("#groupForm").form('validate')) {
                            var data = getFormJson('#groupForm');
                            console.info(data);
                            SaveGroupData(data, 'update');
                        } else {
                            messagerShowTop('提示', '信息验证失败');
                        }
                    }
                }, {
                    text: '取消',
                    iconCls: "icon-cancel",
                    handler: function () {
                        $("#groupDialog").dialog('close');
                    }
                }
            ]
                });

                $('#groupForm').form("load", {
                    groupId: row.groupId,
                    id: row.id,
                    name: row.name,
                    IsDelete: row.IsDelete,
                    IsUpdate: row.IsUpdate,
                    IsAdd: row.IsAdd,
                    IsSync: row.IsSync
                });
            }

        }

        function DeleteGroup(row) {
            if (row.groupId == 0 || row.groupId == 1 || row.groupId == 2) {
                messagerShowTop('提示', '系统分组，不允许删除');
            } else {
                $.messager.confirm('删除用户分组', "确定要删除[" + row.name + "]用户分组吗?", function (action) {
                    if (action) {
                        SaveGroupData(row, 'delete');
                    }
                });
            }
        }

        function SelectGroup(row) {
            console.info(row);
            CreateUsersDataGrid(row.groupId);
        }

        function AddGroup() {
            $("#groupDialog").css('display', 'block');
            $("#groupDialog").dialog({
                title: '新增用户分组信息',
                width: 400,
                height: 200,
                closed: false,
                cache: false,
                modal: true,
                buttons: [
                {
                    text: '保存',
                    iconCls: "icon-ok",
                    handler: function () {
                        if ($("#groupForm").form("validate")) {
                            var data = getFormJson('#groupForm');
                            SaveGroupData(data, 'add');
                        } else {
                            messagerShowTop('提示', '信息验证失败');
                        }
                    }
                }, {
                    text: '取消',
                    iconCls: "icon-cancel",
                    handler: function () {
                        $("#groupDialog").dialog('close');
                    }
                }
            ]
            });
            $('#groupForm').form("load", {
                groupId: '',
                id: '',
                name: '',
                IsDelete: '0',
                IsUpdate: '0',
                IsAdd: '1',
                IsSync: '0'
            });
        }

        //保存方法
        function SaveGroupData(data, type) {
            if (data.name.length > 30) {
                messagerShowTop('提示', '分组名字长度不能超过30个字符');
            } else {
                $.ajax({
                    type: "post",
                    url: "Pages/UserInfo.aspx/GroupFormSubmit",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{ d: '" + JSON.stringify(data) + "',type:'" + type + "' }",
                    success: function (data) {
                        console.info(data);
                        var result = JSON.parse(data.d);
                        console.info(result);
                        if (result.result == "ok") {
                            console.info("ok");
                            if ($("#groupDialog").css('display') == "block") {
                                $("#groupDialog").dialog('close');
                            }
                            messagerShowTop('提示', '保存成功');
                            $("#groupDataGrid").datagrid('reload');
                        } else if (result.result == "err") {
                            messagerShowTop('提示', '保存失败');
                        }
                    },
                    error: function (err) {
                        messagerShowTop('提示', '保存失败');
                    }
                });
            }
        }

        function CreateUsersDataGrid(id) {
            $('#usersDataGrid').datagrid({
                url: 'Ashx/GetData.ashx?type=GetUsersDataGrid&id=' + id,
                method: 'get',
                title: '用户列表',
                iconCls: 'icon-save',
                fitColumns: true,
                singleSelect: true,
                checkbox: true,
                border: false,
                height: 580,
                loadMsg: '在使劲加载中...',
                columns: [
                [
                        { field: 'Id', checkbox: true },
                    { field: 'nickname', title: '昵称', width: 30 },
                    { field: 'openid', title: '用户标识', width: 30 },
                    { field: 'subscribe', title: '是否订阅', formatter: function (value, rowdata, rowindex) {
                        var result = "否";
                        if (value == 1) {
                            result = "是";
                        }
                        else if (value == "0") {
                            result = "否";
                        }
                        return result;
                    }
                    },
                    { field: 'sex', title: '性别', formatter: function (value, rowdata, rowindex) {
                        var result = "未知";
                        if (value == 1) {
                            result = "男";
                        } else if (value == 2) {
                            result = "女";
                        }
                        else if (value == 0) {
                            result = "未知";
                        }
                        return result;
                    }
                    },
                    { field: 'country', title: '国家' },
                    { field: 'province', title: '省份' },
                    { field: 'city', title: '城市' },
                    { field: 'language', title: '语言', formatter: function (value, rowdata, rowindex) {
                        var result = "简体";
                        if (value == "zh_TW") {
                            result = "繁体";
                        }
                        else if (value == "en") {
                            result = "英语";
                        }
                        else if (value == "zh_CN") {
                            result = "简体";
                        }
                        return result;
                    }
                    },
                    { field: 'headimgurl', title: '用户头像', formatter: function (value, rowdata, rowindex) {
                        return "<img height='50px' width='50px' src='" + value + "' />";
                    }
                    },
                    { field: 'subscribe_time', title: '关注时间', formatter: function (value, rowdata, rowindex) {
                        return (new Date(value * 1000)).Format('yyyy-MM-dd hh:mm:ss');
                    }
                    },
                    { field: 'unionid', title: 'unionid' },
                    { field: 'groupid', title: '用户分组' },
                    { field: 'remark', title: '备注', editor: 'text' }
                //                    { field: 'text', title: '名称', editor: 'text' },
                //                    { field: 'text', title: '名称', editor: 'text' },
                //                    { field: 'text', title: '名称', editor: 'text' },
                //                    { field: 'text', title: '名称', editor: 'text' },
                //                    { field: 'text', title: '名称', editor: 'text' },
                //                    { field: 'text', title: '名称', editor: 'text' }

                ]
            ],
                toolbar: [
                {
                    text: '<a href="#" title="从微信服务器上获取数据更新到本地,并删除本地原来数据" class="easyui-tooltip">同步列表</a>',
                    iconCls: "icon-hamburg-down",
                    handler: function () {
                        GetWeixinUserListData(this);
                    }
                }, '-', {
                    text: '<a href="#" title="根据本地用户列表数据获取用户详细信息" class="easyui-tooltip">同步信息</a>',
                    iconCls: "icon-hamburg-down",
                    handler: function () {
                        GetWeixinUserInfoData(this);
                    }
                }, '-', {
                    iconCls: 'icon-add',
                    text: '新增',
                    handler: function () {
                    }
                }, '-', {
                    iconCls: 'icon-add',
                    text: '新增',
                    handler: function () {
                    }
                }, '-'
            ]
            });
        }


        function UpdateWeixinGroupData() {
            $.messager.confirm('更新数据', '确定要将本地用户分组数据更新到微信服务器上吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/UserInfo.aspx/UpdateWeixinGroupData',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var result = JSON.parse(data.d);
                            if (result.result == "ok") {
                                messagerShowTop('提示', '同步数据成功');
                                $("#groupDataGrid").datagrid('reload');
                            } else if (result.result == "err") {
                                messagerShowTop('提示', '同步数据失败' + result.msg);
                            }
                        },
                        error: function (err) {
                            messagerShowTop('提示', err);
                        }
                    });
                }
            });
        }
        function GetWeixinGroupData() {
            $.messager.confirm('同步数据', '确定要删除本地数据重新从微信服务器上获取用户分组数据吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/UserInfo.aspx/GetWeixinGroupData',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var result = JSON.parse(data.d);
                            if (result.result == "ok") {
                                messagerShowTop('提示', '同步数据成功');
                                $("#groupDataGrid").datagrid('reload');
                            } else if (result.result == "err") {
                                messagerShowTop('提示', '同步数据失败:' + result.msg);
                            }
                        },
                        error: function (err) {
                            messagerShowTop('提示', err);
                        }
                    });
                }
            });
        }


        function GetWeixinUserListData(target) {
            $.messager.confirm('获取用户列表', '确定要删除本地数据从新从微信服务器上获取用户列表数据吗?', function (action) {
                $(target).linkbutton('disable');
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/UserInfo.aspx/GetWeixinUserListData',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            var result = JSON.parse(data.d);
                            if (result.result == "ok") {
                                messagerShowTop('提示', '同步数据成功');
                                $("#usersDataGrid").datagrid('reload');
                            } else if (result.result == "err") {
                                messagerShowTop('提示', '同步数据失败:' + result.msg);
                            }
                            $(target).linkbutton('enable');
                        },
                        error: function (msg) {
                            messagerShowTop('提示', msg);
                            $(target).linkbutton('enable');
                        }
                    });
                }
            });
        }

        function GetWeixinUserInfoData(target) {
            $(target).linkbutton('disable');
            $.ajax({
                type: 'post',
                url: 'Pages/UserInfo.aspx/GetWeixinUserInfoData',
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var result = JSON.parse(data.d);
                    if (result.result == "ok") {
                        messagerShowTop('提示', '同步数据成功');
                        $("#usersDataGrid").datagrid('reload');
                    } else if (result.result == "err") {
                        messagerShowTop('提示', '同步数据失败:' + result.msg);
                    }
                    $(target).linkbutton('enable');
                },
                error: function (msg) {
                    messagerShowTop('提示', msg);
                    $(target).linkbutton('enable');
                }
            });
        }
    </script>
    <div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 40%">
                        <div id="groupDataGrid">
                        </div>
                    </td>
                    <td style="width: 60%">
                        <div id="usersDataGrid">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="groupDialog" style="display: none">
            <div style="padding: 10px 60px 20px 60px">
                <form id="groupForm" action="UserInfo.aspx.cs" method="POST">
                <table cellpadding="2">
                    <tr>
                        <td style="width: 100px">
                            Id:
                        </td>
                        <td>
                            <input id="id" name="id" class="easyui-validatebox" style="display: none" />
                            <input id="IsDelete" name="IsDelete" class="easyui-validatebox" style="display: none" />
                            <input id="IsUpdate" name="IsUpdate" class="easyui-validatebox" style="display: none" />
                            <input id="IsAdd" name="IsAdd" class="easyui-validatebox" style="display: none" />
                            <input id="IsSync" name="IsSync" class="easyui-validatebox" style="display: none" />
                            <input id="groupId" name="groupId" class="easyui-validatebox" readonly="readonly"
                                data-options="width: 200" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            名称:
                        </td>
                        <td>
                            <input id="name" name="name" class="easyui-validatebox" data-options="required: true, width: 200" />
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </div>
    </div>
</body>
