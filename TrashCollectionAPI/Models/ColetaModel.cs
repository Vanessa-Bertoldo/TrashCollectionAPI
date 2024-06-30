using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrashCollectionAPI.Models
{
    public class ColetaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdColeta { get; set; }
        public string NomeBairro { get; set; }
        public double NumeroVolume { get; set; }
        public DateTime DataColeta { get; set; }
        public DateTime DataRegistro { get; set; }
        public ICollection<RotaModel> Rotas { get; set; } = new List<RotaModel>();

    }
}

