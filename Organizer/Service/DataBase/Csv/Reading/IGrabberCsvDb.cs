﻿using Organizer.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Service.DataBase.Csv.Reading
{
    interface IGrabberCsvDb
    {
        IList<EventModelBase> GetItemsFromCsvFile(); 
    }
}
