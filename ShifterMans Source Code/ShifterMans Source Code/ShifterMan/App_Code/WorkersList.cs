using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WorkersList
{
    private List<Worker> workersList;

    public WorkersList()
    {
        this.workersList = new List<Worker>();
    }

    public void AddWorker(Worker w)
    {
        workersList.Add(w);
    }

    public List<Worker> GetWorkers()
    {
        return this.workersList;
    }

    public void RemoveWorker(Worker w)
    {
        workersList.Remove(w);
    }

    public Worker getWorkerFromList(int index)
    {
        return workersList.ElementAt(index);
    }

    public int listSize()
    {
        return workersList.Count;
    }
}
