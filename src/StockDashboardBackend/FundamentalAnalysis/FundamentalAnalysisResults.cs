using System;

namespace StockDashboardBackend.FundamentalAnalysis
{

    // Class with the final object that is serialized and passed as json to the frontend
    // (i.e: dto for Fundamental analysis data)
    public class FundamentalAnalysisResults
    {
        public string SharePrice {get;}

        public string ThreeMonthGrowth {get;}

        public string ROTA {get;}

        public string ROE {get;}

        public string DebtToEquity {get;}

        public string CurrentRatio {get;}

        public string GrahamSharePrice {get;}

        public FundamentalAnalysisResults(string shareP, string threeMonthGrowth,
                                            string rota, string roe,
                                            string debtToEquity, string currentRatio, 
                                            string grahamSharePrice)
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
