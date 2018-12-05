using System;
using Miki.Discord.Common;

namespace Kururu.Framework.Commands
{
	[AttributeUsage (AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		public string Name { get; private set; }

		public CommandAttribute (string name)
		{
			Name = name;
		}

	}

	[AttributeUsage (AttributeTargets.Method)]
	public class PermissionAttribute : Attribute
	{
		public GuildPermission Permission = GuildPermission.None;


		public PermissionAttribute (GuildPermission _permissions)
		{
			Permission = _permissions;
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
