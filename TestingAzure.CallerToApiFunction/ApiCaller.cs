using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionWrapper
{
    public static class ApiCaller
    {
        [FunctionName("Run")]
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info("Starting Function");

            int calls = 10;

            string[] apis = new string[] {
                        "http://webapitooracle.azurewebsites.net"
                        ,"http://webapitosqlserver.azurewebsites.net"
                        ,"http://webapitoazuresql.azurewebsites.net"
                        ,"http://webapinetcoretooracle.azurewebsites.net"
                        ,"http://webapinetcoretosqlserver.azurewebsites.net"
                        ,"http://webapinetcoretoazuresql.azurewebsites.net"
                    };

            var apiTimers = new Dictionary<string, long>();

            Stopwatch timer = new Stopwatch();

            try
            {
                foreach (string currentApi in apis)
                {
                    var client = new HttpClient();
                    Task[] taskCalls = new Task[calls];

                    var Stadium = new
                    {
                        Name = currentApi.Replace("http://", "").Replace(".azurewebsites.net", ""),
                        Capacity = 47600,
                        City = "La Plata",
                        Country = "Argentina",
                        Description = $"Registro creado: { DateTime.Now.ToString() }"
                    };

                    timer.Restart();

                    try
                    {
                        for (var i = 0; i < calls; i++)
                        {
                            taskCalls[i] = Task.Run(() => client.PostAsJsonAsync($"{currentApi}/api/Stadium", Stadium));
                        }

                        Task.WaitAll(taskCalls);
                    }
                    catch (Exception e)
                    {
                        log.Error($"Error con {currentApi}: {e.Message}", e);
                    }

                    timer.Stop();
                    apiTimers.Add(currentApi, timer.ElapsedMilliseconds);
                    client.Dispose();
                    System.Threading.Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {
                log.Error($"Error: {e.Message}", e);
            }

            log.Info("Timers:");
            foreach (var item in apiTimers)
                log.Info($"Api: {item.Key.Replace("http://", "").Replace(".azurewebsites.net", "")} - Miliseg: {item.Value}");

            return req.CreateResponse(HttpStatusCode.OK, apiTimers);

        }
    }
}