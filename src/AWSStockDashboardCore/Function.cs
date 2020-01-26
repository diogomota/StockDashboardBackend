using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StockDashboardBackend;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using StockDashboardBackend.FundamentalAnalysis;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSStockDashboardCore
{
    public class Functions
    {

        private readonly string apiKey;
        private readonly string endpoint;
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            //DoingNothing at the moment. will need to use paramter store
            //Seee : https://github.com/aws-samples/aws-net-guides/tree/master/Communications/ParameterStore-Example
            apiKey = Environment.GetEnvironmentVariable("apiKey");
            endpoint = Environment.GetEnvironmentVariable("endpoint");
        }

        public async Task<APIGatewayProxyResponse> TestFunc(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int) HttpStatusCode.OK,
                Body = "TestApp ran",
                Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"},
                    {"Access-Control-Allow-Origin", "*"}
                }
            };

            return response;
        }
        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public async Task<APIGatewayProxyResponse> GetFundamentalAnalysis(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string ticker = null;
            if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("ticker"))
            {
                ticker = request.QueryStringParameters["ticker"];
            }

            if (ticker == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Body = $"Missing ticker parameter",
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        {"Access-Control-Allow-Origin","*"}
                    }
                };
            }

            string responseBody = await FundamentalAnalysisResponseBuilder.Build(ticker,endpoint:this.endpoint,apiKey:this.apiKey);
            
            //How to get parameters from url:
            //var a = request.QueryStringParameters["name"];
            //For more read: https://stackoverflow.com/questions/31329958/how-to-pass-a-querystring-or-route-parameter-to-aws-lambda-from-amazon-api-gatew
            
            context.Logger.LogLine("Get Fundamental analysis\n");

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = responseBody,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    {"Access-Control-Allow-Origin","*"}
                }
            };

            return response;
        }
    }
}
