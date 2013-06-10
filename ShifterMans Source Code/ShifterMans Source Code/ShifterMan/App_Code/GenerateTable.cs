using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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

    private void GenerateScheduleRandomPass(ShiftTable OptionsTable, ShiftTable weeklyShifts, ShiftTable schedule)
    {
        Random rnd = new Random();
        int optionsNum = OptionsTable.tableSize();
        while (optionsNum > 0)
        {
            int i = rnd.Next(0, optionsNum);
            Shift option = OptionsTable.getShiftFromTable(i);

            if (weeklyShifts.shiftExists(option.getDay(), option.getBegin_Time(), option.getEnd_Time()))
            {
                if (schedule.optionLegal(option))
                {
                    schedule.AddShift(option);
                    weeklyShifts.RemoveShift(option.getDay(), option.getBegin_Time(), option.getEnd_Time());
                }
            }
            OptionsTable.RemoveShift(option);
            optionsNum--;
        } //while (optionsNum > 0)...

    } //private ShiftTable GenerateScheduleRandomPass(ShiftTable OptionsTable, ShiftTable weeklyShifts, ShiftTable schedule) ... 


    private ShiftTable GenerateSchedule(ShiftTable OptionsTable, ShiftTable schedule)
    {
        ShiftTable weeklyShifts = new ShiftTable(this.weeklyShifts);

        GenerateScheduleRandomPass(OptionsTable, weeklyShifts, schedule);

        bool done = false;
        bool shiftAdded = false;
        

        int optionsNum = OptionsTable.tableSize();


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