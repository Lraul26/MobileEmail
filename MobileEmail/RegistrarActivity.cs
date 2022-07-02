using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MobileEmail
{
    [Activity(Label = "RegistrarActivity")]
    public class RegistrarActivity : Activity
    {
        mobilemail.MobileMailWS service = new mobilemail.MobileMailWS();
        EditText cuenta, nombre, email, pass1, pass2;
        TextView resultado;
        Button registrarse, sesion, foto;
        ImageView fotoperfil;
        private byte[] bitmapData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registrarlayout);
            nombre = FindViewById<EditText>(Resource.Id.ETnombre);
            email = FindViewById<EditText>(Resource.Id.ETemail);
            cuenta = FindViewById<EditText>(Resource.Id.ETCuenta);
            pass1 = FindViewById<EditText>(Resource.Id.ETpass1);
            pass2 = FindViewById<EditText>(Resource.Id.ETpass2);
            fotoperfil = FindViewById<ImageView>(Resource.Id.imageView1);
            registrarse = FindViewById<Button>(Resource.Id.btnregistrarse);
            sesion = FindViewById<Button>(Resource.Id.sesion);
            foto = FindViewById<Button>(Resource.Id.btnfoto);
            resultado = FindViewById<TextView>(Resource.Id.txtresultado);

            registrarse.Click += Registrarse_Click;
            sesion.Click += Sesion_Click;
            foto.Click += Foto_Click;

        }

        private void Foto_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            fotoperfil.SetImageBitmap(bitmap);

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
        }

        private void Sesion_Click(object sender, EventArgs e)
        {
            var item = new Intent(this, typeof(LoginActivity));
            StartActivity(item);
        }

        private void Registrarse_Click(object sender, EventArgs e)
        {
            if(nombre.Text == String.Empty &&  email.Text == String.Empty && cuenta.Text == String.Empty && pass1.Text == String.Empty && pass2.Text == String.Empty && bitmapData == null)
            {
                resultado.Text = "Llene todos los campos por favor...";
            }
            else
            {
               
                if (pass1.Text.Trim().Length == pass2.Text.Trim().Length)
                {
                    var fotop = Convert.ToByte(bitmapData.ToString());
                    service.Register(nombre.Text, cuenta.Text, fotop, email.Text, pass1.Text);
                    resultado.Text = "Cuenta creada con exito...";
                }
                else
                {
                    resultado.Text = "Las contraseñas no Coinciden...";
                }
            }
        }
        
    }
}