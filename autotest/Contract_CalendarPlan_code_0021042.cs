using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASFund.AutoTest.PrintForms
{
	[TestClass]
	public class Contract_CalendarPlan_code_0021042
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
		/// Загрузка "Календарный план"
		/// Договор №306ГР/21042 от 15.04.2016 (код 0021042), заявка №ERA-SME-14977, конкурс ERA-SME 2014
		/// </summary>
		[TestMethod]
		public void Contract__CalendarPlan_code_0021042()
		{
			string document = "Календарный план_договор_0021042.pdf";
			FileHelper.ClearDownloadsDirectory();
			_driver.Navigate().GoToUrl($"{_baseURL}/Default.aspx");
			Pages page = new Pages(_driver);
			page.LoginTestStand();
			_driver.Navigate().GoToUrl($"{_baseURL}/Pages/Contracts/Contract.aspx?Id=25072");
			_driver.FindElement(By.CssSelector("#ctl00_MainPlaceHolder_StickyContainerControl_ContractsToolsContainerControl_PrintFormControl_PrintFormsListDdc_ButtonShowContextMenu")).Click();
			_driver.FindElement(By.XPath("//div[@id='ctl00_MainPlaceHolder_StickyContainerControl_ContractsToolsContainerControl_PrintFormControl_PrintFormsListDdc_RadContextMenu_detached']/ul/li[3]/a/span")).Click();
			FileHelper.WaitForDownload(document);
			page.Logoff();
			FileHelper.MoveFile(_downloadsDirectory, _destinationDirectoryTestStand, document);

			FileHelper.ClearDownloadsDirectory();
			_driver.Navigate().GoToUrl($"{_baseURLproduction}/Default.aspx");
			page.LoginProduction();
			_driver.Navigate().GoToUrl($"{_baseURLproduction}/Pages/Contracts/Contract.aspx?Id=25072");
			_driver.FindElement(By.CssSelector("#ctl00_MainPlaceHolder_StickyContainerControl_ContractsToolsContainerControl_PrintFormControl_PrintFormsListDdc_ButtonShowContextMenu")).Click();
			_driver.FindElement(By.XPath("//div[@id='ctl00_MainPlaceHolder_StickyContainerControl_ContractsToolsContainerControl_PrintFormControl_PrintFormsListDdc_RadContextMenu_detached']/ul/li[3]/a/span")).Click();
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
