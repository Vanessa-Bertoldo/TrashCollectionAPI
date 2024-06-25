using System.Net.NetworkInformation;

namespace TrashCollectionAPI.Models
{
    public class CaminhaoModel
    {
        public int IdCaminhao;
        public string Modelo;
        public int NumeroCapacidade;
        public int HNumeroMaxCapacidade;
        public int IdStatus { get; set; }
        public StatusModel Status;
    }
}
