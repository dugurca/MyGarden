using System;
using UnityEngine;

namespace _Game.Source.Gameplay
{
    [Serializable]
    public class Plot
    {
        public int Id;
        public string Name;
        public double InitialCost;
        public bool IsOwned;
        
        public double CropWorthPerUnit;
        public double BaseGrowthTime;
        
        public double PlotSize;
        public double PlotSizeIncreaseAmount;
        public double PlotPrice;
        public double PlotPriceIncreaseMultiplier;

        public double WarehouseCapacity;
        public double WarehouseCapacityIncreaseAmount;
        public double WarehousePrice;
        public double WarehouseCapacityIncreasePriceMultiplier;

        public long LastHarvestTime;

        private double GrowthTime => BaseGrowthTime / PlotSize;
        
        public Plot(int id, string name, double initialCost, bool isOwned, 
            double cropWorthPerUnit, double baseGrowthTime, 
            double plotSize, double plotSizeIncreaseAmount, 
            double basePlotPrice, double plotPriceIncreaseMultiplier, 
            double warehouseCapacity, double warehouseCapacityIncreaseAmount, 
            double warehouseCapacityIncreasePrice, double warehouseCapacityIncreasePriceMultiplier)
        {
            Id = id;
            Name = name;
            InitialCost = initialCost;
            IsOwned = isOwned;
            CropWorthPerUnit = cropWorthPerUnit;
            
            BaseGrowthTime = baseGrowthTime;
            PlotSize = plotSize;
            PlotSizeIncreaseAmount = plotSizeIncreaseAmount;
            PlotPrice = basePlotPrice;
            PlotPriceIncreaseMultiplier = plotPriceIncreaseMultiplier;
            
            WarehouseCapacity = warehouseCapacity;
            WarehouseCapacityIncreaseAmount = warehouseCapacityIncreaseAmount;
            WarehousePrice = warehouseCapacityIncreasePrice;
            WarehouseCapacityIncreasePriceMultiplier = warehouseCapacityIncreasePriceMultiplier;
        }

        private float GetWarehouseStorageRatio()
        {
            double timeDiff = GetTimeDiff();
            double maxTime = GrowthTime * WarehouseCapacity;
            float result = (float)(timeDiff / maxTime);
            return Mathf.Clamp(result, 0f, 1f);
        }

        private double GetWarehouseCropAmount(double timeDifference)
        {
            double maxTime = GrowthTime * WarehouseCapacity;
            if (timeDifference > maxTime) return WarehouseCapacity;
            return (timeDifference / maxTime) * WarehouseCapacity;
        }

        public double Sell()
        {
            var timeDifference = GetTimeDiff();
            Debug.Log("Time difference: " + timeDifference);
            double result = GetWarehouseCropAmount(timeDifference) * CropWorthPerUnit;
            LastHarvestTime = DateTime.UtcNow.Ticks;
            Debug.Log("=> Sold Amount: " + result);
            return result;
        }

        public void BuyPlot()
        {
            IsOwned = true;
            LastHarvestTime = DateTime.UtcNow.Ticks;
        }

        public void IncreasePlotSize()
        {
            PlotSize += PlotSizeIncreaseAmount;
            PlotPrice *= PlotPriceIncreaseMultiplier;
        }

        public void IncreaseWarehouseCapacity()
        {
            WarehouseCapacity += WarehouseCapacityIncreaseAmount;
            WarehousePrice *= WarehouseCapacityIncreasePriceMultiplier;
        }

        private double GetTimeDiff()
        {
            var lastHarvestDateTime = new DateTime(LastHarvestTime);
            return (DateTime.UtcNow - lastHarvestDateTime).TotalSeconds;
        }

        public PlotInfo GetPlotInfo()
        {
            return new PlotInfo(Id, Name, IsOwned, InitialCost, PlotSize, PlotPrice, WarehouseCapacity, WarehousePrice, GetWarehouseStorageRatio());
        }
    }
}