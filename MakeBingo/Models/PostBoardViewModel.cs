using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeBingo.Models
{
    public class PostBoardViewModel
    {
        public string Title { get; set; }
        public string Maker { get; set; }
        public string Link { get; set; }
        public string ParentId { get; set; }
        public BoardCell[][] Cells { get; set; }
    }
}
