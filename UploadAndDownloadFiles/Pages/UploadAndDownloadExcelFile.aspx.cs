using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ClosedXML;
namespace UploadAndDownloadFiles.Pages
{
    public partial class UploadAndDownloadExcelFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = btnfileupload.PostedFile.FileName; // getting the file path of uploaded file  
                string filePath = Server.MapPath(Path.GetFileName(filename)); // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename); // getting the file extension of uploaded file  
                
                string type = String.Empty;
                if (!btnfileupload.HasFile)
                {
                    lblUploaded.Text = "Please Select File"; //if file uploader has no file selected  
                }
                else
                {
                    var linenumber = 0;
                    //string filepath = @"C:\Server\Upload\CountryMaster.csv;
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
                    {
                        conn.Open();
                        using (StreamReader reader = new StreamReader(@filePath))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                if (linenumber != 0)
                                {
                                    var values = line.Split(',');
                                    var sql = "Insert into demo.dbo.CountryMasterExcelCsv values('" + values[0] + "','" + values[1] + "')";
                                    var cmd = new SqlCommand();
                                    cmd.CommandText = sql;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.Connection = conn;
                                    int i = cmd.ExecuteNonQuery();
                                    if (i > 0)
                                    {
                                        Console.WriteLine("Number of Lines Affected !!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Number of Lines Not Affected !! " + i);
                                    }
                                }
                                linenumber++;
                            }
                        }

                        conn.Close();
                 
                    }

                }
                lblUploaded.Text = "File Uploaded Successfully with "+ext+"!!!";
            }
                
                catch (Exception ex)
            {
                Console.WriteLine(ex);
                lblUploaded.Text = "File Not Uploaded Successfully !!!";
            }

        }

        protected void Btndown_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [CountryId],[CountryName] FROM[demo].[dbo].[CountryMasterExcelCsv]", conn);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    DataTable dt = ds.Tables[0];

                    string filename = btnfileupload.PostedFile.FileName; // getting the file path of uploaded file  
                    string filepath = "C:\\Server\\Download\\";
                    if (!btnfileupload.HasFile)
                    {
                        lbldownloaded.Text = "Please Select File";
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(filepath+filename);


                        //writer.Write(writer.NewLine);
                        //Taking Table Row Count
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            //Act as a separator for Column header 
                            writer.Write(dt.Columns[i]);
                            if (i < dt.Columns.Count - 1)
                            {
                                writer.Write(",");
                            }
                        }

                        writer.Write(writer.NewLine);

                        foreach (DataRow dr in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                                if (!Convert.IsDBNull(dr[i]))
                                {
                                    string value = dr[i].ToString();
                                    if (value.Contains(','))
                                    {
                                        value = String.Format("\"{0}\"", value);

                                        writer.Write(value);
                                    }
                                    else
                                    {
                                        writer.Write(dr[i].ToString());
                                    }
                                    //
                                    if (i < dt.Columns.Count - 1)
                                    {
                                        //Act as a separator for Row
                                        writer.Write(",");
                                    }
                                }
                            writer.Write(writer.NewLine);

                        }
                        //writer.Write("File Created On : " + writer.NewLine + DateTime.Now.ToString());
                        writer.Flush();
                        writer.Close();

                        lbldownloaded.Text = "File Downloaded Successfully !!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                lbldownloaded.Text = "File Not Downloaded Successfully !!!!";
            }
        }
    }
}