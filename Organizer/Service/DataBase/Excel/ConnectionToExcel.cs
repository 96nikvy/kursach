using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using Organizer.Service.DataBase.Excel.Writing;
using Organizer.Service.DataBase.Excel.Reading;

namespace Organizer.Service.DataBase.Excel
{
    class ConnectionToExcel : IConnectionDb
    {
        #region Fields

        IGrabberExcel _grabber;
        IWriterExcel _writer;

        #endregion

        #region Ctor

        public ConnectionToExcel(IWriterExcel writer, IGrabberExcel grabber)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            if (grabber == null) throw new ArgumentNullException("grabber");

            _grabber = grabber;
            _writer = writer;
        }

        #endregion

        #region Public Properties

        IList<EventModelBase> _events;
        public IList<EventModelBase> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = _grabber.GetItemsFromExcelFile();

                    if (_events == null) throw new InvalidOperationException("events is null");
                }

                return _events;
            }
        }

        #endregion

        #region Public Methods

        public bool AddEvent(EventModelBase eventForAdding)
        {
            if (eventForAdding == null) throw new ArgumentNullException("eventForAdding");

            Events.Add(eventForAdding);

            return _writer.WriteEventsToExcel(Events);
        }

        public bool RemoveEvent(EventModelBase eventForRemoving)
        {
            if (eventForRemoving == null) throw new ArgumentNullException("eventForRemoving");

            if (Events.Contains(eventForRemoving))
            {
                Events.Remove(eventForRemoving);
                return _writer.WriteEventsToExcel(Events);
            }

            return false;
        }

        public bool UpdateEvent(EventModelBase eventForUpdate)
        {
            if (eventForUpdate == null) throw new ArgumentNullException("eventForUpdate");

            return _writer.WriteEventsToExcel(Events);
        }

        #endregion
    }
}
