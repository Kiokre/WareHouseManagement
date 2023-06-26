using System;
using System.Collections.ObjectModel;
using System.Linq;
using WareHouseManagement.Models;
using Object = WareHouseManagement.Models.Object;

namespace WarehouseManagement.ViewModel;

public class OutputViewModel : BaseViewModel
{
    private Object selectedObject;
    private DateTime? dateInput;
    private int count;
    private float priceOutput;
    private object objects;
    private ObservableCollection<Customer> customer;
    private string status;
    private ObservableCollection<OutputInfo> list;

    public object ObjectDisplayName { get => objects; set { objects = value; OnPropertyChanged(nameof(ObjectDisplayName)); } }

    public OutputViewModel()
    {
        ObjectDisplayName = new ObservableCollection<Object>(DataProvider.Instance.DB.Objects);
        List = new ObservableCollection<OutputInfo>(DataProvider.Instance.DB.OutputInfoes);
        Customer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            
        AddCommand = new RelayCommand(param => this.AddExecuted(param),param => 
        {
            if (String.IsNullOrEmpty(Status)) return false;
            return true;
        });
            
        EditCommand = new RelayCommand(param => this.EditExecuted(param), p =>
        {
            if (String.IsNullOrEmpty(Status)) return false;
            var displayList = DataProvider.Instance.DB.InputInfoes.Where(x => x.Object == SelectedObject);
            if (displayList != null || displayList.Count() != 0) return true;
            return false;
        });

        DeleteCommand = new RelayCommand(param => this.DeleteExecuted(param), p =>
        {
            if (SelectItem == null) return false;
            return true;
        });
    }
    
    public Object SelectedObject
    {
        get => selectedObject;
        set
        {
            selectedObject = value;
            OnPropertyChanged(nameof(SelectedObject));
        }
    }
    
    public DateTime? DateInput
    {
        get => dateInput;
        set
        {
            dateInput = value;
            OnPropertyChanged(nameof(DateInput));
        }
    }
    
    public int Count
    {
        get => count;
        set
        {
            if (value == 0) return;
            count = value;
            OnPropertyChanged(nameof(Count));
        }
    }
    
    public float PriceOutput
    {
        get => priceOutput;
        set
        {
            priceOutput = value;
            OnPropertyChanged(nameof(PriceOutput));
        }
    }

    public ObservableCollection<Customer> Customer { get => customer; set { customer = value; OnPropertyChanged(nameof(WareHouseManagement.Models.Customer)); } }
    public Customer SelectedCustomer 
    {
        get => selectedCustomer;
        set
        {
            selectedCustomer = value;
            OnPropertyChanged(nameof(SelectedObject));
        }
    }

    public string Status
    {
        get => status;
        set
        {
            status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

 
    public ObservableCollection<OutputInfo> List
    {
        get => list;
        set
        {
            list = value;
            OnPropertyChanged(nameof(list));
        }
    }
    
    private OutputInfo selectItem;
    private Customer selectedCustomer;

    public OutputInfo SelectItem
    {
        get => selectItem;
        set
        {
            selectItem = value;
            OnPropertyChanged(nameof(SelectItem));
        }
    }
    
    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }
    
    #region RelayCommand
    
            //Add new Object
            private void AddExecuted(object param)
            {
                var output = new Output()
                {
                    Id = Guid.NewGuid().ToString(),
                    DateInput = DateInput
                };
                DataProvider.Instance.DB.Outputs.Add(output);
    
                var outputInfo = new OutputInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    IdObject = SelectedObject.Id,
                    IdOutputInfo = output.Id,
                    IdCustomer = SelectedCustomer.Id,
                    Count = Count,
                    Status = Status,
                    Output = output,
                    Customer = SelectedCustomer,
                    Object = SelectedObject
                };
    
                DataProvider.Instance.DB.OutputInfoes.Add(outputInfo);
                DataProvider.Instance.DB.SaveChanges();
                List.Add(outputInfo);
            }
            private void EditExecuted(object param)
            {
                var supplier = DataProvider.Instance.DB.OutputInfoes.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
                if (supplier != null)
                {
                    supplier.Count = Count;
                    supplier.Status = Status;
                }
    
                DataProvider.Instance.DB.SaveChanges();
                SelectItem.Count = Count;
                SelectItem.Status = Status;
            }
            private void DeleteExecuted(object param)
            {
                var inputInfoes = DataProvider.Instance.DB.OutputInfoes.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
                DataProvider.Instance.DB.OutputInfoes.Remove(inputInfoes);
                DataProvider.Instance.DB.SaveChanges();
    
                List.Remove(inputInfoes);
            }
    
            #endregion RelayCommand
}