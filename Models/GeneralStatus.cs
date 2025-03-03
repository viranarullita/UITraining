namespace UITraining.Models
{
    public class GeneralStatus
    {
        public enum GeneralStatusData
        {
            published, //dilihat semuanya
            unpublished, //dilihat admin
            deleted //tidak dapat dilihat admin dan public
        }
    }
}
