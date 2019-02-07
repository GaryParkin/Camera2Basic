using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace Camera2Basic
{
   class PhotoListAdapter :BaseAdapter<string>
   {
      private readonly List<string> _files;
      private readonly Activity _context;
      public int position;

      public PhotoListAdapter(Activity context, List<string> files)
      {
         _context = context;
         _files = files;
      }

      public List<string> GetList()
      {
         return _files;
      }

      public override long GetItemId(int position)
      {
         return position;
      }

      public override int Count
      {
         get { return _files.Count; }
      }

      public override string this[int position]
      {
         get { return _files[position]; }
      }

      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         var view = convertView;

         if (view == null)
         {
            view = _context.LayoutInflater.Inflate(Resource.Layout.image_row, null);
         }

         ImageView photoImageView = view.FindViewById<ImageView>(Resource.Id.PhotoImageView);

         Bitmap bitmap = BitmapFactory.DecodeFile(_files[position]);

         photoImageView.SetImageBitmap(bitmap);

         return view;

      }

   }
}