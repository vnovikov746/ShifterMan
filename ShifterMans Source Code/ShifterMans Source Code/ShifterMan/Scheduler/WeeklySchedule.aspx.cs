﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Scheduler_WeeklySchedule : System.Web.UI.Page
{
    bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLogged)
        {
            string orgName = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
            orgNameLabel.Text = orgName;
            fillTable(orgName);
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
        DataColumn dcSunday = new DataColumn("Sunday", typeof(DropDownList));
        DataColumn dcMonday = new DataColumn("Monday", typeof(DropDownList));
        DataColumn dcTusday = new DataColumn("Tusday", typeof(DropDownList));
        DataColumn dcWednsday = new DataColumn("Wednsday", typeof(DropDownList));
        DataColumn dcThursday = new DataColumn("Thursday", typeof(DropDownList));
        DataColumn dcFriday = new DataColumn("Friday", typeof(DropDownList));
        DataColumn dcSaturday = new DataColumn("Saturday", typeof(DropDownList));

        dt.Columns.AddRange(new DataColumn[] { dcHourDay, dcSunday, dcMonday, dcTusday, dcWednsday, dcThursday, dcFriday, dcSaturday });

        int numOfWorkers = 0;
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT DISTINCT [Begin Time], [End Time], [Shift Info] FROM [Shift Schedule] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                numOfWorkers = Convert.ToInt16(myReader["Shift Info"].ToString().Trim());
                for (int i = 0; i < numOfWorkers; i++)
                {
                    dt.Rows.Add(new object[] { myReader["Begin Time"].ToString().Trim() + "-" + myReader["End Time"].ToString().Trim()/* + " -> " + myReader["Shift Info"].ToString().Trim() + " Workers In Shift"*/, null, null, null, null, null, null, null });
                }
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
        for (int i = 0; i < WeeklyScheduleGrid.Rows.Count; i++)
        {
            for (int j = 1; j < WeeklyScheduleGrid.Columns.Count; j++)
            {
                DropDownList workers = new DropDownList();

                sql = "SELECT DISTINCT [First Name], [Last Name] FROM [Worker] WHERE [Organization Name] = '" + org_name + "'";
                try
                {
                    conn = new SqlConnection(getConnectionString());
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {
                        string workerName = myReader.GetSqlString(0).Value + " " + myReader.GetSqlString(1).Value;
                        workers.Items.Add(new ListItem(workerName));
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
                WeeklyScheduleGrid.Rows[i].Cells[j].Controls.Add(workers);

                Random ran = new Random();
                int select = ran.Next(0, ((DropDownList)(WeeklyScheduleGrid.Rows[i].Cells[j].Controls[0])).Items.Count);
                ((DropDownList)(WeeklyScheduleGrid.Rows[i].Cells[j].Controls[0])).SelectedIndex = select;
            }
        }
    }
    protected void OrgNameList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string org_name = e.ToString().Trim();
        fillTable(org_name);
    }
    protected void SubmitScheduleButton_Click(object sender, EventArgs e)
    {

    }
    protected void GenerateScheduleButton_Click(object sender, EventArgs e)
    {

    }
}