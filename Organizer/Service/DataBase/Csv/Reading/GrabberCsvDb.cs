using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using System.IO;

namespace Organizer.Service.DataBase.Csv.Reading
{
    class GrabberCsvDb : IGrabberCsvDb
    {
        #region NonPublic Fields

        protected IRowParserCsv _parser;

        #endregion

        #region Ctor

        public GrabberCsvDb (IRowParserCsv parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");

            _parser = parser;
        }

        #endregion

        #region Public Methods

        public IList<EventModelBase> GetItemsFromCsvFile()
        {
            string path = PathFinder.GetPathToCsvDb();

            if (path == null) throw new InvalidOperationException("path");
            if (!File.Exists(path)) throw new InvalidOperationException("path doesn't exist");

            StreamReader reader = new StreamReader(path);
            List<EventModelBase> events = new List<EventModelBase>();

            while (reader.Peek() >= 0)
            {
                string content = reader.ReadLine();

                EventModelBase newEvent = _parser.ParseRow(content);

                if (newEvent != null)
                    events.Add(newEvent);
            }

            return events;
        }

        #endregion
    }
}