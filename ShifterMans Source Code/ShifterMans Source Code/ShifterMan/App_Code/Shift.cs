using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shift
{
    private string worker_ID;
    private string name;
    private string day;
    private string begin_Time;
    private string end_Time;
    private string organization;
    private string priority;
    private string week;
    private string numOfWorkers;

    public Shift(string worker_ID, string name, string day, string begin_Time, string end_Time, string organization, string priority, string numOfWorkers)
    {
        this.worker_ID = worker_ID;
        this.name = name;
        this.day = day;
        this.begin_Time = begin_Time;
        this.end_Time = end_Time;
        this.organization = organization;
        this.priority = priority;
        this.numOfWorkers = numOfWorkers;
    }

    public override string ToString()
    {
        string s = "SHIFT";
        s += " id: " + worker_ID;
        s += " name: " + name;
        s += " day: " + day;
        s += " begin time: " + begin_Time;
        s += " end time: " + end_Time;
        s += " org: " + organization;
        s += " priority: " + priority;
        s += " week: " + week;
        s += " num of workers: " + numOfWorkers;
        return s;
    }

    public int getShiftHours()
    {
        int begin_Time = Convert.ToInt32(this.begin_Time.Trim().Split(':')[0]);
        int end_Time = Convert.ToInt32(this.end_Time.Trim().Split(':')[0]);

        if (begin_Time <= end_Time)
        {
            return end_Time - begin_Time;
        }
        else
        {
            return (24 - begin_Time) + end_Time;
        }
    }

    public string getName()
    {
        return this.name;
    }
    public void setName(string name)
    {
        this.name = name;
    }
    public string getWroker_ID()
    {
        return this.worker_ID;
    }
    public void setWorker_ID(string worker_ID)
    {
        this.worker_ID = worker_ID;
    }
    public string getDay()
    {
        return this.day;
    }
    public void setDay(string day)
    {
        this.day = day;
    }
    public string getBegin_Time()
    {
        return this.begin_Time;
    }
    public void setBegin_Time(string begin_Time)
    {
       this.begin_Time = begin_Time;
    }
    public string getEnd_Time()
    {
        return this.end_Time;
    }
    public void setEnd_Time(string end_Time)
    {
       this.end_Time = end_Time;
    }
    public string getOrganization()
    {
       return this.organization;
    }
    public void setOrganization(string organization)
    {
        this.organization = organization;
    }
    public string getPriority()
    {
        return this.priority;
    }
    public void setPriority(string priority)
    {
        this.priority = priority;
    }
    public string getWeek()
    {
        return this.week;
    }
    public void setWeek(string week)
    {
        this.week = week;
    }
    public string getNumOfWorkers()
    {
        return this.numOfWorkers;
    }
    public void setNumOfWorkers(string numOfWorkers)
    {
        this.numOfWorkers = numOfWorkers;
    }
}
