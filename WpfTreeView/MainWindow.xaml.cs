using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructor
        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //This is the route of the UI in terms of databinding
            //That is, if we write in the MainWindow.xaml <Button Content="{Binding}"/> then
            //the Button will bind to Class1
            this.DataContext = new Class1();
                        
        }
        #endregion 
    }
}
