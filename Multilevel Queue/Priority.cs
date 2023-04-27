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
        private uint id;
        public List<Process> processes;

        public Priority(string Type, uint ID)
        {
            Type = Type.ToLower();
            if (Type != "fcfs" || Type != "rr")
            {
                throw new Exception("priority exception");
            }
            type = Type;
            id = ID;
        }

        public uint getID()
        {
            return id;
        }

        public string getType()
        {
            return type;
        }
    }
}
