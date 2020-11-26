using System;
using System.Threading.Tasks;
using AtlasEngine;

namespace Sample.StartProcess
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var atlasEngineUrl = new Uri("http://localhost:56000");
            var client = ClientFactory.CreateProcessDefinitionsClient(atlasEngineUrl);

            await client.StartProcessInstanceAsync("SimpleAdd");
        }
    }
}
