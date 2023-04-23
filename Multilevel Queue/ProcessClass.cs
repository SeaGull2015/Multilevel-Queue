using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Queue
{
	class Process
	{
		//private
		private int ID;
		private int startTime;
		private int CPUburst;
		private int leftOverTime;
		private int waitingTime;
		private int lastExec;
		private int finishTime;
		//public
		public Process(int time, int id)
		{
			ID = id;
			startTime = 0;
			CPUburst = time;
			leftOverTime = time;
			waitingTime = 0;
			lastExec = 0;
			finishTime = -1;
		}
		public Process(int time, int id, int starterTime)
		{
			ID = id;
			startTime = starterTime;
			CPUburst = time;
			leftOverTime = time;
			waitingTime = 0;
			lastExec = 0;
			finishTime = -1;
		}
		public Process(Process P)
		{
			ID = P.ID;
			startTime = P.startTime;
			CPUburst = P.CPUburst;
			leftOverTime = P.leftOverTime;
			waitingTime = P.waitingTime;
			lastExec = P.lastExec;
			finishTime = P.finishTime;
		}
		~Process()
		{
			// as for now empty
		}
		public void setStartTime(int gtime)
		{
			startTime = gtime;
		}
		public int execute(int time, int gtime)
		{
			waitingTime += gtime - lastExec;
			lastExec = gtime + time;
			if (time < leftOverTime)
			{
				leftOverTime -= time;
				return 0;
			}
			else
			{
				leftOverTime -= time;
				finishTime = gtime + time + leftOverTime;
				return -leftOverTime; // we passed too much time for some reason
			}
		}
		public int showTime()
		{
			return leftOverTime;
		}
		public int getFullTime()
		{
			if (finishTime == -1) return -1;
			return finishTime - startTime;
		}
		public int getID()
		{
			return ID;
		}
		public int getWaitingTime()
		{
			return waitingTime;
		}
		public int getStartTime()
		{
			return startTime;
		}
		public int getCPUBurst()
        {
			return CPUburst;
		}

		
	}
}
