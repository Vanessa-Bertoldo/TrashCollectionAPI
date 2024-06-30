using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class CaminhaoViewModel
    {
        public int IdCaminhao { get; set; }
        public string Modelo { get; set; }
        public int NumeroCapacidade { get; set; }
        public int HNumeroMaxCapacidade { get; set; }
        public int IdStatus { get; set; }
    }
}
