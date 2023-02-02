using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreMvc004.TagHelpers
{
    public class ImageThumbnailTagHelper :TagHelper
    {
        public string ImageSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";  

            string fileName = ImageSrc.Split('.')[0];
            string fileExtention = Path.GetFileName(ImageSrc); 

            output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtention}");
        }
    }
}
