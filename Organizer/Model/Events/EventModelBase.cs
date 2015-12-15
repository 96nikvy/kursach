using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Model.Events
{
    abstract class EventModelBase : INotifyPropertyChanged, IComparable
    {
        
        #region Constructors
        /// <summary>
        /// Этот конструктор мы используем в том случае, если это - первое 
        /// создание события, из графического интерфейса программы. Иначе мы создаем 
        /// событие из csv базы(или из какой-нибудь еще), то используем перегрузку на 4 параметра.
        /// </summary>
        public EventModelBase(string name, string description, DateTime datePlanned)
        {
            
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");

            description = description == null ? string.Empty : description;

            Name = name;
            DateCreated = DateTime.Now;
            DatePlanned = datePlanned;
            Description = description;
        }

        /// <summary>
        /// Конструктор, на 4 параметра, из базы создаем им.
        /// </summary>
        /// <param name="name">Имя события</param>
        /// <param name="description">Описание события</param>
        /// <param name="datePlanned">Дата, на которую запланированно событие</param>
        /// <param name="dateCreated">Дата, в которое событие было создано</param>
        public EventModelBase(string name, string description, DateTime datePlanned, DateTime dateCreated)
        {
            
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");

            description = description == null ? string.Empty : description;

            Name = name;
            DateCreated = DateTime.Now;
            DatePlanned = datePlanned;
            Description = description;
            DateCreated = dateCreated;
        }


        #endregion
        
        #region Public Properties

        protected string _name;
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        protected DateTime _dateCreated;
        public virtual DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                _dateCreated = value;
                OnPropertyChanged("DateCreated");
            }
        }

        protected string _description;
        public virtual string Description
        { 
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        protected DateTime _datePlanned;
        public virtual DateTime DatePlanned
        {
            get
            {
                return _datePlanned;
            }
            set
            {
                _datePlanned = value;
                OnPropertyChanged("DatePlanned");
            }
        }

        protected int? _id;
        public int? ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string nameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameProperty));
            }
        }


        #endregion

        #region Public Methods

        public int CompareTo(object obj)
        {
            if (!(obj is EventModelBase))
                return 1;

            return DateCreated > (obj as EventModelBase).DateCreated ? 1 : -1;
        }

        #endregion

    }
}
