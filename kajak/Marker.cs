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
        public int ID { get; set; }
        public Double lat { get; set; }
        public Double lng { get; set; }
        public string img { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string user { get; set; }
        public DateTime timestamp { get; set; }
        public string tour { get; set; }
    }
}