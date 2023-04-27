using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Queue
{
    class Processor
    {
        private List<Priority> priorities; // gotta make it public to use it for listboxing
        private List<Priority> oldPriorities; // to get data later. Gotta clear it though, some time maybe.
        private int quant;
        private int globalTime;
        private int roundRobinPointer;
        private string log;
        private bool isStepping = false;

        private void FCFS(int priorityID) // solves a priority for 1 quant
        {
            int excess = 0;
            do
            {
                excess = priorities[priorityID].processes[0].execute(excess == 0 ? quant : excess, globalTime);
                globalTime += quant - excess;
                log += string.Format("FCFS`ed process {0} in priority {1}, executed for/time left: {2}/{3}\n", priorities[priorityID].processes[0].getID(), priorityID, quant - excess, priorities[priorityID].processes[0].showTime());
                if (priorities[priorityID].processes[0].showTime() == 0) 
                {
                    oldPriorities[priorityID].processes.Add(priorities[priorityID].processes[0]);
                    priorities[priorityID].processes.RemoveAt(0);
                }
            } while (excess != 0);
        }

        private void RoundRobin(int priorityID)
        {
            int excess = 0;
            if (priorities[priorityID].processes.Count() < 0)
            {
                return;
            }
            do
            {
                excess = priorities[priorityID].processes[roundRobinPointer].execute(excess == 0 ? quant : excess, globalTime);
                globalTime += quant - excess;
                log += string.Format("RR`ed process {0} in priority {1}, executed for/time left: {2}/{3}\n", priorities[priorityID].processes[roundRobinPointer].getID(), priorityID, quant - excess, priorities[priorityID].processes[0].showTime());
                if (priorities[priorityID].processes[roundRobinPointer].showTime() <= 0)
                {
                    oldPriorities[priorityID].processes.Add(priorities[priorityID].processes[roundRobinPointer]);
                    priorities[priorityID].processes.RemoveAt(roundRobinPointer);
                }
                else
                {
                    roundRobinPointer++;
                    if (roundRobinPointer >= priorities[priorityID].processes.Count()) roundRobinPointer = 0;
                }
            } while (excess > 0 && priorities[priorityID].processes.Count() > 0); // if we finish all tasks within a priority we will lose excess time forever
            if (priorities[priorityID].processes.Count() < 0) roundRobinPointer = 0;
        }

        public Processor(int Quant)
        {
            quant = Quant;
            globalTime = 0;
            roundRobinPointer = 0;
            priorities = new List<Priority>();
            oldPriorities = new List<Priority>();
        }

        public void setQuant(int Quant)
        {
            quant = Quant;
        }

        public int getQuant()
        {
            return quant;
        }

        public List<Priority> GetPriorities()
        {
            return priorities;
        }

        public List<string> GetListPriorities()
        {
            List<string> l = new List<string>();
            foreach(Priority p in priorities)
            {
                l.Add("Priority " + p.getID());
            }
            return l;
        }

        public List<string> GetListProcesses(int PriorityID)
        {
            List<string> l = new List<string>();
            foreach (Process p in priorities[PriorityID])
            {
                l.Add(string.Format("Process(id/time) {0};{1}", p.getID(), p.showTime()));
            }
            return l;
        }

        public void AddPriority(string Type)
        {
            priorities.Add(new Priority(Type, priorities.Count()));
            oldPriorities.Add(new Priority(Type, priorities.Count()));
        }

        public void AddProcess(int priorityID, int ProcessID, int CPUBurst)
        {
            priorities[priorityID].processes.Add(new Process(CPUBurst, ProcessID, globalTime));
        }

        public void RemoveLastPriority()
        {
            priorities.RemoveAt(priorities.Count() - 1);
            oldPriorities.RemoveAt(priorities.Count() - 1);
        }

        public bool step()
        {
            for (int i = 0; i < priorities.Count(); i++)
            {
                if (priorities[i].processes.Any()) 
                {
                    if (priorities[i].getType() == "fcfs") // Switch doesn't compile for some reason in .net 7.3
                    {
                        FCFS(i);
                        return true;
                    }
                    else if(priorities[i].getType() == "round robin")
                    {
                        RoundRobin(i);
                        return true;
                    }
                    else
                    {
                        throw new Exception("failed to step into a priority, bc the type is wrong");
                    }
                }
            }
            return false;
        }

        public void run()
        {
            isStepping = true; // we wanna lock our quant while we are running
            while (step()) ;
            logResults();
            isStepping = false;
        }

        public bool isStep()
        {
            return isStepping;
        }

        public string getLog()
        {
            string tlog = log;
            log = "";
            return tlog;
        }

        public void logResults()
        {
            List<double> l = getFullMeanWorkTime();
            for (int i = 0; i < l.Count(); i++)
            {
                log += string.Format("Mean Full Work Time (priority {0}): {1}\n", i, l[i]);
            }
            l = getFullMeanWaitTime();
            for (int i = 0; i < l.Count(); i++)
            {
                log += string.Format("Mean Full Wait Time (priority {0}): {1}\n", i, l[i]);
            }
        }

        public List<double> getFullMeanWorkTime()
        {
            List<double> l = new List<double>();
            foreach (Priority prior in oldPriorities)
            {
                int sum = 0;
                int counter = 0;
                foreach (Process proc in prior)
                {
                    sum += proc.getFullTime();
                    counter++;
                }
                l.Add(Convert.ToDouble(sum) / counter);
            }
            return l;
        }

        public List<double> getFullMeanWaitTime()
        {
            List<double> l = new List<double>();
            foreach (Priority prior in oldPriorities)
            {
                int sum = 0;
                int counter = 0;
                foreach (Process proc in prior)
                {
                    sum += proc.getWaitingTime();
                    counter++;
                }
                l.Add(Convert.ToDouble(sum) / counter);
            }
            return l;
        }
    }
}
