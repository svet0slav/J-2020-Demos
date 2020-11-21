using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFDragDrop.Data;
using WPFDragDrop.Model;

namespace WPFDragDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// ListView XAML code taken from https://www.wpf-tutorial.com/listview-control/listview-grouping/
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<ItemModel> ItemsModel { get; set; }
        internal ObservableCollection<CategoryModel> ButtonsModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            InitializeData();

            BindData();
        }

        private void InitializeData()
        {
            var service = new DataService();
            service.Initialize();
            ItemsModel = new ObservableCollection<ItemModel>();

            foreach (var category in service.Categories)
            {
                foreach (var item in category.Items)
                {
                    ItemsModel.Add(new ItemModel()
                    {
                        GroupId = category.Id,
                        GroupName = category.Name,
                        ItemId = item.Id,
                        ItemName = item.Name
                    });
                }
            }

            ButtonsModel = new ObservableCollection<CategoryModel>();

            foreach (var btn in service.RightCategoryButtons)
            {
                ButtonsModel.Add(new CategoryModel()
                {
                    Name = btn.Name,
                    Matches = btn.Matches,
                    MatchesAll = btn.MatchesAll
                });
            }
        }

        private void BindData()
        {
            lvItems.ItemsSource = ItemsModel;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvItems.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("GroupName");
            view.GroupDescriptions.Add(groupDescription);

            lvButtons.ItemsSource = ButtonsModel;
        }

    }
}
