using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentDialogExample;

internal class Globals {
    public static MainWindow mainWindow;

    public static async void MessageBox(string message, string title) {
        await new ContentDialog {
            XamlRoot = mainWindow.Content.XamlRoot,
            Title = title,
            Content = message,
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close
        }.ShowAsync();
    }
}
