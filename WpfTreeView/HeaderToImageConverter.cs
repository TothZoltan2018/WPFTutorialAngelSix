using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))] //We placed this attribute here so that the xaml can find it.
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //By default, we presume an image
            var image = "Images/file.png";

            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Images/drive.png";
                    break;
                case
                    DirectoryItemType.Folder:
                    image = "Images/folder-closed.png";
                    break;
            }
            
            //Right click on the files in the Solution Explorer, and include project.
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
