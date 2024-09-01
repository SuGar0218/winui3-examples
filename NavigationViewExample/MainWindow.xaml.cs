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

namespace NavigationViewExample {
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            AppWindow.Title = "Navigation View Example";
        }

        private TestPage1 page1 = null;
        private TestPage2 page2 = null;

        private void NavigationSelectionChanged() {
            string selected = nav.SelectedItem as string;
            nav.Header = selected;
            switch (selected) {
                case "Page 1":
                    if (page1 is null) {
                        page1 = new TestPage1();
                    }
                    navContainer.Content = page1;
                    break;
                case "Page 2":
                    if (page2 is null) {
                        page2 = new TestPage2();
                    }
                    navContainer.Content = page2;
                    break;
                case "Navigate Page 1":
                    navContainer.Navigate(typeof(TestPage1));
                    break;
                case "Navigate Page 2":
                    navContainer.Navigate(typeof(TestPage2));
                    break;
            }
        }
    }
}
