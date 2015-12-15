using GalaSoft.MvvmLight;
using Organizer.Model.Events;
using Organizer.ViewModel.Bridges;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ViewModel
{
    class ShowTodayEventsViewModel : ViewModelBase
    {
        #region NonPublic Fields

        BridgeToBd _bridge;

        #endregion

        #region Ctor

        public ShowTodayEventsViewModel(BridgeToBd bridge)
        {
            if (bridge == null) throw new ArgumentNullException("bridge");

            _bridge = bridge;
        }

        #endregion

        #region Public Properties

        private ObservableCollection<EventModelBase> _events;
        public ObservableCollection<EventModelBase> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new ObservableCollection<EventModelBase>();

                    foreach (var z in _bridge.Events)
                    {
                        if (z.DatePlanned.ToShortDateString() == DateTime.Today.ToShortDateString())
                            _events.Add(z);
                    }

                }

                return _events;
            }
        }

        public bool ShowEventsToday
        {
            get
            {
                return Events.Count != 0 ? true : false;
            }
        }

        public bool ShowMessageNoEvents
        {
            get
            {
                return Events.Count == 0 ? true : false;
            }
        }

        #endregion
    }
}
