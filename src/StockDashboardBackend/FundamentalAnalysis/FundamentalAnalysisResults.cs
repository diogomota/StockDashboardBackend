using System;

namespace StockDashboardBackend
{

    // Class with the final object that is serialized and passed as json to the frontend
    // (i.e: dto for Fundamental analysis data)
    public class FundamentalAnalysisResults
    {
        public double SharePrice {get;}

        public double ThreeMonthGrowth {get;}

        public double ROTA {get;}

        public double ROE {get;}

        public double DebtToEquity {get;}

        public double CurrentRatio {get;}

        public double GrahamSharePrice {get;}

        public FundamentalAnalysisResults(double _shareP, double _threeMonthGrowth,
                                            double _rota, double _roe,
                                            double _debtToEquity, double _currentRatio, 
                                            double _grahamSharePrice)
        {
            this.SharePrice = _shareP;
            this.ThreeMonthGrowth = _threeMonthGrowth;
            this.ROTA = _rota;
            this.ROTA = _roe;
            this.DebtToEquity = _debtToEquity;
            this.CurrentRatio = _currentRatio;
            this.GrahamSharePrice = _grahamSharePrice;
        }
    }
}
