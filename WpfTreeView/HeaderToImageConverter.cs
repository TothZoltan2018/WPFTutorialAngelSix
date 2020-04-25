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
    [ValueConversion(typeof(string), typeof(BitmapImage))] //We placed this attribute here so that the xaml can find it.
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Get the full path
            var path = (string)value;
            if (path == null)
                return null;

            //Get the name of the file/folder
            var name = DirectoryStructure.GetFileFolderName(path);

            //By default, we presume an image
            var image = "Images/file.png";

            //if the name is blank, we presume it's a drive as we cannot have a blank file or folder name; e.g. C:/
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            //else if (new FileInfo(path).Directory.Attributes.HasFlag(FileAttributes.Directory)) //Not good, the files are dispalyed as folders, too.
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))            
                image = "Images/folder-closed.png";

            //Right click on the files in the Solution Explorer, and include project.
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
