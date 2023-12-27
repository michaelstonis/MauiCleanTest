using ReactiveUI;
using ReactiveUI.Maui;

namespace MauiCleanTest;

public partial class MainPage : ReactiveContentPage<MainPageViewModel>
{
	int count = 0;

	public MainPage()
	{
		BindingContext = new MainPageViewModel();
		InitializeComponent();
	}

	~MainPage()
	{
        Console.WriteLine("Cleanup Main Page");
    }

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		var controls =
			AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(t => t.GetTypes())
				.Where(t => t.IsClass && t.Namespace == "Microsoft.Maui.Controls" && t.BaseType == typeof(View) && t.GetConstructor(Type.EmptyTypes) is not null)
				.ToList();

        var result =
			await this.DisplayActionSheet(
				"Select a view",
				"NOAP",
				null,
				controls.Select(x => x.Name).ToArray());

		var matchingView = controls.FirstOrDefault(x => x.Name.Equals(result));

		View view = matchingView is not null ? Activator.CreateInstance(matchingView) as View : new ContentView();

		await AppShell.Current.Navigation.PushAsync(new PageWithView(view));
	}

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
		var prememInfo = GC.GetGCMemoryInfo();

		GC.WaitForPendingFinalizers();
		GC.Collect();

        GC.WaitForPendingFinalizers();
        GC.Collect();

		var postmemInfo = GC.GetGCMemoryInfo();

        MemInfo.Text = $"Before: {prememInfo.TotalCommittedBytes/Math.Pow(1024.0,2):N2} MB / Pinned After: {postmemInfo.TotalCommittedBytes/Math.Pow(1024.0,2):N2} MB";
    }
}

public class MainPageViewModel : ReactiveObject
{

}


