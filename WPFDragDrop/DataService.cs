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

        public List<string> Buttons;

        public DataService()
        {
            Categories = new List<CategoryData>();
            Buttons = new List<string>();
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

            Buttons.Add("A");
            Buttons.Add("B");
            Buttons.Add("C");
            Buttons.Add("D");
            Buttons.Add("E");
        }
    }
}
