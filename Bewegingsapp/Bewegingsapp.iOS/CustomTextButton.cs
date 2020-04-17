using Bewegingsapp.iOS;
using Bewegingsapp.Model;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomTextButton))]
namespace Bewegingsapp.iOS
{
    public class CustomTextButton : ButtonRenderer
    {
        public CustomTextButton()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e) //zet de tekst centraal en zet woorden onderelkaar indien deze niet passen
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}