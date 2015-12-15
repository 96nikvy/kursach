using GalaSoft.MvvmLight;
using Organizer.Model.Events;
using Organizer.Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.ViewModel.Bridges
{
    delegate void ChangingEventEvent (EventModelBase a);

    abstract class AbstractBridgeToBd : ViewModelBase
    {
        protected IConnectionDb _connection;

        #region Ctor

        public AbstractBridgeToBd(IConnectionDb connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            _connection = connection;
        }

        #endregion

        #region Public Properties

        public virtual IList<EventModelBase> Events 
        {
            get
            {
                return _connection.Events;
            }
        }

        EventModelBase _currentEvent;
        public virtual EventModelBase CurrentEvent
        {
            get
            {
                return _currentEvent;
            }
            set
            {
                _currentEvent = value;

                if (ChangingEvent != null)
                {
                    ChangingEvent(_currentEvent);
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие , на которое следует подписаться в том случае, если контролу необходимо
        /// знать какое в данный момент выбрано "событие" для редактирования. На аргумент 
        /// принимающему делегату принимать EventModelBase
        /// </summary>
        public event ChangingEventEvent ChangingEvent;

        /// <summary>
        /// Событие, на которое следует подписаться в том случае, если контролу необходимо знать, что изменился
        /// список "событий"
        /// </summary>
        public event Action ChangingEvents;

        public void OnChangingEvents()
        {
            if (ChangingEvents != null)
                ChangingEvents();
        }

        #endregion

        #region Public Methods

        public virtual bool AddEvent(EventModelBase myEvent)
        {
            if (myEvent == null) throw new ArgumentNullException("myEvent");

            bool result = _connection.AddEvent(myEvent);

            OnChangingEvents();

            return result;
        }

        public virtual bool RemoveEvent(EventModelBase myEvent)
        {
            if (myEvent == null) throw new ArgumentNullException("myEvent");

            bool result = _connection.RemoveEvent(myEvent);

            OnChangingEvents();

            return result;
        }

        public virtual bool UpdateEvent(EventModelBase myEvent)
        {
            if (myEvent == null) throw new ArgumentNullException("myEvent");

            _connection.UpdateEvent(myEvent);

            return true;
        }

        #endregion
    }
}
