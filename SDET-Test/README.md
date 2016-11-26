##BGL Business Technology SDET Test

Candidate: Eihab El-Mahi

Freameworks used:
+	- NUnit 
+	- Selenium
	
I have chose to use the NUnit to take advantage to its' matured features of the "SetUp", "Test", and "TearDown" 
which allows for cleanup at the end of the test process.
Selenium profides easy methods of selecting and interacting with elements on the page.

The test process overview:

			1. Start of test and initialise drivers collection continer
			2. Test the fields population and submition of the form
			3. Try to submit the form without invalid fields values and check the notification messages
			4. Repeat steps 2 and 3 for different browsers
			5. Clean up initialised drivers in steps 2 and 3, then end the test.

Some of the test steps are exachtly the same across different browsers. That is why I have created
a seperate class, named "ElementMethods.cs", to host these steps as static methods.
To make it consistent, I have created constants hosted in the "ElementSelectors.cs" class. These constants 
are used in defining the way of looking for web elements on the page.

#Issues:

Have an issue on Microsoft Edge web driver. I fails to recognise the ropdown option been set! It is recognised when using the mouse.
I did not find a solution to this during the time of the test, but will keep looking.

NuGet packages installed:
+	- NUnit						3.5.0
+	- Selenium.Support				3.0.1
+	- Selenium.WebDriver				3.0.1
+	- Selenium.WebDriver.ChromeDriver		3.0.1
+	- Selenium.WebDriver.IEDriver			3.0.1
+	- Selenium.WebDriver.MicrosoftWebDriver		10.0.14393.0
