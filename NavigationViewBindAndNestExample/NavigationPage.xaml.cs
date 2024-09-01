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
            {"ͨ��", ["���ڱ���", "�������", "����Ͷ��", "����", "CarPlay ����"]},
            {"��������", ["Ӧ���ڷ���", "�Զ�����"]},
            {"��ʾ������", ["����", "ҹ��", "�Զ�����", "̧����", "���ִ�С", "�����ı�", "��ͼ"]},
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
            // ʹ�� view.NavigationView1.SelectedItem as string
            // ������ SelectedItem="{x:Bind selected1, Mode=TwoWay}"
            // SelectedItem �ĸı�ͬ����������ı��� SelectionChanged �¼����֮��
            // (��һ�����ͨ���ϵ���ԣ����ڶ���л�ѡ�����鿴�󶨵ı���)
            // ��Ӧ SelectionChanged �¼�ʱ�İ������Ǹı�ǰ��
        }
    }
}
