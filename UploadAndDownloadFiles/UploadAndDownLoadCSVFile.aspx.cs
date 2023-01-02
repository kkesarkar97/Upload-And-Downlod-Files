using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadAndDownloadFiles
{
    public partial class UploadAndDownLoadCSVFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            { 
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from CountryMaster", conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            StreamWriter writer = new StreamWriter(@"C:\Server\Download\CountryMasterCSVFile.csv", false);


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
                Label2.Text = "File Downloaded to Server Successfully !!!"; 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Label2.Text = "File Not Downloaded to Server Successfully !!!";
            }
    
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var linenumber = 0;
                string filepath = @"C:\Server\Download\CountryMasterCSVFile.csv";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
                {
                    conn.Open();
                    using (StreamReader reader = new StreamReader(filepath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (linenumber != 0)
                            {
                                var values = line.Split(',');
                                var sql = "Insert into demo.dbo.CountryMasterExcelFileCsv values('" + values[0] + "','" + values[1] + "')";
                                var cmd = new SqlCommand();
                                cmd.CommandText = sql;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Connection = conn;
                                int i = cmd.ExecuteNonQuery();
 
                            }
                            linenumber++;
                        }
                    }

                    conn.Close();
                    Label1.Text = "File Uploaded Successfully !!!!";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Label1.Text = "File Not Uploaded Successfully !!!!";
            }
        }
    }
}