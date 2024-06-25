using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrashCollectionAPI.Models
{
    public class ColetaModel
    {
        public int IdColeta;
        public string NomeBairro;
        public double NumeroVolume;
        public Date DataColeta;
        public Date DataRegistro;
        public ICollection<RotaModel> Rotas { get; set; } = new List<RotaModel>();
    }
}

