using Quartz;

namespace PrimatScheduleBot
{
    public class Job<T> where T : IJob
    {
        private readonly string _name;
        private readonly object _data;

        public Job(string name, object data)
        {
            _name = name;
            _data = data;
        }

        private IJobDetail GetJob() => JobBuilder.Create<T>().WithIdentity(_name).Build();

        private void SetJobData(IJobDetail job) => job.JobDataMap.Add(nameof(_data), _data);

        public IJobDetail Create() 
        {
            IJobDetail job = GetJob();

            if (_data != null)
            {
                SetJobData(job);
            }

            return job;
        }
    }
}