﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="netTree.test" %>

<%@ Register TagPrefix="wc" Namespace="netTree" Assembly="netTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jsTree example</title>
    <link rel="stylesheet" href="/scripts/jsTree/themes/netTree/style.css" />
    <script type="text/javascript" src="/scripts/jquery-2.1.0.js"></script>
	<script type="text/javascript" src="/scripts/jsTree/jstree.js"></script>
	<script type="text/javascript" src="/scripts/jsTree/netTree.js"></script>
    <script type="text/javascript">
        function nodeSelected(e, data) {            
            alert(parseNodeId(e.target.id, data.node.id));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="litSelectedItems" runat="server" />
        <br />         
        <asp:Button runat="server" ID="btnPostback" Text="ClickMe"/>
        <a href="#" onclick="alert(<%=categoryTree.JSSelectedItems%>)">JS Get selected items</a>
        <div>
            <wc:SimpleTree ID="simpleTree"  runat="server" JSOnNodeSelected="nodeSelected" />
        </div>
        <br /> 
        <div>
            <wc:CategoryTree ID="categoryTree"  runat="server" InitiallyCollapsed="True" />
        </div>              
    </form>
</body>
</html>
