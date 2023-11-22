using prakttwo.Views;

namespace prakttwo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
 
            _MedicinesUserControl = new MedicineUserControl();
            _MedicinesUserControl.DataContext = new MedicineUserControlViewModel();
             _ClientsUserControl = new ClientUserControl();
            _ClientsUserControl.DataContext = new ClientUserControlViewModel();
        }

        public MedicineUserControl _MedicinesUserControl { get; set; }
        public ClientUserControl _ClientsUserControl { get; set; }

    }
}