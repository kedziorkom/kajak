using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using Firebase.Firestore;

namespace kajak
{
    public class Marker
    {
       // public int ID { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}