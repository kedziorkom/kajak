﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Content;
using Android.Content.PM;

namespace kajak
{
    [Activity(Label = "ForgetPasswordcs", Theme = "@style/AppTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ForgetPassword : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        EditText input_email;
        Button btnResetPas;
        TextView btnBack;
        RelativeLayout activity_forget;
        FirebaseAuth auth;
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.forget_btn_back)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.forget_btn_reset)
            {
                ResetPassword(input_email.Text);
            }
        }
        private void ResetPassword(string email)
        {
            auth.SendPasswordResetEmail(email).AddOnCompleteListener(this, this);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ForgetPassword);
            //Init Firebase  
            auth = FirebaseAuth.GetInstance(MainActivity.app);
            //Views  
            input_email = FindViewById<EditText>(Resource.Id.forget_email);
            btnResetPas = FindViewById<Button>(Resource.Id.forget_btn_reset);
            btnBack = FindViewById<TextView>(Resource.Id.forget_btn_back);
            activity_forget = FindViewById<RelativeLayout>(Resource.Id.activity_forget);
            btnResetPas.SetOnClickListener(this);
            btnBack.SetOnClickListener(this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == false)
            {
                Snackbar snackbar = Snackbar.Make(activity_forget, "Reset Password Failed!", Snackbar.LengthShort);
                snackbar.Show();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activity_forget, "Reset Password link send to email : " + input_email.Text, Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}