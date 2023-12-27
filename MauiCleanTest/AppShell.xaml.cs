namespace MauiCleanTest;

public partial class AppShell : Shell
{
	public AppShell()
	{
        Routing.RegisterRoute("MainPage2", typeof(MainPage));
        Routing.RegisterRoute(nameof(PageWithView), typeof(PageWithView));
        InitializeComponent();
	}
}

