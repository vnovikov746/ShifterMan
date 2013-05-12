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
    String type;

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
        bool ok = true;
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
                type = Convert.ToString(reader[0]);
                type = type.Trim();

                FormsAuthentication.SetAuthCookie(organization_name.Trim() + " " + type + ": " + id, false /* createPersistentCookie */);
            }
            else
            {
                ok = false;
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

            if (ok)
            {
                redirectTo(organization_name, id);
            }
            else
            {
                Try_Again();
            }
        }
    }

    private void redirectTo(string organization_name, string id)
    {
        string flagIn;
        SqlConnection conn = new SqlConnection(getConnectionString());
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT flagInfo FROM Worker WHERE [Organization Name] = '" + organization_name + "' AND ID = '" + id + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            flagIn = Convert.ToString(reader[0]);
            flagIn = flagIn.Trim();
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

        if (flagIn.Equals("0"))
        {
            Response.Redirect("~/Workers/" + type + "Info.aspx");
        }
        else
        {
            Response.Redirect("~/Workers/" + type + ".aspx");
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
