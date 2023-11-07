using DeeplinkApproach;

namespace DeepinkTests;

public class Tests
{
    private Dictionary<string, string> _inputUrls;

    [SetUp]
    public void Setup()
    {
        _inputUrls = new Dictionary<string, string>()
        {
            //{ "https://m.betsson.com/en/marketing-settings/marketing-consents", "betsson-casino://notificationSettings" },
            { "www.betsson37.com/pl/casino", "betsson-casino://gameCategories" },
            { "betsson37.com/en/deposit", "betsson-casino://deposit" },
            // { "https://www.betsson.com/en/login", "betsson-casino://login" },
            // { "https://www.betsson.com/en/profile", "betsson-casino://profile" },
            // { "https://www.betsson.com/en/messages", "betsson-casino://messages" },
            // { "https://www.betsson.com/en/promotions/casino-tournaments", "betsson-casino://tournaments" },
            // { "https://www.betsson.com/en/withdrawal", "betsson-casino://withdraw" },
            // { "https://www.betsson.com/en/bonuses", "betsson-casino://bonuses" },
            // { "https://www.betsson.com/en/transaction-history", "" },
            // { "https://www.betsson.com/en/open-account", "" },
            // { "https://www.betsson.com/en/verification", ""}
        };
    }

    [Test]
    public void Betsson_InputUrls_Test()
    {
        var engine = new DeepLinkEngine();

        foreach (var inputUrl in _inputUrls)
        {
            var deepLink = engine.GetDeeplink(inputUrl.Key, "BETSSON", "betsson-casino");
            Assert.AreEqual(inputUrl.Value, deepLink);
        }
    }
}