﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveysView : ContentPage
    {
        public SurveysView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Data>(this, Messages.NewSurvey, async (sender) =>
              {
                  await Navigation.PushAsync(new SurveyDetailsView());
              });
        }
    }
}