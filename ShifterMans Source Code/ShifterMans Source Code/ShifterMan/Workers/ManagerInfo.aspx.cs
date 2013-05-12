using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Workers_ManagerInfo : System.Web.UI.Page
{
    private bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
    string redirect;

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!isLogged)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            string Company = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
            string ManagerID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[2].Trim();

            SqlConnection conn = new SqlConnection(getConnectionString());
            string sql = "SELECT flagWorkers FROM Worker WHERE ID = '" + ManagerID + "' AND Type = 'Manager' AND [Organization Name] = '" + Company + "'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                string flagWor = Convert.ToString(myReader[0]).Trim();
                if (flagWor.Equals("1"))
                {
                    redirect = "~/Workers/Manager.aspx";
                }
                else
                {
                    redirect = "~/Workers/WorkersIDs.aspx";
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

            sql = "SELECT flagInfo FROM Worker WHERE ID = '" + ManagerID + "' AND Type = 'Manager' AND [Organization Name] = '" + Company + "'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader myReader = cmd.ExecuteReader();
                myReader.Read();
                string flagIn = Convert.ToString(myReader[0]).Trim();
                if (flagIn.Equals("0"))
                {
                    CancelEditingButton.Visible = false;
                }
                else
                {
                    CancelEditingButton.Visible = true;
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
        }
    }

    private void Insert_Info(string firstName, string lastName, string password, string eMail, string ID)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "UPDATE Worker SET [First Name] = '" + firstName + "', [Last Name] = '" + lastName + "', Password = '" + password + "', Email = '" + eMail + "', flagInfo = '1' WHERE ID = '" + ID + "' AND Type = 'Manager'";

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
        string ManagerID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[2].Trim();
        Insert_Info(FirstName.Text, LastName.Text, Password.Text, Email.Text, ManagerID);

        Response.Redirect(redirect);
    }

    protected void CancelEditingButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Workers/Manager.aspx");
    }
}