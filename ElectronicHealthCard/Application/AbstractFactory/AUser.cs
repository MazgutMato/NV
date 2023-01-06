using DataStructures.Table;
using System.ComponentModel.DataAnnotations;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class AUser : IComparable<AUser>
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }                
        public int CompareTo(AUser? other)
        {
            return Id.CompareTo(other?.Id);
        }
    }
}
