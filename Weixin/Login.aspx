<%@ Page Title="登录" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Weixin.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(function () {
            $("#LoginForm").form('load',
            {
            //                UserId: '请输入账号...',
            //                PassWord: '请输入密码...'
        });

        $(".UserId ").focus(function () {
            console.info("1");

        });
        $("#UserId").blur(function () {
            console.info("2");

        });
    });
    function submitForm() {
        var data = getFormJson('#LoginForm');
        if ($("#LoginForm").form('validate')) {
            $.ajax({
                type: "post",
                url: "Login.aspx/UserLogin",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ d: '" + JSON.stringify(data) + "'}",
                success: function (data) {
                    console.info(data);
                    var result = JSON.parse(data.d);
                    console.info(result);
                    if (result.result == "ok") {
                        window.location.href = "Default.aspx";
                    } else if (result.result == "err") {
                        messagerShowTop('提示', result.msg);
                    }
                },
                error: function (err) {
                    messagerShowTop('提示', err);
                }
            });
        } else {
            messagerShowTop('提示', '请输入账号密码');
        }
    }
        
    </script>
    <form id="test" action="Login.aspx.cs" method="POST">
    </form>
    <h2>
        Basic Form</h2>
    <p>
        Fill the form and submit it.</p>
    <div style="margin: 20px 0;">
    </div>
    <div style="width: 100%">
        <div class="login_center">
            <div class="login ">
                <span style="display: block; width: 1px; height: 1px"></span>
                <form id="LoginForm" action="Login.aspx.cs" method="POST" style="width: 100%; height: 100%">
                <div style="margin: auto auto">
                    <table cellpadding="2" style="margin: 10% auto">
                        <tr class="UserId">
                            <td>
                                账号:
                            </td>
                            <td>
                                <input class="easyui-textbox" type="text" name="UserId" id="UserId" data-options="required:true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                密码:
                            </td>
                            <td>
                                <input class="easyui-textbox" type="text" name="PassWord" data-options="required:true" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="submitForm()">登录</a>
                            </td>
                        </tr>
                    </table>
                </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
