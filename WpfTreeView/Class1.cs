using System.ComponentModel;
using System.Threading.Tasks;

namespace WpfTreeView
{
    public class Class1 : INotifyPropertyChanged // --> From this Interface this class is a ViewModel
    {
        private string mTest;

        public event PropertyChangedEventHandler PropertyChanged  = (sender, e) => { };

        //if we write in the MainWindow.xaml <Button Content="{Binding}"/> then content wants to write string, which is the name of the class1. Instead,
        // write something more reasonable, like:
        public override string ToString()
        {
            return "Hello Word!";
        }

        public string TestProp
        {
            get
            {
                return mTest;
            }
            set
            {
                if (mTest == value)
                    return;

                mTest = value;
                //In order that "TestProp" is refreshed:
                //PropertyChanged(this, new PropertyChangedEventArgs("TestProp")); //This will fire the PropertyChangedEventHandler event.
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TestProp))); //This will fire the PropertyChangedEventHandler event.
            }
        } 

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
