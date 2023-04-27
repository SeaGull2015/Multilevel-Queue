using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Queue
{
    class Priority
    {
        private string type;
        private int id;
        public List<Process> processes;

        public Priority(string Type, int ID)
        {
            Type = Type.ToLower();
            if (Type != "fcfs" && Type != "round robin")
            {
                throw new Exception("priority exception");
            }
            type = Type;
            id = ID;
            processes = new List<Process>();
        }

        public int getID()
        {
            return id;
        }

        public string getType()
        {
            return type;
        }

        public override string ToString() 
        { 
            return "Prioirty " + Convert.ToString(id); 
        }

        public IEnumerator<Process> GetEnumerator()
        {
            return processes.GetEnumerator();
        }
    }

}

