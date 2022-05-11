using System;

namespace PrimatScheduleBot
{
    public interface IStartable
    {
        public void Start(TimeSpan time, string token);
    }
}
