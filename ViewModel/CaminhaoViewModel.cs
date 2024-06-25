using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.ViewModel
{
    public class CaminhaoViewModel
    {
        public int IdCaminhao;
        public string Modelo;
        public int NumeroCapacidade;
        public int HNumeroMaxCapacidade;
        public int IdStatus { get; set; }
        public StatusModel Status;
    }
}
