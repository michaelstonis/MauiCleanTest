namespace MauiCleanTest;

public partial class MainPage : ContentPage
{
	private List<Type> _controls;
	private string[] _controlNames;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		try
		{
			_controls ??=
				AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(t => t.GetTypes())
					.Where(t => t.IsClass && t.Namespace == "Microsoft.Maui.Controls" && t.BaseType == typeof(View) && t.GetConstructor(Type.EmptyTypes) is not null)
					.ToList();

			_controlNames ??= _controls.Select(x => x.Name).ToArray();

			var result =
				await this.DisplayActionSheet(
					"Select a view",
					"NOAP",
					null,
					_controlNames);

			var matchingView = _controls.FirstOrDefault(x => x.Name.Equals(result));

			View view = matchingView is not null ? Activator.CreateInstance(matchingView) as View : new ContentView();

			await AppShell.Current.Navigation.PushAsync(new PageWithView(view));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
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
