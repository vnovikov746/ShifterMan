using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Workers_EmployeeInfo : System.Web.UI.Page
{
    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Insert_Info(string firstName, string lastName, string password, string eMail, string ID)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "UPDATE Worker SET [First Name] = '" + firstName + "', [Last Name] = '" + lastName + "', Password = '" + password + "', Email = '" + eMail + "' WHERE ID = '" + ID + "'";

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

    protected void infoFinish_Click(object sender, EventArgs e)
    {
        String ID = System.Web.HttpContext.Current.User.Identity.Name;
        Insert_Info(FirstName.Text, LastName.Text, Password.Text, Email.Text, ID);
        Response.Redirect("~/Workers/Employee.aspx");
    }
}