using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace GoogleImageSearchTest1
{
    public class Tests
    {
        private IWebDriver _driver;

        private readonly By _searchInputButton = By.XPath("//input[@name='q']");
        private readonly By _searchGoogleButton = By.XPath("//input[@name='btnK']");
        private readonly By _imagetab = By.XPath("//a[contains(@data-hveid, 'QAw')]");
        private readonly By _image = By.XPath("//img[@jsname='Q4LuWd']");

        private readonly string SearchInput = "image";

        public bool ImagePresent()
        {
           try
           {
                _driver.FindElements(_image);
                return true;
           }
            catch (NoSuchElementException)
           {
                return false;
           }
        }

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.google.com/");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            
            var SearchInputField = _driver.FindElement(_searchInputButton);
            SearchInputField.SendKeys(SearchInput);

            WebDriverWait waitForSearchInput = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitForSearchInput.Until(ExpectedConditions.ElementToBeClickable(_searchGoogleButton));
            var SearchButtonPressing = _driver.FindElement(_searchGoogleButton);
            SearchButtonPressing.Click();

            WebDriverWait waitForSearchingFinish = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitForSearchingFinish.Until(ExpectedConditions.ElementToBeClickable(_imagetab));
            var ImageTab = _driver.FindElement(_imagetab);
            ImageTab.Click();

            WebDriverWait waitImageTabLoading = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitImageTabLoading.Until(ExpectedConditions.ElementToBeClickable(_image));
            Assert.IsTrue(ImagePresent());

        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}