using System;
namespace MauiCleanTest
{
	public class PageWithView : ContentPage
	{
		public PageWithView(View view)
		{
			Content = view;
		}

		~PageWithView()
		{
            Console.WriteLine($"GCed page with {Content?.GetType().Name}");
        }
	}
}

