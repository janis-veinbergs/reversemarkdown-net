using System;

using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class Br : ConverterBase
    {
        public Br(Converter converter) : base(converter)
        {
            Converter.Register("br", this);
        }

		public override string Convert(HtmlNode node)
		{
			if (this.Converter.Config.GithubFlavored || this.Converter.Config.BarePlaintext)
			{
				return Environment.NewLine;
			}

            return $"  {Environment.NewLine}";
        }
    }
}
