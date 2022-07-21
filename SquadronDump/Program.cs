using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text;
using System.Text.Json;
using SquadronDump;
using System.Net.Http.Headers;

async Task<uint> GetSquadronID(HttpClient httpClient, string squadronCode, string platform)
{
    SquadronList? squadronList = JsonSerializer.Deserialize<SquadronList>(
        await httpClient.GetStringAsync($"https://api.orerve.net/2.0/website/squadron/info?platform={platform}&tag={squadronCode}"));
    return squadronList?.Squadron?.Id ?? 0;
}

async Task<MemberList> GetSquadronMembers(HttpClient httpClient, uint squadronId)
{
    return JsonSerializer.Deserialize<MemberList>(
        await httpClient.GetStreamAsync($"https://api.orerve.net/2.0/website/squadron/member/list?squadronId={squadronId}")) ?? new MemberList();
}

string DecodeText(string? text)
{
    return Encoding.UTF8.GetString(Convert.FromHexString(text ?? string.Empty));
}

string DecodeDate(string? date)
{
    return DateTime.TryParse(date ?? string.Empty, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime result) ? result.ToString() : "(None)";
}

void DumpMembers(Member[] members, TextWriter textWriter)
{
    textWriter.WriteLine("Name,Squadron Rank,Date Requested,Pending,Date Joined,Online,Last Online");
    foreach (Member member in members)
    {
        textWriter.Write(DecodeText(member.Name));
        textWriter.Write(",");
        textWriter.Write(member.RankId);
        textWriter.Write(",");
        textWriter.Write(DecodeDate(member.Requested));
        textWriter.Write(",");
        textWriter.Write(member.Pending);
        textWriter.Write(",");
        textWriter.Write(DecodeDate(member.Joined));
        textWriter.Write(",");
        textWriter.Write(member.Presence);
        textWriter.Write(",");
        textWriter.Write(DecodeDate(member.LastOnline));
        textWriter.WriteLine();
    }
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