using Microsoft.SqlServer.Server;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

            IWebElement inputEmail = driver.FindElement(By.Id("userEmail"));
            inputEmail.SendKeys(inputText);

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

            IWebElement outputEmail = driver.FindElement(By.Id("email"));
            string actualResult = outputEmail.Text;

            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }

        [Test]
        public void FillFormWithInvalidEmailAndCheckThatElementDoesNotExist()
        {
            string inputText = "testas";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demoqa.com/text-box";

            IWebElement inputEmail = driver.FindElement(By.Id("userEmail"));
            inputEmail.SendKeys(inputText);

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
                    actions.ScrollByAmount(0, 20);
                    actions.Perform();
                    currentTry++;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            bool outputElementExists = false;
            try
            {
                IWebElement outputEmail = driver.FindElement(By.Id("email"));
                outputElementExists = true;
            }
            catch (NoSuchElementException)
            {
                // Jeigu elemento nėra tai šiuo konkrečiu atveju nieko papildomai daryti nereikia
            }
            catch (Exception exception)
            {
                throw exception;
            }

            Assert.IsFalse(outputElementExists);

            driver.Quit();
        }

        [Test]
        public void FillFormWithInvalidEmailAndCheckEmailInputBorderColor()
        {
            string inputText = "testas";
            string expectedBorderColor = "rgb(255, 0, 0)";

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demoqa.com/text-box";

            IWebElement inputEmail = driver.FindElement(By.Id("userEmail"));
            inputEmail.SendKeys(inputText);

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
                    actions.ScrollByAmount(0, 200);
                    actions.Perform();
                    currentTry++;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            //System.Threading.Thread.Sleep(1000); // Veikia, bet bloga praktika naudoti Sleep() metodą su konkrečiom reikšmėm

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(50); // Laukiam dažniau nes pagal nutylėjimą tikrinimas vyksta kas 500ms
            wait.Until(d => d.FindElement(By.Id("userEmail")).GetCssValue("border-color").Equals(expectedBorderColor)); // Gera praktika, bet sudėtingoka :)

            string actualBorderColor = inputEmail.GetCssValue("border-color");

            Assert.AreEqual(expectedBorderColor, actualBorderColor);

            driver.Quit();
        }

        [Test]
        public void WaitForButtonToBeVisible()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://demoqa.com/dynamic-properties";

            string id = "visibleAfter";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));

            //bool elementDisplayed = wait.Until(d => d.FindElement(By.Id(id)).Displayed);
            //Assert.IsTrue(elementDisplayed);

            driver.Quit();
        }
    }
}
