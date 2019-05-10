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
//KAtie Harris


namespace JSON
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
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"http://pcbstuou.w27.wh-2.com/webservices/3033/api/Movies?number=100").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var movies = JsonConvert.DeserializeObject<List<Movies>>(content);

                    foreach(var m in movies)
                    {
                        //List all of the different genres for the movies
                        var gs = m.genres.Split('|');
                        foreach(var g in gs)
                        {
                            g.Trim();
                            if (!lstGenres.Items.Contains(g))
                            {
                                lstGenres.Items.Add(g);
                            }
                        }

                    }
                    double score = 0.0;
                    foreach(var m in movies)
                    {
                        //Which movie has the highest IMDB score?
                        if (m.imdb_score > score)
                        {
                            lstHighestScore.Items.Clear();
                            lstHighestScore.Items.Add(m.imdb_score);
                            score = score + m.imdb_score;
                        }
                    }
                    foreach(var m in movies)
                    {
                        //List all of the different movies that have a number of voted users with 350000 or more
                        if (m.num_voted_users >= 350000)
                        {
                            lst350000.Items.Add(m.movie_title);
                        }
                    }

                    int number = 0;
                    foreach(var m in movies)
                    {
                        //How many movies where Anthony Russo is the director ?
                        if(m.director_name.ToLower()=="anthony russo")
                        {
                            number++;
                            
                        }
                    }
                    lstAnthonyRusso.Items.Add(number);

                    int rdjnumber = 0;
                    foreach(var m in movies)
                    {
                        //How many movies where Robert Downey Jr. is the actor 1 ?
                        if(m.actor_1_name=="Robert Downey Jr.")
                        {
                            rdjnumber++;
                        }
                    }
                    lstRobertDowneyJr.Items.Add(rdjnumber);
                }
            }
        }
    }
}
