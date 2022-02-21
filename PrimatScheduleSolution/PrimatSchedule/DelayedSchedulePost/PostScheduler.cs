using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace PrimatScheduleBot
{
    public static class PostScheduler
    {
        private static IScheduler _scheduler;

        public static async void TryStart(string token, string chatId, DateTime time)
        {
            if (!IsSuchAJobExist(chatId).Result)
            {
                _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await _scheduler.Start();

                IJobDetail job = CreateJob(token, chatId);
                ITrigger trigger = CreateTrigger(chatId, time);

                await _scheduler.ScheduleJob(job, trigger);
            }

            else
            {
                throw new QuartzStartException();
            }
        }

        private static IJobDetail CreateJob(string token, string chatId)
        {
            return JobBuilder.Create<PostSender>()
                .WithIdentity(chatId)
                .UsingJobData(nameof(token), token)
                .UsingJobData(nameof(chatId), Convert.ToString(chatId))
                .Build();
        }

        private static ITrigger CreateTrigger(string chatId, DateTime time)
        {
            return TriggerBuilder.Create()
                .WithIdentity(chatId)
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(time.Hour, time.Minute))
                .Build();
        }

        public static async void TryStop(string chatId)
        {
            if (IsSuchAJobExist(chatId).Result)
            {
                await _scheduler.DeleteJob(new JobKey(chatId));
            }

            else
            {
                throw new QuartzStopException();
            }
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
