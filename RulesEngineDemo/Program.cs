using Newtonsoft.Json;
using RulesEngine.Models;

namespace RulesEngineDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {            
            var workflowJson = File.ReadAllText("DiscountRules.json");
            
            var workflowRules = JsonConvert.DeserializeObject<Workflow[]>(workflowJson);
            var rulesEngine = new RulesEngine.RulesEngine(workflowRules, null);
           
            var input1 = new
            {
                country = "aus",
                loyaltyFactor = 2,
                totalPurchasesToDate = 6000
            };

            var input2 = new
            {
                totalOrders = 3
            };

            var input3 = new
            {
                noOfVisitsPerMonth = 4
            };
            
            var resultList = await rulesEngine.ExecuteAllRulesAsync("Discount", input1, input2, input3);
            
            foreach (var result in resultList)
            {
                Console.WriteLine($"Rule: {result.Rule.RuleName}, Success: {result.IsSuccess}");
                if (result.IsSuccess)
                {
                    Console.WriteLine("The discount was applied successfully.");
                }
                else
                {
                    Console.WriteLine("The conditions for the discount were not met.");
                }
            }

            Console.ReadLine();
        }
    }
}
