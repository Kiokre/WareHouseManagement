using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WarehouseManagement.ViewModel
{
    public class ControlBarViewmModel: BaseViewModel
    {
        #region commands
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand  MinimizeWindowCommand { get; set; }
        public RelayCommand  MouseMoveWindowCommand { get; set; }
        #endregion

        public ControlBarViewmModel()
        {
            CloseWindowCommand = new RelayCommand(param => this.CloseWindowExecuted(param));
            MaximizeWindowCommand = new RelayCommand(param => this.MaximizeWindowExecuted(param));
            MinimizeWindowCommand = new RelayCommand(param => this.MinimizeWindowExecuted(param));
            MouseMoveWindowCommand = new RelayCommand(param => this.MouseMoveWindowExecuted(param));
        }

        

        #region Relay command
        private void CloseWindowExecuted(object p)
        {
            FrameworkElement window = GetWindowParent((UserControl)p);
            var w = window as Window;
            w?.Close();
        }
        private void MaximizeWindowExecuted(object p)
        {
            FrameworkElement window = GetWindowParent((UserControl)p);
            var w = window as Window;
            if (w != null)
            {
                w.WindowState = w.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
            }
        }
        private void MinimizeWindowExecuted(object p)
        {
            FrameworkElement window = GetWindowParent((UserControl)p);
            var w = window as Window;
            if (w != null)
            {
                w.WindowState = w.WindowState != WindowState.Minimized ? WindowState.Minimized : WindowState.Normal;
            }
        }
        private void MouseMoveWindowExecuted(object p)
        {
            FrameworkElement window = GetWindowParent((UserControl)p);
            var w = window as Window;
            if (w != null)
            {
                w.DragMove();
            }
        }



        #endregion

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
