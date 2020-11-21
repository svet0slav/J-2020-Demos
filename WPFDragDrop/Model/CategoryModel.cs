using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFDragDrop.Model
{
    //FROM https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8
    // This class implements INotifyPropertyChanged
    // to support one-way and two-way bindings
    // (such that the UI element updates when the source
    // has been changed dynamically)
    internal class CategoryModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Matches { get; set; }
        public bool MatchesAll { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemModel> Dropped { get; set; }

        public string MatchesName
        {
            get { return (MatchesAll) ? "All" : Matches; }
        }

        public string DroppedItems
        {
            get
            {
                if (Dropped == null) return "";
                var s = "";
                foreach (var item in Dropped)
                {
                    if (s == "")
                        s = item.ItemName;
                    else
                        s += ", " + item.ItemName;
                }
                return s;
            }
        }

        public bool Drop(ItemModel item)
        {
            if (item == null)
                return false;

            if (Dropped == null)
                Dropped = new ObservableCollection<ItemModel>();

            if (Dropped.Contains(item))
                return true;

            Dropped.Add(item);
            OnPropertyChanged("Dropped");
            OnPropertyChanged("DroppedItems");
            return true;
        }


        //FROM https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
