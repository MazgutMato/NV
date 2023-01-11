using ElectronicHealthCard.Application.Hospital;

namespace ElectronicHealthCard.Application.AbstractFactory
{
    public abstract class AFactory
    {
        private static AFactory? instance;
        public static AFactory Instance
        {
            get { 
                if(instance == null)
                {
                    instance = new HospitalFactory();
                    return instance;
                }
                else
                {
                    return instance;
                }
            }
        }
        public abstract ASystem CreateSystem();
        public abstract AUser CreateUser();
    }
}
