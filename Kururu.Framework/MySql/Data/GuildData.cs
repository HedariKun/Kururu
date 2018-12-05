using System;

namespace Kururu.Framework.MySql
{
    public class GuildData
    {
        [MySqlProperty] public ulong GuildID;
        [MySqlProperty] public string Prefix;
        [MySqlProperty] public int WelcomeMessageActive;
        [MySqlProperty] public string WelcomeMessage;
        [MySqlProperty] public ulong WelcomeMessageChannel;
        [MySqlProperty] public int GoodbyeMessageActive;
        [MySqlProperty] public string GoodbyeMessage;
        [MySqlProperty] public ulong GoodbyeMessageChannel;
        [MySqlProperty] public ulong DefaultRole;
        [MySqlProperty] public DateTime AddDate;

    }
}