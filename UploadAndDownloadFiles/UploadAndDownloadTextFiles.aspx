<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAndDownloadTextFiles.aspx.cs" Inherits="UploadAndDownloadFiles.UploadAndDownloadTextFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table><tr><th><h1>Import And Export TextFile</h1></th></tr>
            <tr><td><asp:Button ID="Importbtn" runat="server" Text="Import Data" OnClick="Importbtn_Click" /></td><td>
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
            <tr><td><asp:Button ID="Exportbtn" runat="server" Text="Export Data" OnClick="Exportbtn_Click" /></td><td>
                <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
        </table>
        </div>
    </form>
</body>
</html>
