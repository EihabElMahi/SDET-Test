using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;

namespace SDET_Test
{
    [TestFixture]
    public class BudgetInsurance_Test
    {
        // The collection of the used web drivers in thistest 
        private List<IWebDriver> drivers;

        #region --- Test fields values ---
        private string targetUrl = @"https://www.budgetinsurance.com/Car/Z001/NLE";
        private string titleFieldName = "input_1-1";
        private string firstnameFieldName = "input_1-2";
        private string surnameFieldId = "input_1-3";
        private string dateOfBirthFields = "input_1-4";
        private string bDayFieldName = "input_1-4_d";
        private string bMonthFieldName = "input_1-4_m";
        private string bYearFieldName = "input_1-4_y";
        private string houseFieldName = "input_1-5_house";
        private string postcodeFieldName = "input_1-5_pcode";
        private string findAddrButtonId = "findAddressButton_1-5";
        private string changeAddressButtonText = "change address";
        private string changeAddressButtonId = "changeAddressButton";
        private string regFieldName = "input_1-6";
        private string continueButtonText = "continue";
        private string continueButtonClass = "continueButton";

        private string testElementId = "input_1-8"; // Email field in the next page

        private string userTitle = "Mr";
        private string firstName = "John";
        private string surname = "Smith";
        private string targetAddress = "B G L GROUP";
        private string postcode = "PE2 6YS";
        private string dayOfBirth = "07";
        private string monthOfBirth = "04";
        private string yearOfBirth = "1979";
        private string houseNameOrNumber = "B G L";
        private string regFielValue = "AK55 OWB";

        private string validationClass = "validationText";
        private string houseValidationClass = "autoValidationRiskAddress_house";
        private string pcodeValidationClass = "autoValidationRiskAddress_pcode";
        private string forAttribute = "for";
        private string validationContainer = "span";
        #endregion

        [SetUp]
        public void SetupTest()
        {
            drivers = new List<IWebDriver>();
        }

        //Test that uses Edge Driver
        [Test]
        [Order(0)]
        public void Test_Edge()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new EdgeDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            // Logging test steps for debug
            Console.WriteLine("\nStart of Edge Test:\n---------------------");

            // --- Fill form fields ---
            FillDetails(driver);

            // --- Address ---
            FillAddress(driver);

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByCssClass, continueButtonClass);

            // Check if the next page loaded successfuly
            ElementMethods.CheckLoadedPage(driver, ElementSelectors.ById, testElementId); // Edge is case-sensitive on selecting by text

