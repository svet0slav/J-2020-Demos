using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.Model
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Matches { get; set; }
        public bool MatchesAll { get; set; } = false;
    }
}
