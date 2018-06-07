using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

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
                Headless = false

            };

            Console.WriteLine("Downloading chromium");
            await Downloader.CreateDefault().DownloadRevisionAsync(Downloader.DefaultRevision);

            Console.WriteLine("Navigating google");
            using (var browser = await Puppeteer.LaunchAsync(options, Downloader.DefaultRevision))
            using (var page = await browser.NewPageAsync())
            {
                try
                {
                   
                    Directory.SetCurrentDirectory(@"C:\Sistemas");
                    page.DefaultNavigationTimeout=80000;
                    await page.GoToAsync("http://sistemasenem.inep.gov.br/EnemSolicitacao/login.seam");
                    await page.SetViewportAsync(new ViewPortOptions { Width = 1920, Height = 920 });
                    //await page.Frames.FirstOrDefault().SelectAsync("#username", "111111");
                    await page.TypeAsync("#username", "191010104484");
                    await page.TypeAsync("#password", "UniSL2016");
                    await page.ClickAsync("input[type='image']");

                    var menu = await page.WaitForSelectorAsync("#LeftMenu #menugroup_4_3 #row_menugroup_4_3");
                    await menu.ClickAsync();
                    await page.ClickAsync("#j_id50");
                    Thread.Sleep(2000);
                    var inputCPF = await page.WaitForSelectorAsync("#formularioForm");
                    await page.TypeAsync("#formularioForm input[type='text']", "35168598811");
                    await page.ClickAsync("#formularioForm input[type='image']");
                    await page.WaitForSelectorAsync("#resultadoForm input[type='image']");
                    Thread.Sleep(2000);
                    await page.ClickAsync("#j_id57");

                    await page.WaitForSelectorAsync("#listaSolicitacaoAtendidas");
                    Thread.Sleep(2000);
                    await page.QuerySelectorAsync("#listaSolicitacaoAtendidas tbody tr td:nth-last-child(1) a");
                    var href = await page.EvaluateExpressionAsync("document.querySelector('#listaSolicitacaoAtendidas tbody tr td:nth-last-child(1) a').href");
                    //await page.ClickAsync("#listaSolicitacaoAtendidas tbody tr td:nth-last-child(1) a");
                    //await page.GoToAsync(href);
                    

                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(page.GoToAsync(href), @"c:\Sistemas\myfile.txt");
                    }




                    //await page.ClickAsync("#resultadoForm input[type='image']");
                    Console.WriteLine("Terminou!");
                    await browser.CloseAsync();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.Write("O erro foi: " + ex);
                    Console.ReadKey();
                }

            }

        }
    }
}
