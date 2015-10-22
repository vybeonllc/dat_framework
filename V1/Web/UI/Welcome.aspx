<%@ Page Language="C#" AutoEventWireup="true" %>
<html>
<head id="Head1" runat="server">
    <script data-role="page-scripts">
        var y = {
            Name: "amir"
        };
    </script>
</head>
<body>
    hey
    <dat:ListView runat="server" ID="test">
        <databinder runat="server" id="databinder">
            <SelectCommand Target="new Date()" />
        </databinder>
        <ItemTemplate>
            <input type="text" id="username" />
        </ItemTemplate>
    </dat:ListView>
    <dat:ListView runat="server" ID="test2">
        <databinder runat="server" id="databinder2">
            <SelectCommand Target="new Date()" />
        </databinder>
        <ItemTemplate>
            <input type="text" id="password" />
        </ItemTemplate>
    </dat:ListView>
</body>
</html>
