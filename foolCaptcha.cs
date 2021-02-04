class foolCaptcha 
{
	public static async Task Main(string[] args)
	{
		string accKey = {ACCOUNT_KEY};
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

		await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
		
		var Browser = await Puppeteer.LaunchAsync(new LaunchOptions() {Headless = false});
		//var page = await Puppeteer.NewPageAsync();
		var page = await browser.NewPageAsync();

		await page.GoToAsync("{URL_TO_PAGE}");
		// _2Captcha captcha = new _2Captcha(browser.NewPageAsync());
		_2Captcha captcha = new _2Captcha(accKey);
		await Solve(captcha, page, page.Url);

		//await Task.Delay(30000);
		//await Task.Delay(30000);

		//await new BrowserFetcher().EvaluateFunctionAsync(BroswerFetcher.DefaultRevision);
		await new BrowserFetcher().DownloadAsync(BroswerFetcher.DefaultRevision);
		var browser = await Puppeteer.LaunchAsync(new LaunchOptions {Headless = false});

	}

	private async static Task<Task> Solve(_2Captcha captcha, Page page, string url)
	{
		var balance = await captcha.GetBalance();
		var reCaptcha = await captcha.SolveV2("{HELP_KEY}", url);
		string token = reCaptcha.Response;

		Console.WriteLine("Balance: " + balance);
		Console.WriteLine("Solving...")
		Console.WriteLine("Received Token");

		//await Task.Delay(60000);
		//await Task.Delay(60000);

		await page.EvaluateFunctionAsync($"() => handleCaptcha('" + token + "')");

		Console.WriteLine("Captcha solved");


		return Task.CompletedTask;
	}
}

