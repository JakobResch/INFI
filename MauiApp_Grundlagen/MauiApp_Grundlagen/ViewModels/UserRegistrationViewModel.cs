using MauiApp_Grundlagen.Views;
using Onlineshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;

namespace MauiApp_Grundlagen.ViewModels {
    public class UserRegistrationViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public string _username;
        public string _password;

        public string Username {
            get { return _username; }
            set {
                if (value != this._username) {
                    this._username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password {
            get { return _password; }
            set {
                if (value != this._password) {
                    this._password = value;
                    OnPropertyChanged();
                }
            }
        }

        public UserRegistrationViewModel() {   // Hier muss die Method eangegeben werden, welche audgeführt wird wenn dieses Command
            // aktiviert wird (beim Click auf den Button)
            this.CmdCreateUser = new Command(OnCreateUser);
            this.CmdClear = new Command(OnClear);
        }

        private void OnClear() {
            this.Username = "";
            this.Password = "";
        }

        public ICommand CmdCreateUser { get; private set; }
        public ICommand CmdClear { get; private set; }

        private void OnCreateUser() {
            User u = new User() {
                Username = this.Username,
                Password = this.Password,

            };
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7500");

            var response = client.PostAsJsonAsync<User>("/api/shop/user/add", u).Result;

            // Meldung (Erfolg/Misserfolg) ausgeben
            if (response.IsSuccessStatusCode) {
                Application.Current.MainPage.DisplayAlert("HURRAAAA!!", "Der User wurde erfolgreich hinzugefügt!", "OK");
            } else {
                Application.Current.MainPage.DisplayAlert("ERROR 492", "Ein Fehler ist aufgetreten!", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }


