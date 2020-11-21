using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFDragDrop.Data
{
    public class DataService
    {
        public List<CategoryDto> Categories;

        public List<RightCategoryDto> RightCategoryButtons;

        public DataService()
        {
            Categories = new List<CategoryDto>();
            RightCategoryButtons = new List<RightCategoryDto>();
        }

        public void Initialize()
        {
            Categories.Add(new CategoryDto()
            {
                Id = 1,
                Name = "Category A"
            });
            Categories[0].Items.Add(new ItemDto() { Id = 11, Name = "Item A1" });
            Categories[0].Items.Add(new ItemDto() { Id = 12, Name = "Item A2" });
            Categories[0].Items.Add(new ItemDto() { Id = 13, Name = "Item A3" });

            Categories.Add(new CategoryDto()
            {
                Id = 2,
                Name = "Category B"
            });
            Categories[1].Items.Add(new ItemDto() { Id = 21, Name = "Item B1" });
            Categories[1].Items.Add(new ItemDto() { Id = 22, Name = "Item B2" });
            Categories[1].Items.Add(new ItemDto() { Id = 23, Name = "Item B3" });
            Categories[1].Items.Add(new ItemDto() { Id = 24, Name = "Item B4" });

            Categories.Add(new CategoryDto()
            {
                Id = 3,
                Name = "Category C"
            });
            Categories[2].Items.Add(new ItemDto() { Id = 31, Name = "Item C1" });
            Categories[2].Items.Add(new ItemDto() { Id = 32, Name = "Item C2" });
            Categories[2].Items.Add(new ItemDto() { Id = 33, Name = "Item C3" });
            Categories[2].Items.Add(new ItemDto() { Id = 34, Name = "Item C4" });
            Categories[2].Items.Add(new ItemDto() { Id = 34, Name = "Item C5" });

            RightCategoryButtons.Add(new RightCategoryDto() { Name = "A", Matches = Categories[0].Name });
            RightCategoryButtons.Add(new RightCategoryDto() { Name = "B", Matches = Categories[1].Name });
            RightCategoryButtons.Add(new RightCategoryDto() { Name = "C", Matches = Categories[2].Name });
            RightCategoryButtons.Add(new RightCategoryDto() { Name = "D", Matches = string.Empty });
            RightCategoryButtons.Add(new RightCategoryDto() { Name = "E", Matches = string.Empty, MatchesAll = true });
        }
    }
}
