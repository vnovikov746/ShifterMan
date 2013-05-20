using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Scheduler_ShiftOptions : System.Web.UI.Page
{
    private bool isLogged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

    protected void Page_Init(object sender, EventArgs e)
    {
        load();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private void load()
    {
        if (isLogged)
        {
            string ManagerID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[2].Trim();
            SqlConnection conn = new SqlConnection(getConnectionString());
            string sql = "SELECT DISTINCT [Organization Name] FROM Worker WHERE ID = '" + ManagerID + "' AND Type = 'Employee'";

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
        DataTable dt = new DataTable();

        DataColumn dcHourDay = new DataColumn("HourDay", typeof(string));
        DataColumn dcSunday = new DataColumn("Sunday", typeof(ButtonField));
        DataColumn dcMonday = new DataColumn("Monday", typeof(ButtonField));
        DataColumn dcTusday = new DataColumn("Tusday", typeof(ButtonField));
        DataColumn dcWednsday = new DataColumn("Wednsday", typeof(ButtonField));
        DataColumn dcThursday = new DataColumn("Thursday", typeof(ButtonField));
        DataColumn dcFriday = new DataColumn("Friday", typeof(ButtonField));
        DataColumn dcSaturday = new DataColumn("Saturday", typeof(ButtonField));

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
                dt.Rows.Add(new object[] { myReader["Begin Time"].ToString().Trim() + "-" + myReader["End Time"].ToString().Trim() });
            }
            ShiftOptionsGrid.DataSource = dt;
            ShiftOptionsGrid.DataBind();
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

    protected void ShiftOptionsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        int col = 0;
        for (int i = 1; i < ShiftOptionsGrid.Columns.Count; i++)
        {
            if (ShiftOptionsGrid.Columns[i].HeaderText.Trim().Equals(e.CommandName.Trim()))
            {
                col = i;
            }
        }
        if (((Button)ShiftOptionsGrid.Rows[index].Cells[col].Controls[0]).BackColor == System.Drawing.Color.Empty)
        {
            ((Button)ShiftOptionsGrid.Rows[index].Cells[col].Controls[0]).BackColor = System.Drawing.Color.Indigo;
        }
        else if (((Button)ShiftOptionsGrid.Rows[index].Cells[col].Controls[0]).BackColor == System.Drawing.Color.Indigo)
        {
            ((Button)ShiftOptionsGrid.Rows[index].Cells[col].Controls[0]).BackColor = System.Drawing.Color.Fuchsia;
        }
        else
        {
            ((Button)ShiftOptionsGrid.Rows[index].Cells[col].Controls[0]).BackColor = System.Drawing.Color.Empty;
        }
    }

    protected void SubmitShiftsButton_Click(object sender, EventArgs e)
    {
        if (isLogged)
        {
            string EmployeeID = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[2].Trim();
            string organization = System.Web.HttpContext.Current.User.Identity.Name.Split(' ')[0].Trim();
            for (int i = 0; i < ShiftOptionsGrid.Rows.Count; i++)
            {
                for (int j = 1; j < ShiftOptionsGrid.Columns.Count; j++)
                {
                    if (((Button)ShiftOptionsGrid.Rows[i].Cells[j].Controls[0]).BackColor == System.Drawing.Color.Indigo)
                    {
                        addOption(organization, EmployeeID, ShiftOptionsGrid.Rows[i].Cells[0].Text.Trim(), ShiftOptionsGrid.Columns[j].HeaderText.Trim(), "High");
                    }
                    else if (((Button)ShiftOptionsGrid.Rows[i].Cells[j].Controls[0]).BackColor == System.Drawing.Color.Fuchsia)
                    {
                        addOption(organization, EmployeeID, ShiftOptionsGrid.Rows[i].Cells[0].Text.Trim(), ShiftOptionsGrid.Columns[j].HeaderText.Trim(), "Low");
                    }
                }
            }
            Response.Redirect("~/Workers/Employee.aspx");
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }

    }

    private void addOption(string organization, string EmployeeID, string time, string day, string priority)
    {
        string begin = time.Split('-')[0].Trim();
        string end = time.Split('-')[1].Trim();
        SqlConnection conn = new SqlConnection(getConnectionString());
        string sql = "INSERT INTO [Shift Options] ([Worker ID], [Organization Name], [Begin Time], [End Time], Day, Priority) VALUES ('" + EmployeeID + "','" + organization + "','" + begin + "','" + end + "','" + day + "','" + priority +"')";

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
}