            // Logging the end of this test
            Console.WriteLine("End of Edge Test\n---------------------");
        }

        //Test that uses Edge Driver for validations
        [Test]
        [Order(1)]
        public void Test_Edge_Validations()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new EdgeDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            // Logging test steps for debug
            Console.WriteLine("\nStart of Edge valuation Test:\n---------------------");

            // Fill first name with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ByName, firstnameFieldName, firstName.Substring(0, 1));

            // Fill surname  with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ById, surnameFieldId, surname.Substring(0, 1));

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByCssClass, continueButtonClass);

            // Title validation
            string errors = ElementMethods.CheckValidations(driver, validationClass, new string[] {
                titleFieldName,
                firstnameFieldName,
                surnameFieldId,
                dateOfBirthFields
            });
            Console.WriteLine($"Edge Validation errors:\n{errors}");
            Assert.IsEmpty(errors);
            // Address validators - House name or number
            string hVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, houseValidationClass);
            Assert.IsNotNull(hVal);
            Assert.IsNotEmpty(hVal);
            // Address validators - Postcode
            string pVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, pcodeValidationClass);
            Assert.IsNotNull(pVal);
            Assert.IsNotEmpty(pVal);

            // Logging the end of this test
            Console.WriteLine("End of Edge valuation Test\n---------------------");
        }
        //Test that uses IE Driver
        [Test]
        [Order(2)]
        public void Test_IE()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new InternetExplorerDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            Console.WriteLine("\nStart of IE Test:\n---------------------");
            // --- Fill form fields ---
            FillDetails(driver);

            // --- Address ---
            FillAddress(driver);

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByPartialLinkText, continueButtonText);

            // Check if the next page loaded successfuly
            ElementMethods.CheckLoadedPage(driver, ElementSelectors.ById, testElementId);

            // Logging the end of this test
            Console.WriteLine("End of IE Test\n---------------------");
        }

        //Test that uses IE Driver for validations
        [Test]
        [Order(3)]
        public void Test_IE_Validations()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new InternetExplorerDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            // Logging test steps for debug
            Console.WriteLine("\nStart of IE validation Test:\n---------------------");

            // Fill first name with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ByName, firstnameFieldName, firstName.Substring(0, 1));

            // Fill surname  with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ById, surnameFieldId, surname.Substring(0, 1));

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByPartialLinkText, continueButtonText);

            // Title validation
            string errors = ElementMethods.CheckValidations(driver, validationClass, new string[] {
                titleFieldName,
                firstnameFieldName,
                surnameFieldId,
                dateOfBirthFields
            });
            Console.WriteLine($"IE Validation errors:\n{errors}");
            Assert.IsEmpty(errors);
            // Address validators - House name or number
            string hVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, houseValidationClass);
            Assert.IsNotNull(hVal);
            Assert.IsNotEmpty(hVal);
            // Address validators - Postcode
            string pVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, pcodeValidationClass);
            Assert.IsNotNull(pVal);
            Assert.IsNotEmpty(pVal);

            // Logging the end of this test
            Console.WriteLine("End of IE valuation Test\n---------------------");
        }

        // Test that uses Google chrome driver
        [Test]
        [Order(4)]
        public void Test_Chrome()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new ChromeDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            // Logging test steps for debug
            Console.WriteLine("\nStart of Chrome Test:\n---------------------");

            // --- Fill form fields ---
            FillDetails(driver);

            // --- Address ---
            FillAddress(driver);

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByPartialLinkText, continueButtonText);

            // Check if the next page loaded successfuly
            ElementMethods.CheckLoadedPage(driver, ElementSelectors.ById, testElementId);

            // Logging the end of this test
            Console.WriteLine("End of Chrome Test\n---------------------");
        }

        // Test that uses Google chrome driver for validations
        [Test]
        [Order(5)]
        public void Test_Chrome_Validations()
        {
            // Initialize Initiating IE webdriver which is copied by the package installer to the bi\Debug folder
            // If a different driver is required then the full path must be pased as a string parameter
            // Then add to the collection to be cleaned at the end of the test process
            IWebDriver driver = new ChromeDriver();
            drivers.Add(driver);

            // Navigate to target Url
            driver.Navigate().GoToUrl(targetUrl);

            // Logging test steps for debug
            Console.WriteLine("\nStart of Chrome valuation Test:\n---------------------");

            // Fill first name with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ByName, firstnameFieldName, firstName.Substring(0, 1));

            // Fill surname  with less than the required length
            ElementMethods.SetText(driver, ElementSelectors.ById, surnameFieldId, surname.Substring(0, 1));

            // Submit the for by clicking on "continue" button
            ElementMethods.Click(driver, ElementSelectors.ByPartialLinkText, continueButtonText);

            // Title validation
            string errors = ElementMethods.CheckValidations(driver, validationClass, new string[] {
                titleFieldName,
                firstnameFieldName,
                surnameFieldId,
                dateOfBirthFields
            });
            Console.WriteLine($"Chrome Validation errors:\n{errors}");
            Assert.IsEmpty(errors);
            // Address validators - House name or number
            string hVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, houseValidationClass);
            Assert.IsNotNull(hVal);
            Assert.IsNotEmpty(hVal);
            // Address validators - Postcode
            string pVal = ElementMethods.GetContentText(driver, ElementSelectors.ByCssClass, pcodeValidationClass);
            Assert.IsNotNull(pVal);
            Assert.IsNotEmpty(pVal);

            // Logging the end of this test
            Console.WriteLine("End of Chrome valuation Test\n---------------------");
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                // Loop through and quit all initialised drivers
                foreach (IWebDriver driver in drivers)
                {
                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                Console.WriteLine($"Tear down error:\n{ex.Message}");
            }
        }

        #region --- Shared Steps Between Browsers ---
        private void FillDetails(IWebDriver driver)
        {

            // Fill title field
            ElementMethods.SetText(driver, ElementSelectors.ByName, titleFieldName, userTitle);

            // Fill first name field
            ElementMethods.SetText(driver, ElementSelectors.ByName, firstnameFieldName, firstName);

            // Fill surname field
            ElementMethods.SetText(driver, ElementSelectors.ById, surnameFieldId, surname);

            // Fill Day of Birth field
            ElementMethods.SetDropdownOption(driver, ElementSelectors.ByName, bDayFieldName, dayOfBirth);

            // Fill Month of Birth field
            ElementMethods.SetDropdownOption(driver, ElementSelectors.ByName, bMonthFieldName, monthOfBirth);

            // Fill Year of Birth field
            ElementMethods.SetDropdownOption(driver, ElementSelectors.ByName, bYearFieldName, yearOfBirth);

            // Fill house name or number field
            ElementMethods.SetText(driver, ElementSelectors.ByName, houseFieldName, houseNameOrNumber);

            // Fill postcode field
            ElementMethods.SetText(driver, ElementSelectors.ByName, postcodeFieldName, postcode);

            // Fill Vehicle registration field
            //ElementMethods.SetText(driver, ElementSelectors.ByName, regFieldName, regFielValue); // does not affect the functionality in test

        }
        private void FillAddress(IWebDriver driver)
        {
            // Click on find address
            ElementMethods.Click(driver, ElementSelectors.ById, findAddrButtonId);
            // Pick "B G L Group" address from the list
            ElementMethods.PickAddress(driver, targetAddress);

            // Click on "change address" 
            ElementMethods.Click(driver, ElementSelectors.ById, changeAddressButtonId);
            // Click on find address (with the same set values)
            ElementMethods.Click(driver, ElementSelectors.ById, findAddrButtonId);
            // Pick "B G L Group" address from the list
            ElementMethods.PickAddress(driver, targetAddress);
        }
        #endregion
    }
}