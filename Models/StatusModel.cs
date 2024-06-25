namespace TrashCollectionAPI.Models
{
    public class StatusModel
    {
        public int IdStatus;
        public string NomeStatus;
        public ICollection<CaminhaoModel> Caminhoes { get; set; } = new List<CaminhaoModel>();  
    }
}
