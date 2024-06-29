using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data
{
    public static class InitialData
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated(); 

            if (context.Coleta.Any() || context.Rota.Any() || context.Caminhao.Any() || context.Status.Any())
            {
                return; 
            }

            var status1 = new StatusModel { NomeStatus = "Ativo" };
            var status2 = new StatusModel { NomeStatus = "Inativo" };
            context.Status.AddRange(status1, status2);

            var coleta1 = new ColetaModel { NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro" };
            var coleta2 = new ColetaModel { NumeroVolume = 18.3, DataRegistro = DateTime.Now, NomeBairro = "Periferia" };
            context.Coleta.AddRange(coleta1, coleta2);

            var rota1 = new RotaModel { NomeRota = "Rota 1", DescricaoRota = "Descrição da Rota 1", Coleta = coleta1 };
            var rota2 = new RotaModel { NomeRota = "Rota 2", DescricaoRota = "Descrição da Rota 2", Coleta = coleta2 };
            context.Rota.AddRange(rota1, rota2);

            var caminhao1 = new CaminhaoModel { HNumeroMaxCapacidade = 100, Status = status1 };
            var caminhao2 = new CaminhaoModel { HNumeroMaxCapacidade = 120, Status = status2 };
            context.Caminhao.AddRange(caminhao1, caminhao2);

            context.SaveChanges();
        }
    }

}
