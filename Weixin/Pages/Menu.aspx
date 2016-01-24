<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Weixin.Pages.Menu" %>

<body>
    <script type="text/javascript">
        var editIndex = undefined;
        $(function () {
            $('#mainDataGrid').datagrid({
                url: 'Ashx/GetData.ashx?type=GetMenu',
                method: 'get',
                title: 'Context Menu on DataGrid',
                iconCls: 'icon-save',
                fitColumns: true,
                singleSelect: true,
                checkbox: true,
                loadMsg: '在使劲加载中...',
                columns: [
                    [
                        { field: 'Id', checkbox: true },
                        { field: 'MenuId', hidden: true },
                                        {
                                            field: 'ParentId',
                                            title: '父菜单',
                                            width: 10,
                                            formatter: function (value, rowData, rowIndex) {
                                                return rowData["ParentName"];
                                            },
                                            editor: {
                                                type: 'combobox',
                                                options: {
                                                    url: 'Ashx/GetData.ashx?type=GetParentRows',
                                                    valueField: 'MenuId',
                                                    textField: 'Name',
                                                    required: true,
                                                    method: 'get'
                                                }
                                            }
                                        },
                        { field: 'Name', title: '名称', editor: 'text' },
                        { field: 'Type', title: '菜单类型', editor: 'text' },
                        { field: 'Key', title: '菜单KEY值', editor: 'text' },
                        { field: 'Url', title: '网页链接', editor: 'text' },
                        { field: 'Media_id', title: '素材接口', editor: 'text' },
                        { field: 'Sort', title: '排序', editor: 'text' },
                        { field: 'UpdateUser', title: 'UpdateUser' },
                        { field: 'UpdateDate', title: 'UpdateDate' },
                        { field: 'CreateUser', title: 'CreateUser' },
                        { field: 'CreateDate', title: 'CreateDate' }

                    ]
                ],
                toolbar: [
                    {
                        iconCls: 'icon-add',
                        text: '新增',
                        handler: function () {
                            AddRow();
                        }
                    }, '-', {
                        iconCls: 'icon-remove',
                        text: '删除',
                        handler: function () {
                            deleteRow();
                        }
                    }, '-', {
                        iconCls: 'icon-edit',
                        text: '修改',
                        handler: function () {
                            editRow();
                        }
                    }, '-', {
                        iconCls: 'icon-reload',
                        text: '刷新',
                        handler: function () {
                            reload();
                        }
                    }, '-', {
                        iconCls: 'icon-save',
                        text: '<a href="#" title="将新增或修改的数据保存到本地" class="easyui-tooltip">保存</a>',
                        handler: function () {
                            save();
                        }

                    }, '-', {
                        iconCls: 'icon-cancel',
                        text: '取消',
                        handler: function () {
                            cancel();
                        }

                    }, '-', {
                        iconCls: 'icon-hamburg-publish',
                        text: '<a href="#" title="将本地的数据更新到微信服务器,并修改本地数据状态" class="easyui-tooltip">更新数据</a>',
                        handler: function () {
                            updateWeixinMenu();
                        }

                    }, '-', {
                        iconCls: 'icon-hamburg-down',
                        text: '<a href="#" title="从微信服务器上获取数据更新到本地,并删除本地原来数据" class="easyui-tooltip">同步数据</a>',
                        handler: function () {
                            GetWeixinMenu();
                        }

                    }, '-'
                ],
                onCheck: function (i, d) {
                    if (editIndex != undefined && i != editIndex) {
                        $('#mainDataGrid').datagrid('uncheckRow', i);
                    }
                },
                onUncheck: function (i, d) {
                    if (editIndex != undefined && i == editIndex) {
                        $('#mainDataGrid').datagrid('checkRow', i);
                    }
                },
                selectOnCheck: false,
                onLoadSuccess: function (d) {

                }

            });
        });



        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#mainDataGrid').datagrid('validateRow', editIndex)) {
                var ed = $('#mainDataGrid').datagrid('getEditor', { index: editIndex, field: 'ParentId' });
                var productname = $(ed.target).combobox('getText');
                $('#mainDataGrid').datagrid('getRows')[editIndex]['productname'] = productname;
                $('#mainDataGrid').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }

        function onClickRow(index) {
            if (editIndex != index) {
                if (endEditing()) {
                    $('#mainDataGrid').datagrid('selectRow', index)
							.datagrid('beginEdit', index);
                    editIndex = index;
                } else {
                    $('#mainDataGrid').datagrid('selectRow', editIndex);
                }
            }
        }

        //新增行;
        function AddRow() {
            if (editIndex == undefined) {
                $('#mainDataGrid').datagrid('insertRow', {
                    index: 0,
                    row: {
                        Id: '',
                        Name: '',
                        Type: '',
                        Key: '',
                        Url: '',
                        Media_id: '',
                        Sort: '',
                        title: '',
                        UpdateUser: '666',
                        UpdateDate: new Date().Format('yyyy-MM-dd hh:mm:ss'),
                        CreateUser: '',
                        CreateDate: new Date().Format('yyyy-MM-dd hh:mm:ss')
                    }
                });
                $('#mainDataGrid').datagrid('uncheckAll').datagrid('checkRow', 0).datagrid('selectRow', 0).datagrid('beginEdit', 0);
                editIndex = 0;
            } else {
                $.messager.alert('Warning', '请先取消或保存好正在编辑的行再进行新增!');
            }
        }

        //编辑行
        function editRow() {
            if (editIndex == undefined) {
                if ($('#mainDataGrid').datagrid('getChecked').length == 1) {
                    var checkRow = $('#mainDataGrid').datagrid('getChecked');
                    var index = $('#mainDataGrid').datagrid('getRowIndex', checkRow[0]);
                    $('#mainDataGrid').datagrid('selectRow', index).datagrid('beginEdit', index);
                    editIndex = index;
                }
                else if ($('#mainDataGrid').datagrid('getChecked').length == 0) {
                    $.messager.alert('Warning', '请先选择再编辑!');

                } else {
                    $.messager.alert('Warning', '只能选择一行数据进行编辑!!');

                }

            } else {
                $.messager.alert('Warning', '请先取消或保存好正在编辑的行再进行新增!');
            }

        }
        //删除行
        function deleteRow() {
            if (editIndex == undefined) {
                if ($('#mainDataGrid').datagrid('getChecked').length == 0) {
                    $.messager.alert('Warning', '请先选择再删除!');

                } else {
                    $.messager.confirm('删除数据', '确定要选择删除选择的数据吗?', function (action) {
                        if (action) {
                            $.ajax({
                                type: 'post',
                                url: 'Pages/Menu.aspx/DeleteWeixinMenu',
                                dataType: 'json',
                                contentType: "application/json;charset=utf-8",
                                success: function (result) {
                                    console.info("1");
                                    console.info(result);
                                    editIndex = undefined;

                                },
                                error: function (msg) {
                                    console.info(msg);
                                    console.info("2");
                                }
                            });
                        }
                    });
                }
            } else {
                $.messager.alert('Warning', '请先取消或保存好正在编辑的行再进行新增!');
            }
        }

        //保存
        function save() {
            if (editIndex != undefined) {
                if ($('#mainDataGrid').datagrid('validateRow', editIndex)) {
                    $('#mainDataGrid').datagrid('acceptChanges');
                    $.ajax({
                        type: "Post",
                        url: "Pages/Menu.aspx/SaveWeixinMenu",
                        data: "{d:'" + JSON.stringify($('#mainDataGrid').datagrid('getChecked')[0]) + "'}",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (result) {
                            console.info("1");
                            console.info(result.d);
                            $('#mainDataGrid').datagrid('endEdit', editIndex);

                            $('#mainDataGrid').datagrid('acceptChanges');
                            editIndex = undefined;
                        },
                        error: function (msg) {
                            console.info(msg);
                            console.info("2");
                        }
                    });
                } else {
                    $.messager.alert('Warning', '请将数据填写完整再保存!');
                }

            }
        }

        function updateWeixinMenu() {
            $.messager.confirm('更新数据', '确定要将本地微信菜单数据更新到微信服务器上吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/Menu.aspx/UpdateWeixinMenu',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (result) {
                            console.info("1");
                            console.info(result.d);
                        },
                        error: function (msg) {
                            console.info(msg);
                            console.info("2");
                        }
                    });
                }
            });
        }

        function GetWeixinMenu() {
            $.messager.confirm('同步数据', '确定要删除本地数据重新从微信服务器上获取菜单数据吗?', function (action) {
                if (action) {
                    $.ajax({
                        type: 'post',
                        url: 'Pages/Menu.aspx/GetWeixinMenu',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (result) {
                            console.info($.parseJSON(result.d));
                            $('#mainDataGrid').datagrid('loadData', $.parseJSON(result.d));
                        },
                        error: function (msg) {
                            console.info(msg);
                            console.info("2");
                        }
                    });
                }
            });
        }
        //取消
        function cancel() {
            $('#mainDataGrid').datagrid('rejectChanges');
            editIndex = undefined;
        }

        //刷新
        function reload() {
            $('#mainDataGrid').datagrid('reload', {
        });
        editIndex = undefined;
    }

    

    </script>
    <div id="mainDataGrid">
    </div>
</body>
