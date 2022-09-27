using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions(){};

options.AddArguments(new List<string>() { "headless", "disable-gpu" });

var browser = new ChromeDriver(options);

string fullUrl = "https://eparduotuve.iki.lt/";

browser.Navigate().GoToUrl(fullUrl);
// var selector = "#__next > div.chakra-stack.css-1g4pjqx > div.css-1e753tq > div.css-k008qs > div.chakra-stack.css-i19d2p > div.chakra-stack.css-1xn8lk1";

// var adultConsentSelector = @"#chakra-modal--body-\:r2u\: > div > div.css-qjhnk7 > button.chakra-button.css-43yk5h";
var adultConsentSelector = @"#chakra-modal--body-\:r2u\: > div > div.css-qjhnk7 > button.chakra-button.css-43yk5h";

// TODO:
// This is a temporary workaround
// Should wait untill required element is loaded
Thread.Sleep(8000); // 4 seconds

// var adultConsentButton = browser.FindElement(By.CssSelector(adultConsentSelector));
// var adultConsentButton = browser.FindElement(By.ClassName("chakra-button css-43yk5h"));
var adultConsentButton = browser.FindElements(By.CssSelector(adultConsentSelector));
Thread.Sleep(1000); // 1 second
// adultConsentButton.Click();
foreach (var b in adultConsentButton)
{
    b.Click();
    Console.WriteLine(b);
}
Console.WriteLine(adultConsentButton);

var ss = browser.GetScreenshot();
ss.SaveAsFile("lol.png");

browser.Quit();
