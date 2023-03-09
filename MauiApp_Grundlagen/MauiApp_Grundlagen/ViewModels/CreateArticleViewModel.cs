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

namespace MauiApp_Grundlagen.ViewModels {
    //zu jeder(!) View (CreateArticle) gehört ein eigenes ViewModel (CreateArticleViewModel)
    //die ViewModel-Klasse beinhaltet alle Properties, welche für die View benötigt werden
    //DataBinding: die Eigenschaften der VM-Klasse (z.B. ArticleName)
    //  werden mit Eigenschaften der View-Komponenten (z.B. Text von Entry) verbunden (über das Observer-Pattern)
    //Commands: sind Methoden in der VM-Klasse (diese werden z.B. ausgeführt wenn auf einen Button geklickt wird)

    //die VM-Klasse muss das Interface INotifyPropertyChanged implementieren (Observer-Pattern)
    class CreateArticleViewModel : INotifyPropertyChanged {
        //event: ähnlich wie ein Delegate() (ein Zeiger auf eine/mehrere Methoden)
        //das MAUI-Framework fügt hier die notwendigen Methoden ein
        public event PropertyChangedEventHandler PropertyChanged;

        private string _articlename;

        public string ArticleName {
            get { return this._articlename; }
            set {
                if (value != this._articlename) {
                    this._articlename = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _price;
        public decimal Price {
            get { return this._price; }
            set {
                if (value != this._price) {
                    this._price = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate {
            get { return this._releaseDate; }
            set {
                if (value != this._releaseDate) {
                    this._releaseDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private Category _category;
        public Category Category {
            get { return this._category; }
            set {
                if (value != this._category) {
                    this._category = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> CategoryItems {
            get {
                return Enum.GetNames<Category>().ToList();
            }
        }



        // ctors
        public CreateArticleViewModel() {   // Hier muss die Method eangegeben werden, welche audgeführt wird wenn dieses Command
            // aktiviert wird (beim Click auf den Button)
            this.CmdCreateArticle = new Command(OnCreateArticle);
            this.CmdClear = new Command(OnClear);
        }

        private void OnClear() {
                this.ArticleName = "";
                this.Price = 0;
                this.ReleaseDate = DateTime.Now;
                this.Category = Category.notSpecified;
          
        }

        // 1. zusätzlichen Button: Formular leeren

        // 2. View: Userregistration (Userdaten eingeben können
        //          diese Userdaten an unser Webservice senden und eintragen
        //          dazu WebAPI erweitern

        // 3. View: Userlogin



        // Commands
        public ICommand CmdCreateArticle { get; private set; }
        public ICommand CmdClear { get; private set; }

        private void OnCreateArticle() {
            // Eingabedaten der View überprüfen
            // z.B.: ArticleName mind. 3 Zeichen
            //          Preis >= 0
            //          Releasdate ?

            // Daten in der Instanz von Article kopieren
            Article a = new Article() {
                Name = this.ArticleName,
                Price = this.Price,
                ReleaseDate = this.ReleaseDate,
                Category = this.Category
            };

            // diese Daten an unsere WebAPI (Microservice, Restful-Service) senden
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7500");

            var response = client.PostAsJsonAsync<Article>("/api/shop/article/add", a).Result;

            // Meldung (Erfolg/Misserfolg) ausgeben
            if (response.IsSuccessStatusCode) {
                Application.Current.MainPage.DisplayAlert("HURRAAAA!!", "Der Artikel wurde erfolgreich hinzugefügt!", "OK");
            } else {
                Application.Current.MainPage.DisplayAlert("ERROR 492", "Ein Fehler ist aufgetreten!", "OK");
            }
        }

        //Name der Methode kann frei gewählt werden 
        //diese Methode muss bei den Properties in der set-Methode aufgerufen werden - dann wird die Eigenschaft
        //  in der Komponente automatisch informiert (geändert))
        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}