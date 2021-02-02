using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _04_UIWait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TestUI : Window
    {
        public TestUI()
        {
            InitializeComponent();
            button.Click += (sender, e) => Go();
            buttonNet.Click += (sender, e) => Request();
        }

        private async void Request()
        {
            buttonNet.IsEnabled = false;

            string[] urls = "www.baidu.com www.qq.com www.github.com".Split();

            int totalLength = 0;
            try
            {
                foreach (var url in urls)
                {
                    var uri = new Uri("http://" + url);
                    byte[] data = await new WebClient().DownloadDataTaskAsync(uri);
                    results.Text += "Length of: " + url + " is " + data.Length + Environment.NewLine;
                    totalLength += data.Length;
                }
                results.Text += "Total length: " + totalLength;
            }
            catch (WebException ex)
            {
                results.Text += "Error: " + ex.Message;
            }
            finally
            {
                buttonNet.IsEnabled = true;
            }
        }

        private async void Go()
        {
            button.IsEnabled = false;
            for (int i = 0; i < 5; i++)
            {
                // 1
                //results.Text += GetPrimesCount(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1) + Environment.NewLine;

                results.Text += await GetPrimesCountAsync(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1) + Environment.NewLine;
            }
            button.IsEnabled = true;
        }

        private int GetPrimesCount(int start, int count)
        {
            return ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }

        private Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count)
                                    .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
}
