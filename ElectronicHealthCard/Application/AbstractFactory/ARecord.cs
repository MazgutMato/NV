namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class ARecord : IComparable<ARecord>
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Info { get; set; }
        public bool QuitRecord { get; set; }

        public int CompareTo(ARecord? other)
        {
            return Start.CompareTo(other?.Start);
        }
    }
}
