using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PuppeteerSharpPdfDemo
{
    class MainClass
    {
       

        static void Main(string[] args)
        {
            Matestein(args).Wait();
        }
        public static async Task Matestein(string[] args)
        {
            var options = new LaunchOptions
            {
                Headless = true
            };

            Console.WriteLine("Downloading chromium");
            await Downloader.CreateDefault().DownloadRevisionAsync(Downloader.DefaultRevision);

            Console.WriteLine("Navigating google");
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            }, Downloader.DefaultRevision))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetViewportAsync(new ViewPortOptions{ Width = 1920, Height = 1080});
                await page.GoToAsync("http://sistemasenem.inep.gov.br/EnemSolicitacao/");

                Console.WriteLine("Generating PDF");
                //await page.PdfAsync(Path.Combine(Directory.GetCurrentDirectory(), "google.pdf"));

                Console.WriteLine("Export completed");

                if (!args.Any(arg => arg == "auto-exit"))
                {
                    Console.ReadLine();
                }
            }

        }
    }
}
