using System;

namespace TheLastHope.Helpers
{
    /// <summary>
    /// Time counter
    /// </summary>
    public class Timer
    {
        private DateTime _start;
        private float _elapsed = -1;
        public float Elapsed { get { return _elapsed; } set { _elapsed = value; } }

        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// Timer start
        /// </summary>
        /// <param name="elapsed">Time to count</param>
        public void Start(float elapsed)
        {
            _elapsed = elapsed;
            _start = DateTime.Now;
            Duration = TimeSpan.Zero;
        }

		/// <summary>
		/// Timer main method.
		/// Counts time. When finished sets "true" to Finished.
		/// </summary>
		public void TimerUpdate()
        {
            if (_elapsed > 0)
            {
                Duration = DateTime.Now - _start;

                if (Duration.TotalSeconds > _elapsed)
                {
                    _elapsed = 0;
                }
            }
            else if (_elapsed == 0)
            {
                _elapsed = -1;
            }
        }
        /// <summary>
        /// Finished counting time.
        /// </summary>
        /// <returns>bool</returns>
        public bool Finished()
        {
            return _elapsed == 0;
        }

		/// <summary>
		/// The overall amount of seconds being count.
		/// </summary>
		/// <returns>int</returns>
		public int TotalSeconds()
        {
            return (int)(_elapsed - Duration.TotalSeconds);
        }
    }
}