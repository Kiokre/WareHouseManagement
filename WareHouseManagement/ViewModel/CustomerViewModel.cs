using System.Collections.ObjectModel;
using WareHouseManagement.Models;
using System;
using System.Linq;

namespace WarehouseManagement.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private ObservableCollection<Customer> list;
        private Customer selectItem;
        private string displayName;
        private string phone;
        private string address;
        private string mail;
        private string moreInfo;
        private DateTime? contractDate;



        public ObservableCollection<Customer> List { get => list; set { list = value; OnPropertyChanged(); } }
        public string DisplayName { get => displayName; set { displayName = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }
        public string Address { get => address; set { address = value; OnPropertyChanged(); } }
        public string Mail { get => mail; set { mail = value; OnPropertyChanged(); } }
        public string MoreInfo { get => moreInfo; set { moreInfo = value; OnPropertyChanged(); } }
        public DateTime? ContractDate { get => contractDate; set { contractDate = value; OnPropertyChanged(); } }


        public Customer SelectItem
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

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            AddCommand = new RelayCommand(param => this.AddExecuted(param));

            EditCommand = new RelayCommand(param => this.EditExecuted(param), p =>
            {
                if (string.IsNullOrEmpty(DisplayName) && SelectItem == null) return false;
                var displayList = DataProvider.Instance.DB.Customers.Where(x => x.DisplayName == DisplayName);
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

        //Add new Customer
        private void AddExecuted(object param)
        {
            var Customer = new Customer() { DisplayName = DisplayName, Phone = Phone, Mail = Mail,Address = Address, ContractDate =ContractDate, MoreInfo = MoreInfo};
            DataProvider.Instance.DB.Customers.Add(Customer);
            DataProvider.Instance.DB.SaveChanges();
            List.Add(Customer);
        }
        private void EditExecuted(object param)
        {
            var customer = DataProvider.Instance.DB.Customers.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            customer.DisplayName = DisplayName;
            customer.Phone = Phone;
            customer.Mail = Mail;
            customer.Address = Address;
            customer.ContractDate = ContractDate;
            customer.MoreInfo = MoreInfo;

            DataProvider.Instance.DB.SaveChanges();
            SelectItem.DisplayName = DisplayName;
        }
        private void DeleteExecuted(object param)
        {
            var Customer = DataProvider.Instance.DB.Customers.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            DataProvider.Instance.DB.Customers.Remove(Customer);
            DataProvider.Instance.DB.SaveChanges();

            List.Remove(Customer);
        }

        #endregion RelayCommand
    }
}