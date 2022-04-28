using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatScheduleBot.DialogueNavigator.MessageHandling.Interfaces
{
    public interface IStatable
    {
        public StateBehaviour States { get; }
    }
}
