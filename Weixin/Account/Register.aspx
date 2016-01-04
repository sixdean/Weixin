<%@ Page Title="注册" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Weixin.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form>
    <asp:FormView ID="FormView1" DataSourceID="ObjectDataSource1" EnableModelValidation="True"
        OnItemInserted="FormView1_OnItemInserted" DefaultMode="Insert" runat="server">
        <InsertItemTemplate>
            <table>
                <tr>
                    <th>
                        账号:
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="tbUserId" Text='<%#Bind("UserId") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        姓名:
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="tbName" Text='<%#Bind("Name") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        密码:
                    </th>
                    <td>
                        <asp:TextBox ID="tbPassword" runat="server" Text='<%#Bind("Password") %>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
        <FooterTemplate>
            <center>
                <asp:Button runat="server" Text="注册" CommandName="Insert" />
            </center>
        </FooterTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjectDataSource1" DataObjectTypeName="Weixin.DAL.SysUser"
        InsertMethod="AddSysUser" TypeName="Weixin.BLL.SysUserBll" OnInserted="ObjectDataSource1_OnInserted"
        runat="server"></asp:ObjectDataSource>
    </form>
</asp:Content>
