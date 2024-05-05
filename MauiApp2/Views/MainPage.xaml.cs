using System.Reflection;
using System.Text.Json;

namespace MauiApp2.Views
{
    public partial class MainPage : ContentPage
    {
        private HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
            var version = typeof(MauiApp).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;
            VersionLabel.Text = $".NET MAUI ver. {version?[..version.IndexOf('+')]}";
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.None)
            {
                string fact = await client.GetStringAsync("https://catfact.ninja/fact");
                CatFact? deserializedFact = JsonSerializer.Deserialize<CatFact>(fact);
                CounterLabel.Text = deserializedFact.fact;

                SemanticScreenReader.Announce(CounterLabel.Text);
            }
        }
    }

    public class CatFact
    {
        public  string fact { get; set; }
        private int length { get; set; }
    }
}