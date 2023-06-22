using System.Collections.ObjectModel;
using WareHouseManagement.Models;
using System;
using System.Linq;

namespace WarehouseManagement.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<Unit> list;
        private Unit selectItem;
        private string displayName;
        public ObservableCollection<Unit> List { get => list; set { list = value; OnPropertyChanged(); } }
        public string DisplayName { get => displayName; set { displayName = value; OnPropertyChanged(); } }

        public Unit SelectItem
        {
            get => selectItem;
            set
            {
                selectItem = value;
                OnPropertyChanged();
                if (SelectItem != null)
                {
                    DisplayName = SelectItem.DisplayName;
                }
            }
        }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Instance.DB.Units);
            AddCommand = new RelayCommand(param => this.AddExecuted(param), p => 
            {
                if (string.IsNullOrEmpty(DisplayName)) return false;
                var displayList = DataProvider.Instance.DB.Units.Where(x => x.DisplayName == DisplayName);
            if (displayList == null || displayList.Count() != 0) return false;
                return true;
            });

            EditCommand = new RelayCommand(param => this.EditExecuted(param), p =>
            {
                if (string.IsNullOrEmpty(DisplayName) && SelectItem == null) return false;
                var displayList = DataProvider.Instance.DB.Units.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0) return false;
                return true;
            });

            DeleteCommand = new RelayCommand(param => this.RemoveExecuted(param), p =>
            {
                if (SelectItem == null) return false;
                return true;
            });
        }

        #region RelayCommand

        //Add new unit
        private void AddExecuted(object param)
        {
            var unit = new Unit() { DisplayName = DisplayName };
            DataProvider.Instance.DB.Units.Add(unit);
            DataProvider.Instance.DB.SaveChanges();
            List.Add(unit);
        }
        private void EditExecuted(object param)
        {
            var unit = DataProvider.Instance.DB.Units.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            unit.DisplayName = DisplayName;
            DataProvider.Instance.DB.SaveChanges();

            SelectItem.DisplayName = DisplayName;
        }
        private void RemoveExecuted(object param)
        {
            var unit = DataProvider.Instance.DB.Units.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            DataProvider.Instance.DB.Units.Remove(unit);
            DataProvider.Instance.DB.SaveChanges();

            List.Remove(unit);
        }

        #endregion RelayCommand
    }
}