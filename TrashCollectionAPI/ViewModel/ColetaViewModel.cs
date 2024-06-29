using static System.Runtime.InteropServices.JavaScript.JSType;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class ColetaViewModel
    {
        public int IdColeta { get; set; }
        public string NomeBairro { get; set; }
        public double NumeroVolume { get; set; }
        public DateTime DataColeta { get; set; }
        public DateTime DataRegistro { get; set; }
        public List<RotaViewModel> Rotas { get; set; }
    }
}
