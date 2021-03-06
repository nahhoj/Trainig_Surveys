﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.Core
{
    class Survey
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteTeam { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public override string ToString()
        {
            return $"{Name}|{Birthdate}|{FavoriteTeam}|{Lat}|{Lon}";
        }
    }
}
