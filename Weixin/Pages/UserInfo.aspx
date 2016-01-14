<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="Weixin.Pages.UserInfo" %>

<body>
    <script type="text/javascript">
        $(function () {

            CreateGroupDataGrid();
            CreateUsersDataGrid("1");

        });

        function CreateGroupDataGrid() {
            console.info("1");
            $("#groupDataGrid").datagrid({
                url: "Ashx/GetData.ashx?type=GetGroupDataGrid",
                method: 'get',
                title: '用户分组',
                iconCls: 'icon-save',
                fitColumns: true,
                singleSelect: true,
                checkbox: true,
                loadMsg: '在使劲加载中...',
                columns: [[
                        { field: 'id', title: 'id', editor: 'text' },
                        { field: 'name', title: '名称', editor: 'text' },
                        { field: 'count', title: '数量', editor: 'text' }
                    ]
                ],
                toolbar: [
                    {
                        iconCls: 'icon-add',
                        text: '新增',
                        handler: function () {
                        }
                    }, '-'
                ],
                onClick: function (n) {
                    CreateUsersDataGrid(n.id);
                }

            });
        }

        function CreateUsersDataGrid(id) {
            $('#usersDataGrid').datagrid({
                url: 'Ashx/GetData.ashx?type=GetUsersDataGrid',
                method: 'get',
                title: '用户列表',
                iconCls: 'icon-save',
                fitColumns: true,
                singleSelect: true,
                checkbox: true,
                loadMsg: '在使劲加载中...',
                columns: [
                    [


                        { field: 'id', title: '名称', editor: 'text' }


                    ]
                ],
                toolbar: [
                    {
                        iconCls: 'icon-add',
                        text: '新增',
                        handler: function () {
                        }
                    }, '-'
                ],
                loader: function (param, success, error) {
                    var that = $(this);
                    param = { id: id };
                    console.info(that);
                    var opts = that.datagrid("options");
                    console.info(opts);

                    if (!opts.url) {
                        return false;
                    }
                    $.ajax({
                        type: opts.method,
                        url: opts.url,
                        data: param,
                        dataType: "json",
                        success: function (data) {
                            that.data().datagrid['cache'] = data;
                            success(data);
                        },
                        error: function () {
                            error.apply(this, arguments);
                        }
                    });
                }
            });
        }
        function GetWeixinGroupData() {
            $.messager.confirm('获取用户分组', '确定要删除本地数据从新从微信服务器上获取用户分组数据吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/UserInfo.aspx/GetWeixinGroupData',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (result) {
                            console.info(result);
                            return true;
                        },
                        error: function (msg) {
                            console.info(msg);
                        }
                    });
                }
            });

        }


        function GetWeixinUserInfoData() {
            $.messager.confirm('获取用户列表', '确定要删除本地数据从新从微信服务器上获取用户列表数据吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/UserInfo.aspx/GetWeixinUserInfoData',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (result) {
                            console.info(result);
                            return true;
                        },
                        error: function (msg) {
                            console.info(msg);
                        }
                    });
                }
            });

        }
    </script>
    <table style="width: 100%">
        <%-- <tr>
            <td style="width: 10%">
                <div>
                    <label>
                        </label>
                    <a href="#" class="btn_a" onclick="GetWeixinGroupData()">获取</a>
                </div>
            </td>
            <td style="width: 90%">
                <label>
                    </label>
                <a href="#" class="btn_a" onclick="GetWeixinUserInfoData()">获取</a>
            </td>
        </tr>--%>
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
</body>
