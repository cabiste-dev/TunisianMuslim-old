using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TunisiaPrayer.Models
{

    public class Rootobject
    {
        public short Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public List<Delegation> Delegations { get; set; }

        public override string ToString()
        {
            return this.NameEn;
        }
    }

    public class Delegation
    {
        public short Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public override string ToString()
        {
            return this.NameEn;
        }
    }
    public class StateService
    {

        public List<Rootobject> LoadData()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream($"TunisiaPrayer.Resources.states.json");
            //string text = "";
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<Rootobject>>(reader.ReadToEndAsync().Result);
            }
        }
    }
}
