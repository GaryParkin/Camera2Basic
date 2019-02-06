using Android.Media;
using Android.Util;
using Java.IO;
using Java.Lang;
using Java.Nio;
using System;

namespace Camera2Basic.Listeners
{
   public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
   {
      private static readonly string TAG = "ImageAvailableListener";

      public ImageAvailableListener(Camera2BasicFragment fragment, File file, string folder)
      {
         if (fragment == null)
            throw new System.ArgumentNullException("fragment");
         if (file == null)
            throw new System.ArgumentNullException("file");
         if (folder == null)
            throw new System.ArgumentNullException("folder");

         owner = fragment;
         this.file = file;
         this.folder = folder;
      }

      private readonly File file;
      private readonly string folder;
      private readonly Camera2BasicFragment owner;

      //public File File { get; private set; }
      //public Camera2BasicFragment Owner { get; private set; }

      /// <summary>
      /// Fires when the image is available from the camera
      /// </summary>
      /// <param name="reader"></param>
      public void OnImageAvailable(ImageReader reader)
      {
         owner.mBackgroundHandler.Post(new ImageSaver(reader.AcquireNextImage(), file, folder));
      }

      // Saves a JPEG {@link Image} into the specified {@link File}.
      private class ImageSaver : Java.Lang.Object, IRunnable
      {
         // The JPEG image
         private Image mImage;

         // The folder we save the image into.
         private string mFolder;

         // The file we save the image into.
         private File mFile;

         public ImageSaver(Image image, File file, string folder)
         {
            if (image == null)
               throw new System.ArgumentNullException("image");
            if (file == null)
               throw new System.ArgumentNullException("file");
            if (folder == null)
               throw new System.ArgumentNullException("folder");

            mImage = image;
            mFile = file;
            mFolder = folder;

            Log.Debug(TAG, "********************************************************************************************************************************");
            Log.Debug(TAG, $"mImage: {mImage}");
            Log.Debug(TAG, $"mImage: {mImage}  mFolder: {mFolder}  mFile: {mFile}");
            Log.Debug(TAG, "********************************************************************************************************************************");
         }

         public void Run()
         {
            // "/storage/emulated/0/Android/data/Camera2Basic.Camera2Basic/files"

            mFile = new File(mFolder, GetNewFileName());

            ByteBuffer buffer = mImage.GetPlanes()[0].Buffer;
            byte[] bytes = new byte[buffer.Remaining()];
            buffer.Get(bytes);
            using (var output = new FileOutputStream(mFile))
            {
               try
               {
                  output.Write(bytes);
               }
               catch (IOException e)
               {
                  e.PrintStackTrace();
               }
               finally
               {
                  mImage.Close();
               }
            }
         }
      }

      /// <summary>
      /// Filename is a Guid with a DateTime
      /// </summary>
      /// <returns>Calculated filename</returns>
      private static string GetNewFileName()
      {
         string fileName = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");

         return fileName + ".jpg";
      }
   }
}