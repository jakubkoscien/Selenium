using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography.X509Certificates;

namespace Selenium
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new System.Drawing.Point(1720, 0);
            driver.Manage().Window.Size = new System.Drawing.Size(1720, 1440);

            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://google.pl");
            IWebElement cookieButton = driver.FindElement(By.Id("L2AGLb"));
            cookieButton.Click();
            IWebElement searchField = driver.FindElement(By.CssSelector("[title='Szukaj']"));
            string searchEntry = "wszechœwiaty równolegle";
            
            searchField.SendKeys(searchEntry);
            searchField.Submit();

            string title = "Wieloœwiat – Wikipedia, wolna encyklopedia";
            driver.FindElement(By.XPath(".//*[text()='" + title + "']")).Click();
            var element = driver.FindElement(By.XPath(".//*[text()='" + title + "']"));

            string entryUrl = "https://pl.wikipedia.org/wiki/Wielo%C5%9Bwiat";
            Assert.AreEqual(entryUrl, driver.Url, "URL is not correct.");

            Screenshot ele = (element as ITakesScreenshot).GetScreenshot();
            ele.SaveAsFile("ss.png");
            
        }

        [TearDown] public void TearDown() 
        {
            driver.Quit();
        }
    }
}