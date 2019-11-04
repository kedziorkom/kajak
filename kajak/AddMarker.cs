using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
//using Firebase.Database.Query;
//using Firebase.Firestore;

namespace kajak
{
    [Activity(Label = "Dodaj znacznik", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddMarker : Activity
    {
        FirebaseClient firebase = new FirebaseClient("https://kajak-c39fd.firebaseio.com/");
        double pX = 0.0;
        double pY = 0.0;
        string Markid = "";
        // GeoPoint point;
        string Mname;
        string Mtype;

        public string Mname1 { get => Mname; set => Mname = value; }
        public string Mtype1 { get => Mtype; set => Mtype = value; }

       // public GeoPoint geop { get => point; set => point = value; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddMarker);

            // Create your application here
            
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            TextView t1 = FindViewById<TextView>(Resource.Id.textView5);
            TextView t2 = FindViewById<TextView>(Resource.Id.textView6);
            Button b1 = FindViewById<Button>(Resource.Id.buttonAddMarker);
            
            b1.Click += B1_Click;

            

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.marker_types, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            pX = Intent.GetDoubleExtra("PointX", 0.0);
            pY = Intent.GetDoubleExtra("PointY", 0.0);
            Markid = pX.ToString() + pY.ToString();
            t1.Text = "Szerokość geo.: " + pX.ToString();
            t2.Text = "Długość geo.: " + pY.ToString();

            //geop = new GeoPoint(pX, pY);

            
        }

        private void B1_Click(object sender, EventArgs e)
        {
            EditText e1 = FindViewById<EditText>(Resource.Id.editText1);

            Mname1 = e1.Text;

            _ = AddMark(pX,pY,
                        Mname,
                        Mtype,Markid);
            var layout = FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(layout, Resource.String.MarkerApplied, Snackbar.LengthLong).Show();
            Finish();
        }

        public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
            Mtype1 = toast;
        }

        public async Task AddMark(double latitude, double longitude, string name, string type, string id)
        {
            // firebase.auth().currentUser.toString();

            await firebase
              .Child("points")
              .PostAsync(new Marker() { Latitude=latitude,Longitude=longitude, Name=name,Type=type, ID= id });
        }

    }
}