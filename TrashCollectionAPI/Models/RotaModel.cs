using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollectionAPI.Models
{
    public class RotaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRota { get; set; }
        public string NomeRota { get; set; }
        public string DescricaoRota { get; set; }
        public int IdColeta { get; set; }
        public ColetaModel Coleta { get; set; }
    }
}
