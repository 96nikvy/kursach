using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Organizer.Model.Events;
using Organizer.ViewModel.Bridges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Organizer.ViewModel 
{
    class EventEditorViewModel : ViewModelBase
    {
        #region NonPublic Fields

        protected BridgeToBd _bridge;

        #endregion
        
        #region Constructor

        public EventEditorViewModel(BridgeToBd bridge)
        {
            if (bridge == null) throw new ArgumentNullException("bridge");
            _bridge = bridge;

            _bridge.ChangingEvent += OnChangingEvent;
            _bridge.ChangingEvents += () =>
            {
                RaisePropertyChanged("Events");
            };
        }

        #endregion

        #region Public Properties

        private bool flafVirtualEvent = false;
        public bool EventSelected
        {
            get
            {
                if (_bridge.CurrentEvent == null || _bridge.CurrentEvent.ID == -1) return false;
                return flafVirtualEvent;
            }
        }

        public bool EventIsNotSelected
        {
            get
            {
                return !EventSelected;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private DateTime _datePlanned;
        public DateTime DatePlanned
        {
            get
            {
                return _datePlanned;
            }
            set
            {
                _datePlanned = value;
                RaisePropertyChanged("DatePlanned");
            }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                _dateCreated = value;
                RaisePropertyChanged("DateCreated");
            }
        }

        #endregion

        #region Commands

        private RelayCommand _butSave;
        public RelayCommand ButSave
        {
            get
            {
                if (_butSave == null)
                {
                    _butSave = new RelayCommand(butSaveExecute);
                }
                return _butSave;
            }
        }

        private void butSaveExecute()
        {
            if (_bridge == null) throw new InvalidOperationException("_bridge is null");

            if (_bridge.CurrentEvent != null)
            {
                _bridge.CurrentEvent.Name = Name;
                _bridge.CurrentEvent.Description = Description;
                _bridge.CurrentEvent.DateCreated = DateCreated;
                _bridge.CurrentEvent.DatePlanned = DatePlanned;

                _bridge.UpdateEvent(_bridge.CurrentEvent);
            }
        }

        private RelayCommand _butDelete;
        public RelayCommand ButDelete
        {
            get
            {
                if (_butDelete == null)
                {
                    _butDelete = new RelayCommand(butDeleteExecute);
                }
                return _butDelete;
            }
        }

        private void butDeleteExecute()
        {
            if (_bridge == null) throw new InvalidOperationException("_bridge is null");

            if (_bridge.CurrentEvent != null)
                _bridge.RemoveEvent(_bridge.CurrentEvent);
        }

        #endregion

        #region Events

        private void OnChangingEvent(EventModelBase myEvent)
        {
            if (myEvent == null) throw new ArgumentNullException("myEvent");

            Name = myEvent.Name;
            Description = myEvent.Description;
            DatePlanned = myEvent.DatePlanned;
            DateCreated = myEvent.DateCreated;

            if (myEvent.Name == "No Event is selected")
                flafVirtualEvent = false;
            else
                flafVirtualEvent = true;

            RaisePropertyChanged("EventSelected");
            RaisePropertyChanged("EventIsNotSelected");
        }

        #endregion
    }
}
