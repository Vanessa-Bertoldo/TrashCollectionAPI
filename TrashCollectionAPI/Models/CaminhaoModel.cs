using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace TrashCollectionAPI.Models
{
    public class CaminhaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCaminhao { get; set; }
        public string Modelo { get; set; }
        public int NumeroCapacidade { get; set; }
        public int HNumeroMaxCapacidade { get; set; }
        public int IdStatus { get; set; }
        public StatusModel Status { get; set; }

    }
}
