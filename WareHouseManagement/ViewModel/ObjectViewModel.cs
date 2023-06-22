using System.Collections.ObjectModel;
using WareHouseManagement.Models;
using System;
using System.Linq;
using Object = WareHouseManagement.Models.Object;

namespace WarehouseManagement.ViewModel
{
    public class ObjectViewModel : BaseViewModel
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<Object> list;
        private ObservableCollection<Unit> unit;
        private ObservableCollection<Supplier> supplier;
        private Object selectItem;
        private Unit selectedUnit;
        private Supplier selectedSupplier;
        private string displayName;
        private string qRCode;
        private string barCode;




        public ObservableCollection<Object> List { get => list; set { list = value; OnPropertyChanged(); } }
        public ObservableCollection<Unit> Unit { get => unit; set { unit = value; OnPropertyChanged(); } }
        public ObservableCollection<Supplier> Supplier { get => supplier; set { supplier = value; OnPropertyChanged(); } }
        public string DisplayName { get => displayName; set { displayName = value; OnPropertyChanged(); } }
        public string QRCode { get => qRCode; set { qRCode = value; OnPropertyChanged(); } }
        public string BarCode { get => barCode; set { barCode = value; OnPropertyChanged(); } }
       

        public Object SelectItem
        {
            get => selectItem;
            set
            {
                selectItem = value;
                OnPropertyChanged();
                if (SelectItem != null)
                {
                    DisplayName = SelectItem.DisplayName;
                    QRCode = SelectItem.QRCode;
                    BarCode = SelectItem.BarCode;
                    SelectedUnit = SelectItem.Unit;
                    SelectedSupplier = SelectItem.Supplier;
                  
                }
            }
        }
        public Unit SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                OnPropertyChanged();
            }
        }
        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set
            {
                selectedSupplier = value;
                OnPropertyChanged();
            }
        }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Object>(DataProvider.Instance.DB.Objects);
            Unit = new ObservableCollection<Unit>(DataProvider.Instance.DB.Units);
            Supplier = new ObservableCollection<Supplier>(DataProvider.Instance.DB.Suppliers);

            AddCommand = new RelayCommand(param => this.AddExecuted(param),param => 
            {
                if (SelectedSupplier == null || SelectedUnit == null) return false;
                return true;
            });


            EditCommand = new RelayCommand(param => this.EditExecuted(param), p =>
            {

                if (SelectedSupplier == null || SelectedUnit == null) return false;
                var displayList = DataProvider.Instance.DB.Objects.Where(x => x.DisplayName == DisplayName);
                if (displayList != null || displayList.Count() != 0) return true;
                return false;
            });

            DeleteCommand = new RelayCommand(param => this.DeleteExecuted(param), p =>
            {
                if (SelectItem == null) return false;
                return true;
            });
        }

        #region RelayCommand

        //Add new Object
        private void AddExecuted(object param)
        {
            var Object = new Object() { DisplayName = DisplayName, QRCode = QRCode, BarCode = BarCode, IdSupplier = SelectedSupplier.Id, IdUnit = SelectedUnit.Id, Id = Guid.NewGuid().ToString() };
            DataProvider.Instance.DB.Objects.Add(Object);
            DataProvider.Instance.DB.SaveChanges();
            List.Add(Object);
        }
        private void EditExecuted(object param)
        {
            var supplier = DataProvider.Instance.DB.Objects.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            supplier.DisplayName = DisplayName;
            supplier.QRCode = QRCode;
            supplier.BarCode = BarCode;
            supplier.IdSupplier = SelectedSupplier.Id;
            supplier.IdUnit = SelectedUnit.Id;  
          
            DataProvider.Instance.DB.SaveChanges();
            SelectItem.DisplayName = DisplayName;
        }
        private void DeleteExecuted(object param)
        {
            var Object = DataProvider.Instance.DB.Objects.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            DataProvider.Instance.DB.Objects.Remove(Object);
            DataProvider.Instance.DB.SaveChanges();

            List.Remove(Object);
        }

        #endregion RelayCommand
    }
}