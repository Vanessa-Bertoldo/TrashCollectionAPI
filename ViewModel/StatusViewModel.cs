using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class StatusViewModel
    {
        public int IdStatus { get; set; }
        public string NomeStatus { get; set; }
        public ICollection<CaminhaoViewModel> Caminhoes { get; set; } = new List<CaminhaoViewModel>();
    }
}
