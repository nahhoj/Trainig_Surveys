using Surveys.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public SurveyDetailsView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<SurveyDetailsViewModel, ObservableCollection<string>>(this,Messages.SelectTeam,async(sender,args)=>
            {
                string[] teams = new string[args.Count];
                for (int x=0;x<args.Count;x++)                                    
                    teams[x] = args[x].ToString();
                
                var favoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle,null,null, teams);
                if (!string.IsNullOrWhiteSpace(favoriteTeam))
                {
                    MessagingCenter.Send((ContentPage)this,Messages.SelectTeam,favoriteTeam);
                }
            });

            MessagingCenter.Subscribe<SurveyDetailsViewModel,Survey>(this,Messages.NewSurveyComplete,async(sender,args)=>
            {
                await Navigation.PopAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SurveyDetailsViewModel, string[]>(this,Messages.SelectTeam);
            MessagingCenter.Unsubscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete);
        }
    }
}