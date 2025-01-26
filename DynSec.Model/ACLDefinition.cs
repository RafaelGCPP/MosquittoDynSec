namespace DynSec.Model
{

    public class ACLDefinition
    {
        public ACLType? ACLType { get; set; }
        public string? Topic { get; set; }
        public int? Priority { get; set; }
        public bool? Allow { get; set; }
    }

}
