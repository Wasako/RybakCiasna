﻿using System;

namespace GameUtils.Time
{
    public class AutoTimeMachine
    {
        public double Interval { get; set; }
        public Action Action { get; set; }
        
        private readonly TimeMachine machine;
        
        public AutoTimeMachine(Action action, double interval) 
        {
            Action = action;
            Interval = interval;
            machine = new TimeMachine();
        }

        /// <summary>
        /// Forwards the time by given amount, triggers assigned action relevant amount of times
        /// </summary>
        /// <param name="time">Amount of time to forward by</param>
        public void Forward(double time)
        {
            machine.Forward(time);
            int rolls = machine.MultiWarp(Interval);
            for (int i = 0; i < rolls; i++)
            { 
                Action();
            }
        }
    }
}
