
using hm3_serialization;
using Newtonsoft.Json;

F f = new F().Get();
int count = 1000000;
string objStr = "";

// Моя сериализация
var startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    objStr = MySerializer<F>.Serialize(f);
}

startTime.Stop();
Console.WriteLine($"My serialization time: {startTime.Elapsed}");
Console.WriteLine($"My searilizated string: {objStr}");

// Моя десериализация
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    F deserializedF = MySerializer<F>.DeSerialize(objStr);
}
startTime.Stop();
Console.WriteLine($"My deserialization time: {startTime.Elapsed}");
Console.WriteLine();


// Newtonsoft.Json сериализация
string jsonStr = "";
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    jsonStr = JsonConvert.SerializeObject(f);
}
startTime.Stop();
Console.WriteLine($"Newtonsoft.Json serialization time: {startTime.Elapsed}");


// Newtonsoft.Json десериализация
startTime = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < count; i++)
{
    F deserializedJsonF = JsonConvert.DeserializeObject<F>(jsonStr);
}
startTime.Stop();
Console.WriteLine($"Newtonsoft.Json deserialization time: {startTime.Elapsed}");



