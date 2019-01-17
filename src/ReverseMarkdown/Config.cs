namespace ReverseMarkdown
{
	public class Config
	{	
		public Config() : this(UnknownTagsOption.PassThrough, false, true, false, true) {} 

		public Config(UnknownTagsOption unknownTags=UnknownTagsOption.PassThrough, bool githubFlavored=false, bool removeComments = true, bool textNotMarkdown=false, bool compressNewlines = true)
		{
			this.UnknownTags = unknownTags;
			this.GithubFlavored = githubFlavored;
            this.TextNotMarkdown = textNotMarkdown;
            this.CompressNewlines = compressNewlines;
            this.RemoveComments = removeComments;
        }

        public UnknownTagsOption UnknownTags { get; }

        public bool GithubFlavored { get; }

        public bool RemoveComments { get; }

        public bool TextNotMarkdown { get; }

        public bool CompressNewlines { get; }

        public enum UnknownTagsOption
        {
            PassThrough,
            Drop,
            Bypass,
            Raise
        }
    }
}
