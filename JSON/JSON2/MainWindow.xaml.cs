using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace JSON2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDiagnostics_Click(object sender, RoutedEventArgs e)
        {
            using(var client = new HttpClient())
            {
                var response = client.GetAsync(@"http://pcbstuou.w27.wh-2.com/webservices/3033/api/Movies?number=100").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var movies = JsonConvert.DeserializeObject<List<YEP>>(content);
                    //List all of the different genres for the movies
                    foreach(var m in movies)
                    {
                        var gs = m.genres.Split('|');
                        foreach(var g in gs)
                        {
                            if (!lstGenres.Items.Contains(g))
                            {
                                lstGenres.Items.Add(g);
                            }
                        }
                    }

                    //Which movie has the highest IMDB score?
                    
                    //List all of the different movies that have a number of voted users with 350000 or more
                    //How many movies where Anthony Russo is the director ?
                    //How many movies where Robert Downey Jr. is the actor 1 ?
                }
            }
        }
    }
}
