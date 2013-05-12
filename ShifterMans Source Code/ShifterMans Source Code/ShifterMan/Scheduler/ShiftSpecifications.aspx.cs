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
    bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

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
                    if (!OrgNameList2.Items.Contains(new ListItem(OrgName)))
                    {
                        OrgNameList2.Items.Add(new ListItem(OrgName));
                    }
                }
                OrgNameList2.SelectedValue = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
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

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    private void addShift(string day, string begin, string end, string info, string org_name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
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
        }

    }

    protected void AddShiftButton_Click(object sender, EventArgs e)
    {
        string begin = BeginHourDropDown.Text.Trim() + ":" + BeginMinDropDown.Text.Trim();
        string end = EndHourDropDown.Text.Trim() + ":" + EndMinDropDown.Text.Trim();
        string info = NumOfWorList.Text.Trim();
        string org_name = OrgNameList2.Text.Trim();

        for (int i = 0; i < DayList.Items.Count; i++)
        {
            if (DayList.Items[i].Selected)
            {
                string day = DayList.Items[i].Text.Trim();
                addShift(day, begin, end, info, org_name);
            }
        }
        Response.Redirect("~/Scheduler/ShiftSpecifications.aspx");
    }

    private void removeShift(string day, string begin, string end, string info)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "DELETE FROM [Shift Schedule] WHERE Day =  '" + day + "' AND [Begin Time] =  '" + begin + "' AND [End Time] = '" + end + "' AND [Shift Info] = '" + info + "'";

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

    protected void RemoveShiftButton_Click(object sender, EventArgs e)
    {
        string begin = BeginHourDropDown.Text.Trim() + ":" + BeginMinDropDown.Text.Trim();
        string end = EndHourDropDown.Text.Trim() + ":" + EndMinDropDown.Text.Trim();
        string info = NumOfWorList.Text.Trim();

        for (int i = 0; i < DayList.Items.Count; i++)
        {
            if (DayList.Items[i].Selected)
            {
                string day = DayList.Items[i].Text.Trim();
                removeShift(day, begin, end, info);
            }
        }
        Response.Redirect("~/Scheduler/ShiftSpecifications.aspx");
    }
    protected void SelectAllButton_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < DayList.Items.Count; i++)
        {
            DayList.Items[i].Selected = true;
        }
    }
}