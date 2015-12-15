using Organizer.Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ViewModel.Bridges
{
    class BridgeToBd : AbstractBridgeToBd
    {
        public BridgeToBd(IConnectionDb connection) : base(connection)
        { }
    }
}
