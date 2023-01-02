<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoadFileAndDownLoadXMLFiLe.aspx.cs" Inherits="UploadAndDownloadFiles.UpLoadFileAndDownLoadXMLFiLe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <table border="1">
                    <tr>
                        <th><h1>Import And Export XML File</h1></th>
                    </tr>
                    <tr>
                        <td><asp:Button ID="BtnImport" runat="server" Text="Import" OnClick="BtnImport_Click"></asp:Button></td><td><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="BtnExport" runat="server" Text="Export" OnClick="BtnExport_Click"></asp:Button></td><td><asp:Label ID="Label2" runat="server" ForeColor="#FF3300"></asp:Label></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
