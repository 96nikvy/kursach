using System;
using GalaSoft.MvvmLight;
using Organizer.Service.DataBase;
using System.Windows;
using Organizer.ViewModel.Bridges;
using System.Collections.ObjectModel;
using Organizer.Model.Events;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Collections.Generic;
using Organizer.View;
using System.Diagnostics;
using Organizer.Service.DataBase.Csv;
using System.Linq;
using Organizer.Service.DataBase.Excel.Writing;

namespace Organizer.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        #region NonPublic Fields

        protected AbstractBridgeToBd _bridge;
        protected IWriterExcel _writerExcel;

        #endregion
        
        #region Constructor

        public MainViewModel(AbstractBridgeToBd bridge, IWriterExcel writerExcel)
        {
            if (bridge == null) throw new ArgumentNullException("bridge");
            if (writerExcel == null) throw new ArgumentNullException("writerExcel");

            _writerExcel = writerExcel;
            _bridge = bridge;

            _bridge.ChangingEvents += () =>
            {
                OnChangingEvents();
            };

            Events.Sort();

            ShowTodayEventsWindow todayEvents = new ShowTodayEventsWindow();
            todayEvents.ShowDialog();
        }

        #endregion

        #region Events

        private void OnChangingEvents()
        {
            List<EventModelBase> listForRemoving = new List<EventModelBase>();

            foreach (var z in _events)
            {
                if (!_bridge.Events.Contains(z))
                    listForRemoving.Add(z);
            }

            foreach (var z in listForRemoving)
                _events.Remove(z);

            foreach (var z in _bridge.Events)
                if (!_events.Contains(z))
                    _events.Add(z);

            _events.Sort();
        }

        #endregion

        #region Public Properties

        protected ObservableCollection<EventModelBase> _events;
        public ObservableCollection<EventModelBase> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new ObservableCollection<EventModelBase>();

                    foreach (var z in _bridge.Events)
                        _events.Add(z);
                }

                return _events;
            }
        }

        #endregion

        #region Commands

        protected RelayCommand<SelectionChangedEventArgs> _selectionChanged;
        public RelayCommand<SelectionChangedEventArgs> SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                {
                    _selectionChanged = new RelayCommand<SelectionChangedEventArgs>(selectionChangedExecute);
                }
                return _selectionChanged;
            }
        }

        private void selectionChangedExecute(SelectionChangedEventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");

            if (e.AddedItems.Count != 0)
            {
                EventModelBase myEvent = e.AddedItems[0] as EventModelBase;
                _bridge.CurrentEvent = myEvent;
            }
            else
            {
                _bridge.CurrentEvent = new SimpleEventModel("No Event is selected", "", DateTime.Now);
                
                // если вызвать команду "Удалить" или "Сохранить",
                // то на данном событие оно не сработает, пометили как бы виртуальное.
                _bridge.CurrentEvent.ID = -1;
            }
        }

        protected RelayCommand _butAddNewEvent;
        public RelayCommand ButAddNewEvent
        {
            get
            {
                if (_butAddNewEvent == null)
                {
                    _butAddNewEvent = new RelayCommand(butAddNewEventExecute);
                }
                return _butAddNewEvent;
            }
        }

        private void butAddNewEventExecute()
        {
            AddingNewEventWindow newWindow = new AddingNewEventWindow();
            newWindow.ShowDialog();
        }

        protected RelayCommand _butSaveExcel;
        public RelayCommand ButSaveExcel
        {
            get
            {
                if (_butSaveExcel == null)
                {
                    _butSaveExcel = new RelayCommand(butSaveExcelExecute);
                }
                return _butSaveExcel;
            }
        }

        private void butSaveExcelExecute()
        {
            _writerExcel.WriteEventsToExcel(Events);
        }

        #endregion

    }

    /// <summary>
    /// чтобы сортануть коллекцию в возрастающем порядке по дате создания.
    /// </summary>
    static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}