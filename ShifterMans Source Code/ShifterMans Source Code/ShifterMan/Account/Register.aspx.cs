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
        string sql = "INSERT INTO Worker ([Organization Name], ID, Type, flagInfo, flagWorkers) VALUES ('" + organization_name + "','" + id + "','Manager','0','0')";

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
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
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT [Organization Name] FROM Worker";
        bool orgExist = false;

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                string OrgName = myReader.GetSqlString(0).Value;
                if (TxtOrganizationName.Text.Trim().Equals(OrgName))
                {
                    orgExist = true;
                }
            }
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

        if (!orgExist)
        {
            ExecuteInsert(TxtOrganizationName.Text, TxtID.Text);
            FormsAuthentication.SetAuthCookie(TxtOrganizationName.Text.Trim() + " Manager: " + TxtID.Text.Trim(), false /* createPersistentCookie */);

            Response.Redirect("~/Workers/ManagerInfo.aspx");
        }

        else
        {
            orgExistLabel.Visible = true;
            TxtOrganizationName.Text = "";
            TxtVerifyID.Text = "";
            TxtID.Text = "";
        }
    }
}
