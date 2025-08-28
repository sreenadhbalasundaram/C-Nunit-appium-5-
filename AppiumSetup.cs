#nullable enable
using System;                         // For Uri, Exception, TimeSpan, NullReferenceException
using NUnit.Framework;                // For NUnit attributes
using OpenQA.Selenium;                // For By, WebDriver types
using OpenQA.Selenium.Appium;         // For AppiumDriver
using OpenQA.Selenium.Appium.Android; // For AndroidDriver

namespace UITests;

[SetUpFixture]
public class AppiumSetup
{
    private static AndroidDriver? driver;

    public static AppiumDriver AppDriver => driver ?? throw new NullReferenceException("AppiumDriver is null");

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = "Galaxy.*",
            PlatformVersion = "14",
            App = "Your_App_URL_Here" // e.g., "lt://APP101534739901378560"
        };

        // LambdaTest creds
        options.AddAdditionalAppiumOption("user", "Your_Username_Here");
        options.AddAdditionalAppiumOption("accessKey", "Your_AccessKey_Here");

        // LambdaTest caps
        options.AddAdditionalAppiumOption("isRealMobile", true);
        options.AddAdditionalAppiumOption("build", "Csharp NUnit Build");
        options.AddAdditionalAppiumOption("name", TestContext.CurrentContext.Test?.Name ?? "Sample Test");

        driver = new AndroidDriver(
            new Uri("https://mobile-hub.lambdatest.com/wd/hub"),
            options,
            TimeSpan.FromSeconds(120) // keep it, ensures enough time to start session
        );
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        driver?.Quit();
    }
}
