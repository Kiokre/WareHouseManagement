using System;
using System.Collections.ObjectModel;
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
            
            AddCommand = new RelayCommand(_ =>
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

        public ICommand AddCommand { get; }
    }
}