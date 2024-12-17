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
        };

        builder.Services.AddSingleton(azureSettings);
		
        // Register services
        builder.Services.AddSingleton(new OpenAIClient(new ApiKeyCredential(azureSettings.OpenAIApiKey), new OpenAIClientOptions(){Endpoint = new Uri(azureSettings.OpenAIEndpoint) }));
        builder.Services.AddSingleton(new FormRecognizerClient(new Uri(azureSettings.FormRecognizerEndpoint), new AzureKeyCredential(azureSettings.FormRecognizerApiKey)));
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(azureSettings.SqlConnectionString));

		return builder.Build();
	}
}

