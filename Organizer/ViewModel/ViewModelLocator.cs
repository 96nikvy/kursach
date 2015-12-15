using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Organizer.Service.DataBase;
using Organizer.Service.DataBase.Csv;
using Organizer.Service.DataBase.Csv.Reading;
using Organizer.Service.DataBase.Csv.Writing;
using Organizer.Service.DataBase.Excel;
using Organizer.Service.DataBase.Excel.Reading;
using Organizer.Service.DataBase.Excel.Writing;
using Organizer.ViewModel.Bridges;
using System;
using System.Windows;

namespace Organizer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // для работы с CSV базой
            SimpleIoc.Default.Register<IConnectionDb, ConnectionToCsvDb>();



            SimpleIoc.Default.Register(() =>
            {
                return new BridgeToBd(ServiceLocator.Current.GetInstance<IConnectionDb>());
            });

            SimpleIoc.Default.Register(() =>
            {
                return new MainViewModel(SimpleIoc.Default.GetInstance<BridgeToBd>(), SimpleIoc.Default.GetInstance<IWriterExcel>());
            });

            SimpleIoc.Default.Register<EventEditorViewModel>();
            SimpleIoc.Default.Register<AddingNewEventViewModel>();

            // Настройка работы с CSV
            SimpleIoc.Default.Register<IGrabberCsvDb, GrabberCsvDb>();
            SimpleIoc.Default.Register<IRowParserCsv, RowParserCsv>();
            SimpleIoc.Default.Register<IWriterCsvDb, WriterCsvDb>();

            // Настройка работы с Excel
            SimpleIoc.Default.Register<IGrabberExcel, GrabberExcel>();
            SimpleIoc.Default.Register<IWriterExcel, WriterExcel>();


            SimpleIoc.Default.Register<ShowTodayEventsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public EventEditorViewModel EventEditor
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EventEditorViewModel>();
            }
        }

        public AddingNewEventViewModel AddingNewEvent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddingNewEventViewModel>();
            }
        }

        public ShowTodayEventsViewModel ShowTodayEvents
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShowTodayEventsViewModel>();
            }
        }

        public static void Cleanup()
        {

        }
    }
}