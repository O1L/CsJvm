

namespace CsJvm.VirtualMachine.MAUI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OpcodesListView.ItemSelected += OnItemSelected;
        }

        protected override void OnDisappearing()
        {
            OpcodesListView.ItemSelected -= OnItemSelected;
            base.OnDisappearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Dispatcher.DispatchAsync(() =>
            {
                if (sender is not ListView listView || e == null)
                    return;

                listView.ScrollTo(e.SelectedItem, ScrollToPosition.End, false);
            });
        }
    }
}