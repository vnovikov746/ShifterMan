using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    //Input:
  
    //1. shiftOptionsTable  
    //"SELECT [Worker ID], [Day], [Begin Time], [End Time], [Organization Name], [Priority] FROM [Shift Options] WHERE [Organization Name] 
    //private string worker_ID;
    //private string day;
    //private string begin_Time;
    //private string end_Time;
    //private string organization;
    //private string priority;

    //2. weeklyShifts
    //"SELECT DISTINCT [Day], [Begin Time], [End Time], [Shift Info] FROM [Shift Schedule] WHERE [Organization Name] 

    //3. workersList
    //"SELECT DISTINCT [ID], [First Name], [Last Name] FROM [Worker] WHERE [Organization Name]

    //Output:
    
    //1. weeklySchedule


public class GenerateTable
{
    private ShiftTable shiftOptionsTable;
    private ShiftTable weeklyShifts;

    private ShiftTable highPriorityOptions;
    private ShiftTable lowPriorityOptions;


    public GenerateTable(ShiftTable shiftOptionsTable, ShiftTable weeklyShifts)
	{
        this.shiftOptionsTable = shiftOptionsTable;
        this.weeklyShifts = weeklyShifts;

        this.highPriorityOptions = new ShiftTable();
        this.lowPriorityOptions = new ShiftTable();
        divideLowOrHigh();

    }

    public ShiftTable GenerateSchedule()
    {
        ShiftTable schedule = new ShiftTable();
        GenerateSchedule(this.highPriorityOptions, schedule);
        GenerateSchedule(this.lowPriorityOptions, schedule);
        return schedule;
    }


    public ShiftTable GenerateSchedule(ShiftTable OptionsTable, ShiftTable schedule)
    {
        ShiftTable weeklyShifts = new ShiftTable(this.weeklyShifts);

        bool done = false;
        bool shiftAdded = false;
        

        int optionsNum = OptionsTable.tableSize();
        int shiftsNum = weeklyShifts.tableSize();

        while (!done)
        {
            shiftAdded = false;
            for (int i = 0; i < optionsNum; i++)
            {
                Shift option = OptionsTable.getShiftFromTable(i);
                if (weeklyShifts.shiftExists(option.getDay(), option.getBegin_Time(), option.getEnd_Time()))
                {
                    if (schedule.optionLegal(option))
                    {
                        schedule.AddShift(option);
                        OptionsTable.RemoveShift(option);
                        optionsNum--;
                        weeklyShifts.RemoveShift(option.getDay(), option.getBegin_Time(), option.getEnd_Time());
                        shiftsNum--;
                        shiftAdded = true;
                        break;
                    }

                }
            } // for ()
            if (shiftAdded == false)
            {
                done = true;
            }
            
        }
        return schedule;
    } //public ShiftTable GenerateSchedule()

    private void divideLowOrHigh()
    {
        foreach (Shift shift in shiftOptionsTable.GetAllShifts())
        {
            if (shift.getPriority().Trim().Equals("Low"))
            {
                this.lowPriorityOptions.AddShift(shift);
            }
            else
            {
                this.highPriorityOptions.AddShift(shift);
            }
        }
    }


    public ShiftTable getHighPriorityOptions()
    {
        return this.highPriorityOptions;
    }

    public ShiftTable getLowPriorityOptions()
    {
        return this.lowPriorityOptions;
    }

}