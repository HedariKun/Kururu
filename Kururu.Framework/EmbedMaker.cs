using Miki.Discord.Common;
using Miki.Discord.Rest;

namespace Kururu.Framework
{
	public class EmbedMaker : DiscordEmbed
	{
		public EmbedMaker ()
		{
			Fields = new System.Collections.Generic.List<EmbedField>();
			setColor (new Color (14423100));
		}
		public EmbedMaker setTitle (string title)
		{
			this.Title = title;
			return this;
		}

		public EmbedMaker setDesctiption (string description)
		{
			this.Description = description;
			return this;
		}

		public EmbedMaker setImage (string url)
		{
			this.Image = new EmbedImage ()
			{
				Url = url
			};
			return this;
		}

		public EmbedMaker setThumbnail (string url)
		{
			this.Thumbnail = new EmbedImage ()
			{
				Url = url
			};
			return this;
		}

		public EmbedMaker setColor (Color color)
		{
			this.Color = color?.Value ?? 0;
			return this;
		}

		public EmbedMaker setColor (int r, int g, int b)
		{
			this.Color = new Color (r, g, b).Value;
			return this;
		}

		public EmbedMaker setColor (float r, float g, float b)
		{
			this.Color = new Color (r, g, b).Value;
			return this;
		}

		public EmbedMaker setAuthor (string name, string iconurl = null, string url = null)
		{
			this.Author.Name = name;
			this.Author.IconUrl = iconurl;
			this.Author.Url = url;
			return this;
		}

		public EmbedMaker setFooter (string text, string iconurl = null)
		{
			this.Footer = new EmbedFooter() {IconUrl = iconurl, Text = text};
			return this;
		}

		public EmbedMaker addField (string title, string content)
		{
			this.Fields.Add (new EmbedField () { Title = title, Content = content, Inline = false});
			return this;
		}

		public EmbedMaker addInlineField (string title, string content)
		{
			this.Fields.Add (new EmbedField () { Title = title, Content = content, Inline = true });
			return this;
		}

	}
}
