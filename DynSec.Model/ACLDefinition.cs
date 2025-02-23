namespace DynSec.Model
{

    public class ACLDefinition
    {
        public ACLType ACLType { get; set; } = ACLType.subscribePattern;
        public string Topic { get; set; } = "";
        public int Priority { get; set; } = 0;
        public bool Allow { get; set; } = true;
    }

}
