using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ContentDialogExample {
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Globals.mainWindow = this;

            MessageBox.SetParent(this);
            // 必须做这个初始化，
            // 因为它用于初始化 MessageBox 包装的 ContentDialog 的 XamlRoot 属性，
            // 这在桌面 WinUI 3 中是必须的。
            // winui3gallery://item/ContentDialog
        }

        private int defaultButton = 0;
        private async void ShowContentDialog(object _, object __) {
            var result = await new ContentDialog {
                XamlRoot = this.Content.XamlRoot,  // 桌面 WinUI 3 中不能为 null，否则抛出异常。
                Title = "标题",
                Content = "内容（可以是 string，也可以是 UIElement）。对于默认按钮，设为 None 时，按下空格或Enter，仍然会选择 Primary 按钮。",
                PrimaryButtonText = "PrimaryButton",
                SecondaryButtonText = "SecondaryButton",
                CloseButtonText = "CloseButton (Esc)",
                DefaultButton = (ContentDialogButton)defaultButton
            }.ShowAsync();
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowOK(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.OK);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowOKCancel(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.OKCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowYesNo(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.YesNo);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowYesNoCancel(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.YesNoCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowAbortRetryIgnore(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.AbortRetryIgnore);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowRetryCancel(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.RetryCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowCancelTryContinue(object _, object __) {
            var result = await MessageBox.Show("内容", "标题", MessageBoxButtons.CancelTryContinue);
            resultTextBlock.Text = result.ToString();
        }
    }
}
