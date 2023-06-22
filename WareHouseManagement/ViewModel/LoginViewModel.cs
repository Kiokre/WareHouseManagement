using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WareHouseManagement;
using WareHouseManagement.Models;

namespace WarehouseManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand PasswordChangedCommand { get; set; }

        public bool IsLogin { get; set; } = false;

        private string userName = string.Empty;
        private string password = string.Empty;

        public string UserName { get => userName; set { userName = value; OnPropertyChanged(); } }
        public string Password{ get => password; set { password = value; OnPropertyChanged(); } }




        public LoginViewModel()
        {

            LoginCommand = new RelayCommand(param => this.LoginExecuted(param));
            CancelCommand = new RelayCommand(param => this.CancelExecuted(param));
            PasswordChangedCommand = new RelayCommand(param => this.PasswordExecuted(param));



        }

        #region RelayCommand
        private void LoginExecuted(object param)
        {
            if (param == null) return;
            string passEncode = MD5Hash(Base64Encode(Password));
            var count = DataProvider.Instance.DB.Users.Where(x => x.UserName == UserName &&  x.Password == passEncode).Count();
            if(count == 0)
            {
                IsLogin = true;
                var win = param as Window;
                win.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Username or Password incorrect!");
            }

           
        }

        private void CancelExecuted(object param)
        {
            if (param == null) return;
            var win = param as Window;
            IsLogin = false;
            win.Close();
        }
        private void PasswordExecuted(object param)
        {
            var p = param as PasswordBox;
            Password = p.Password;
        }

        #endregion
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}

