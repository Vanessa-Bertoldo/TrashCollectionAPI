using static System.Runtime.InteropServices.JavaScript.JSType;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class ColetaViewModel
    {
        public int IdColeta;
        public string NomeBairro;
        public double NumeroVolume;
        public Date DataColeta;
        public Date DataRegistro;
        public ICollection<RotaModel> Rotas { get; set; } = new List<RotaModel>();
    }
}
