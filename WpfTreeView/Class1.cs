using PropertyChanged;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WpfTreeView
{
    //Fody (from nuget package) related attribute.
    //This finds all properties in ths class and fires the PropertyChanged event when the prop is changed
    //It seems that we don't need to implement INotifyPropertyChanged interface.
    [AddINotifyPropertyChangedInterface]
    public class Class1
    {
        //if we write in the MainWindow.xaml <Button Content="{Binding}"/> then content wants to write string, which is the name of the class1. Instead,
        // write something more reasonable, like:
        public override string ToString()
        {
            return "Hello Word!";
        }

        public string TestProp { get; set; }

        public Class1()
        {
            Task.Run(async () =>
            {
                int i = 0;
                while (true)
                {
                    await Task.Delay(200);
                    TestProp = (i++).ToString();
                 }
            });
        }
    }
}
