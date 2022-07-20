using System.Diagnostics;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Traderfy.TarkovSharp;

namespace wrappertest.TarkovSharp;

public class TarkovApiClient
{
    private readonly string _query;
    private readonly string[] _details;
    private readonly HttpClient _httpClient;
    private LanguageCode _languageCode;

    public TarkovApiClient(string query, string[] details, LanguageCode languageCode = LanguageCode.en)
    {
        _query = query;
        _details = details;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
        _languageCode = languageCode;
    }
    
    public TarkovApiClient(string query, string details, LanguageCode languageCode = LanguageCode.en)
    {
        _query = query;
        _details = new[] {details};
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
        _languageCode = languageCode;
    }
    
    public void SetLanguageCode(LanguageCode languageCode)
    {
        _languageCode = languageCode;
    }

    public async Task<string[]?> GetResponse(IEnumerable<TarkovRequestArgs> args)
    {
        var responses = new List<string>();


        //var arguments = args.Aggregate("(", (current, arg) => current + (arg.ArgumentName[^1] == 's' ? $"{arg.ArgumentName}: {arg.ArgumentValue}" : $"{arg.ArgumentName}: \"{arg.ArgumentValue}\"" + ", "));

        var arguments = "(";
        
        if (_languageCode != LanguageCode.en)
        {
            arguments += $"lang: {_languageCode}, ";
        }
        
        
        foreach (var arg in args)
        {
            if (arg.ArgumentValue is string)
            {
                arguments += $"{arg.ArgumentName}: \"{arg.ArgumentValue}\", ";
            } else if (IsArray(arg.ArgumentValue))
            {
                if (arg.ArgumentValue.GetType() == typeof(string[]))
                {
                    arguments += $"{arg.ArgumentName}: [";
                    foreach (var item in arg.ArgumentValue)
                    {
                        arguments += $"\"{item}\", ";
                    }
                    arguments = arguments.Remove(arguments.Length - 2);
                    arguments += "], ";
                }
                else
                {
                    arguments += $"{arg.ArgumentName}: [";
                    foreach (var item in arg.ArgumentValue)
                    {
                        arguments += $"{item}, ";
                    }
                    arguments = arguments.Remove(arguments.Length - 2);
                    arguments += "], ";
                }
            }
            else
            {
                arguments += $"{arg.ArgumentName}: {arg.ArgumentValue}, ";
            }
        }
        arguments = arguments.Remove(arguments.Length - 2);
        arguments += ")";
        
        
        


        /*if (_args is not string)
        {
            arguments = arguments = $"({_args.Key}: {_args.Value})";
        } else
       {
                
        }
            arguments = $"({_args.Key}: {_args.Value})";*/

        var details = _details.Aggregate<string?, string?>(null, (current, detail) => current + $"{detail} ");

        details = details?.TrimEnd();

        var q = "{" + _query + arguments + "{" + details + "}}";

        /*var request = new GraphQLRequest
        {
            Query = q
        };*/

        //var r = client.SendQueryAsync<>(request);

        //var responses = await client.SendQueryAsync<dynamic>(request);
        var response = await _httpClient.PostAsJsonAsync("", new Dictionary<string, string> { { "query", q } });

        var jsonString = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(jsonString);
        //var value = json.TryGetValue("item", out var name);
        //json.First.ToString();
        //Console.WriteLine(json.First.First.Type.ToString());

        if (json.First == null) return responses.Count == 0 ? null : responses.ToArray();
        
        if (json.First.Path == "errors")
        {
            throw new GraphQlException(json.First.First[0].First.First.ToString()/*, new Exception(json.First.First[0].First.Next.First[0].ToString())*/);
        }

        
        if (json.First.First == null) return responses.Count == 0 ? null : responses.ToArray();
        if (json.First.First.First == null) return responses.Count == 0 ? null : responses.ToArray();
        var result = json.First.First.First.First;
        if (result == null) return null;
        if (result.Type == JTokenType.Array)
        {
            responses.AddRange(from item in result
                from child in item.Children()
                from baby in child.Children()
                select baby.ToString());
        }
        else
        {
            var children = result.Children();
            responses.AddRange(from child in children from baby in child.Children() select baby.ToString());
        }

        return responses.Count == 0 ? null : responses.ToArray();
    }
    
