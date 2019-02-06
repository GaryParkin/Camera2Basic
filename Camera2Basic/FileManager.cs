using System.IO;

namespace Camera2Basic
{
   internal class FileManager
   {
      private readonly string mFolder;
      private DirectoryInfo mInfo;

      public FileManager(string folder)
      {
         mFolder = folder;
         mInfo = new DirectoryInfo(mFolder);
      }

      public FileInfo[] GetImageFiles()
      {
         FileInfo[] files = mInfo.GetFiles();
         return files;
      }

      public void DeleteAllImageFiles()
      {
         foreach (FileInfo file in mInfo.EnumerateFiles())
         {
            file.Delete();
         }
      }

      public void DeleteImageByFile(FileInfo file)
      {
            file.Delete();
      }

   }
}