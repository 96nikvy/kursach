using Organizer.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Service.DataBase
{
    interface IConnectionDb
    {
        /// <summary>
        ///  для поддержки
        /// </summary>
        IList<EventModelBase> Events { get; }
        bool RemoveEvent(EventModelBase eventForRemoving);
        bool AddEvent(EventModelBase eventForAdding);
        bool UpdateEvent(EventModelBase eventForUpdate);
    }
}
