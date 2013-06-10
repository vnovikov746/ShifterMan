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
    private ShiftTable weeklyShiftTable = new ShiftTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLogged)
        {
            string orgName = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();

            fillWeeklyShiftTable(orgName);

            fillTable(orgName);
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    private void fillWeeklyShiftTable(string org_name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT [Day], [Begin Time], [End Time], [Shift Info], [Worker ID] FROM [Shift Schedule] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Shift shiftInWeek = new Shift(null, null, null, null, null, null, null, null);
                shiftInWeek.setDay(myReader[0].ToString().Trim());
                shiftInWeek.setBegin_Time(myReader[1].ToString().Trim());
                shiftInWeek.setEnd_Time(myReader[2].ToString().Trim());
                shiftInWeek.setWorker_ID(myReader[4].ToString().Trim());
                shiftInWeek.setOrganization(org_name);
                weeklyShiftTable.AddShift(shiftInWeek);
            }
            myReader.Close();
            if (weeklyShiftTable.getShiftFromTable(0).getWroker_ID() != null && weeklyShiftTable.getShiftFromTable(0).getWroker_ID() != "NULL")
            {
                for (int i = 0; i < weeklyShiftTable.tableSize(); i++)
                {
                    sql = "SELECT [First Name], [Last Name] FROM [Worker] WHERE [Organization Name] = '" + org_name + "' AND [ID] = '" + weeklyShiftTable.getShiftFromTable(i).getWroker_ID() + "'";
                    cmd = new SqlCommand(sql, conn);
                    myReader = cmd.ExecuteReader();
                    myReader.Read();
                    weeklyShiftTable.getShiftFromTable(i).setName(myReader[0].ToString().Trim() + " " + myReader[1].ToString().Trim());
                    myReader.Close();
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
//                dt.Rows.Add(new object[] { "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------", "----------------------" });
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
        fillWeeklySchedule();
    }

    private void fillWeeklySchedule()
    {
        int index = 0;
        if (weeklyShiftTable.getShiftFromTable(0).getName() != null)
        {
            foreach (Shift sh in weeklyShiftTable.GetAllShifts())
            {
                switch (sh.getDay())
                {
                    case "Sunday":
                        index = 1;
                        break;
                    case "Monday":
                        index = 2;
                        break;
                    case "Tusday":
                        index = 3;
                        break;
                    case "Wednsday":
                        index = 4;
                        break;
                    case "Thursday":
                        index = 5;
                        break;
                    case "Friday":
                        index = 6;
                        break;
                    case "Saturday":
                        index = 7;
                        break;
                }
                for (int i = 0; i < WeeklyScheduleGrid.Rows.Count; i++)
                {
                    if (WeeklyScheduleGrid.Rows[i].Cells[0].Text.Trim().Split('-')[0].Trim().Equals(sh.getBegin_Time()) &&
                        WeeklyScheduleGrid.Rows[i].Cells[0].Text.Trim().Split('-')[1].Trim().Equals(sh.getEnd_Time()) &&
                        WeeklyScheduleGrid.Rows[i].Cells[index].Text.Trim() == "&nbsp;")
                    {
                        WeeklyScheduleGrid.Rows[i].Cells[index].Text = sh.getName();
                        break;
                    }
                }
            }            
        }
    }
}