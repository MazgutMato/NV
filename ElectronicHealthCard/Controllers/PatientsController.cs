using DataStructures.Iterator;
using DataStructures.Tree.Binary;
using ElectronicHealthCard.Models;

namespace ElectronicHealthCard.Controllers
{
    public class PatientsController
    {
        private BSTree<Patient> Patients;
        public PatientsController()
        {
            Patients = new BSTree<Patient>();
        }
        public bool AddPatient(Patient patient)
        {
            return Patients.Add(patient);
        }
        public int GetCount()
        {
            return this.Patients.Count;
        }
        public Iterator<Patient> GetPatients()
        {
            return Patients.createIterator();
        }
        public Patient FindPatient(Patient patient)
        {
            return Patients.Find(patient);
        }
    }
}
