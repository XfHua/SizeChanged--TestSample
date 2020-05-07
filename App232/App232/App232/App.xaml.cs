using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App232
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Change this to false to see that ShowSizePage works properly if not loaded through MenuPage
            bool showMenu = true;

            if (showMenu)
                MainPage = new MenuPage(); // Throws exception
            else
                MainPage = new ShowSizePage(); // Works
        }
    }

    public class MenuPage : ContentPage
    {
        readonly StackLayout menu = new StackLayout();
        readonly ScrollView menuView = new ScrollView();
        public MenuPage()
        {
            Button showSizeButton = new Button { Text = "Show Size" };
            showSizeButton.Clicked += OnShowSizeClicked;
            menu.Children.Add(showSizeButton);

            Button showLabelButton = new Button { Text = "Show Label" };
            showLabelButton.Clicked += OnShowLabelClicked;
            menu.Children.Add(showLabelButton);

            menuView.Content = menu;

            Content = menuView;
        }
        void OnShowSizeClicked(object sender, EventArgs args)
        {
            ContentPage c = new ShowSizePage();

            Content = c.Content; // Exception is caused here
        }

        void OnShowLabelClicked(object sender, EventArgs args)
        {
            ContentPage c = new ShowLabelPage();

            Content = c.Content;
        }

        protected override bool OnBackButtonPressed()
        {
            Content = menuView;

            return true;
        }
    }
    public class ShowSizePage : ContentPage
    {
        readonly Label label;
        public ShowSizePage()
        {
            label = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "initialized",
            };

            SizeChanged += OnPageSizeChanged;
            Content = label;
        }
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            label.Text = String.Format("{0} \u00D7 {1}", Width, Height);
        }

    }
    public class ShowLabelPage : ContentPage
    {
        public ShowLabelPage()
        {
            Content = new Label { Text = "Hello World! Press hardware back button for menu." };
        }
    }
}
