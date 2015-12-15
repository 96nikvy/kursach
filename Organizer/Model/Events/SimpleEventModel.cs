using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Model.Events
{
    class SimpleEventModel : EventModelBase
    {
        public SimpleEventModel(string name, string description, DateTime datePlanned) 
            : base(name,description,datePlanned)
        { }

        public SimpleEventModel(string name, string description, DateTime datePlanned, DateTime dateCreated) 
            : base(name, description, datePlanned, dateCreated)
        { }



    }
}
