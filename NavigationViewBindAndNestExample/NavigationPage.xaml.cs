using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NavigationViewBindAndNestExample {
    public sealed partial class NavigationPage : Page {
        public NavigationPage() {
            InitializeComponent();
            ViewModel = new NavigationPageViewModel(this, new NavigationPageModel());
        }

        private NavigationPageViewModel ViewModel { get; }

        public NavigationView NavigationView1 { get => nav1; }
        public NavigationView NavigationView2 { get => nav2; }
    }

    internal class NavigationPageModel {
        public static readonly Dictionary<string, string[]> menu = new() {
            {"通用", ["关于本机", "软件更新", "隔空投送", "接力", "CarPlay 车载"]},
            {"控制中心", ["应用内访问", "自定控制"]},
            {"显示与亮度", ["亮度", "夜览", "自动锁定", "抬起唤醒", "文字大小", "粗体文本", "视图"]},
        };

        public string[] Select(string item) => menu[item];
    }

    internal class NavigationPageViewModel : ObservableObject {
        public NavigationPageViewModel(NavigationPage view, NavigationPageModel model) {
            this.view = view;
            this.model = model;
        }

        internal readonly NavigationPage view;
        internal readonly NavigationPageModel model;

        private ObservableCollection<string> menu1 = new(NavigationPageModel.menu.Keys);
        internal ObservableCollection<string> Menu1 {
            get => menu1;
            private set => SetProperty(ref menu1, value);
        }

        private ObservableCollection<string> menu2 = [];
        internal ObservableCollection<string> Menu2 {
            get => menu2;
            private set => SetProperty(ref menu2, value);
        }

        internal void Navigation1SelectionChanged() {
            Menu2 = new(model.Select(view.NavigationView1.SelectedItem as string));
            // 使用 view.NavigationView1.SelectedItem as string
            // 而不是 SelectedItem="{x:Bind selected1, Mode=TwoWay}"
            // SelectedItem 的改变同步到绑定项发生改变在 SelectionChanged 事件完成之后
            // (这一点可以通过断点调试，并在多次切换选中项后查看绑定的变量)
            // 响应 SelectionChanged 事件时的绑定项仍是改变前的
        }
    }
}
