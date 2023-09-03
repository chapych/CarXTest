using System;
using UnityEngine;

namespace Logic
{
    public class StopWatch
    {
        private readonly float interval;
        private readonly Action onIntervalPassed;
        private float nextShot;

        public StopWatch(float interval)
        {
            this.interval = interval;
        }

        public bool IsTimeForPeriodicAction()
        {
            if(IsPeriodOfTime())
                return false;
            nextShot += interval;
            return true;

            bool IsPeriodOfTime() => Time.time < nextShot;
        }
    }
}