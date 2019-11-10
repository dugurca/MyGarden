using System.Collections.Generic;
using _Game.Source.CS;
using _Game.Source.Microservices;
using UnityEngine;

namespace _Game.Source.Gameplay
{
    public class Garden
    {
        private readonly List<Plot> _plots;
        private readonly SaveMicro _saveMicro;
        
        public Garden()
        {
            _saveMicro = Micros.GetSaveMicro();
            _saveMicro.AddCoins(InitialData.InitialCoins);

            _plots = InitialData.GetPlotsData();
            _saveMicro.SetPlots(_plots);
        }

        public Garden(List<Plot> plots)
        {
            _saveMicro = Micros.GetSaveMicro();
            _plots = plots;
        }

        public int GetNumPlots()
        {
            return _plots.Count;
        }

        public PlotInfo GetPlotInfo(int plotId)
        {
            return _plots[plotId].GetPlotInfo();
        }
        
        public void BuyPlot(int plotId)
        {
            var info = GetPlotInfo(plotId);
            if (!info.IsOwned && _saveMicro.GetCoins() >= info.PlotInitialCost)
            {
                _plots[plotId].BuyPlot();
                _saveMicro.RemoveCoins(info.PlotInitialCost);
            }
            else
            {
                Debug.LogWarning("Can not afford plot: " + info.Name);
            }
        }

        public bool ExtendPlot(int plotId)
        {
            CheckAssertions(plotId);

            var plot = _plots[plotId];
            if (plot.IsOwned && _saveMicro.GetCoins() >= plot.PlotPrice)
            {
                _saveMicro.RemoveCoins(plot.PlotPrice);
                plot.IncreasePlotSize();
                Debug.Log("Plot size increased");
                return true;
            }

            Debug.LogWarning("Insufficient funds!");
            return false;
        }

        public bool ExtendWarehouse(int plotId)
        {
            CheckAssertions(plotId);
            
            var plot = _plots[plotId];
            if (plot.IsOwned && _saveMicro.GetCoins() >= plot.WarehousePrice)
            {
                _saveMicro.RemoveCoins(plot.WarehousePrice);
                plot.IncreaseWarehouseCapacity();
                Debug.Log("Warehouse size increased");
                return true;
            }
            Debug.LogWarning("Insufficient funds!");
            return false;
        }

        public void Sell(int plotId)
        {
            CheckAssertions(plotId);
            
            var plot = _plots[plotId];
            double transactionResult = plot.Sell();
            if (transactionResult > 0)
            {
                _saveMicro.AddCoins(transactionResult);
                Debug.Log("Harvested: " + transactionResult + " coins");
            }
        }

        private void CheckAssertions(int plotId)
        {
            Debug.Assert(plotId < _plots.Count);
        }
    }
}