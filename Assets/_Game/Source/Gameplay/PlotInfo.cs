namespace _Game.Source.Gameplay
{
    public class PlotInfo
    {
        public readonly int Id;
        public readonly string Name;
        public readonly bool IsOwned;
        public readonly double PlotInitialCost;
        public readonly double PlotSize;
        public readonly double PlotExtendPrice;
        public readonly double WarehouseCapacity;
        public readonly double WarehouseCapacityExtendPrice;
        public readonly float WarehouseStorageRatio;

        public PlotInfo(int id, string name, bool isOwned, double plotInitialCost, 
                        double plotSize, double plotExtendPrice, 
                        double warehouseCapacity, double warehouseCapacityExtendPrice, float warehouseStorageRatio)
        {
            Id = id;
            Name = name;
            IsOwned = isOwned;
            PlotInitialCost = plotInitialCost;
            PlotSize = plotSize;
            PlotExtendPrice = plotExtendPrice;
            WarehouseCapacity = warehouseCapacity;
            WarehouseCapacityExtendPrice = warehouseCapacityExtendPrice;
            WarehouseStorageRatio = warehouseStorageRatio;
        }

        public override string ToString()
        {
            string res =
                "Name: " + Name + " | " +
                "IsOwned: " + IsOwned +  " | " +
                "PlotInitialCost: " + PlotInitialCost;
            return res;
        }
    }
}