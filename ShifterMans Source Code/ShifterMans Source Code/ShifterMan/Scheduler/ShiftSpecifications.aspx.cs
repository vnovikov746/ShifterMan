using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Scheduler_ShiftSpecifications : System.Web.UI.Page
{
    string ManagerID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[1].Trim();

    protected void Page_Load(object sender, EventArgs e)
    {
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
                if (!OrgNameList2.Items.Contains(new ListItem(OrgName)))
                {
                    OrgNameList2.Items.Add(new ListItem(OrgName));
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

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    protected void AddShiftButton_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string day = DayDropDown.Text.Trim();
        string begin = BeginHourDropDown.Text.Trim() + ":" + BeginMinDropDown.Text.Trim();
        string end = EndHourDropDown.Text.Trim() + ":" + EndMinDropDown.Text.Trim();
        string info = ShiftNameTxt.Text.Trim();
        string org_name = OrgNameList2.Text.Trim();

        string sql = "INSERT INTO [Shift Schedule] ([Organization Name], Day, [Begin Time], [End Time], [Shift Info]) VALUES ('" + org_name + "','" + day + "','" + begin + "','" + end + "','" + info + "')";

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
            Response.Redirect("~/Scheduler/ShiftSpecifications.aspx");
        }
    }
    protected void RemoveShiftButton_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string day = DayDropDown.Text.Trim();
        string begin = BeginHourDropDown.Text.Trim() + ":" + BeginMinDropDown.Text.Trim();
        string end = EndHourDropDown.Text.Trim() + ":" + EndMinDropDown.Text.Trim();
        string info = ShiftNameTxt.Text.Trim();

        string sql = "DELETE FROM [Shift Schedule] WHERE Day =  '" + day + "' AND [Begin Time] =  '" + begin + "' AND [End Time] = '" + end + "'";

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
            Response.Redirect("~/Scheduler/ShiftSpecifications.aspx");
        }

    }
}