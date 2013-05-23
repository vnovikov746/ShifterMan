using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateTable
/// </summary>
public class GenerateTable
{
    private ShiftTable generatedTable;
    private ShiftTable shiftOptionsTable;
    private ShiftTable highPriorityShifts;
    private ShiftTable lowPriorityShifts;

    public GenerateTable(ShiftTable shiftOptionsTable, ShiftTable weeklyShifts)
	{
        this.shiftOptionsTable = shiftOptionsTable;
        this.generatedTable = weeklyShifts;
        divideLowOrHigh();
    }

    private void divideLowOrHigh()
    {
        foreach (Shift shift in shiftOptionsTable.GetShifts())
        {
            if (shift.getPriority().Trim().Equals("Low"))
            {
                this.lowPriorityShifts.AddShift(shift);
            }
            else
            {
                this.highPriorityShifts.AddShift(shift);
            }
        }
    }

    public void generate()
    {

    }
}