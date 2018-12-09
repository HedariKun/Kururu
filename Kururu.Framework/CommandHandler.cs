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
		private Dictionary<string[], Type> _commands = new Dictionary<string[], Type> ();

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
					var CommandInfo = command.GetCustomAttribute<CommandAttribute>();
					string[] alias = new string[]{CommandInfo.Name.ToLower()};
					if (CommandInfo.Alias != null)
					{
						foreach (var Name in CommandInfo.Alias)
						{
							alias = alias.Append(Name.ToLower()).ToArray();
						}
					}
					_commands.Add (alias, t);
				}
			}
		}

		public async Task ExecuteCommand (MessageContext context)
		{
			foreach (var Command in _commands)
			{
				foreach (var Name in Command.Key)
				{
					if (Name == context.Command)
					{
						var data = Command.Value;
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
						var methods = data.GetMethods().Where(x => {
							var CommandInfo = x.GetCustomAttribute<CommandAttribute>();
							if (CommandInfo != null)
							{
								if (CommandInfo.Name.ToLower() == context.Command)
									return true;

								if (CommandInfo.Alias != null)
									foreach(var Alias in CommandInfo.Alias)
									{
										if (Alias.ToLower() == context.Command)
											return true;
									}
							}
							return false;
						});
						foreach (var method in methods)
						{
							var Permission = method.GetCustomAttribute<CommandAttribute> ().Permission;
							if (Permission != GuildPermission.None)
							{
								var hasPermission = await ((IDiscordGuildUser) instance.Author).HasPermissionsAsync (Permission);
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

	}
}
