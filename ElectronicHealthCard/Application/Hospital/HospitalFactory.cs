using ElectronicHealthCard.Application.AbstractFactory;

namespace ElectronicHealthCard.Application.Hospital
{
    public class HospitalFactory : AFactory
    {
        public override ARecord CreateRecord()
        {
            return new Hospitalization();
        }

        public override ASystem CreateSystem()
        {
            return new Hospital();
        }

        public override AUser CreateUser()
        {
            return new Patient();
        }
    }
}
