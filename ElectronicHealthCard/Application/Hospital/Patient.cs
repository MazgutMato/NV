using ElectronicHealthCard.Application.AbstractFactory;

namespace ElectronicHealthCard.Application.Hospital
{
    public class Patient : AUser
    {
        public string Insurance { get; set; }
    }
}
