namespace MVCproject_Elearning.Models
{
	public class InformationIcon:BaseEntity
	{

        public string Icon { get; set; }
        public List<Information> Information { get; set; }

    }
}
