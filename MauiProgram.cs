using Microsoft.Extensions.Logging;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;

namespace TriviaAppClean;

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
			})
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginView> ();
        builder.Services.AddTransient<SignUpView>();
        builder.Services.AddTransient<AllQuestionsView>();
        builder.Services.AddTransient<BestScoresView>();
        builder.Services.AddTransient<HomePageView>();
        builder.Services.AddTransient<ListOfUsersView>();
        builder.Services.AddTransient<OnePendingQuestionView>();
        builder.Services.AddTransient<OneQuestionView>();
        builder.Services.AddTransient<OneUserView>();
        builder.Services.AddTransient<TheGameView>();
        builder.Services.AddTransient<UserProfileView>();
        builder.Services.AddTransient<PendingView>();


        builder.Services.AddTransient<MasterAppShell>();
        builder.Services.AddTransient<ManagerAppShell>();
        builder.Services.AddTransient<UserAppShell>();

        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<AllQuestionViewModel>();
        builder.Services.AddTransient<BestScoresViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
        builder.Services.AddTransient<ListOfUsersViewModel>();
        builder.Services.AddTransient<OnePendingQuestionViewModel>();
        builder.Services.AddTransient<OneQuestionViewModel>();
        builder.Services.AddTransient<OneUserViewModel>();
        builder.Services.AddTransient<TheGameViewModel>();
        builder.Services.AddTransient<UserProfileViewModel>();
        builder.Services.AddTransient<PendingViewModel>();

        return builder;
    }
}
