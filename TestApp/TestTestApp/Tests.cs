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
		public void EnterText()
		{
			app.EnterText("maskedEntry", "154569");
			app.Screenshot("154569");
			app.ClearText("maskedEntry");
			app.EnterText("maskedEntry", "488631");
			app.Screenshot("488631");
			app.ClearText("maskedEntry");
			app.EnterText("maskedEntry", "1006998891778630");
			app.Screenshot("1006998891778630");
			app.ClearText("maskedEntry");
		}

		[Test]
		public void StartRepl()
		{
			app.Repl();
		}
	}
}
