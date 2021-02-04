# reCAPTCHA
A C# application that solves any CAPTCHA or reCAPTCHA query, built in tandem with a python web scraping script to fetch data from a website.

# How it works
The program first visits the CAPTCHA protected website, and views the prompt of the CAPTCHA. It creates a new variable in the console, leaving it blank. It then sends a request to 2Captcha, a CAPTCHA solving service that solves the problem the program sends its way. When 2Captcha solves the request, it sends the program a token (which is used to verify if the CAPTCHA is solved). The token is then passed into the website's console (and into the variable the program created earlier), which permits the program to proceed to the website. 

# Problems that occurred (and how I overcame them)
Google's CAPTCHA service was built by very talented engineers. Although I managed to bypass the security measures, I had to overcame a lot of obstacles to get me to this point; the points mentioned below outline a few of my struggles, and how I overcame them.
- Google checks if the CAPTCHA service is being solved quickly, or if it is being solved at a moderate pace. This severely hindered my program, as it was solving the CAPTCHAs too fast for a human to decipher what the images were, which caused the CAPTCHA service to realize what was going on. Luckily, delays were on my side, and below is an implementation of how they were utilized in my program:
```cs
public static async Task Main(string[] args)
{
  _2Captcha captcha = new _2Captcha(accKey);
  await Solve(captcha, page, page.Url);

  await Task.Delay(30000);
  await Task.Delay(30000);

  await new BrowserFetcher().EvaluateFunctionAsync(BroswerFetcher.DefaultRevision);
  ...
}
```
- Google also actively checks if the console is being tampered with in any way, shape, or form (in this case, it caught the program creating a new variable for the token). To solve this problem, I had the program implement the variable right when the page loaded, which tricked the CAPTCHA service into thinking it was an original variable from the console. This allowed for the program to pass the token to an "authentic" variable, which the CAPTCHA service did not think much about.
Below is an implementation of the variable being set as soon as the page loaded:
```cs
public static async Task Main(string[] args)
{
  string RemoveCookie = @"
  function checkToken() {
    if(typeof window.zt != 'undefined') {
      handleCaptcha(window.zt);
    }
    else
    {
      setTimeout(checkToken, 1000);
    }
  }
  checkToken();
  ";
  ...
}
```

# Educational Purposes Only
This project was built for fun, just to test the waters with CAPTCHA solving. I tested it on Google's [Demo Captcha Service](https://www.google.com/recaptcha/api2/demo), a demo built to test the CAPTCHA's functionality while running tests on it. This program is for **Educational Purposes Only**, and is not meant for one to use while breaching other websites' TOS. This program requires the input of 2Captcha's account key, which hinders them from receiving a token to verify the CAPTCHA, therefore limiting the use of this program. 

# Dependencies Utilized
- C# (framework utilized)
- 2Captcha
