using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
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
    class AddingNewEventViewModel : ViewModelBase
    {
        #region NonPublic Fields

        BridgeToBd _bridge;

        #endregion

        #region Ctor

        public AddingNewEventViewModel(BridgeToBd bridge)
        {
            if (bridge == null) throw new ArgumentNullException("bridge");

            _bridge = bridge;

            DatePlanned = DateTime.Now;
        }

        #endregion

        #region Public Properties

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
            get { return _datePlanned; }
            set
            {
                _datePlanned = value;
                RaisePropertyChanged("DatePlanned");
            }
        }

        #endregion

        #region Commands

        private RelayCommand<MetroWindow> _butSave;
        public RelayCommand<MetroWindow>  ButSave
        {
            get
            {
                if (_butSave == null)
                {
                    _butSave = new RelayCommand<MetroWindow>(butSaveExecute);
                }
                return _butSave;
            }
        }

        private void butSaveExecute(MetroWindow myWindow)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Пустое имя");
                return;
            }

            _bridge.AddEvent(new SimpleEventModel(Name, Description, DatePlanned));
            myWindow.Close();

            // Чтобы при повторном открытии приложения не сохрнились старые значения снесем эти
            Name = null;
            Description = null;
            DatePlanned = DateTime.Now;
        }

        #endregion

    }
}
