using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using System.Windows;

namespace Organizer.Service.DataBase.Csv.Reading
{
    class RowParserCsv : IRowParserCsv
    {
        public EventModelBase ParseRow(string content)
        {
            if (content == null) throw new ArgumentNullException("content");

            // ; используется для разделения в файле
            var list = content.Split(';');

            string name, description, dateP, dateC;
            name = description = dateC = dateP = null;

            DateTime datePlanned, dateCreated;
            datePlanned = dateCreated = DateTime.Now;

            foreach (var z in list)
            {
                // Тильда используется как разделитель Ключ-Значение
                var pair = z.Split('`');

                if (pair.Count() != 2)
                {
                    continue;
                }
                else if (pair[0].ToLower() == "datecreated")
                {
                    dateC = pair[1];

                    if (!(DateTime.TryParse(pair[1], out dateCreated)))
                    {
                        return null;
                    }
                }
                else if (pair[0].ToLower() == "dateplanned")
                {
                    dateP = pair[1];

                    if (!(DateTime.TryParse(pair[1], out datePlanned)))
                    {
                        return null;
                    }
                }
                else if (pair[0].ToLower() == "name")
                {
                    name = pair[1];
                }
                else if (pair[0].ToLower() == "description")
                {
                    description = pair[1];
                }
            }

            if (name == null || dateP == null || dateC == null)
                return null;

            description = description == null ? string.Empty : description;

            return new SimpleEventModel(name, description, datePlanned, dateCreated);
        }
    }
}
