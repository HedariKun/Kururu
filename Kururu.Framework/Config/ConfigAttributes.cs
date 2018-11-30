using System;

namespace Kururu.Framework.Config
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConfigurationAttribute : Attribute 
    {
        public string Key;
        public ConfigurationAttribute (string key = "")
        {
            Key = key;
        }
    }
}