using System.Collections.Generic;
using _Game.Source.Microservices;
using _Game.Source.UI;
using UnityEngine;

namespace _Game.Source.Gameplay
{
    public class Game : MonoBehaviour
    {
        public GameObject PlotPanelPrefab;
        public Transform PlotsParent;
        public MainUi MainUi;
        
        private Garden _garden;
        private SaveMicro _saveMicro;
        
        private readonly List<PlotView> _plotViews = new List<PlotView>();

        private void Start()
        {
            Micros.Init();
            _saveMicro = Micros.GetSaveMicro();
            
            if (_saveMicro.LoadedSaveGame)
            {
                //There is a saved game, let's get it
                _garden = new Garden(_saveMicro.GetPlots());
            }
            else
            {
                //Nope, let's create some data
                //Garden will take care of this
                _garden = new Garden();
            }
            
            InitializePlotViews();
            MainUi.UpdateView();
        }

        private void InitializePlotViews()
        {
            int numPlots = _garden.GetNumPlots();

            for (int i = 0; i < numPlots; i++)
            {
                var plotInfo = _garden.GetPlotInfo(i);
                
                var plotPanel = Instantiate(PlotPanelPrefab, PlotsParent);
                plotPanel.GetComponent<PlotController>().Init(this, plotInfo);
                
                var plotView = plotPanel.GetComponent<PlotView>();
                plotView.Init(plotInfo);
                _plotViews.Add(plotView);
                
                UpdatePlotView(plotInfo.Id);
            }
        }

        private void Update()
        {
            UpdateStorageViews();
        }

        public void BuyPlot(int plotId)
        {
            _garden.BuyPlot(plotId);
            _plotViews[plotId].Init(_garden.GetPlotInfo(plotId));
            MainUi.UpdateView();
            UpdatePlotView(plotId);
        }

        public bool ExtendPlot(int plotId)
        {
            bool result = _garden.ExtendPlot(plotId);
            if (result)
            {
                MainUi.UpdateView();
                UpdatePlotView(plotId);
            }

            return result;
        }

        public bool ExtendWarehouse(int plotId)
        {
            bool result = _garden.ExtendWarehouse(plotId);
            if (result) 
            {
                MainUi.UpdateView();
                UpdatePlotView(plotId);
            }
            return result;
        }

        public void Sell(int plotId)
        {
            _garden.Sell(plotId);
            MainUi.UpdateView();
        }

        private void UpdatePlotView(int plotId)
        {
            var info = _garden.GetPlotInfo(plotId);
            _plotViews[plotId].UpdateView(info);
        }
        
        private void UpdateStorageViews()
        {
            for (int i = 0; i < _garden.GetNumPlots(); i++)
            {
                var plotInfo = _garden.GetPlotInfo(i);
                if (plotInfo.IsOwned)
                {
                    _plotViews[i].UpdateStorageView(plotInfo);
                }
            }
        }
    }
}
