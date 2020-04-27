using PropertyChanged;
//using System.ComponentModel;

namespace WpfTreeView
{
    /// <summary>
    /// A base view model that fires the Property Changed events as needed. (In the background, by Fody nuget package.)
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel// : INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
    }
}