using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyDetailsView : ContentPage
    {

        private readonly string[] teams =
        {
            "Nacional",
            "America",
            "Medellin",
            "Cali",
            "Santa fe"
        };

        public SurveyDetailsView()
        {
            InitializeComponent();
        }

        private async void FavoriteTeamButton_Clicked(object sender, EventArgs e)
        {
            var FavoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle,null,null,teams);
            if (!string.IsNullOrWhiteSpace(FavoriteTeam))
            {
                FavoriteTeamLabel.Text = FavoriteTeam;
                FavoriteTeamLabel.TextColor = (Color)new TeamColorConverter().Convert(FavoriteTeam,null,null,null);
            }
        }

        private async void OkButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(FavoriteTeamLabel.Text))
            {
                return;
            }
            var newSurvey = new Survey()
            {
                Name = NameEntry.Text,
                Birthdate = BirthdatePicker.Date,
                FavoriteTeam = FavoriteTeamLabel.Text
            };
            MessagingCenter.Send((ContentPage)this,Messages.NewSurveyComplete,newSurvey);
            await Navigation.PopAsync();
        }
    }
}