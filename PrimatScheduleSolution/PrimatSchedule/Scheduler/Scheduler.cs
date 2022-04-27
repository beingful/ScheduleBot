using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace PrimatScheduleBot
{
    public class Scheduler
    {
        private readonly string _name;
        private static IScheduler _scheduler;

        static Scheduler() => CreateScheduler();

        public Scheduler(string name) => _name = name;

        private static async void CreateScheduler()
        {
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await _scheduler.Start();
        }

        public async void Start<T>(TimeSpan time, object data = null) where T : IJob
        {
            StopIfExists();

            IJobDetail job = Job<T>(data);
            ITrigger trigger = Trigger(time);

            await _scheduler.ScheduleJob(job, trigger);
        }

        private async void StopIfExists()
        {
            bool doesExist = await IsSuchAJobExist();

            if (doesExist)
            {
                Stop();
            }
        }

        private IJobDetail Job<T>(object data = null) where T : IJob
        {
            var job = new Job<T>(_name, data);

            return job.Create();
        }

        private ITrigger Trigger(TimeSpan time)
        {
            var trigger = new Trigger(_name, time);

            return trigger.Create();
        }

        public void TryStop()
        {
            MessageValidator.ValidateQuartzStop(IsSuchAJobExist().Result);

            Stop();
        }

        private async void Stop()
        {
            var job = new JobKey(_name);

            await _scheduler.DeleteJob(job);
        }

        public async Task<bool> IsSuchAJobExist()
        {
            var job = new JobKey(_name);

            return await _scheduler.CheckExists(job);
        }
    }
}