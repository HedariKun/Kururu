using System;
using Miki.Discord.Common;

namespace Kururu.Framework.Commands
{
	[AttributeUsage (AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		public string Name { get; set; }
		public GuildPermission Permission = GuildPermission.None;
		public string[] Alias;

		public CommandAttribute ()
		{
		}

	}

	[AttributeUsage (AttributeTargets.Class)]
	public class ModuleAttribute : Attribute
	{
		public string Name = "";

		public ModuleAttribute (string name)
		{
			this.Name = name;
		}

	}

	[AttributeUsage (AttributeTargets.Method)]
	public class OwnerAttribute : Attribute 
	{
		
	}
}
