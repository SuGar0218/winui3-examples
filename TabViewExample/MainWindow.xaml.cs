using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace TabViewExample;

public sealed partial class MainWindow : Window {
    public MainWindow() {
        Globals.mainWindow = this;

        InitializeComponent();
        page = new TabViewPage(this);
        Content = page;

        AppWindow.Title = "TabView Example";
        //ExtendsContentIntoTitleBar = true;

        page.NewTab(new TestPage1(), "ÐÂ±êÇ©Ò³");
    }

    private readonly TabViewPage page;
}
