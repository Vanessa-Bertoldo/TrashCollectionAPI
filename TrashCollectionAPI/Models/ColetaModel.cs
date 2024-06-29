using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrashCollectionAPI.Models
{
    public class ColetaModel
    {
        public int IdColeta { get; set; }
        public string NomeBairro { get; set; }
        public double NumeroVolume { get; set; }
        public DateTime DataColeta { get; set; }
        public DateTime DataRegistro { get; set; }
        public ICollection<RotaModel> Rotas { get; set; } = new List<RotaModel>();

    }
}

