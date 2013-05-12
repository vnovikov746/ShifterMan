using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Workers_WorkersIDs : System.Web.UI.Page
{
    bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLogged)
        {
            string ManagerID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[2].Trim();
            SqlConnection conn = new SqlConnection(getConnectionString());
            string sql = "SELECT DISTINCT [Organization Name] FROM Worker WHERE ID = '" + ManagerID + "' AND Type = 'Manager'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string OrgName = myReader.GetSqlString(0).Value;
                    if (!OrgNameList.Items.Contains(new ListItem(OrgName)))
                    {
                        OrgNameList.Items.Add(new ListItem(OrgName));
                    }
                }
                OrgNameList.SelectedValue = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
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
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    private void Insert_Info(string organization_name, string Type, string ID)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "INSERT INTO Worker ([Organization Name], ID, Type, flagInfo) VALUES ('" + organization_name + "','" + ID + "','" + Type +"','0')";

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
    
    protected void AddWorkerButton_Click(object sender, EventArgs e)
    {
        Insert_Info(OrgNameList.Text, WorTypeList.Text, WorkerIDTxt.Text);
        Response.Redirect("~/Workers/WorkersIDs.aspx");
    }
    protected void DoneButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Workers/Manager.aspx");
    }

    protected void RemoveWorButton_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "DELETE FROM Worker WHERE ID = '" + WorkerIDTxt.Text.Trim() + "' AND [Organization Name] = '" + System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim() + "'";

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
            Response.Redirect("~/Workers/WorkersIDs.aspx");
        }
    }
}