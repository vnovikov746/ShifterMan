using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
     
        if (isLogged)
        {
            string type = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[1].Replace(':',' ').Trim();
            Response.Redirect("~/Workers/" + type + ".aspx");
        }
    }
}
