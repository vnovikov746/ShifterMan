using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Workers_Manager : System.Web.UI.Page
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
                    if (!OrgNameList3.Items.Contains(new ListItem(OrgName)))
                    {
                        OrgNameList3.Items.Add(new ListItem(OrgName));
                    }
                }
                OrgNameList3.SelectedValue = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
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

            fillTable(OrgNameList3.SelectedItem.Value.ToString().Trim());
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

    private void fillTable(string org_name)
    {
        //
        // We Need To Add here the information about the workers
        //
        DataTable dt = new DataTable();

        DataColumn dcHourDay = new DataColumn("HourDay", typeof(string));
        DataColumn dcSunday = new DataColumn("Sunday", typeof(string));
        DataColumn dcMonday = new DataColumn("Monday", typeof(string));
        DataColumn dcTusday = new DataColumn("Tusday", typeof(string));
        DataColumn dcWednsday = new DataColumn("Wednsday", typeof(string));
        DataColumn dcThursday = new DataColumn("Thursday", typeof(string));
        DataColumn dcFriday = new DataColumn("Friday", typeof(string));
        DataColumn dcSaturday = new DataColumn("Saturday", typeof(string));

        dt.Columns.AddRange(new DataColumn[] { dcHourDay, dcSunday, dcMonday, dcTusday, dcWednsday, dcThursday, dcFriday, dcSaturday });

        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT DISTINCT [Begin Time], [End Time], [Shift Info] FROM [Shift Schedule] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                int numOfWorkers = Convert.ToInt16(myReader["Shift Info"].ToString().Trim());
                for (int i = 0; i < numOfWorkers; i++)
                {
                    dt.Rows.Add(new object[] { myReader["Begin Time"].ToString().Trim() + "-" + myReader["End Time"].ToString().Trim()/* + " -> " + myReader["Shift Info"].ToString().Trim() + " Workers In Shift"*/, "", "", "", "", "", "", "" });
                }
                dt.Rows.Add(new object[] { "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------" });
            }
            WeeklyScheduleGrid.DataSource = dt;
            WeeklyScheduleGrid.DataBind();
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
    protected void OrgNameList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string org_name = e.ToString().Trim();
        fillTable(org_name);
    }
}