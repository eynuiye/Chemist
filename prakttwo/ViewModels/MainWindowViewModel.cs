using prakttwo.Views;

namespace prakttwo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _MedicinesUserControl = new MedicineUserControl();
            _MedicinesUserControl.DataContext = new MedicineUserControlViewModel();
        }

        public MedicineUserControl _MedicinesUserControl { get; set; }
    }
}