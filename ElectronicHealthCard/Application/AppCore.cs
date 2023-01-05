using DataStructures.Table;
using ElectronicHealthCard.Application.AbstractFactory;
using ElectronicHealthCard.Application.Hospital;

namespace ElectronicHealthCard.Application
{
    public class AppCore
    {
        public AFactory Factory { get; } = new HospitalFactory();
        public Table<ASystem> Systems { get; set; }
        public Table<AUser> Users{ get; set; }
        public AppCore() {}
    }
}
