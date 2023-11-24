namespace ConnectivityChangeTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            SetInfo();

        }

        private void SetInfo()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                lblWorld.Text = "Hello Internet World";
            }
            else
            {
                lblWorld.Text = "Hello World... Hello????";
            }
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            SetInfo();
            if (e.NetworkAccess == NetworkAccess.ConstrainedInternet)
                Console.WriteLine("Internet access is available but is limited.");

            else if (e.NetworkAccess != NetworkAccess.Internet)
                Console.WriteLine("Internet access has been lost.");

            // Log each active connection
            Console.Write("Connections active: ");

            foreach (var item in e.ConnectionProfiles)
            {
                switch (item)
                {
                    case ConnectionProfile.Bluetooth:
                        Console.Write("Bluetooth");
                        break;
                    case ConnectionProfile.Cellular:
                        Console.Write("Cell");
                        break;
                    case ConnectionProfile.Ethernet:
                        Console.Write("Ethernet");
                        break;
                    case ConnectionProfile.WiFi:
                        Console.Write("WiFi");
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine();
        }

    }



}
