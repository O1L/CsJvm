using CsJvm.VirtualMachine.MAUI.PageModels;
using FreshMvvm.Maui;

namespace CsJvm.VirtualMachine.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                appWindow.Resize(new Windows.Graphics.SizeInt32(1280, 720));
#endif
            });

            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");
            masterDetailNav.FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;

            masterDetailNav.AddPage<MainPageModel>("Main", null);
            //masterDetailNav.AddPage<SettingsPageModel>("Settings", null);
            MainPage = masterDetailNav;
        }
    }
}