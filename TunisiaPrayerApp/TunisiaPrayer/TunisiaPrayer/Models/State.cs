
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TunisiaPrayer.Models
{

    
    public class Rootobject
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Gouvernorat gouvernorat { get; set; }
        public Delegation[] delegation { get; set; }
    }

    public class Gouvernorat
    {
        public short id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

    public class Delegation
    {
        public short id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

    public class Something
    {
        
        public async Task<List<Rootobject>> LoadData()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"TunisiaPrayer.states.json");
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<List<Rootobject>>(text);
        }
    }
}
