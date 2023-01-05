using DataStructures.Table;
using ElectronicHealthCard.Application.AbstractFactory;

namespace ElectronicHealthCard.Application.Hospital
{
    public class Hospital : ASystem
    {
        public int Doctors { get; set; }
        public int Nurses { get; set; }
    }
}
