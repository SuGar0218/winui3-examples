using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ContentDialogExample {
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Globals.mainWindow = this;

            MessageBox.SetParent(this);
            // �����������ʼ����
            // ��Ϊ�����ڳ�ʼ�� MessageBox ��װ�� ContentDialog �� XamlRoot ���ԣ�
            // �������� WinUI 3 ���Ǳ���ġ�
            // winui3gallery://item/ContentDialog
        }

        private int defaultButton = 0;
        private async void ShowContentDialog(object _, object __) {
            var result = await new ContentDialog {
                XamlRoot = this.Content.XamlRoot,  // ���� WinUI 3 �в���Ϊ null�������׳��쳣��
                Title = "����",
                Content = "���ݣ������� string��Ҳ������ UIElement��������Ĭ�ϰ�ť����Ϊ None ʱ�����¿ո��Enter����Ȼ��ѡ�� Primary ��ť��",
                PrimaryButtonText = "PrimaryButton",
                SecondaryButtonText = "SecondaryButton",
                CloseButtonText = "CloseButton (Esc)",
                DefaultButton = (ContentDialogButton)defaultButton
            }.ShowAsync();
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowOK(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.OK);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowOKCancel(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.OKCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowYesNo(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.YesNo);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowYesNoCancel(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.YesNoCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowAbortRetryIgnore(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.AbortRetryIgnore);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowRetryCancel(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.RetryCancel);
            resultTextBlock.Text = result.ToString();
        }

        private async void ShowCancelTryContinue(object _, object __) {
            var result = await MessageBox.Show("����", "����", MessageBoxButtons.CancelTryContinue);
            resultTextBlock.Text = result.ToString();
        }
    }
}
