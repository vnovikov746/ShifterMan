using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Account_Login : System.Web.UI.Page
{
    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void VerifyUser(string organization_name, string id)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Type FROM Worker WHERE [Organization Name] = '" + organization_name + "' AND ID = '" + id + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            int rowCount = dataTable.Rows.Count;
            if (rowCount != 0)
            {
                reader = cmd.ExecuteReader();
                reader.Read();
                String type = Convert.ToString(reader[0]);
                type = type.Trim();

                FormsAuthentication.SetAuthCookie(type + ": " + id , false /* createPersistentCookie */);

                Response.Redirect("~/Workers/" + type + ".aspx");
            }
            else
            {
                Try_Again();
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Validation Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }

    protected void Try_Again()
    {
        NoUserLiteral.Visible = true;
        TxtID1.Text = "";
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        VerifyUser(OrgNameList.Text, TxtID1.Text);
    }
}
