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
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManConnectionString"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
//        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }

    private void ExecuteInsert(string organization_name, string id)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "INSERT INTO Organization (Org_Name, Wor_ID) VALUES "
                   + " (@Org_name,@Wor_ID)";

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Org_Name", SqlDbType.NVarChar, 50);
            param[1] = new SqlParameter("@Wor_ID", SqlDbType.NVarChar, 50);

            param[0].Value = organization_name;
            param[1].Value = id;

            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

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

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        FormsAuthentication.SetAuthCookie(TxtOrganizationName.Text, false /* createPersistentCookie */);

        string continueUrl = "~/Workers/Manager.aspx";
        if (String.IsNullOrEmpty(continueUrl))
        {
            continueUrl = "~/Workers/Manager.aspx";
        }
        Response.Redirect(continueUrl);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TxtID.Text == TxtVerifyID.Text)
        {
            //call the method to execute insert to the database
            ExecuteInsert(TxtOrganizationName.Text, TxtID.Text);
            Response.Write("Record was successfully added!");
            ClearControls(Page);
            RegisterUser_CreatedUser(sender, e);
        }
        else
        {
            Response.Write("Password did not match");
            TxtID.Focus();
        }
    }

    public static void ClearControls(Control Parent)
    {

        if (Parent is TextBox)
        { (Parent as TextBox).Text = string.Empty; }
        else
        {
            foreach (Control c in Parent.Controls)
                ClearControls(c);
        }
    }
}
