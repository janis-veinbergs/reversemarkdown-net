﻿using System;
using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class H : ConverterBase
    {
        public H(Converter converter) : base(converter)
        {
            var elements = new [] { "h1", "h2", "h3", "h4", "h5", "h6" };
            foreach (var element in elements)
            {
                Converter.Register(element, this);
            }
        }

		public override string Convert(HtmlNode node)
		{
            string prefix = "";
            if (!Converter.Config.BarePlaintext)
                prefix = new string('#', System.Convert.ToInt32(node.Name.Substring(1)));

            return $"{Environment.NewLine}{prefix} {TreatChildren(node)}{Environment.NewLine}";
        }
    }
}
