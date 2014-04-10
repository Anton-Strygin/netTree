<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="netTree.test" %>

<%@ Register TagPrefix="wc" Namespace="netTree" Assembly="netTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jsTree example</title>
    <link rel="stylesheet" href="/scripts/jsTree/themes/argustree/style.css" />
    <script type="text/javascript" src="/scripts/jquery-2.1.0.js"></script>
	<script type="text/javascript" src="/scripts/jsTree/jstree.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wc:SimpleTree ID="simpleTree"  runat="server" />
        </div>
        <br /> 
        <div>
            <wc:CategoryTree ID="categoryTree"  runat="server" />
        </div>              
    </form>
</body>
</html>
