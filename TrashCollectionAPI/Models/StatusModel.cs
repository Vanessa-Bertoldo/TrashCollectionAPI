﻿namespace TrashCollectionAPI.Models
{
    public class StatusModel
    {
        public int IdStatus { get; set; }
        public string NomeStatus { get; set; }
        public ICollection<CaminhaoModel> Caminhoes { get; set; } = new List<CaminhaoModel>();
    }
}