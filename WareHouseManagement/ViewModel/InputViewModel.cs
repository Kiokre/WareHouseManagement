using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WareHouseManagement.Models;
using Object = WareHouseManagement.Models.Object;

// ReSharper disable RedundantArgumentDefaultValue

namespace WarehouseManagement.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private int objectDisplayName;
        private DateTime? dateInput;
        private int count;
        private float priceInput;
        private float priceOutput;
        private string status;
        private Object selectedObject;
        private InputInfo selectItem;
        private ObservableCollection<Object> objects;
        private ObservableCollection<InputInfo> list;

        public InputViewModel()
        {
            Objects = new ObservableCollection<Object>(DataProvider.Instance.DB.Objects);
            List = new ObservableCollection<InputInfo>(DataProvider.Instance.DB.InputInfoes);
            
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

        
        public ObservableCollection<Object> Objects { get => objects; set { objects = value; OnPropertyChanged(nameof(Objects)); } }
        public ObservableCollection<InputInfo> List { get => list; set { list = value; OnPropertyChanged(nameof(List)); } }
        public Object SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
            }
        }
        
        public InputInfo SelectItem
        {
            get => selectItem;
            set
            {
                selectItem = value;
                OnPropertyChanged();
            }
        }
        
        public int ObjectDisplayName
        {
            get => objectDisplayName;
            set
            {
                objectDisplayName = value;
                OnPropertyChanged(nameof(ObjectDisplayName));
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

        public float PriceInput
        {
            get => priceInput;
            set
            {
                priceInput = value;
                OnPropertyChanged(nameof(PriceInput));
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

        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        
        #region RelayCommand

        //Add new Object
        private void AddExecuted(object param)
        {
            var input = new Input
            {
                Id = Guid.NewGuid().ToString(),
                DateInput = DateInput
            };
            DataProvider.Instance.DB.Inputs.Add(input);

            var inputInfo = new InputInfo
            {
                Id = Guid.NewGuid().ToString(),
                IdObject = SelectedObject.Id,
                IdInput = input.Id,
                Count = Count,
                InputPrice = PriceInput,
                OutputPrice = PriceOutput,
                Status = Status
            };

            DataProvider.Instance.DB.InputInfoes.Add(inputInfo);
            DataProvider.Instance.DB.SaveChanges();
            List.Add(inputInfo);
        }
        private void EditExecuted(object param)
        {
            var supplier = DataProvider.Instance.DB.InputInfoes
                .Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            if (supplier != null)
            {
                supplier.OutputPrice = PriceOutput;
                supplier.Count = Count;
                supplier.Status = Status;
                supplier.InputPrice = PriceInput;
            }

            DataProvider.Instance.DB.SaveChanges();
            SelectItem.InputPrice = PriceInput;
            SelectItem.OutputPrice = PriceOutput;
            SelectItem.Count = Count;
            SelectItem.Status = Status;
        }
        private void DeleteExecuted(object param)
        {
            var inputInfoes = DataProvider.Instance.DB.InputInfoes.Where(x => x.Id == SelectItem.Id).SingleOrDefault();
            DataProvider.Instance.DB.InputInfoes.Remove(inputInfoes);
            DataProvider.Instance.DB.SaveChanges();

            List.Remove(inputInfoes);
        }

        #endregion RelayCommand
    }
}