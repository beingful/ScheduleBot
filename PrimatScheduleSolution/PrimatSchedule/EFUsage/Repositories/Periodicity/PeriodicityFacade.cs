using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class PeriodicityFacade : IDisposable
    {
        private readonly PeriodicityRepository _repository;

        public PeriodicityFacade() => _repository = new PeriodicityRepository();

        private List<Periodicity> GetAllInArray() => _repository.GetAll().ToList();

        public Guid GetDailyPeriodicityId() => GetAllInArray()[0].Id;

        public Guid GetWeeklyPeriodicityId() => GetAllInArray()[1].Id;

        public void Dispose() => _repository.Dispose();
    }
}
