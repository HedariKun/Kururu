using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kururu.Framework.Config;
using Kururu.Framework.Commands;
using Miki.Discord.Common;

namespace Kururu.Framework
{
	public class CommandHandle
	{
		private List<Type> _modules = new List<Type> ();
		private Dictionary<string, Type> _commands = new Dictionary<string, Type> ();

		public CommandHandle ()
		{
			var assembly = Assembly.GetExecutingAssembly ();
			var types = assembly.GetTypes ().Where (x => x.IsSubclassOf (typeof (ModuleBase)));
			foreach (Type t in types)
			{
				_modules.Add (t);
				var methods = t.GetMethods ().Where (x => x.GetCustomAttributes<CommandAttribute> ().Count () > 0);
				foreach (var command in methods)
				{
					var name = command.GetCustomAttribute<CommandAttribute> ().Name.ToLower ();
					_commands.Add (name, t);
				}
			}
		}

		public async Task ExecuteCommand (MessageContext context)
		{
			if (_commands.TryGetValue (context.Command, out var data))
			{
				ModuleBase instance = (ModuleBase) Activator.CreateInstance (data);
				instance.Channel = await context.Message.GetChannelAsync ();
				if (instance.Channel is IDiscordGuildChannel guildChannel)
				{
					instance.Guild = await guildChannel.GetGuildAsync ();
				}

				instance.Author = instance.Guild == null ? context.Message.Author : await instance.Guild.GetMemberAsync (context.Message.Author.Id);

				instance.Message = context.Message;
				instance.Arg = context.Arg;

				var Fields = data.GetFields().Where(x => x.GetCustomAttribute<ConfigurationAttribute>() != null);
				foreach (var Field in Fields)
				{
					var name = Field.GetCustomAttribute<ConfigurationAttribute>().Key;
					if (string.IsNullOrEmpty(name))
						name = Field.Name;
					var Value = await DiscordBot.Instance.cacheManger.GetAsync(name);
					Field.SetValue(instance, Convert.ChangeType(Value, Field.FieldType));
				}

				var methods = data.GetMethods ().Where (x => x.GetCustomAttribute<CommandAttribute> () != null ? x.GetCustomAttribute<CommandAttribute> ().Name.ToLower () == context.Command : false);
				foreach (var method in methods)
				{
					var Permission = method.GetCustomAttribute<PermissionAttribute> ();
					if (Permission != null)
					{
						var hasPermission = await ((IDiscordGuildUser) instance.Author).HasPermissionsAsync (Permission.Permission);
						if (!hasPermission)
						{
							return;
						}
					}
					if (method.GetCustomAttribute<OwnerAttribute>() != null)
					{
						if (instance.Author.Id != Convert.ToUInt64(await DiscordBot.Instance.cacheManger.GetAsync("OwnerID")))
						{
							return;
						}
					}
					method.Invoke (instance, null);
				}
			}
		}

	}
}
