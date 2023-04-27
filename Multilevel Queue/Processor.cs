﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Queue
{
    class Processor
    {
        public List<Priority> priorities; // gotta make it public to use it for listboxing
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
                log += string.Format("FCFS`ed process {0} in priority {1}, executed for/time left: {2}/{3}", priorities[priorityID].processes[0].getID(), priorityID, quant - excess, priorities[priorityID].processes[0].showTime());
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
                log += string.Format("RR`ed process {0} in priority {1}, executed for/time left: {2}/{3}", priorities[priorityID].processes[roundRobinPointer].getID(), priorityID, quant - excess, priorities[priorityID].processes[0].showTime());
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

        public Processor(int Quant)
        {
            quant = Quant;
            globalTime = 0;
            roundRobinPointer = 0;
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
    }
}
