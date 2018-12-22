using System;
using Kururu.Framework.MySql;

namespace Kururu.Common
{
    public class PastaData
    {
        [MySqlProperty] public string Key;
        [MySqlProperty] public string Value;
        [MySqlProperty] public ulong UserID;
        [MySqlProperty] public ulong ServerID;
        [MySqlProperty] public DateTime AddDate;

    }
}