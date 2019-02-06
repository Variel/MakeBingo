using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeBingo.Models
{
    public class BoardCell
    {
        public string Label { get; set; }
        public bool Checked { get; set; }
        public byte Pattern { get; set; }
    }
}
