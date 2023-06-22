using System;
using System.Windows.Input;
using WareHouseManagement.Models;

// ReSharper disable RedundantArgumentDefaultValue

namespace WarehouseManagement.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private string objectDisplayName;
        private DateTime dateInput;
        private int count;
        private float priceInput;
        private float priceOutput;
        private string status;

        public InputViewModel()
        {
            AddCommand = new RelayCommand(_ =>
            {
                var input = new InputInfo
                {
                    IdObject = ObjectDisplayName,
                    Count = Count,
                    InputPrice = PriceInput,
                    OutputPrice = PriceOutput,
                    Status = Status
                };

                DataProvider.Instance.DB.InputInfoes.Add(input);
                DataProvider.Instance.DB.SaveChanges();
            });
        }

        public string ObjectDisplayName
        {
            get => objectDisplayName;
            set
            {
                objectDisplayName = value;
                OnPropertyChanged(nameof(ObjectDisplayName));
            }
        }

        public DateTime DateInput
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