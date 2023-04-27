using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Queue
{
    class Processor
    {
        private List<Priority> priorities;
        private List<Priority> oldPriorities; // to get data later. Gotta clear it though, some time maybe.
        private int quant;
        private int globalTime;
        private int roundRobinPointer;

        private void FCFS(int priorityID) // solves a priority for 1 quant
        {
            int excess = 0;
            do
            {
                excess = priorities[priorityID].processes[0].execute(excess == 0 ? quant : excess, globalTime);
                globalTime += quant - excess;
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
            do
            {
                excess = priorities[priorityID].processes[roundRobinPointer].execute(excess == 0 ? quant : excess, globalTime);
                globalTime += quant - excess;
                if (priorities[priorityID].processes[roundRobinPointer].showTime() == 0)
                {
                    oldPriorities[priorityID].processes.Add(priorities[priorityID].processes[roundRobinPointer]);
                    priorities[priorityID].processes.RemoveAt(roundRobinPointer);
                }
                else
                {
                    roundRobinPointer++;
                }
            } while (excess != 0);
        }

        Processor(int Quant)
        {
            quant = Quant;
            globalTime = 0;
            roundRobinPointer = 0;
        }

        public void setQuant(int Quant)
        {
            quant = Quant;
        }

        public List<Priority> GetPriorities()
        {
            return priorities;
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
                    else if(priorities[i].getType() == "rr")
                    {
                        RoundRobin(i);
                        return true;
                    }
                    else
                    {
                        throw new Exception("failed to step a priority, bc the type is wrong");
                    }
                }
            }
            return false;
        }
        public void run()
        {
            while (step()) ;
        }
    }
}
