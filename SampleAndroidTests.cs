using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium;

namespace UITests;

[TestFixture]
public class SampleAndroidTests
{
    private AndroidDriver driver;

    [SetUp]
    public void Init()
    {
        driver = (AndroidDriver)AppiumSetup.AppDriver;
    }

    [Test]
    public void CheckPageSource()
    {
        string source = driver.PageSource;
        Assert.IsNotNull(source);
        TestContext.WriteLine("Got Page Source OK");
    }

    [Test]
    public void FindElementExample()
    {
        try
        {
            var el = driver.FindElement(MobileBy.Id("com.lambdatest.proverbial:id/color"));
            Assert.IsNotNull(el, "Element not found");
            el.Click();
            TestContext.WriteLine("Found and clicked the element successfully");
        }
        catch (Exception e)
        {
            Assert.Fail("Could not find element: " + e.Message);
        }
    }

    [TearDown]
    public void MarkResult()
    {
        bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
        driver.ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
    }
}
