namespace DeeplinkApproach;

public class DeepLinkEngine
{
    
    public string GetDeeplink(string inputUrl, string brandId, string schema)
    {
        var brandRules = GetRulesByBrand(brandId);
        var deeplink = ConstructDeeplink(brandRules, inputUrl, schema);
        
        return deeplink;
    }

    /// <summary>
    /// Get rules from database by brand id 
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    private List<Rule> GetRulesByBrand(string brandId)
    {
        var rules = new List<Rule>()
        {
            new("BETSSON", "/{a-z}/marketing-settings/marketing-consents", "{{SCHEMA}}://notificationSettings", 1),
            new("BETSSON", "/[a-z]{2}/casino", "{{SCHEMA}}://gameCategories", 2),
            new("BETSSON", "/[a-z]{2}/deposit", "{{SCHEMA}}://deposit", 3),
            new("BETSSON", "/[a-z]{2}/(?<segment>[a-zA-Z-0-9]*)/", "{{SCHEMA}}://{{SEGMENT}}", 4),
            
        };
        return rules.Where(x => x.BrandId == brandId).OrderBy(x => x.Order).ToList();
    }
    
    
    private string ConstructDeeplink(IReadOnlyList<Rule> rules, string inputUrl, string schema)
    {
        var deepLink = string.Empty;
        foreach (var rule in rules)
        {
            if (!rule.IsMatch(inputUrl))
            {
                continue;
            }
            deepLink = ReplaceSchemaLabel(rule.OutputUrl, schema);
            deepLink = ReplaceCategoryLabel(deepLink, out var category);
            deepLink = ReplaceGameLabel(deepLink, category);
            deepLink = ReplaceParametersLabel(deepLink);
            break;
        }
        return deepLink;
    }

    /// <summary>
    ///  if contains parameters
    /// filled the parameters label
    /// </summary>
    /// <param name="deepLink"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private string ReplaceParametersLabel(string deepLink)
    {
        return deepLink;
    }

    /// <summary>
    /// if contains game
    ///  Make a call to game service and get it
    /// filled the game label
    /// </summary>
    /// <param name="deepLink"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private string ReplaceGameLabel(string deepLink, string category)
    {
        return deepLink;
    }

    /// <summary>
    ///  If contains category label
    ///  Make a call to category service and get it
    ///  Filled the category label
    /// </summary>
    /// <param name="deepLink"></param>
    /// <returns></returns>
    private string ReplaceCategoryLabel(string deepLink, out string category)
    {
        category = "category";
        return deepLink;
    }

    /// <summary>
    /// Replace schema label if it contains the label 
    /// </summary>
    /// <param name="deepLink"></param>
    /// <param name="schema"></param>
    /// <returns></returns>
    private string ReplaceSchemaLabel(string deepLink, string schema)
    {
        return ReplaceLabel(deepLink, schema, ConstantLabels.SCHEMA);
    }
    
    private string ReplaceLabel(string input, string replacement, string label)
    {
        ReadOnlySpan<char> inputSpan = input.AsSpan();
        ReadOnlySpan<char> labelSpan = label.AsSpan();
        ReadOnlySpan<char> replacementSpan = replacement.AsSpan();
        int labelLength = labelSpan.Length;

        while (true)
        {
            int index = inputSpan.IndexOf(labelSpan);
            if (index == -1)
            {
                break; // No more occurrences of label
            }
            
            Span<char> resultSpan = new char[input.Length + (replacement.Length - labelLength)];
            
            inputSpan.Slice(0, index).CopyTo(resultSpan);
            
            replacementSpan.CopyTo(resultSpan.Slice(index));
            
            inputSpan.Slice(index + labelLength).CopyTo(resultSpan.Slice(index + replacement.Length));

            inputSpan = resultSpan;
        }
        return inputSpan.ToString();
    }
    
    
}