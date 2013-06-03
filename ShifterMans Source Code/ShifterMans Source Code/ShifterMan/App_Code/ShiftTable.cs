using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShiftTable
{
    private List<Shift> shiftTable;
    public const int MINIMUM_REST_TIME = 8;
    public const int DAY_HOURS = 24;

    public ShiftTable()
    {
        this.shiftTable = new List<Shift>();
    }

    public ShiftTable(ShiftTable other)
    {
        this.shiftTable = new List<Shift>();
        foreach(Shift sh in other.GetAllShifts())
        {
            this.shiftTable.Add(sh);
        }
    }

    public override string ToString()
    {
        string s = "SHIFT LIST: \n";
        foreach (Shift sh in shiftTable)
        {
            s += "#" + shiftTable.IndexOf(sh).ToString() + ":";
            s += sh.ToString() + "\n";
        }
        return s;
    }

    public bool shiftExists(string day, string beginTime, string endTime)
    {
        foreach (Shift sh in shiftTable)
        {
            if (sh.getDay() == day && sh.getBegin_Time() == beginTime && sh.getEnd_Time() == endTime)
            {
                return true;
            }
        }
        return false;
    }

    public bool optionExists(Shift other)
    {
        foreach (Shift sh in shiftTable)
        {
            if (sh.getWroker_ID() == other.getWroker_ID() && sh.getDay() == other.getDay() && sh.getBegin_Time() == other.getBegin_Time() && sh.getEnd_Time() == other.getEnd_Time())
            {
                return true;
            }
        }
        return false;
    }

    public bool optionLegal(Shift option)
    {

        int optBeginHour = Convert.ToInt32(option.getBegin_Time().Trim().Split(':')[0]);
        int optEndHour = Convert.ToInt32(option.getEnd_Time().Trim().Split(':')[0]);
        string optBeginDay = option.getDay();
        string optEndDay = option.getDay();

        if (optBeginHour > optEndHour)
        {
            optEndDay = getNextDay(optEndDay);
        }

        optBeginHour -= MINIMUM_REST_TIME;
        optEndHour += MINIMUM_REST_TIME;

        if (optBeginHour < 0)
        {
            optBeginHour = optBeginHour + DAY_HOURS;
            optBeginDay = getPrevDay(optBeginDay);
        }

        if (optEndHour > DAY_HOURS)
        {
            optEndHour -= DAY_HOURS;
            optEndDay = getNextDay(optEndDay);
        }

        foreach (Shift sh in shiftTable)
        {
            if (sh.getWroker_ID() == option.getWroker_ID())
            {
                int shBeginHour = Convert.ToInt32(sh.getBegin_Time().Trim().Split(':')[0]);
                int shEndHour = Convert.ToInt32(sh.getEnd_Time().Trim().Split(':')[0]);
                string shBeginDay = sh.getDay();
                string shEndDay = sh.getDay();

                if (shBeginHour > shEndHour)
                {
                    shEndDay = getNextDay(shEndDay);
                }

                if (shBeginDay == optBeginDay)  //SHIFT AND OPTION START AT THE SAME DAY
                {
                    if (shBeginHour == optBeginHour) //shift and otions starts at the same time
                    {
                        return false;
                    }
                    if (shBeginHour > optBeginHour) //shift starts after option
                    {
                        if (shBeginDay == optEndDay && shBeginHour < optEndHour)
                        {
                            return false;
                        }
                        if (optBeginDay != optEndDay)
                        {
                            return false;
                        }
                    }
                    if (shBeginHour < optBeginHour) //shift starts before option
                    {
                        if (shEndDay == optBeginDay && shEndHour > optBeginHour)
                        {
                            return false;
                        }
                        if (shBeginDay != shEndDay)
                        {
                            return false;
                        }

                    }
                }

                if (shBeginDay == optEndDay)  //SHIFT STARTS AT THE DAY OPTION ENDS
                {
                    if (shBeginHour < optEndHour)
                    {
                        return false;
                    }
                }

                if (shEndDay == optBeginDay)  //SHIFT ENDS AT THE DAY OPTION STARTS
                {
                    if (shEndHour > optBeginHour)
                    {
                        return false;
                    }
                }

                if (optBeginDay == getPrevDay(shBeginDay) && optEndDay == getNextDay(shEndDay)) //SHIFT STARTS END ENDS WHILE OPTION LASTS
                {
                    return false;
                }
                
            }

        }
        return true;
    } //public bool optionLegal(Shift option) ...

    public string getNextDay(string Day)
    {
        switch (Day)
        {
            case "Sunday":      return "Monday";
            case "Monday":      return "Tuesday";
            case "Tuesday":     return "Wednesday";
            case "Wednesday":   return "Thursday";
            case "Thursday":    return "Friday";
            case "Friday":      return "Saturday"; 
        }
        return ""; //need to deal with next week
    }

    public string getPrevDay(string Day)
    {
        switch (Day)
        {
            case "Monday": return "Sunday";
            case "Tuesday": return "Monday";
            case "Wednesday": return "Tuesday";
            case "Thursday": return "Wednesday";
            case "Friday": return "Thursday";
            case "Saturday": return "Friday";
        }
        return ""; //need to deal with next week
    }

    public void AddShift(Shift s)
    {
        shiftTable.Add(s);
    }

    public List<Shift> GetAllShifts() //THIS METHOD RETURNS COPYIES OF ALL SHIFTS IN THE LIST
    {
        List<Shift> result = new List<Shift>();
        foreach (Shift sh in shiftTable)
        {
            Shift newShift = new Shift(sh.getWroker_ID(),sh.getName(),sh.getDay(),sh.getBegin_Time(),sh.getEnd_Time(),sh.getOrganization(),sh.getPriority(),sh.getNumOfWorkers());
            result.Add(newShift);
        }
        return result;
    }

    public void RemoveShift(Shift s)
    {
        shiftTable.Remove(s);
    }

    public void RemoveShift(string day, string beginTime, string endTime)
    {
        for (int i = 0; i < this.tableSize(); i++)
        {
            Shift sh = shiftTable[i];
            if (sh.getDay() == day && sh.getBegin_Time() == beginTime && sh.getEnd_Time() == endTime)
            {
                shiftTable.Remove(sh);
                break;
            }
        }
    }

    public Shift getShiftFromTable(int index)
    {
        return shiftTable.ElementAt(index);
    }

    public int tableSize()
    {
        return shiftTable.Count;
    }
}
