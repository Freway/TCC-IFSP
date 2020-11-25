using Android.Gms.Plus.Model.People;
using DonorAppMob.Business;

namespace DonorAppMob.Configure {
    static class ConfigUser{
        public static UsuarioData UsuarioLogado;

        public static void LoadUserData(IPerson person){
            UsuarioLogado = new UsuarioData(person);
        }

        public static void UnloadUserData(){
            UsuarioLogado = null;
        }
    }
}