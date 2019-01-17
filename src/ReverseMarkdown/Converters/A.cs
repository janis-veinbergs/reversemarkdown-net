using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class A : ConverterBase
    {
        public A(Converter converter)
            : base(converter)
        {
            Converter.Register("a", this);
        }

        public override string Convert(HtmlNode node)
        {
            var name = TreatChildren(node);

            var href = node.GetAttributeValue("href", string.Empty);
            var title = ExtractTitle(node);
            title = title.Length > 0 ? $" \"{title}\"" : "";

			if (Converter.Config.BarePlaintext || href.StartsWith("#") || string.IsNullOrEmpty(href) || string.IsNullOrEmpty(name))
			{
				return name;
			}
			else
			{
				return string.Format("[{0}]({1}{2})", name, href, title);
			}
		}
	}
}
