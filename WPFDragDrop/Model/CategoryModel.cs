using System;

namespace WPFDragDrop.Model
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Matches { get; set; }
        public bool MatchesAll { get; set; } = false;

        public string MatchesName
        {
            get { return (MatchesAll) ? "All" : Matches; }
        }
    }
}
