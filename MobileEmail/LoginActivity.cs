using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileEmail
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity

    {
        mobilemail.MobileMailWS service = new mobilemail.MobileMailWS();
        Button btnguardar, btnregistrar;
        EditText etcuenta, etpass;
        TextView txtresultado;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Loginlayout);
            // Create your application here

            etcuenta = FindViewById<EditText>(Resource.Id.ETCuenta);
            etpass = FindViewById<EditText>(Resource.Id.ETpass);
            btnguardar = FindViewById<Button>(Resource.Id.Btnguardar);
            btnregistrar = FindViewById<Button>(Resource.Id.bntregistrarse);
            txtresultado = FindViewById<TextView>(Resource.Id.txtresultado);

            btnguardar.Click += Btnguardar_Click;
            btnregistrar.Click += Btnregistrar_Click;
        }

        private void Btnregistrar_Click(object sender, EventArgs e)
        {
            var item = new Intent(this, typeof(RegistrarActivity));
            StartActivity(item);
        }

        private void Btnguardar_Click(object sender, EventArgs e)
        {
            if(etcuenta.Text.Trim().Length != 0 && etpass.Text.Trim().Length != 0)
            {
                service.Login(etcuenta.Text, etpass.Text);
            }
            else
            {
                txtresultado.Text = "Ingrese una cuenta y una contraseña por favor...";
                limpiar();
            }
        }
        private void limpiar()
        {
            etcuenta.Text = "";
            etpass.Text = " ";
        }
    }
}