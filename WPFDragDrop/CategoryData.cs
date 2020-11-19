using System;
using System.Collections.Generic;

namespace WPFDragDrop
{
    public class CategoryData
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<ItemData> Items { get; set; }

        public CategoryData()
        {
            Items = new List<ItemData>();
        }
    }
}
