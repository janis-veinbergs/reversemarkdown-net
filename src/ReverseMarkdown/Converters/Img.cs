using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class Img : ConverterBase
    {
        public Img(Converter converter) : base(converter)
        {
            Converter.Register("img", this);
        }

		public override string Convert(HtmlNode node)
		{
            if (Converter.Config.BarePlaintext)
                return "";
			string alt = node.GetAttributeValue("alt", string.Empty);
			string src = node.GetAttributeValue("src", string.Empty);
			string title = this.ExtractTitle(node);

            title = title.Length > 0 ? $" \"{title}\"" : "";

            return $"![{alt}]({src}{title})";
        }
    }
}
