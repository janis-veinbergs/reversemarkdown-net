﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HtmlAgilityPack;
using ReverseMarkdown.Converters;

namespace ReverseMarkdown
{
    public class Converter
    {
        private readonly IDictionary<string, IConverter> _converters = new Dictionary<string, IConverter>();
        private readonly IConverter _passThroughTagsConverter;
        private readonly IConverter _dropTagsConverter;
        private readonly IConverter _byPassTagsConverter;

        public Converter() : this(new Config()) {}

        public Converter(Config config)
        {
            Config = config;

            // instanciate all converters excluding the unknown tags converters
            foreach (var ctype in typeof(IConverter).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetTypeInfo().GetInterfaces().Contains(typeof(IConverter)) && 
                !t.GetTypeInfo().IsAbstract
                && t != typeof(PassThrough)
                && t != typeof(Drop)
                && t != typeof(ByPass)))
            {
                Activator.CreateInstance(ctype, this);
            }

            // register the unknown tags converters
            _passThroughTagsConverter = new PassThrough(this);
            _dropTagsConverter = new Drop(this);
            _byPassTagsConverter = new ByPass(this);
        }

        public Config Config { get; }

        public string Convert(string html)
        {
            html = Cleaner.PreTidy(html, Config.RemoveComments);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var root = doc.DocumentNode;

            var result = Lookup(root.Name).Convert(root);

            return result;
        }

        public void Register(string tagName, IConverter converter)
        {
            _converters[tagName] = converter;
        }

        public IConverter Lookup(string tagName)
        {
            return _converters.ContainsKey(tagName) ? _converters[tagName] : GetDefaultConverter(tagName);
        }

        private IConverter GetDefaultConverter(string tagName)
        {
            get { return _defaultedTagCount; }
        }

        public string DefaultedTagContext(string tagName)
        {
            return _defaultedTagContext[tagName];
        }

        public string CurrentContext = null;

        protected Dictionary<string, int> _defaultedTagCount = new Dictionary<string, int>();
        protected Dictionary<string, string> _defaultedTagContext = new Dictionary<string, string>();

		protected IConverter GetDefaultConverter(string tagName)
		{
            if (CurrentContext != null)
            {
                if (!_defaultedTagCount.TryGetValue(tagName, out int count))
                {
                    count = 0;
                    _defaultedTagContext[tagName] = CurrentContext;
                }
                _defaultedTagCount[tagName] = count + 1;
            }
			switch (this._config.UnknownTags)
			{
				case Config.UnknownTagsOption.PassThrough:
                    return _passThroughTagsConverter;
                case Config.UnknownTagsOption.Drop:
                    return _dropTagsConverter;
                case Config.UnknownTagsOption.Bypass:
                    return _byPassTagsConverter;
                default:
                    throw new UnknownTagException(tagName);
            }
        }
    }
}
