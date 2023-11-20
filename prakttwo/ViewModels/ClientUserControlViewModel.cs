using Avalonia.Controls;
using DynamicData.Binding;
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
    public class ClientUserControlViewModel : ViewModelBase
    {
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
        }
        private HttpClient client = new HttpClient();
        private ObservableCollection<Client>? _clients;
        public ObservableCollection<Client>? Clients
        {
            get => _clients;
            set => this.RaiseAndSetIfChanged(ref _clients, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public ClientUserControlViewModel()
        {
           client.BaseAddress = new Uri("http://localhost:5091");
           Update();
        }

        public async Task Update()
        {
            var response = await client.GetAsync("/clients");
            if (!response.IsSuccessStatusCode)
            {
                Message = $"Ошибка сервера {response.StatusCode}";
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                Message = "Пустой ответ от сервера";
            }
            Clients = JsonSerializer.Deserialize<ObservableCollection<Client>>(content);
            Message = "";
        }

        public async Task Delete()
        {
            if (SelectedClient == null) return;
            var response = await client.DeleteAsync($"/clients/{SelectedClient.Id}");
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка удаления со стороны сервера";
                return;
            }
            Clients.Remove(SelectedClient);
            SelectedClient = null;
            Message = "";
        }

        public async Task Add()
        {
            var _client = new Client();
            var response = await client.PostAsJsonAsync($"/clients", client);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Client>();
            if (content == null)
            {
                Message = "При добавлении сервер отправил пустой ответ";
                return;
            }
            _client = content;
            Clients.Add(_client);
            SelectedClient = _client;
        }

        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/students", SelectedClient);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка изменения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Client>();
            if (content == null)
            {
                Message = "При изменении сервер отправил пустой ответ";
                return;
            }
            SelectedClient = content;
        }
    }
}
