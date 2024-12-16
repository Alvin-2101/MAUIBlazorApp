using System;

namespace MyMauiBlazorApp;

public class AzureSettings
{
    public string OpenAIApiKey { get; set; }
    public string OpenAIEndpoint { get; set; }
    public string FormRecognizerApiKey { get; set; }
    public string FormRecognizerEndpoint { get; set; }
    public string SqlConnectionString { get; set; }
}
