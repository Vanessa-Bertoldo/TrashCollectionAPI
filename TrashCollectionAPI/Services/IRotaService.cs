﻿using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IRotaService
    {
        IEnumerable<RotaModel> GetAllRotas();
        IEnumerable<RotaModel> GetAllRotas(int id);
        RotaModel GetRotaById(int id);
        void AddNewRota(RotaModel rota);
        void UpdateRota(RotaModel rota);
        void DeleteRota(int id);
    }
}
