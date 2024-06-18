using CommunityToolkit.Mvvm.ComponentModel;
using JsonPlaceholderAPI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace JsonPlaceholderAPI.ViewModel
{
    public partial class PhotosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PhotoModel> _photos;

        public PhotosViewModel()
        {
            Photos = new ObservableCollection<PhotoModel>();
            LoadPhotos();
        }

        private async void LoadPhotos()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/photos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _photos = JsonSerializer.Deserialize<ObservableCollection<PhotoModel>>(json);
                    foreach (var photo in _photos)
                    {
                        Photos.Add(photo);
                    }
                }
            }
        }
    }
}
