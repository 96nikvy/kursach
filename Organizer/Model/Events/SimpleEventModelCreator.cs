using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Model.Events
{
    class SimpleEventModelCreator : IEventModelBaseCreator
    {
        public EventModelBase GetNewEvent(string name, string description, DateTime datePlanned)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (description == null) throw new ArgumentNullException("description");

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("description");

            return new SimpleEventModel(name, description, datePlanned);
        }

        public EventModelBase GetNewEvent(string name, string description, DateTime datePlanned, DateTime dateCreated)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (description == null) throw new ArgumentNullException("description");

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("description");

            return new SimpleEventModel(name, description, datePlanned, dateCreated);
        }
    }
}
