using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.MarkupExtensions
{
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResorceId { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(ResorceId))
                return null;
            return ImageSource.FromResource(ResorceId);
        }
    }
}