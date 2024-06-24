using System.Net.NetworkInformation;

namespace TrashCollectionAPI.Models
{
    public class CaminhaoModel
    {
        public int idCaminhao;
        public string modelo;
        public int numeroCapacidade;
        public int numeroMaxCapacidade;
        public StatusModel nomeStatus;
    }
}
