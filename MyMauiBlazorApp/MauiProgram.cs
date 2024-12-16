using System.ClientModel;
using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenAI;

namespace MyMauiBlazorApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		// Add configuration settings
        var azureSettings = new AzureSettings
        {
            OpenAIApiKey = "bqRGN3bJyGO7jjaWGbikhFzCp0k6h71HPKceOgbGgUyTT1gYvRI2JQQJ99ALACYeBjFXJ3w3AAAAACOG0t5k",
            OpenAIEndpoint = "https://prasa-m4qyzdgh-eastus.cognitiveservices.azure.com/",
			// OpenAIApiKey = "bqRGN3bJyGO7jjaWGbikhFzCp0k6h71HPKceOgbGgUyTT1gYvRI2JQQJ99ALACYeBjFXJ3w3AAAAACOG0t5k",
			// OpenAIEndpoint = "https://prasa-m4qyzdgh-eastus.cognitiveservices.azure.com/openai/deployments/gpt-35-turbo/chat/completions?api-version=2024-08-01-preview",
            FormRecognizerApiKey = "Dc5ujlFzD7Z1iZH2KmMsKs4aUaJRWsfx1Zy0O2nizc0NX2KDqidlJQQJ99ALACHYHv6XJ3w3AAALACOGVMWd",
            FormRecognizerEndpoint = "https://gensdi.cognitiveservices.azure.com/",
            SqlConnectionString = "Server=tcp:genssqlserver.database.windows.net,1433;Initial Catalog=SqlDBGenS;Persist Security Info=False;User ID=gensadmin;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        };

        builder.Services.AddSingleton(azureSettings);
		
        // Register services
        builder.Services.AddSingleton(new OpenAIClient(new ApiKeyCredential(azureSettings.OpenAIApiKey), new OpenAIClientOptions(){Endpoint = new Uri(azureSettings.OpenAIEndpoint) }));
        builder.Services.AddSingleton(new FormRecognizerClient(new Uri(azureSettings.FormRecognizerEndpoint), new AzureKeyCredential(azureSettings.FormRecognizerApiKey)));
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(azureSettings.SqlConnectionString));

		return builder.Build();
	}
}

