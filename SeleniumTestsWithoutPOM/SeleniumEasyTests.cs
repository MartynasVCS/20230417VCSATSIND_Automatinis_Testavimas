using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestsWithoutPOM
{
    internal class SeleniumEasyTests
    {
        [Test]
        public void SingleInputFieldTest()
        {
            string expectedValue = "Sveiki gyvi";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demo.seleniumeasy.com/basic-first-form-demo.html";

            IWebElement inputEnterMessage = driver.FindElement(By.XPath("//*[@id='user-message']"));
            inputEnterMessage.SendKeys(expectedValue);

            IWebElement buttonShowMessage = driver.FindElement(By.XPath("//*[@id='get-input']/button"));
            buttonShowMessage.Click();

            IWebElement outputYourMessage = driver.FindElement(By.XPath("//*[@id='display']"));
            string actualValue = outputYourMessage.Text;

            Assert.AreEqual(expectedValue, actualValue);

            driver.Quit();
        }

        [Test]
        public void TwoInputFieldsTest()
        {
            string valueA = "11";
            string valueB = "25";
            string expectedValue = "36";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demo.seleniumeasy.com/basic-first-form-demo.html";

            IWebElement inputEnterA = driver.FindElement(By.XPath("//*[@id='value1']"));
            inputEnterA.SendKeys(valueA);

            IWebElement inputEnterB = driver.FindElement(By.XPath("//*[@id='value2']"));
            inputEnterB.SendKeys(valueB);

            IWebElement buttonGetTotal = driver.FindElement(By.XPath("//*[@id='gettotal']/button"));
            buttonGetTotal.Click();

            IWebElement outputTotal = driver.FindElement(By.XPath("//*[@id='displayvalue']"));
            string actualValue = outputTotal.Text;

            Assert.AreEqual(expectedValue, actualValue);

            driver.Quit();
        }
    }
}
