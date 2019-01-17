﻿
using System.Linq;

using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class Em : ConverterBase
    {
        public Em(Converter converter) : base(converter)
        {
            var elements = new [] { "em", "i" };
            
            foreach (var element in elements)
            {
                Converter.Register(element, this);
            }
        }

		public override string Convert(HtmlNode node)
		{
			string content = this.TreatChildren(node);
			if (Converter.Config.TextNotMarkdown || string.IsNullOrEmpty(content.Trim()) || AlreadyItalic(node))
			{
				return content;
			}
			else
			{
                return $"*{content.Trim()}*";
            }
		}

        private bool AlreadyItalic(HtmlNode node)
        {
            return node.Ancestors("i").Any() || node.Ancestors("em").Any();
        }
    }
}
