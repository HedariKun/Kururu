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
            foreach (var line in Data)
            {
                if(line.StartsWith("#"))
                    continue;
                var KeyValue = line.Split("=");
                await manger.AddAsync(KeyValue[0], KeyValue[1]);
            }
        }

    }
}