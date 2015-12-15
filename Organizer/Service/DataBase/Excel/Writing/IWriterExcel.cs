using Organizer.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Service.DataBase.Excel.Writing
{
    interface IWriterExcel
    {
        bool WriteEventsToExcel(IEnumerable<EventModelBase> events, string path = null);
    }
}
