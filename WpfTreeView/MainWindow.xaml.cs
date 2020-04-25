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
        }
        #endregion

        #region On Loaded
        /// <summary>
        /// When the aplication first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem();

                //Set the header
                item.Header = drive;
                //And the full path
                item.Tag = drive;

                //Add a dummy item
                item.Items.Add(null);

                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add it to the main treeview
                FolderView.Items.Add(item);
            }
        }

        #endregion

        #region Folder Expanded
        /// <summary>
        /// When a folder is expanded, find the sub fodlers/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;

            //If the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();

            //Get full path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders
            //Create a blank list for directories
            var directories = new List<string>();

            //Try and get directories from the folder ignoring any issues
            try
            {
                var dirs = Directory.GetDirectories(fullPath); //E.g. USer has no permissiuon to Windows/System folder --> Exception

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch (Exception)
            { } //Bad programming practice. In the try block we should no tplace instructon we know it will likely cause exception.

            //For each directory
            directories.ForEach(directoryPath =>
            {
                //Create directory item
                var subItem = new TreeViewItem()
                {
                    //Set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    //And tag as full path
                    Tag = directoryPath
                };
                                
                //Add dummy item so we can expand folder
                subItem.Items.Add(null);

                //Handle expanding
                subItem.Expanded += Folder_Expanded;

                //Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion

            #region Getfiles
            //Create a blank list for files
            var files = new List<string>();

            //Try and get files from the folder ignoring any issues
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch (Exception)
            { } //Bad programming practice. In the try block we should no tplace instructon we know it will likely cause exception.

            //For each file
            files.ForEach(filePath =>
            {
                //Create file item
                var subItem = new TreeViewItem()
                {
                    //Set header as file name
                    Header = GetFileFolderName(filePath),
                    //And tag as full path
                    Tag = filePath
                };

                //Add this item to the parent
                item.Items.Add(subItem);
            });
            #endregion

        }

        #endregion

        #region Helpers
        /// <summary>
        /// Find a File or Folder from a full path
        /// </summary>
        /// <param name="path">The ful path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //C:\Something\a folder
            //C:\Something/a folder
            //C:\Something\a file
            //file.png

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //make all slashes to backslashes (paltform independency)
            var normalizedPath = path.Replace('/', '\\');

            //Find the last backslash position
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If we dont find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;
            
            //Return the name after the last backslash
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