    public async Task<JToken?> GetRawResponse(IEnumerable<TarkovRequestArgs> args)
    {
        //var arguments = args.Aggregate("(", (current, arg) => current + (arg.ArgumentName[^1] == 's' ? $"{arg.ArgumentName}: {arg.ArgumentValue}" : $"{arg.ArgumentName}: \"{arg.ArgumentValue}\"" + ", "));

        var arguments = "(";
        
        if (_languageCode != LanguageCode.en)
        {
            arguments += $"lang: {_languageCode}, ";
        }
        
        
        foreach (var arg in args)
        {
            if (arg.ArgumentValue is string)
            {
                arguments += $"{arg.ArgumentName}: \"{arg.ArgumentValue}\", ";
            } else if (IsArray(arg.ArgumentValue))
            {
                if (arg.ArgumentValue.GetType() == typeof(string[]))
                {
                    arguments += $"{arg.ArgumentName}: [";
                    foreach (var item in arg.ArgumentValue)
                    {
                        arguments += $"\"{item}\", ";
                    }
                    arguments = arguments.Remove(arguments.Length - 2);
                    arguments += "], ";
                }
                else
                {
                    arguments += $"{arg.ArgumentName}: [";
                    foreach (var item in arg.ArgumentValue)
                    {
                        arguments += $"{item}, ";
                    }
                    arguments = arguments.Remove(arguments.Length - 2);
                    arguments += "], ";
                }
            }
            else
            {
                arguments += $"{arg.ArgumentName}: {arg.ArgumentValue}, ";
            }
        }
        arguments = arguments.Remove(arguments.Length - 2);
        arguments += ")";
        
        
        


        /*if (_args is not string)
        {
            arguments = arguments = $"({_args.Key}: {_args.Value})";
        } else
       {
                
        }
            arguments = $"({_args.Key}: {_args.Value})";*/

        var details = _details.Aggregate<string?, string?>(null, (current, detail) => current + $"{detail} ");

        details = details?.TrimEnd();

        var q = "{" + _query + arguments + "{" + details + "}}";

        /*var request = new GraphQLRequest
        {
            Query = q
        };*/

        //var r = client.SendQueryAsync<>(request);

        //var responses = await client.SendQueryAsync<dynamic>(request);
        var response = await _httpClient.PostAsJsonAsync("", new Dictionary<string, string> { { "query", q } });

        var jsonString = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(jsonString);
        //var value = json.TryGetValue("item", out var name);
        //json.First.ToString();
        //Console.WriteLine(json.First.First.Type.ToString());


        if (json.First == null) return null;
        if (json.First.Path == "errors")
        {
            throw new GraphQlException(json.First.First[0].First.First.ToString()/*, new Exception(json.First.First[0].First.Next.First[0].ToString())*/);
        }

        return json.First.First.ToString();
    }
    
    public async Task<string> GetRawResponse()
    {
        //var arguments = args.Key[^1] == 's' ? $"({args.Key}: {args.Value})" : $"({args.Key}: \"{args.Value}\")";


        /*if (_args is not string)
        {
            arguments = arguments = $"({_args.Key}: {_args.Value})";
        } else
       {
                
        }
            arguments = $"({_args.Key}: {_args.Value})";*/

        var details = _details.Aggregate<string?, string?>(null, (current, detail) => current + $"{detail} ");

        details = details?.TrimEnd();

        string q;
        if (_languageCode == LanguageCode.en) //saves time
        {
            q = "{" + _query + "{" + details + "}}";
        }
        else
        {
            q = "{" + _query + "(" + "lang: " + _languageCode + ")" + "{" + details + "}}";
        }
        

        /*var request = new GraphQLRequest
        {
            Query = q
        };*/

        //var r = client.SendQueryAsync<>(request);

        //var responses = await client.SendQueryAsync<dynamic>(request);
        var response = await _httpClient.PostAsJsonAsync("", new Dictionary<string, string> { { "query", q } });

        var jsonString = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(jsonString);
        //var value = json.TryGetValue("item", out var name);
        //json.First.ToString();
        //Console.WriteLine(json.First.First.Type.ToString());


        if (json.First == null) return null;
        
        if (json.First.Path == "errors")
        {
            throw new GraphQlException(json.First.First[0].First.First.ToString()/*, new Exception(json.First.First[0].First.Next.First[0].ToString())*/);
        }

        return json.First.First.ToString();
    }
    
    

    public async Task<string[]?> GetResponse()
    {
        var responses = new List<string>();


        //var arguments = args.Key[^1] == 's' ? $"({args.Key}: {args.Value})" : $"({args.Key}: \"{args.Value}\")";


        /*if (_args is not string)
        {
            arguments = arguments = $"({_args.Key}: {_args.Value})";
        } else
       {
                
        }
            arguments = $"({_args.Key}: {_args.Value})";*/

        var details = _details.Aggregate<string?, string?>(null, (current, detail) => current + $"{detail} ");

        details = details?.TrimEnd();

        string q;
        if (_languageCode == LanguageCode.en) //saves time
        {
            q = "{" + _query + "{" + details + "}}";
        }
        else
        {
            q = "{" + _query + "(" + "lang: " + _languageCode + ")" + "{" + details + "}}";
        }
        

        /*var request = new GraphQLRequest
        {
            Query = q
        };*/

        //var r = client.SendQueryAsync<>(request);

        //var responses = await client.SendQueryAsync<dynamic>(request);
        var response = await _httpClient.PostAsJsonAsync("", new Dictionary<string, string> { { "query", q } });

        var jsonString = await response.Content.ReadAsStringAsync();

        var json = JObject.Parse(jsonString);
        //var value = json.TryGetValue("item", out var name);
        //json.First.ToString();
        //Console.WriteLine(json.First.First.Type.ToString());


        if (json.First == null) return responses.Count == 0 ? null : responses.ToArray();
        
        if (json.First.Path == "errors")
        {
            throw new GraphQlException(json.First.First[0].First.First.ToString()/*, new Exception(json.First.First[0].First.Next.First[0].ToString())*/);
        }
        
        if (json.First.First == null) return responses.Count == 0 ? null : responses.ToArray();
        if (json.First.First.First == null) return responses.Count == 0 ? null : responses.ToArray();
        var result = json.First.First.First.First;
        if (result == null) return null;
        if (result.Type == JTokenType.Array)
        {
            responses.AddRange(from item in result
                from child in item.Children()
                from baby in child.Children()
                select baby.ToString());
        }
        else
        {
            var children = result.Children();
            responses.AddRange(from child in children from baby in child.Children() select baby.ToString());
        }

        return responses.Count == 0 ? null : responses.ToArray();
    }

    public async Task<string[]?> GetResponse(TarkovRequestArgs args)
    {
        return await GetResponse(new[] { args });
    }
    
    private static bool IsArray(dynamic value)
    {
        try
        {
            var dummy = value[0];
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
