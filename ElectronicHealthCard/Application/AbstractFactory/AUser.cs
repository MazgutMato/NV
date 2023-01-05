using DataStructures.Table;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class AUser : IComparable<AUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Table<ARecord> Records { get; set; }
        public bool AdmitRecord(ARecord record)
        {
            return Records.Add(record);
        }
        public int CompareTo(AUser? other)
        {
            return Id.CompareTo(other?.Id);
        }
    }
}
