using Android.Views;

namespace Camera2Basic.Listeners
{
   public class Camera2BasicSurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
   {
      private readonly Camera2BasicFragment owner;

      public Camera2BasicSurfaceTextureListener(Camera2BasicFragment owner)
      {
         if (owner == null)
            throw new System.ArgumentNullException("owner");
         this.owner = owner;
      }

      // This activates first
      public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int width, int height)
      {
         // Call the open camera - gets the active camera
         owner.OpenCamera(width, height);
      }

      public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
      {
         return true;
      }

      public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
      {
         owner.ConfigureTransform(width, height);
      }

      public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
      {

      }
   }
}