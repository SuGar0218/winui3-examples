using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Dynamic;
using CommunityToolkit.WinUI.UI.Controls;

namespace DataGridDynamicColumnsExample {
    public sealed partial class DataGridPage : Page {
        public DataGridPage() {
            InitializeComponent();
            ViewModel = new DataGridPageViewModel(this, new DataGridPageModel());
        }

        private DataGridPageViewModel ViewModel { get; }

        public DataGrid DataGrid { get => dataGrid; }
    }

    internal class DataGridPageModel {
        private static dynamic Student(string sno, string sname, string gender) {
            dynamic student = new ExpandoObject();
            student.sno = sno;
            student.sname = sname;
            student.gender = gender;
            return student;
        }

        public readonly List<string> studentColumns = ["sno", "sname", "gender"];
        public readonly List<dynamic> students = [
            Student("001", "Tom", "Male"),
            Student("002", "Jerry", "Male"),
        ];


        private static dynamic Sale(string product, double price) {
            dynamic sale = new ExpandoObject();
            sale.product = product;
            sale.price = price;
            return sale;
        }

        public readonly List<string> saleColumns = ["product", "price"];
        public readonly List<dynamic> sales = [
            Sale("iPhone 15 Pro", 7999.00),
            Sale("iPhone 15", 5999.00),
            Sale("iPhone 14", 5399.00),
        ];
    }

    internal class DataGridPageViewModel {
        public DataGridPageViewModel(DataGridPage view, DataGridPageModel model) {
            this.view = view;
            this.model = model;
        }

        internal readonly DataGridPage view;
        internal readonly DataGridPageModel model;

        public readonly ObservableCollection<dynamic> data = [];

        // ComboBox SelectedIndex="{x:Bind ViewModel.query, Mode=TwoWay}"
        // SelectedIndex = -1 时，ComboBox 选中为空。
        internal int query = -1;
        internal void ComboBoxSelectionChanged() {
            switch (query) {
                case 0:
                    LoadData(model.studentColumns, model.students);
                    break;
                case 1:
                    LoadData(model.saleColumns, model.sales);
                    break;
            }
        }

        public void LoadData(List<string> columns, List<dynamic> source) {
            data.Clear();
            view.DataGrid.Columns.Clear();
            foreach (string column in columns) {
                view.DataGrid.Columns.Add(new DataGridTextColumn {
                    Header = column,
                    Binding = new Binding { Path = new(column) }  // Microsoft.UI.Xaml.Data.Binding
                });
            }
            foreach (dynamic item in source) {
                data.Add(item);
            }
        }
    }
}
