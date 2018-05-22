namespace Beehouse.FrameworkStandard.Entities
{
    public class UserEntity:Entity
    {
        public string CreatedBy { get; set; }
        public string CreatorAlias { get; set; }
        public string OwnerId { get; set; }
    }
}