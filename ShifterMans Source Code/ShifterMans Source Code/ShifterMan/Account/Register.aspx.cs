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
        string sql = "INSERT INTO Worker ([Organization Name], ID, Type) VALUES ('" + organization_name + "','" + id + "','Manager')";

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
        ExecuteInsert(TxtOrganizationName.Text, TxtID.Text);
        Response.Write("Record was successfully added!");

        SqlConnection conn = new SqlConnection(getConnectionString());
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Type FROM Worker WHERE [Organization Name] = '" + TxtOrganizationName.Text.Trim() + "' AND ID = '" + TxtID.Text.Trim() + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            int rowCount = dataTable.Rows.Count;
            reader = cmd.ExecuteReader();
            reader.Read();
            String type = Convert.ToString(reader[0]);
            type = type.Trim();

            FormsAuthentication.SetAuthCookie(type + ": " + TxtID.Text.Trim(), false /* createPersistentCookie */);
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

        Response.Redirect("~/Workers/ManagerInfo.aspx");
    }
}
