<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAndDownloadExcelFile.aspx.cs" Inherits="UploadAndDownloadFiles.Pages.UploadAndDownloadExcelFile" %>

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
                        <th>UPLOADING AND DOWNLOADING FILE</th>
                    </tr>
                    <tr>
                        <td>Select File :<asp:FileUpload ID="btnfileupload" runat="server" ToolTip="Upload File"></asp:FileUpload></td>
                        <td><asp:Button ID="BtnUpload" runat="server" Text="Upload" OnClick="BtnUpload_Click"></asp:Button></td>
                        <td><asp:Label ID="lblUploaded" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Btndown" runat="server" Text="DownLoad" OnClick="Btndown_Click"></asp:Button>
                        </td>
                        <td><asp:Label ID="lbldownloaded" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
