using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace TourAgency.Web.TagHelpers
{
    public class HotelStarTagHelper : TagHelper
    {
        [HtmlAttributeName("for-hotelcategoryname")]
        public string HotelCategoryName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            int starNumber = GetStarNumber();
            if (starNumber > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < starNumber; i++)
                {
                    stringBuilder.Append("<i class=\"fa fa-star fa-lg\"></i>");
                }
                output.Content.SetHtmlContent(stringBuilder.ToString());
            }
            else
            {
                output.Content.SetHtmlContent($"<span>{HotelCategoryName}</span>");
            }
        }

        private int GetStarNumber()
        {
            if (HotelCategoryName.TrimEnd()[HotelCategoryName.Length - 1] == '*')
            {
                HotelCategoryName =  HotelCategoryName.Remove(HotelCategoryName.Length - 1);
                if (int.TryParse(HotelCategoryName, out int starNumber))
                {
                    return starNumber;
                }
            }

            return -1;
        }
    }
}
