using System;

namespace StockDashboardBackend.FundamentalAnalysis
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

        public FundamentalAnalysisResults(double shareP, double threeMonthGrowth,
                                            double rota, double roe,
                                            double debtToEquity, double currentRatio, 
                                            double grahamSharePrice)
        {
            this.SharePrice = shareP;
            this.ThreeMonthGrowth = threeMonthGrowth;
            this.ROTA = rota;
            this.ROTA = roe;
            this.DebtToEquity = debtToEquity;
            this.CurrentRatio = currentRatio;
            this.GrahamSharePrice = grahamSharePrice;
        }
    }
}
