using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfTreeView
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Get every logical drive on the machine
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            //Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders

            //Try and get directories from the folder ignoring any issues
            try
            {
                var dirs = Directory.GetDirectories(fullPath); //E.g. User has no permission to Windows/System folder --> Exception

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(d => new DirectoryItem { FullPath = d, Type = DirectoryItemType.Folder }));
            }
            catch { } //Bad programming practice. In the try block we should not place instructon we know it will likely cause exception.

            #endregion

            #region Getfiles

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(f => new DirectoryItem { FullPath = f, Type = DirectoryItemType.File }));
            }
            catch { } //Bad programming practice. In the try block we should no tplace instructon we know it will likely cause exception.

            return items;
            
            #endregion
        }

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
