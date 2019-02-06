using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MakeBingo.Models
{
    public class Board
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }
        public string Maker { get; set; }
        public string Link { get; set; }

        [JsonIgnore]
        public string ParentId { get; set; }
        [JsonIgnore]
        public Board Parent { get; set; }

        [JsonIgnore]
        public ICollection<Board> Children { get; set; } = new HashSet<Board>();

        [JsonIgnore]
        public ICollection<Result> Results { get; set; } = new HashSet<Result>();

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
