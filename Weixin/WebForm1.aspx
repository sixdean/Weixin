<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Weixin.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type=""></script>
    <style type="text/css">
        div
        {
            background: #9919;
            float: left;
            height: 150px;
            width: 150px;
            margin: 10px;
        }
        .all-corners
        {
            border-radius: 80px/60px;
        }
        .one-corner
        {
            border-top-left-radius: 20px;
        }
        p {
          <%--  color: #222;
            font-size: 4.5em;
            font-weight: bold;--%>
        }
        .basic {
            text-shadow: 3px 3px white,
            6px 6px rgba(50,50,50,25)
        }
        .shadow {
            box-shadow: 4px 5px 5px #999 inset;
        }</style>
</head>
<body>
    <div class="all-corners">
    </div>
    <div class="one-corner">
    </div>
    <br/>
    <p class="basic">Basic Shadow</p>
    <div class="shadow">
        <p style="margin: 5px">
            Shadow with Offses Zero ,Blur, and Sprad
        </p>
    </div>
</body>
</html>
