 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAndDownLoadCSVFile.aspx.cs" Inherits="UploadAndDownloadFiles.UploadAndDownLoadCSVFile" %>

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
                        <th><h1>Import And Export Data</h1></th>
                    </tr>
                    <tr>
                        <td><asp:Button ID="BtnUpload" runat="server" Text="Import" OnClick="BtnUpload_Click"></asp:Button></td>
                        <td><asp:Label ID="Label1" runat="server" Text="" ForeColor="#ff0000"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="BtnDownload" runat="server" Text="Export" OnClick="BtnDownload_Click"></asp:Button></td>
                        <td><asp:Label ID="Label2" runat="server" Text="" ForeColor="#cc0000"></asp:Label></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
