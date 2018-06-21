using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASFund.AutoTest.PrintForms
{
	[TestClass]
	public class ContestQuery_C1_05290
	{
		private IWebDriver _driver;
		private const string _baseURL = "http://demo.ru";
		private const string _baseURLproduction = "http://online.ru";
		private const string _testusername = "";
		private const string _testpassword = "";
		private const string _destinationDirectoryTestStand = @"C:\CI\Tests\DownloadFiles\TestStand\";
		private const string _destinationDirectoryProduction = @"C:\CI\Tests\DownloadFiles\Production\";
		private string _downloadsDirectory = $@"C:\Users\{Environment.UserName}\Downloads\";

		[TestInitialize]
		public void start()
		{
			_driver = new ChromeDriver();
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		/// <summary>
		/// Тест загружает заявки "Заявка_С1-05290.pdf" с тестового и боевого сервера и сравнивает их по string
		/// </summary>
		[TestMethod]
		public void ContestQuery__C1_05290()
		{
			string document = "Заявка_С1-05290.pdf";
			//-- Загружаем заявку Заявка_С1-05290.pdf с тестового стенда
			FileHelper.ClearDownloadsDirectory();
			_driver.Navigate().GoToUrl($"{_baseURL}/Default.aspx");
			Pages page = new Pages(_driver);
			page.LoginTestStand();
			_driver.Navigate().GoToUrl($"{_baseURL}/Pages/Contests/ContestQuery.aspx?Id=8161");
			_driver.FindElement(By.CssSelector("#ctl00_MainPlaceHolder_StickyContainerControl_ContestQueryToolsContainerControl_PrintFormControl_PrintFormsListDdc_ButtonShowContextMenu")).Click();
			_driver.FindElement(By.CssSelector("span.rmText")).Click();
			FileHelper.WaitForDownload(document);
			page.Logoff();
			FileHelper.MoveFile(_downloadsDirectory, _destinationDirectoryTestStand, document);

			//-- Загружаем заявку Заявка_С1-05290.pdf с боевого сервера
			FileHelper.ClearDownloadsDirectory();
			_driver.Navigate().GoToUrl($"{_baseURLproduction}/Default.aspx");
			page.LoginProduction();
			_driver.Navigate().GoToUrl($"{_baseURLproduction}/Pages/Contests/ContestQuery.aspx?Id=8161");
			_driver.FindElement(By.CssSelector("#ctl00_MainPlaceHolder_StickyContainerControl_ContestQueryToolsContainerControl_PrintFormControl_PrintFormsListDdc_ButtonShowContextMenu")).Click();
			_driver.FindElement(By.CssSelector("span.rmText")).Click();
			FileHelper.WaitForDownload(document);
			page.Logoff();
			FileHelper.MoveFile(_downloadsDirectory, _destinationDirectoryProduction, document);
			FileHelper.ComparePDFbyString(_destinationDirectoryTestStand, _destinationDirectoryProduction, document);
		}

		[TestCleanup]
		public void stop()
		{
			_driver.Quit();
			_driver = null;
		}

	}
}