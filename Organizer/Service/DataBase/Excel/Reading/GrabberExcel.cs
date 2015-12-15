using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;

namespace Organizer.Service.DataBase.Excel.Reading
{
    class GrabberExcel : IGrabberExcel
    {
        public IList<EventModelBase> GetItemsFromExcelFile()
        {
            List<EventModelBase> events = new List<EventModelBase>();

            for (int i = 0; i < 5; ++i)
                events.Add(new SimpleEventModel("Заглушка из GrabberExcel", "Заглушка из GrabberExcel", DateTime.Now));

            return events;
        }
    }
}
