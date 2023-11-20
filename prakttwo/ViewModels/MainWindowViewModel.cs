using prakttwo.Views;

namespace prakttwo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _ClientsUserControl = new ClientUserControl();
            _ClientsUserControl.DataContext = new ClientUserControlViewModel();
        }
        public ClientUserControl _ClientsUserControl { get; set; }
    }
}