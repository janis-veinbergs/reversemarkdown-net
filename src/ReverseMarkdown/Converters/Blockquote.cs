﻿using System;
using System.Linq;

using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class Blockquote : ConverterBase
    {
        public Blockquote(Converter converter) : base(converter)
        {
            Converter.Register("blockquote", this);
        }

        public override string Convert(HtmlNode node)
        {
            var content = TreatChildren(node).Trim();

            if (this.Converter.Config.TextNotMarkdown)
                return content;

			// get the lines based on carriage return and prefix "> " to each line
			var lines = content.ReadLines().Select(item => "> " + item + Environment.NewLine);

            // join all the lines to a single line
            var result = lines.Aggregate(string.Empty, (curr, next) => curr + next);

            return $"{Environment.NewLine}{Environment.NewLine}{result}{Environment.NewLine}";
        }
    }
}
