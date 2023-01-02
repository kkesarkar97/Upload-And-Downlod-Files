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
    public partial class UploadAndDownloadTextFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Exportbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM CountryMaster"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);

                                //Build the Text file data.
                                string txt = string.Empty;

                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Header row for Text file.
                                    txt += column.ColumnName + "\t\t";
                                }

                                //Add new line.
                                txt += "\r\n";

                                foreach (DataRow row in dt.Rows)
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        //Add the Data rows.
                                        txt += row[column.ColumnName].ToString() + "\t\t";
                                    }

                                    //Add new line.
                                    txt += "\r\n";
                                }

                                //Download the Text file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExportCountryMaster.txt");
                                Response.Charset = "";
                                Response.ContentType = "application/text";
                                Response.Output.Write(txt);
                                Response.Flush();
                                Response.End();
                                Label2.Text = "Data Downloaded successfully !!!";
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Label2.Text = "Data not downloaded successfully !!!";
            }

        }

        protected void Importbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var linenumber = 0;
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
                    {
                        conn.Open();
                        using (StreamReader reader = new StreamReader(@"C:\Server\Download\SqlExportCountryMaster.txt"))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                if (linenumber != 0)
                                {
                                    var value = line.Replace("\t\t", ",");
                                Console.WriteLine(value);
                                    var values = value.Split(',');
                                    var sql = "Insert into demo.dbo.CountryMasterTextFile values('" + Convert.ToInt32(values[0]) + "','" + values[1] + "')";
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
                    }
               
                Label1.Text = "Data Imported successfully !!!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Label1.Text = "Data Not Imported successfully !!!";
            }
        }
    }
}