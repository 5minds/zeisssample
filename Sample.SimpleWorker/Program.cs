namespace Sample.SimpleWorker
{
  using System;
  using AtlasEngine;
  using AtlasEngine.ExternalTasks;
  using AtlasEngine.Logging;
  using global::Sample.SimpleWorker.Sample;

  class Program
  {
    static void Main(string[] args)
    {
      var atlasEngineUrl = new Uri("http://localhost:56000");
      var client = ClientFactory.CreateExternalTaskClient(atlasEngineUrl, logger: ConsoleLogger.Default);

      var worker = client.SubscribeToExternalTaskTopic(
        "Calculation.Add", 
        p => p.UseHandlerMethod<AddPayload, AddResult>(DoAddSync));

      // Use an async worker method:
      //var worker = client.SubscribeToExternalTaskTopic(
      // "Calculation.Add",
      // p => p.UseHandlerMethod<AddPayload, AddResult>(DoAddAsync));

      // Create a new typed worker using a factory method:
      //var worker = client.SubscribeToExternalTaskTopic(
      // "Calculation.Add",
      // p => p.UseHandlerFactory<AddHandler, AddPayload, AddResult>(() => new AddHandler()));

      // Create a new typed worker using a custom activator (e.g. using DI):
      //var worker = client.SubscribeToExternalTaskTopic(
      // "Calculation.Add",
      // p => p.UseHandlerActivator<AddHandler, AddPayload, AddResult>(new ServiceBasedWorkerActivator()));

      worker.StartAsync();

      Console.WriteLine("Started");
      Console.ReadKey(true);

      worker.Stop();
    }

    private static AddResult DoAddSync(AddPayload payload, ExternalTask task)
    {
      return new AddResult() { Sum = payload.Number1 + payload.Number2 };
    }

    //private static Task<AddResult> DoAddAsync(AddPayload payload, ExternalTask task)
    //{
    //  var result = new AddResult() { Sum = payload.Number1 + payload.Number2 };
    //  return Task.FromResult(result);
    //}
  }
}
