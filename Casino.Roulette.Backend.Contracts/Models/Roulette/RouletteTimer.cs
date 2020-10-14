using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;
using static Casino.Roulette.Backend.Contracts.Delegates;

namespace Casino.Roulette.Backend.Contracts.Models.Roulette
{
    public class RouletteTimer
    {
        private Timer _timer;
        private long _interval;

        public VoidMethod Elapsed { get; set; }

        private Stopwatch _stopwatch;
        public long TimeLeft => _interval - _stopwatch.ElapsedMilliseconds;
        public RouletteTimer(long interval)
        {
            _interval = interval;
            _timer = new Timer()
            {
                AutoReset = false,
                Interval = _interval,
            }; 
            _timer.Elapsed += (sender, args) =>
            {
                Elapsed();
            };
            _stopwatch = new Stopwatch();
        }


        public void Start()
        {
            _timer.Start();
            _stopwatch.Restart();
        }

        public void Stop()
        {
            _timer.Stop();
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public void SetInterval(long interval)
        {
            _timer.Close();
            _timer.Interval = interval;
            _interval = interval;
        }
    }
}
