using System.Text.RegularExpressions;

namespace DeeplinkApproach;

public class Rule
{
    public Rule(string brandId, string regexPattern, string outputUrl, int order)
    {
        BrandId = brandId;
        RegexPattern = regexPattern;
        OutputUrl = outputUrl;
        Order = order;
        _inputRegex = new Regex(RegexPattern, RegexOptions.Compiled);
    }


    public int Order { get;  }
    public string BrandId { get; }
    public string RegexPattern { get; }
    public string OutputUrl { get; }

    public string Description { get; set; }
    
    private readonly Regex _inputRegex;
    public bool IsMatch(string url)
    {
        return _inputRegex.IsMatch(url); 
    }
    
    
    
}