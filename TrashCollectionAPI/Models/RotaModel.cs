namespace TrashCollectionAPI.Models
{
    public class RotaModel
    {
        public int IdRota { get; set; }
        public string NomeRota { get; set; }
        public string DescricaoRota { get; set; }
        public int IdColeta { get; set; }
        public ColetaModel Coleta { get; set; }
    }
}
