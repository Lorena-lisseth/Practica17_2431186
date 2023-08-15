using Microsoft.Extensions.Logging;
using Practica17_2431186.Data;
using Practica17_2431186.Views;

namespace Practica17_2431186;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<TodoListPage>();

        builder.Services.AddTransient<TodoItemPage>();

        builder.Services.AddSingleton<TodoItemDatabase>();

        return builder.Build();
	}
}
