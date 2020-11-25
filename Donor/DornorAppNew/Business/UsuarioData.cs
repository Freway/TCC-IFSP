using Android.Gms.Plus.Model.People;

namespace DonorAppMob.Business {
    class UsuarioData {
        public string Nome{ get; set; }
        public string IdGoogle{ get; set; }
        public int IdUsuario{ get; set; }

        public UsuarioData(){
            
        }

        public UsuarioData(IPerson person){
            Nome = person.DisplayName;
        }
    }
}