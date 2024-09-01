using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TabViewExample {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabViewPage : Page {
        public TabViewPage(Window window) {
            this.InitializeComponent();
            this.window = window;
            this.window.ExtendsContentIntoTitleBar = true;
            this.window.SetTitleBar(titleBar);
        }

        private readonly Window window;

        public void NewTab(Page page, string header, IconSource icon = null) {
            tabView.TabItems.Add(new TabViewItem {
                Header = header,
                Content = new TabViewItemContentContainer(page),
                IconSource = icon
            });
            tabView.SelectedIndex = tabView.TabItems.Count - 1;
        }

        public void CloseTab(TabViewItem tab) {
            tabView.TabItems.Remove(tab);
        }

        private void AddTabButtonClick() {
            NewTab(new TestPage1(), "Test Page");
        }

        private void CloseTabButtonClick() {
            CloseTab(tabView.SelectedItem as TabViewItem);
        }
    }
}
