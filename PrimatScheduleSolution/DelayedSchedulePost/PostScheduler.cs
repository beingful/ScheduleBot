using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace DelayedSchedulePost
{
    public static class PostScheduler
    {
        private static IScheduler _scheduler;

        public static async Task<bool> Start(string token, long chatId)
        {
            if (!IsSuchAJobExist(chatId).Result)
            {
                _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await _scheduler.Start();

                IJobDetail job = CreateJob(token, chatId);
                ITrigger trigger = CreateTrigger(chatId);

                await _scheduler.ScheduleJob(job, trigger);

                return true;
            }

            return default(bool);
        }

        private static IJobDetail CreateJob(string token, long chatId)
        {
            return JobBuilder.Create<PostSender>()
                .WithIdentity($"{chatId}")
                .UsingJobData(nameof(token), token)
                .UsingJobData(nameof(chatId), Convert.ToString(chatId))
                .Build();
        }

        private static ITrigger CreateTrigger(long chatId)
        {
            return TriggerBuilder.Create()
                .WithIdentity($"{chatId}")
                .StartAt(DateTime.Now.AddSeconds(10))
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
        }

        public static async Task<bool> Stop(long chatId)
        {
            if (IsSuchAJobExist(chatId).Result)
            {
                return await _scheduler.DeleteJob(new JobKey($"{chatId}"));
            }

            return default(bool);
        }

        public static async Task<bool> IsSuchAJobExist(long chatId)
        {
            if (_scheduler != default(IScheduler))
            {
                return await _scheduler.CheckExists(new JobKey($"{chatId}"));
            }

            return default(bool);
        }
    }
}
