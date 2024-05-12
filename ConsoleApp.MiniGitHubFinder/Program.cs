using Dumpify;
using System.Net.Http.Headers;
using System.Net.Http.Json;

// Initialize HttpClient
using var client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubInfo", "1.0"));
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

result:
Console.Write("Enter GitHub User Name : ");
string username = Console.ReadLine()!;

// Fetch user information
var userResponse = await client.GetAsync($"https://api.github.com/users/{username}");
var user = await userResponse.Content.ReadFromJsonAsync<User>();

if (user is null)
{
    Console.WriteLine("User not found.");
    return;
}
//Console.WriteLine($"Username: {user.Login}");
//Console.WriteLine($"Name: {user.Name}");
//Console.WriteLine($"Location: {user.Location}");
//Console.WriteLine($"Followers: {user.Followers}");
//Console.WriteLine($"Following: {user.Following}");
new
{
    UserName = user.Login,
    Name = user.Name,
    Location = user.Location,
    Followers = user.Followers,
    Following = user.Following,
}.Dump();

// Fetch repositories
var reposResponse = await client.GetAsync($"https://api.github.com/users/{username}/repos");
var repositories = await reposResponse.Content.ReadFromJsonAsync<Repository[]>();

Console.WriteLine("Repositories:");
//foreach (var repo in repositories!)
//{
//    Console.WriteLine($"{repo.Name}: {repo.Description}");
//}

string[] lst = repositories!.Select(x => x.Name).ToArray();
lst.Dump();

goto result;
