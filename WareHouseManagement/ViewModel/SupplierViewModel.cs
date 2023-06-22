using System.Collections.ObjectModel;
using WareHouseManagement.Models;
using System;
using System.Linq;

namespace WarehouseManagement.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<Supplier> list;
        private Supplier selectItem;
        private string displayName;
        private string phone;
        private string address;
        private string mail;
        private string moreInfo;
        private DateTime? contractDate;



        public ObservableCollection<Supplier> List { get => list; set { list = value; OnPropertyChanged(); } }
        public string DisplayName { get => displayName; set { displayName = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }
        public string Address { get => address; set { address = value; OnPropertyChanged(); } }
        public string Mail { get => mail; set { mail = value; OnPropertyChanged(); } }
        public string MoreInfo { get => moreInfo; set { moreInfo = value; OnPropertyChanged(); } }
        public DateTime? ContractDate { get => contractDate; set { contractDate = value; OnPropertyChanged(); } }


        public Supplier SelectItem
        {
            get => selectItem;
            set
            {
                selectItem = value;
                OnPropertyChanged();
                if (SelectItem != null)
                {
                    DisplayName = SelectItem.DisplayName;
                    Phone = SelectItem.Phone;
                    Address = SelectItem.Address;
                    Mail = SelectItem.Mail;
                    MoreInfo = SelectItem.MoreInfo;
                    ContractDate = SelectItem.ContractDate;
                }
            }
        }

        public SupplierViewModel()
        {
            List = new ObservableCollection<Supplier>(DataProvider.Instance.DB.Suppliers);
            AddCommand = new RelayCommand(param => this.AddExecuted(param));

            EditCommand = new RelayCommand(param => this.EditExecuted(param), p =>
            {
                if (string.IsNullOrEmpty(DisplayName) && SelectItem == null) return false;
                var displayList = DataProvider.Instance.DB.Suppliers.Where(x => x.DisplayName == DisplayName);
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

        //Add new Supplier
        private void AddExecuted(object param)
        {
            var Supplier = new Supplier() { DisplayName = DisplayName, Phone = Phone, Mail = Mail,Address = Address, ContractDate =ContractDate, MoreInfo = MoreInfo};
            DataProvider.Instance.DB.Suppliers.Add(Supplier);
            DataProvider.Instance.DB.SaveChanges();
            List.Add(Supplier);
        }
        private void EditExecuted(object param)
        {
            var supplier = DataProvider.Instance.DB.Suppliers.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            supplier.DisplayName = DisplayName;
            supplier.Phone = Phone;
            supplier.Mail = Mail;
            supplier.Address = Address;
            supplier.ContractDate = ContractDate;
            supplier.MoreInfo = MoreInfo;

            DataProvider.Instance.DB.SaveChanges();
            SelectItem.DisplayName = DisplayName;
        }
        private void DeleteExecuted(object param)
        {
            var Supplier = DataProvider.Instance.DB.Suppliers.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            DataProvider.Instance.DB.Suppliers.Remove(Supplier);
            DataProvider.Instance.DB.SaveChanges();

            List.Remove(Supplier);
        }

        #endregion RelayCommand
    }
}