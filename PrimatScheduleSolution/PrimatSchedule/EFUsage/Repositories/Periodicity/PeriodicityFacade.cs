using System;
using System.Linq;

namespace PrimatScheduleBot
{
    public class PeriodicityFacade : IDisposable
    {
        private readonly PeriodicityRepository _repository;

        public PeriodicityFacade() => _repository = new PeriodicityRepository();

        private Periodicity[] GetAllInArray() => _repository.GetAll().ToArray();

        public Guid GetDailyPeriodicityId() => GetAllInArray()[0].Id;

        public Guid GetWeeklyPeriodicityId() => GetAllInArray()[1].Id;

        public void Dispose() => _repository.Dispose();
    }
}
