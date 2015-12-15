using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Model.Events
{
    interface IEventModelBaseCreator
    {
        EventModelBase GetNewEvent(string name, string description, DateTime datePlanned);
        EventModelBase GetNewEvent(string name, string description, DateTime datePlanned, DateTime dateCreated);
    }
}
