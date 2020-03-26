using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Bewegingsapp.Model;
using Bewegingsapp.iOS;
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

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
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