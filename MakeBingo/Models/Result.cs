using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MakeBingo.Models
{
    public class Result
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string BoardId { get; set; }
        public Board Board { get; set; }

        private string _cells;

        [NotMapped]
        public BoardCell[][] Cells
        {
            get => JsonConvert.DeserializeObject<BoardCell[][]>(String.IsNullOrWhiteSpace(_cells) ? "[]" : _cells);
            set => _cells = JsonConvert.SerializeObject(value);
        }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
