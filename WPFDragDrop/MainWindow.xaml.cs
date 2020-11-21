using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
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
        internal ListView dragSource { get; set; } = null;

        public MainWindow()
        {
            InitializeComponent();

            InitializeData();

            BindData();
        }

        private void InitializeData()
        {
            dragSource = null;
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

        private void lvItems_DragEnter(object sender, DragEventArgs e)
        {
            // Item from left is dragged
            //if (e.Data.GetDataPresent("GroupName"))
            if (e.Data.GetDataPresent("ItemModel") && sender != e.Source)
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void lvButtons_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("WPFDragDrop.Model.ItemModel"))
            {
                var data = e.Data.GetData("WPFDragDrop.Model.ItemModel");
                var itemData = (ItemModel)data;
                foreach(var model in ButtonsModel)
                {
                    if (model.Dropped != null && model.Dropped.Any(m => m.ItemName == itemData.ItemName))
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                }
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void lvButtons_TextBlock_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("WPFDragDrop.Model.ItemModel"))
            {
                var data = e.Data.GetData("WPFDragDrop.Model.ItemModel");
                var itemData = (ItemModel)data;
                CategoryModel category = (CategoryModel)(((TextBlock)sender).DataContext);

                //If same category and have not been added already, show Copy, otherwise show "block".
                if (category.MatchesAll || category.Matches.Contains(itemData.GroupName))
                {
                    foreach (var model in ButtonsModel)
                    {
                        if (model.Dropped != null && model.Dropped.Any(m => m.ItemName == itemData.ItemName))
                        {
                            e.Effects = DragDropEffects.None;
                            return;
                        }
                    }
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
        }

        private void lvButtons_TextBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("WPFDragDrop.Model.ItemModel"))
            {
                var data = e.Data.GetData("WPFDragDrop.Model.ItemModel");
                var itemData = (ItemModel)data;
                CategoryModel category = (CategoryModel)(((TextBlock)sender).DataContext);
                //If same category and have not been added already, show Copy, otherwise show "block".
                if (category.MatchesAll || category.Matches.Contains(itemData.GroupName))
                {
                    category.Drop(itemData);
                    e.Handled = true;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                    e.Handled = true;
                }

            }
        }

        private void lvItems_ItemTextBlock_DragEnter(object sender, DragEventArgs e)
        {
            // Item from left is dragged and can not drop here
                e.Effects = DragDropEffects.None;
        }

        #region GetDataFromListView(ListBox,Point) on Mouse LeftButton Down

        // Code from https://www.c-sharpcorner.com/uploadfile/dpatra/drag-and-drop-item-in-listbox-in-wpf/
        private void lvItems_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView control = (ListView)sender;
            dragSource = control;
            object data = GetDataFromListView(dragSource, e.GetPosition(control));

            if (data != null)
            {
                DragDrop.DoDragDrop(control, data, DragDropEffects.Copy);
                //BottomHover.Text = ((ItemModel)data)
            }
        }

        
        private object GetDataFromListView(ListView source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }

        #endregion

    }
}
