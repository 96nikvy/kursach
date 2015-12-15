using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using System.Windows;
using System.IO;
using Organizer.Service.DataBase.Csv.Reading;
using Organizer.Service.DataBase.Csv.Writing;

namespace Organizer.Service.DataBase.Csv
{
    class ConnectionToCsvDb : IConnectionDb
    {
        #region NonPublic Fields

        IGrabberCsvDb _grabber;
        IWriterCsvDb _writer;

        #endregion

        #region Constructor

        public ConnectionToCsvDb(IGrabberCsvDb grabber, IWriterCsvDb writer)
        {
            if (grabber == null) throw new ArgumentNullException("grabber");
            if (writer == null) throw new ArgumentNullException("writer");

            _grabber = grabber;
            _writer = writer;
        }

        #endregion

        #region Public Properites

        protected List<EventModelBase> _events;
        public IList<EventModelBase> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = _grabber.GetItemsFromCsvFile() as List<EventModelBase>;

                    if (_events == null) throw new InvalidOperationException("_events is null");

                    //_events.Add(new SimpleEventModel("a", "a", DateTime.Now));
                    //_events.Add(new SimpleEventModel("b", "b", DateTime.Now));
                    //_events.Add(new SimpleEventModel("c", "c", DateTime.Now));
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
            _writer.WriteAllEventsToBd(Events);

            return true;
        }

        public bool RemoveEvent(EventModelBase eventForRemoving)
        {
            if (eventForRemoving == null) throw new ArgumentNullException("eventForRemoving");
            if (_events == null) throw new InvalidOperationException("_events is null");

            if (_events.Contains(eventForRemoving))
                _events.Remove(eventForRemoving);

            _writer.WriteAllEventsToBd(Events);

            return true;
        }

        public bool UpdateEvent(EventModelBase eventForUpdate)
        {
            _writer.WriteAllEventsToBd(Events);
            return true;
        }

       
        #endregion
    }
}