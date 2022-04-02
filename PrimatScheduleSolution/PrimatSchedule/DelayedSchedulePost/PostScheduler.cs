using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace PrimatScheduleBot
{
    public class PostScheduler
    {
        private static IScheduler _scheduler;

        public static async void Start(string token, string chatId, TimeSpan time)
        {
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await _scheduler.Start();

            IJobDetail job = CreateJob(token, chatId);
            ITrigger trigger = CreateTrigger(chatId, time);

            await _scheduler.ScheduleJob(job, trigger);
        }

        private static IJobDetail CreateJob(string token, string chatId)
        {
            return JobBuilder.Create<PostSender>()
                .WithIdentity(chatId)
                .UsingJobData(nameof(token), token)
                .UsingJobData(nameof(chatId), Convert.ToString(chatId))
                .Build();
        }

        private static ITrigger CreateTrigger(string chatId, TimeSpan time)
        {
            return TriggerBuilder.Create()
                .WithIdentity(chatId)
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(time.Hours, time.Minutes))
                .Build();
        }

        public static void TryStop(string chatId)
        {
            if (!IsSuchAJobExist(chatId).Result)
            {
                throw new QuartzStopException();
            }

            Stop(chatId);
        }

        private static async void Stop(string chatId)
        {
            await _scheduler.DeleteJob(new JobKey(chatId));
        }

        public static async Task<bool> IsSuchAJobExist(string chatId)
        {
            if (_scheduler != null)
            {
                return await _scheduler.CheckExists(new JobKey(chatId));
            }

            return false;
        }
    }
}
