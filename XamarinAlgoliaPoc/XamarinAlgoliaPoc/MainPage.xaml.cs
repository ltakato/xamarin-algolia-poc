using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace XamarinAlgoliaPoc
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public class Article
        {
            public string Title { get; set; }
            public string Text { get; set; }
            public int HelpedTimes { get; set; }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        SearchClient client = new SearchClient("1O8025CHOL", "36745d5c73f105c909e761b43fe40600");

        ObservableCollection<Article> articles = new ObservableCollection<Article>();
        public ObservableCollection<Article> Articles { 
            get { return articles; }
            set {
                articles = value;
                OnPropertyChanged("Articles");
            }
        }

        private async void Search(object sender, EventArgs e)
        {
            Articles.Clear();

            ArticlesView.ItemsSource = Articles;

            SearchIndex index = client.InitIndex("articles");
            var result = await index.SearchAsync<Article>(new Query(text.Text));

            Articles = new ObservableCollection<Article>(result.Hits);

            ArticlesView.ItemsSource = Articles;
        }
    }
}
