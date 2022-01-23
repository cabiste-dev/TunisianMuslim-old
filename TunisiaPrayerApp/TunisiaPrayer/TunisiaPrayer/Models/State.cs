
namespace TunisiaPrayer.Models
{

    public class StateData
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
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
        public int id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

    public class Delegation
    {
        public int id { get; set; }
        public string intituleAr { get; set; }
        public string intituleAn { get; set; }
    }

}
