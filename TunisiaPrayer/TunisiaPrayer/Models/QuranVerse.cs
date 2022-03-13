namespace TunisiaPrayer.Models
{
    public class VerseRootobject
    {
        public Vers[] verses { get; set; }
    }

    public class Vers
    {
        public int id { get; set; }
        public string verse_key { get; set; }
        public string text_uthmani { get; set; }
    }
}