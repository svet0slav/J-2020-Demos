using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop
{
    public class DataService
    {
        public List<CategoryData> Categories;

        public List<CategoryButton> CategoryButtons;

        public DataService()
        {
            Categories = new List<CategoryData>();
            CategoryButtons = new List<CategoryButton>();
        }

        public void Initialize()
        {
            Categories.Add(new CategoryData()
            {
                Id = 1,
                Name = "Category A"
            });
            Categories[0].Items.Add(new ItemData() { Id = 11, Name = "Item A1" });
            Categories[0].Items.Add(new ItemData() { Id = 12, Name = "Item A2" });
            Categories[0].Items.Add(new ItemData() { Id = 13, Name = "Item A3" });

            Categories.Add(new CategoryData()
            {
                Id = 2,
                Name = "Category B"
            });
            Categories[1].Items.Add(new ItemData() { Id = 21, Name = "Item B1" });
            Categories[1].Items.Add(new ItemData() { Id = 22, Name = "Item B2" });
            Categories[1].Items.Add(new ItemData() { Id = 23, Name = "Item B3" });
            Categories[1].Items.Add(new ItemData() { Id = 24, Name = "Item B4" });

            Categories.Add(new CategoryData()
            {
                Id = 3,
                Name = "Category C"
            });
            Categories[2].Items.Add(new ItemData() { Id = 31, Name = "Item C1" });
            Categories[2].Items.Add(new ItemData() { Id = 32, Name = "Item C2" });
            Categories[2].Items.Add(new ItemData() { Id = 33, Name = "Item C3" });
            Categories[2].Items.Add(new ItemData() { Id = 34, Name = "Item C4" });
            Categories[2].Items.Add(new ItemData() { Id = 34, Name = "Item C5" });

            CategoryButtons.Add(new CategoryButton() { Name = "A", Matches = "Category A" });
            CategoryButtons.Add(new CategoryButton() { Name = "B", Matches = "Category B" });
            CategoryButtons.Add(new CategoryButton() { Name = "C", Matches = "Category C" });
            CategoryButtons.Add(new CategoryButton() { Name = "D", Matches = string.Empty });
            CategoryButtons.Add(new CategoryButton() { Name = "E", Matches = string.Empty, MatchesAll = true });
        }
    }
}
