
using System;

using Unity.Mathematics;

namespace Templates
{
    public struct Timer
    {
        public TimeSpan Duration;
        public TimeSpan Elapsed;
        public TimeSpan Remaining => Duration - Elapsed;
        public float Percent => (float)math.clamp(Elapsed.TotalMilliseconds / Duration.TotalSeconds, 0, 1);
        public bool IsFinished => Elapsed >= Duration;

        public Timer(float seconds, float elapsed = 0)
        {
            Duration = TimeSpan.FromSeconds(seconds);
            this.Elapsed = TimeSpan.FromSeconds(elapsed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">Delta time in seconds</param>
        public void Update(double dt)
        {
            Elapsed += TimeSpan.FromSeconds(dt);
        }

        public void SoftReset()
        {
            Elapsed -= Duration;
        }

        public void HardReset()
        {
            Elapsed = default;
        }
    }
}