using Quartz;
using System;

namespace PrimatScheduleBot
{
    public class Trigger
    {
        private readonly string _name;
        private readonly TimeSpan _time;

        public Trigger(string name, TimeSpan time)
        {
            _name = name;
            _time = time;
        }

        public ITrigger Create()
        {
            return TriggerBuilder
                .Create()
                .WithIdentity(_name)
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(_time.Hours, _time.Minutes))
                .Build();
        }
    }
}