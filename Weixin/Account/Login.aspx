<%@ Page Title="登录" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Weixin.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        登录
    </h2>
    <p>
        请输入用户名和密码。
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">注册</asp:HyperLink>
        如果您没有帐户。
    </p>
    <form id="Form1">
    <table>
        <tr>
            <th>
                账号:
            </th>
            <td>
                <asp:TextBox ID="tbUserId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                密码:
            </th>
            <td>
                <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <center>
        <asp:Button ID="btnLogin" runat="server" Text="登陆" OnClick="btnLogin_OnClick" />
    </center>
    </form>
</asp:Content>
