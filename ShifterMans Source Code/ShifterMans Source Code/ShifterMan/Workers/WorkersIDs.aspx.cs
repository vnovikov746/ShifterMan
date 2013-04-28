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
    
    string ManagerID = System.Web.HttpContext.Current.User.Identity.Name;

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT DISTINCT Org_Name FROM Worker WHERE Wor_ID = '" + ManagerID + "' AND Wor_Type = 'Manager'";

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

    private void Insert_Info(string organization_name, string Type, string ID)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "INSERT INTO Organization (Org_Name, Wor_ID) VALUES ('" + organization_name + "','" + ID + "')";
        string sql2 = "INSERT INTO Worker (Org_Name, Wor_ID, Wor_Type) VALUES ('" + organization_name + "','" + ID + "','" + Type +"')";

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
    
    protected void AddWorkerButton_Click(object sender, EventArgs e)
    {
        Insert_Info(OrgNameList.Text, WorTypeList.Text, WorkerIDTxt.Text);
        Response.Redirect("~/Workers/WorkersIDs.aspx");
    }
    protected void DoneButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Workers/Manager.aspx");
    }
}