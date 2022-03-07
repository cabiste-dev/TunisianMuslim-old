using MvvmHelpers;

public class ChaptersRootobject
{
    public Chapter[] chapters { get; set; }
}
public class Chapter
{
    public int id { get; set; }
    //public string revelation_place { get; set; }
    //public int revelation_order { get; set; }
    public bool bismillah_pre { get; set; }
    public string name_simple { get; set; }
    //public string name_complex { get; set; }
    public string name_arabic { get; set; }
    public int verses_count { get; set; }
    //public int[] pages { get; set; }
    public Translated_Name translated_name { get; set; }

}

public class Translated_Name
{
    //public string language_name { get; set; }
    public string name { get; set; }
}