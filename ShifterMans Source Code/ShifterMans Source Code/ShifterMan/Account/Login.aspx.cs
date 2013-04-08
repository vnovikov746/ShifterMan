using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Account_Login : System.Web.UI.Page
{
    SqlConnection myconcection;

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        getConnection();
    }

    private void getConnection()
    {
        String connstring = "Data Source=KAKIFISH;Initial Catalog=ShifterMan;Integrated Security=True";//your connection string
        SqlConnection myconcection = new SqlConnection(connstring);
    }

    // method to get data
    public DataSet getData(String sql)
    {
        getConnection();
        SqlDataAdapter adptre = new SqlDataAdapter();
        DataSet resultSet = new DataSet();
        SqlCommand sqlcmd = new SqlCommand(sql, myconcection);
        adptre.SelectCommand = sqlcmd;
        myconcection.Open();
        try
        {
            adptre.Fill(resultSet);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        myconcection.Close();
        return resultSet;
    }
    // method to add data
    public void setData(string sqlcmd)
    {
        getConnection();
        myconcection.Open();
        string sqlcommand = sqlcmd;
        SqlCommand cmd = new SqlCommand(sqlcommand, myconcection);
        cmd.ExecuteNonQuery();
        myconcection.Close();
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string userName = LoginUser.UserName;
        string password = LoginUser.Password;

        bool result = UserLogin(userName, password);
        if ((result))
        {
        }
        else
        {
        }
/*        // if there is no returnUrl in the query string , we redirect based on user role
        if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
        {
            // please don't use User.IsInRole here , because it will not be populated yet at this stage.
            if (Roles.IsUserInRole(Login1.UserName, "Admins"))
                Response.Redirect("~/Admins/Default.aspx");
            else if (Roles.IsUserInRole(Login1.UserName, "Editors"))
                Response.Redirect("~/Editors/Default.aspx");
        }*/
    }

    private bool UserLogin(string userName, string password)
    {

        // read the coonection string from web.config 
//        string conString = ConfigurationManager.ConnectionStrings["Data Source=KAKIFISH;Initial Catalog=ShifterMan;Integrated Security=TrueingName"].ConnectionString;

        using (myconcection)
        {
            //' declare the command that will be used to execute the select statement 
            SqlCommand com = new SqlCommand("SELECT Wor_UserName FROM Worker WHERE Wor_Name = @UserName AND Wor_Password = @Password", myconcection);

            // set the username and password parameters
            com.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            com.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

//            myconcection.Open();
            //' execute the select statment 
//            string result = Convert.ToString(com.ExecuteScalar());
            //' check the result 
//            if (string.IsNullOrEmpty(result))
//            {
                //invalid user/password , return flase 
 //               return false;
   //         }
     //       else
       //     {
                // valid login
                return true;
           // }
        }
    }
}
