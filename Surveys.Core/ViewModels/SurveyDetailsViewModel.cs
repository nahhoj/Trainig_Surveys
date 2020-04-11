using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Surveys.Core.ViewModels
{
    class SurveyDetailsViewModel: NotificationObject
    {

        public ICommand SelectTeamCommand { get; set; }
        public ICommand EndSuveysCommand { get; set; }

        public SurveyDetailsViewModel()
        {
            Teams = new ObservableCollection<string>(new[] {
                "Nacional",
                "America",
                "Medellin",
                "Cali",
                "Santa fe"
            });
            SelectTeamCommand = new Command(SelectTeamCommandExecute);
            EndSuveysCommand = new Command(EndSuveysCommandExecute, EndSuveysCommandCanExecute);
            MessagingCenter.Subscribe<ContentPage, string>(this,Messages.SelectTeam,(sender,args)=>
            {
                FavoriteTeam = args;
            });

            PropertyChanged += SurveyDetailsViewModel_PropertyChanged;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();
            }
        }

        private DateTime birthdate;
        public DateTime Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                if (birthdate == value)
                    return;
                birthdate = value;
                OnPropertyChanged();
            }
        }

        private string favoriteTeam;
        public string FavoriteTeam
        {
            get
            {
                return favoriteTeam;
            }
            set
            {
                if (favoriteTeam == value)
                    return;
                favoriteTeam = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> teams;
        public ObservableCollection<string> Teams
        {
            get
            {
                return teams;
            }

            set
            {
                if (teams == value)
                    return;
                teams = value;
                OnPropertyChanged();
            }
        }

        public void SelectTeamCommandExecute()
        {
            MessagingCenter.Send(this,Messages.SelectTeam,Teams);
        }

        public async void EndSuveysCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Name = Name,
                Birthdate = Birthdate,
                FavoriteTeam = FavoriteTeam
            };

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            if (location != null)
            {
                try
                {
                    
                    newSurvey.Lat = location.Latitude;
                    newSurvey.Lon = location.Longitude;
                }
                catch (Exception)
                {
                    newSurvey.Lat = 0;
                    newSurvey.Lon=0;
                }
            }

            MessagingCenter.Send(this,Messages.NewSurveyComplete,newSurvey);
        }

        public bool EndSuveysCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(FavoriteTeam);
        }

        private void SurveyDetailsViewModel_PropertyChanged(object sender,System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(FavoriteTeam))
            {
                (EndSuveysCommand as Command)?.ChangeCanExecute();
            }
        }
    }
}
