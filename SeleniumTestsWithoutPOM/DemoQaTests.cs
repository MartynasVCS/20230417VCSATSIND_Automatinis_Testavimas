using Microsoft.SqlServer.Server;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace SeleniumTestsWithoutPOM
{
    internal class DemoQaTests
    {
        [Test]
        public void FillFormWithName()
        {
            string inputText = "Martynas";
            string expectedResult = $"Name:{inputText}";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demoqa.com/text-box";

            IWebElement inputFullName = driver.FindElement(By.Id("userName"));
            inputFullName.SendKeys(inputText);

            Actions actions = new Actions(driver);
            actions.ScrollByAmount(0, 200);
            actions.Perform();

            IWebElement buttonSubmit = driver.FindElement(By.Id("submit"));
            buttonSubmit.Click();

            IWebElement outputFullName = driver.FindElement(By.Id("name"));
            string actualResult = outputFullName.Text;

            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }

        [Test]
        public void FillFormWithEmail()
        {
            string inputText = "testas@testas.lt";
            string expectedResult = $"Email:{inputText}";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demoqa.com/text-box";

            IWebElement inputFullName = driver.FindElement(By.Id("userEmail"));
            inputFullName.SendKeys(inputText);

            IWebElement buttonSubmit = driver.FindElement(By.Id("submit"));

            Actions actions = new Actions(driver);
            
            bool wasClicked = false;
            int maxTries = 20;
            int currentTry = 1;
            while (wasClicked == false && currentTry < maxTries)
            {
                try
                {
                    buttonSubmit.Click();
                    wasClicked = true;
                }
                catch (ElementClickInterceptedException)
                {
                    actions.ScrollByAmount(0, 10);
                    actions.Perform();
                    currentTry++;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            IWebElement outputFullName = driver.FindElement(By.Id("email"));
            string actualResult = outputFullName.Text;

            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }
    }
}
