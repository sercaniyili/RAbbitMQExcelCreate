﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitMQWeb.Excel.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime? CreateDate { get; set; }
        public FileStatus FileStatus { get; set; }

        [NotMapped]
        public string GetCreatedDate => CreateDate.HasValue ? CreateDate.Value.ToShortDateString() : "-";
    }

    public enum FileStatus
    {
        Creating,
        Comleted
    }
}
