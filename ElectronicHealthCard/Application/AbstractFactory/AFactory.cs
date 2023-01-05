using ElectronicHealthCard.Application.Hospital;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class AFactory
    {
        public abstract ASystem CreateSystem();
        public abstract AUser CreateUser();
        public abstract ARecord CreateRecord();
    }
}
