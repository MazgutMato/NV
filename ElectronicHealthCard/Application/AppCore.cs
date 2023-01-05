using DataStructures.Table;
using DataStructures.Tree.Balance;
using DataStructures.Tree.Balance.Strategy;
using DataStructures.Tree.Binary;
using ElectronicHealthCard.Application.AbstractFactory;
using ElectronicHealthCard.Application.Hospital;

namespace ElectronicHealthCard.Application
{
    public static class TableFactory<T> where T : IComparable<T>
    {
        public static Table<T>? createTable(int type)
        {
            switch(type)
            {
                case 0:
                    return null;
                case 1: 
                    return new BSTree<T>();
                case 2:
                    return new BalanceTree<T>(new MyStrategy<T>());
                default:
                    throw new Exception("Wrong type passed!");
            }
        }
    }
    public class AppCore
    {
        public AFactory Factory { get; } = new HospitalFactory();
        public int StructureType { get; set; } = 0;
        public Table<ASystem> Systems { get; set; }
        public Table<AUser> Users{ get; set; }
        public AppCore() {}
    }
}
