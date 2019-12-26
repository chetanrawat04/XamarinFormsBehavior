using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsBehavior.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRendererDroid))]
namespace XamarinFormsBehavior.Droid.CustomRenderer
{
    class CustomEntryRendererDroid : EntryRenderer
    {

        public CustomEntryRendererDroid(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}