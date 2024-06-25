using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class RotaViewModel
    {
        public int IdRota;
        public string NomeRota;
        public string DescricaoRota;
        public int IdColeta { get; set; }
        public ColetaModel Coleta { get; set; }
    }
}
