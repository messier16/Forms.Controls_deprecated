using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestTestApp
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void TakeScreenshots()
		{
			app.Screenshot("First screen.");
			app.Tap("GoToRatingBarButton");
			app.WaitForElement("page");
			app.Screenshot("RatingBar page.");
			app.Back();
			app.Tap("GoToCheckboxButton");
			app.WaitForElement("page");
			app.Screenshot("Checkbox page.");
			app.Back();
			app.Tap("GoToSegmentedControl");
			app.WaitForElement("page");
			app.Screenshot("SegmentedControl page.");
			app.Back();

		}

		[Test]
		public void StartRepl()
		{
			app.Repl();
		}
	}
}
