// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;

using Grpc.Net.Client;

using UtilitiesGrpCService;

Console.WriteLine("Hello, World!");


// one channel per "server" - if you have multiple services running on the same server, you only need one channel.
var channel = GrpcChannel.ForAddress(new Uri("https://localhost:7231"));

var client = new Greeter.GreeterClient(channel);

Console.Write("What is your name?");
var name = Console.ReadLine();

var response = await client.SayHelloAsync(new HelloRequest { Name = name });

Console.WriteLine("The server said " + response.Message);


var client2 = new DateUtils.DateUtilsClient(channel);

var request = new DateUtilsRequest {  Date = Timestamp.FromDateTime(DateTime.UtcNow) };

var answer = await client2.isWeekendAsync(request);

Console.WriteLine("Is it the weekend? ", answer.Ok);