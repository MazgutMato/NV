using DataStructures.Table;
using ElectronicHealthCard.Application.AbstractFactory;
using System.ComponentModel.DataAnnotations;

namespace ElectronicHealthCard.Application.Hospital
{
    public class Hospital : ASystem
    {
        [Required]
        public int Doctors { get; set; }
        public int Nurses { get; set; }
    }
}
