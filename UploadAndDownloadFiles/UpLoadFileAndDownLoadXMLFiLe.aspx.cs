using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadAndDownloadFiles
{
    public partial class UpLoadFileAndDownLoadXMLFiLe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM CountryMaster"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            ds.WriteXml(Server.MapPath("CountryMaster.xml"));
                        }
                    }
                    con.Close();
                }

                string msg = "Successfully Data Downloaded !!!";
                Label2.Text = "<a href=CountryMaster.xml> XML file</a>"+msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Label2.Text = "Data Not Downloaded Successfully !!!";
            }
        }
        protected void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    ds.ReadXml(Server.MapPath("~/CountryMaster.xml"));
                    DataTable dt = ds.Tables[0];
                    using (SqlBulkCopy bc = new SqlBulkCopy(conn))
                    {
                        bc.DestinationTableName = "CountryMasterXMLFile";
                        bc.ColumnMappings.Add("CountryId","CountryId");
                        bc.ColumnMappings.Add("CountryName", "CountryName");
                        bc.WriteToServer(dt);
                    }
                    conn.Close();
                }
                Label1.Text = " Data Successfully Imported !!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Label1.Text = " Data Not Successfully Imported !!";
            }
        }
    }
}