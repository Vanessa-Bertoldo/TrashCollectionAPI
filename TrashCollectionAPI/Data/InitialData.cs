using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data
{
    public static class InitialData
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Status.Any() || context.Coleta.Any() || context.Rota.Any() || context.Caminhao.Any())
            {
                return; 
            }

            var status1 = new StatusModel { NomeStatus = "Disponível" };
            var status2 = new StatusModel { NomeStatus = "Indisponível" };
            var status3 = new StatusModel { NomeStatus = "Em Manutenção" };
            context.Status.AddRange(status1, status2, status3);

            var coleta1 = new ColetaModel { NumeroVolume = 20.5, DataRegistro = DateTime.Now.AddDays(-1), NomeBairro = "Centro" };
            var coleta2 = new ColetaModel { NumeroVolume = 18.3, DataRegistro = DateTime.Now.AddDays(-2), NomeBairro = "Periferia" };
            var coleta3 = new ColetaModel { NumeroVolume = 15.0, DataRegistro = DateTime.Now.AddDays(-3), NomeBairro = "Industrial" };
            context.Coleta.AddRange(coleta1, coleta2, coleta3);

            var rota1 = new RotaModel { NomeRota = "Rota 1", DescricaoRota = "Descrição da Rota 1", IdColeta = coleta1.IdColeta };
            var rota2 = new RotaModel { NomeRota = "Rota 2", DescricaoRota = "Descrição da Rota 2", IdColeta = coleta1.IdColeta };
            var rota3 = new RotaModel { NomeRota = "Rota 3", DescricaoRota = "Descrição da Rota 3", IdColeta = coleta1.IdColeta };
            context.Rota.AddRange(rota1, rota2, rota3);

            var caminhao1 = new CaminhaoModel { HNumeroMaxCapacidade = 100, Status = status1 };
            var caminhao2 = new CaminhaoModel { HNumeroMaxCapacidade = 120, Status = status2 };
            var caminhao3 = new CaminhaoModel { HNumeroMaxCapacidade = 110, Status = status3 };
            context.Caminhao.AddRange(caminhao1, caminhao2, caminhao3);

            context.SaveChanges();
        }
    }

}
