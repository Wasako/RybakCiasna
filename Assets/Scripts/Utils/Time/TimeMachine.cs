using System;

namespace GameUtils.Time
{
    public class TimeMachine
    {
        private double time;

        public TimeMachine()
        {
            time = 0;
        }

        /// <summary>
        /// Adds time int this TimeMachine
        /// </summary>
        /// <param name="time">Amount of time to be added</param>
        public void Forward(double time) 
        {
            this.time += time;
        }

        /// <summary>
        /// Decreeses the amount of time in this TimeMashine by up to given amount
        /// </summary>
        /// <param name="maxTime"></param>
        /// <returns>Amount of time decressed</returns>
        public double Warp(double maxTime) 
        {
            double timeLeft = time - maxTime;
            time = Math.Max(timeLeft, 0);
            return Math.Min(maxTime + timeLeft, maxTime);
        }

        /// <summary>
        /// Attempts to decrese the amount of time in this TimeMachine <br/>
        /// If there is at least <paramref name="time"/> time accumulated in this machine subtructs that amount and returns true, otherwise returns false
        /// </summary>
        /// <param name="time"></param>
        public bool TryWarp(double time)
        {
            if (this.time >= time)
            {
                this.time -= time;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Result is equivalent to calling <see cref="TryWarp(double)"/> as many times as possible, but is faster fo larger <paramref name="limit"/> values
        /// </summary>
        /// <param name="interval">Single unit of warp time, must be non negative/param>
        /// <param name="limit">Maximum amount of warps, must be non negative</param>
        /// <returns>Amount of warps</returns>
        public int MultiWarp(double interval, int limit = int.MaxValue)
        {
            int result = (int)Math.Floor(time / interval);
            result = Math.Clamp(result, 0, limit);
            time -= result * interval;
            return result;
        }
    }
}
