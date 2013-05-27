using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Worker
{
    private string worker_ID;
    private string first_Name;
    private string last_Name;

    public Worker(string worker_ID, string first_Name, string last_Name)
    {
        this.worker_ID = worker_ID;
        this.first_Name = first_Name;
        this.last_Name = last_Name;
    }

    public override string ToString() 
    {
        string s = "WORKER";
        s += " id: " + worker_ID;
        s += " name: " + first_Name;
        s += " last name: " + last_Name;
        return s;
    }

    public string getWroker_ID()
    {
        return this.worker_ID;
    }
    public void setWorker_ID(string worker_ID)
    {
        this.worker_ID = worker_ID;
    }
    public string getFirst_Name()
    {
        return this.first_Name;
    }
    public void setFirst_Name(string first_Name)
    {
        this.first_Name = first_Name;
    }
    public string getLast_Name()
    {
        return this.last_Name;
    }
    public void setLast_Name(string last_Name)
    {
        this.last_Name = last_Name;
    }
}
