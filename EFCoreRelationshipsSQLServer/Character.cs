using System.Text.Json.Serialization;

namespace EFCoreRelationshipsSQLServer
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RPGClass { get; set; } = "Knight";
        [JsonIgnore]
        public User? User { get; set; } 
        public int UserId { get; set; }
        public Weapon? Weapon { get; set; }

        public List<Skill>? Skills { get; set; }
    }
}
