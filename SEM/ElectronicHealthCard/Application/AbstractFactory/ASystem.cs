using DataStructures.Table;
using System.ComponentModel.DataAnnotations;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class ASystem : IComparable<ASystem>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        public Table<AUser>? Users { get; set; }
        public bool AddUser(AUser user)
        {
            return this.Users.Add(user);
        }
        public bool RemoveUser(AUser user)
        {
            return this.Users.Delete(user);
        }
        public int CompareTo(ASystem? other)
        {
            return Name.CompareTo(other?.Name);
        }
    }
}
