using DataStructures.Table;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class ASystem : IComparable<ASystem>
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public Table<AUser>? Users { get; set; }
        public bool AdmitUser(AUser user)
        {
            return this.Users.Add(user);
        }
        public int CompareTo(ASystem? other)
        {
            return Name.CompareTo(other?.Name);
        }
    }
}
