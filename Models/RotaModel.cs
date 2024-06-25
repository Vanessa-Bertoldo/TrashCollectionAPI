namespace TrashCollectionAPI.Models
{
    public class RotaModel
    {
        public int IdRota;
        public string NomeRota;
        public string DescricaoRota;
        public int IdColeta { get; set; }
        public ColetaModel Coleta { get; set; }
    }
}
