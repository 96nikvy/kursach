using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using System.IO;

namespace Organizer.Service.DataBase.Csv.Writing
{
    class WriterCsvDb : IWriterCsvDb
    {
        public bool WriteAllEventsToBd(IEnumerable<EventModelBase> events)
        {
            string pathToBd = PathFinder.GetPathToCsvDb();

            if (!File.Exists(pathToBd))
                throw new InvalidOperationException("bd file didn`t was found");

            StringBuilder builder = new StringBuilder();

            foreach (var z in events)
            {
                // Очищаем значения полей от знаков разделителей.
                z.Name = z.Name.Replace(';',' ');
                z.Name = z.Name.Replace('`', ' ');

                z.Description = z.Description == null ? string.Empty : z.Description;

                // Очищаем значения полей от знаков разделителей .
                z.Description = z.Description.Replace(';', ' ');
                z.Description = z.Description.Replace('`', ' ');

                builder.AppendFormat("name`{0};", z.Name);
                builder.AppendFormat("description`{0};", z.Description);
                builder.AppendFormat("dateplanned`{0};", z.DatePlanned.ToShortDateString());
                builder.AppendFormat("datecreated`{0};", z.DateCreated.ToShortDateString());

                builder.Append(Environment.NewLine);
            }

            File.WriteAllText(pathToBd, builder.ToString());

            return true;
        }
    }
}
