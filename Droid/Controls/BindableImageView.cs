using System;
using Android.Widget;
using Android.Graphics.Drawables;

namespace TestLocation.Droid
{
	public class BindableImageView : ImageView
	{
		public BindableImageView (IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}
		

		public BindableImageView (Android.Content.Context context) : base (context)
		{
		}
		

		public BindableImageView (Android.Content.Context context, Android.Util.IAttributeSet attrs) : base (context, attrs)
		{
		}
		

		public BindableImageView (Android.Content.Context context, Android.Util.IAttributeSet attrs, int defStyleAttr) : base (context, attrs, defStyleAttr)
		{
		}
		

		public BindableImageView (Android.Content.Context context, Android.Util.IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base (context, attrs, defStyleAttr, defStyleRes)
		{
		}

		int _imageResource;
		public int ImageResource { 
			get{
				return _imageResource;
			}
			set{
				if (_imageResource != value) {
					_imageResource = value;
					SetImageResource (_imageResource);
				}
			}
		}
	}
}

