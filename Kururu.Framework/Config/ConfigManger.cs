using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kururu.Framework.Cache;

namespace Kururu.Framework.Config
{
    public class ConfigManger
    {
        private string _path;

        public ConfigManger (string path)
        {
            _path = path;
        }

        public async Task LoadData (CacheManger<string> manger)
        {

            string[] Data = await File.ReadAllLinesAsync(Path.GetFullPath(_path));
            bool CommentFound = false;
            foreach (var line in Data)
            {
                string Value = line;
                bool JustFound = false;
                if (Value.Contains("<!--")) 
                {
                    CommentFound = true;
                    if (Value.Contains("-->"))
                        CommentFound = false;
                    JustFound = true;
                    var index = Value.IndexOf("<");
                    Value = Value.Remove(index, Value.Length-index);
                }
                if (CommentFound && Value.Contains("-->"))
                {
                    Console.WriteLine("\n happens");
                    CommentFound = false;
                    JustFound = false;
                    var index = Value.IndexOf(">");
                    Value = Value.Remove(0, ++index);
                }
                if (Value.Contains("#"))
                {
                    var index = Value.IndexOf("#");
                    Value = Value.Remove(index, Value.Length-index);
                }
                if(string.IsNullOrEmpty(Value) || (!JustFound && CommentFound))
                    continue;
                if (!Value.Contains("="))
                    throw new Exception("Error, the data need to be in key=value pairs");
                Value = Value.Replace(" ", "");
                var KeyValue = Value.Split("=");
                await manger.AddAsync(KeyValue[0], KeyValue[1]);
            }
        }

    }
}