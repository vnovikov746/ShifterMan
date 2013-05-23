using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShiftTable
{
    private List<Shift> shiftTable;

    public ShiftTable()
    {
        this.shiftTable = new List<Shift>();
    }

    public void AddShift(Shift s)
    {
        shiftTable.Add(s);
    }

    public List<Shift> GetShifts()
    {
        return this.shiftTable;
    }

    public void RemoveShift(Shift s)
    {
        shiftTable.Remove(s);
    }

    public int tableSize()
    {
        return shiftTable.Count;
    }
}
