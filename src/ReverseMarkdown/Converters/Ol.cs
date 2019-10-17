using System;
using System.Linq;
using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
	public class Ol : ConverterBase
	{
		public Ol(Converter converter)
			: base(converter)
		{
			this.Converter.Register("ol", this);
			this.Converter.Register("ul", this);
			this.Converter.Register("dl", this);
		}

            return $"{prefixSuffix}{TreatChildren(node)}{prefixSuffix}";
        }
    }
}
