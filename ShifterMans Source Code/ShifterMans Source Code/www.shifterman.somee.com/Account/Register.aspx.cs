using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Account_Register : System.Web.UI.Page
{
    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void ExecuteInsert(string organization_name, string id)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "INSERT INTO Organization (Org_Name, Wor_ID) VALUES ('" + organization_name + "','" + id + "')";
        string sql2 = "INSERT INTO Worker (Org_Name, Wor_ID, Wor_Type) VALUES ('" + organization_name + "','" + id + "','Manager')";

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlCommand cmd2 = new SqlCommand(sql2, conn);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }

    protected void SignUp_Click(object sender, EventArgs e)
    {
        ExecuteInsert(TxtOrganizationName.Text, TxtID.Text);
        Response.Write("Record was successfully added!");
        FormsAuthentication.SetAuthCookie(TxtID.Text, false /* createPersistentCookie */);
        Response.Redirect("~/Workers/ManagerInfo.aspx");
    }
}
