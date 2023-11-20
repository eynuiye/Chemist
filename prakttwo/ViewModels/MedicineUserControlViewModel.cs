using prakt.Models;
using prakt.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace prakttwo.ViewModels
{
    public class MedicineUserControlViewModel : ViewModelBase 
    {
        private Medicine _selectedMedicine;
        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set => this.RaiseAndSetIfChanged(ref _selectedMedicine, value);
        }

        private HttpClient client = new HttpClient();
        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines
        {
            get => _medicines;
            set => this.RaiseAndSetIfChanged(ref _medicines, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public MedicineUserControlViewModel()
        {
            client.BaseAddress = new Uri("http://localhost:5091");
            Update();
        }

        public async Task Update()
        {
            var response = await client.GetAsync("/medicines");
            if (!response.IsSuccessStatusCode)
            {
                Message = $"Ошибка сервера {response.StatusCode}";
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                Message = "Пустой ответ от сервера";
            }
            Medicines = JsonSerializer.Deserialize<ObservableCollection<Medicine>>(content);
            Message = "";
        }

        public async Task Delete()
        {
            if (SelectedMedicine == null) return;
            var response = await client.DeleteAsync($"/medicines/{SelectedMedicine.Id}");
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка удаления со стороны сервера";
                return;
            }
            Medicines.Remove(SelectedMedicine);
            SelectedMedicine = null;
            Message = "";
        }

        public async Task Add()
        {
            var medicine = new Medicine();
            var response = await client.PostAsJsonAsync($"/medicines", medicine);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Medicine>();
            if (content == null)
            {
                Message = "При добавлении сервер отправил пустой ответ";
                return;
            }
            medicine = content;
            Medicines.Add(medicine);
            SelectedMedicine = medicine;
        }

        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/medicines", SelectedMedicine);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка изменения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Medicine>();
            if (content == null)
            {
                Message = "При изменении сервер отправил пустой ответ";
                return;
            }
            SelectedMedicine = content;
        }
    }
}
