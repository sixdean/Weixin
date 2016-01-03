<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Weixin._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(function () {

            CreateMainMenu();
        });
        function CreateMainMenu() {
            $("#mainMenu").tree({
                url: "Ashx/GetData.ashx?type=GetSysMenu",
                animate: true,
                lines: true,
                onClick: function (n) {
                    CreateMainTal(n);
                }

            });
        }

        function CreateMainTal(n) {
            var selectIndex = null;
            $($("#mainTabs").tabs('tabs')).each(function (i, t) {
                if (t.panel('options').id == n.id) {
                    selectIndex = t.panel('options').index;
                }
            });
            if (selectIndex != null) {
                $("#mainTabs").tabs('select', selectIndex);
            } else {
                $("#mainTabs").tabs('add', {
                    title: n.text,
                    id: n.id,
                    href: n.Url,
                    closable: true
                });
            }
        } 

        //        $(function () {
        //            $("#mainDataGrid").datagrid({
        //                url: "../Ashx/GetData.ashx?type=GetMenu",
        //                colums: [[
        //                                        { field: 'Id', title: 'Id' },
        //                                        { field: 'Name', title: 'Name' },
        //                                        { field: 'Type', title: 'Type' },
        //                                        { field: 'Key', title: 'Key' },
        //                                        { field: 'Url', title: 'Url' },
        //                                        { field: 'Media_id', title: 'Media_id' },
        //                                        { field: 'ParentId', title: 'ParentId' },
        //                                        { field: 'UpdateUser', title: 'UpdateUser' },
        //                                        { field: 'UpdateDate', title: 'UpdateDate' },
        //                                        { field: 'CreateUser', title: 'CreateUser' },
        //                                        { field: 'CreateDate', title: 'CreateDate' }
        //                                        ]]
        //                //            url: 'datagrid_data1.json'
        //            });

        //            //        $("#mainDataGrid").datagrid({
        //            //            data: [
        //            //            { itemid: '2112', productid: 'fdasfad' }
        //            //            ]
        //            //        });
        //        })
    </script>
    <h2>
        Basic Layout</h2>
    <div style="margin: 20px 0;">
    </div>
    <div class="easyui-layout" style="width: 95%; height: 500px;">
        <div data-options="region:'north'" style="height: 50px">
        </div>
        <div data-options="region:'south',split:true" style="height: 50px;">
        </div>
        <div data-options="region:'west',split:true" title="导航" style="width: 150px;">
            <ul id="mainMenu">
            </ul>
        </div>
        <div data-options="region:'center'">
            <div id="mainTabs" class="easyui-tabs">
            </div>
        </div>
    </div>
</asp:Content>
