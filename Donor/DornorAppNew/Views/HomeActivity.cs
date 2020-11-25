using Android.App;
using Android.OS;
using Android.Widget;
using DonorAppMob.Configure;

namespace DonorAppMob.Views{
    [Activity(Label = "Home", Theme = "@android:style/Theme.Material")]
    public class HomeActivity : Activity{
        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Home);

            var edtNome = FindViewById<TextView>(Resource.Id.textView1);
            edtNome.Text = ConfigUser.UsuarioLogado.Nome;
        }
    }
}