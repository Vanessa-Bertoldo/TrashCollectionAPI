using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class StatusViewModel
    {
        public int IdStatus;
        public string NomeStatus;
        public ICollection<CaminhaoModel> Caminhoes { get; set; } = new List<CaminhaoModel>();
    }
}
