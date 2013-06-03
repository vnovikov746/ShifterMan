using System;
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
    private bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
    private WorkersList allWorkersList = new WorkersList();
    private WorkersList goodWorkersList = new WorkersList();
    private ShiftTable shiftOptionsTable = new ShiftTable();
    private ShiftTable weeklyShiftTable = new ShiftTable();
    private GenerateTable GT;
    bool[,] selectedIndexes;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (isLogged)
        {
            string orgName = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
            orgNameLabel.Text = orgName;
            fillNamesList(orgName);
            fillGoodWorkersList(orgName);
            fillShiftOptionsTable(orgName);
            fillWeeklyShiftTable(orgName);
            fillTable(orgName);
            fillSelectedIndexes();
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    private void fillSelectedIndexes()
    {
        int rows = WeeklyScheduleGrid.Rows.Count;
        int col = WeeklyScheduleGrid.Columns.Count-1;
        this.selectedIndexes = new bool[rows,col];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
            {
                this.selectedIndexes[i, j] = false;
            }
        }
    }

    private void fillShiftOptionsTable(string org_name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT [Worker ID], [Day], [Begin Time], [End Time], [Organization Name], [Priority] FROM [Shift Options] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Shift shiftOption = new Shift(null, null, null, null, null, null, null, null);
                shiftOption.setWorker_ID(myReader[0].ToString().Trim());
                shiftOption.setDay(myReader[1].ToString().Trim());
                shiftOption.setBegin_Time(myReader[2].ToString().Trim());
                shiftOption.setEnd_Time(myReader[3].ToString().Trim());
                shiftOption.setOrganization(myReader[4].ToString().Trim());
                shiftOption.setPriority(myReader[5].ToString().Trim());
                shiftOptionsTable.AddShift(shiftOption);
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

    private void fillWeeklyShiftTable(string org_name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT [Day], [Begin Time], [End Time], [Shift Info] FROM [Shift Schedule] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                for(int i = 0; i < Convert.ToInt32(myReader[3]); i++)
                {
                    Shift shiftInWeek = new Shift(null, null, null, null, null, null, null, null);
                    shiftInWeek.setDay(myReader[0].ToString().Trim());
                    shiftInWeek.setBegin_Time(myReader[1].ToString().Trim());
                    shiftInWeek.setEnd_Time(myReader[2].ToString().Trim());
                    weeklyShiftTable.AddShift(shiftInWeek);
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
    
    private void fillNamesList(string org_name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT DISTINCT [ID], [First Name], [Last Name] FROM [Worker] WHERE [Organization Name] = '" + org_name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Worker allWorker = new Worker(null, null, null);
                allWorker.setWorker_ID(myReader[0].ToString().Trim());
                allWorker.setFirst_Name(myReader[1].ToString().Trim());
                allWorker.setLast_Name(myReader[2].ToString().Trim());
                allWorkersList.AddWorker(allWorker);
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

    private void fillGoodWorkersList(string org_Name)
    {
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "SELECT DISTINCT [Worker ID] FROM [Shift Options] WHERE [Organization Name] = '" + org_Name + "'";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Worker goodWorker = new Worker(null, null, null);
                goodWorker.setWorker_ID(myReader[0].ToString().Trim());
                goodWorkersList.AddWorker(goodWorker);
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
        for (int i = 0; i < allWorkersList.listSize(); i++)
        {
            for (int j = 0; j < goodWorkersList.listSize(); j++)
            {
                if (goodWorkersList.getWorkerFromList(j).getWroker_ID().Trim().Equals(allWorkersList.getWorkerFromList(i).getWroker_ID().Trim()))
                {
                    goodWorkersList.getWorkerFromList(j).setFirst_Name(allWorkersList.getWorkerFromList(i).getFirst_Name().Trim());
                    goodWorkersList.getWorkerFromList(j).setLast_Name(allWorkersList.getWorkerFromList(i).getLast_Name().Trim());
                }
            }
        }
        for(int i = 0; i < goodWorkersList.listSize(); i++)
        {
            string item = goodWorkersList.getWorkerFromList(i).getFirst_Name() + " " + goodWorkersList.getWorkerFromList(i).getLast_Name();
            goodWorkerDropDown.Items.Add(item);
        }
    }

    private string getConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.ConnectionStrings["ShifterManDB"].ConnectionString;
    }

    private void fillTable(string org_name)
    {
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

                int k = 0;
                while (k < allWorkersList.listSize())
                {
                    workers.Items.Add(new ListItem(allWorkersList.getWorkerFromList(k).getFirst_Name() + " " + allWorkersList.getWorkerFromList(k).getLast_Name()));
                    k++;
                }
                WeeklyScheduleGrid.Rows[i].Cells[j].Controls.Add(workers);
                Random ran = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
                int select = ran.Next(0, ((DropDownList)(WeeklyScheduleGrid.Rows[i].Cells[j].Controls[0])).Items.Count);
                ((DropDownList)(WeeklyScheduleGrid.Rows[i].Cells[j].Controls[0])).SelectedIndex = select;
            }
        }
    }

    protected void SubmitScheduleButton_Click(object sender, EventArgs e)
    {

    }

    protected void GenerateScheduleButton_Click(object sender, EventArgs e)
    {
        GT = new GenerateTable(shiftOptionsTable, weeklyShiftTable);
        weeklyShiftTable = GT.GenerateSchedule();
        setNames();
        foreach (Shift sh in weeklyShiftTable.GetAllShifts())
        {
            string day = sh.getDay().Trim();
            string begin = sh.getBegin_Time().Trim();
            string name = sh.getName().Trim();
            for(int i = 1; i < WeeklyScheduleGrid.Columns.Count; i++)
            {
                if (day.Equals(WeeklyScheduleGrid.Columns[i].HeaderText.Trim()))
                {
                    for (int j = 0; j < WeeklyScheduleGrid.Rows.Count; j++)
                    {
                        if (WeeklyScheduleGrid.Rows[j].Cells[0].Text.Split('-')[0].Trim().Equals(begin))
                        {
                            if (selectedIndexes[j, i] != true)
                            {
                                for (int k = 0; k < ((DropDownList)WeeklyScheduleGrid.Rows[j].Cells[i].Controls[0]).Items.Count; k++)
                                {
                                    if (((DropDownList)WeeklyScheduleGrid.Rows[j].Cells[i].Controls[0]).Items[k].Text.Trim().Equals(name) && ((DropDownList)WeeklyScheduleGrid.Rows[j].Cells[i].Controls[0]).ForeColor != System.Drawing.Color.Red)
                                    {
                                        ((DropDownList)WeeklyScheduleGrid.Rows[j].Cells[i].Controls[0]).SelectedIndex = k;
                                        ((DropDownList)WeeklyScheduleGrid.Rows[j].Cells[i].Controls[0]).ForeColor = System.Drawing.Color.Red;
                                        selectedIndexes[j, i] = true;
                                        break;
                                    }
                                }
                                break;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    private void setNames()
    {
        for (int i = 0; i < weeklyShiftTable.tableSize(); i++ )
        {
            for (int j = 0; j < goodWorkersList.listSize(); j++)
            {
                if (weeklyShiftTable.getShiftFromTable(i).getWroker_ID().Trim().Equals(goodWorkersList.getWorkerFromList(j).getWroker_ID().Trim()))
                {
                    weeklyShiftTable.getShiftFromTable(i).setName(goodWorkersList.getWorkerFromList(j).getFirst_Name().Trim() + " " + goodWorkersList.getWorkerFromList(j).getLast_Name().Trim());
                }
            }
        }
    }
}