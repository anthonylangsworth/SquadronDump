using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text;
using System.Text.Json;
using SquadronDump;
using System.Net.Http.Headers;
using System.Web;
using CsvHelper;

async Task<uint> GetSquadronID(HttpClient httpClient, string squadronCode, string platform)
{
    SquadronResult? squadronList = JsonSerializer.Deserialize<SquadronResult>(
        await httpClient.GetStringAsync($"https://api.orerve.net/2.0/website/squadron/info?platform={HttpUtility.UrlEncode(platform)}&tag={HttpUtility.UrlEncode(squadronCode)}"));
    return squadronList?.Squadron?.Id ?? 0;
}

async Task<MemberResult> GetSquadronMembers(HttpClient httpClient, uint squadronId)
{
    return JsonSerializer.Deserialize<MemberResult>(
        await httpClient.GetStreamAsync($"https://api.orerve.net/2.0/website/squadron/member/list?squadronId={squadronId}")) ?? new MemberResult();
}

void DumpMembers(Member[] members, TextWriter textWriter)
{
    using CsvWriter csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
    csvWriter.Context.RegisterClassMap<MemberClassMap>();
    csvWriter.WriteRecords(members);
}

try
{
    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    using HttpClient httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer", config.GetRequiredSection("token").Value);
    uint squadronId = await GetSquadronID(httpClient, config.GetRequiredSection("tag").Value, 
        config.GetRequiredSection("platform").Value);
    if (squadronId != 0)
    {
        DumpMembers((await GetSquadronMembers(httpClient, squadronId)).Members, Console.Out);
    }
    else
    {
        Console.Error.WriteLine("Squadron not found");
    }
}
catch(Exception ex)
{
    Console.Error.WriteLine(ex.Message);
}