using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Firebase.Auth;
using System;
using static Android.Gms.Maps.GoogleMap;

namespace kajak
{
    [Activity(Label = "@string/activity_label_mylocation", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MyLocationActivity : AppCompatActivity, IOnMapReadyCallback
    {
        static readonly string TAG = "MyLocationActivity";

        static readonly int REQUEST_PERMISSIONS_LOCATION = 1000;
        FirebaseAuth auth;

        //LatLng point;
        double pointX = 0.0;
        double pointY = 0.0;

        GoogleMap theMap;
        public void OnMapReady(GoogleMap googleMap)
        {   
            theMap = googleMap;
            if (this.PerformRuntimePermissionCheckForLocation(REQUEST_PERMISSIONS_LOCATION))
            {
                InitializeUiSettingsOnMap();
            }

            theMap.MapType = GoogleMap.MapTypeHybrid;
            //theMap.SetOnMapLongClickListener(this);
            // theMap.SetOnMapClickListener(this);
            theMap.MapLongClick += TheMap_MapLongClick;
           
        }

      

        private void TheMap_MapLongClick(object sender, MapLongClickEventArgs e)
        {
            LatLng point1 = e.Point;

            pointX = point1.Latitude;
            pointY = point1.Longitude;

            Intent intent = new Intent(this, typeof(AddMarker));
            intent.PutExtra("PointX", pointX);
            intent.PutExtra("PointY", pointY);
            
            StartActivity(intent);
            

            

        }



        void InitializeUiSettingsOnMap()
        {
            theMap.UiSettings.MyLocationButtonEnabled = true;
            theMap.UiSettings.CompassEnabled = true;
            theMap.UiSettings.ZoomControlsEnabled = true;
            theMap.MyLocationEnabled = true;
        
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyLocationLayout);

            this.AddMapFragmentToLayout(Resource.Id.map_container);

            auth = FirebaseAuth.GetInstance(MainActivity.app);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == REQUEST_PERMISSIONS_LOCATION)
            {
                if (grantResults.AllPermissionsGranted())
                {
                    // Permissions granted, nothing to do.
                    // Carry on and let the MapFragment do it's own thing.
                    InitializeUiSettingsOnMap();
                }
                else
                {
                    // Permissions not granted!
                    Log.Info(TAG, "Aplikacja nie ma praw do lokalizaji!");

                    var layout = FindViewById(Android.Resource.Id.Content);
                    Snackbar.Make(layout, Resource.String.location_permission_missing, Snackbar.LengthLong).Show();
                    Finish();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }


    }
}
