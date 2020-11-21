using System;
using System.Collections.Generic;

namespace WPFDragDrop.Data
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<ItemDto> Items { get; set; }

        public CategoryDto()
        {
            Items = new List<ItemDto>();
        }
    }
}
