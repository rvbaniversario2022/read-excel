using Microsoft.Win32;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using ExcelDataReader;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> filterConstraints;

        public MainWindow()
        {
            InitializeComponent();

            PopulateCombo();
        }

        private void PopulateCombo()
        {
            filterConstraints = new List<String>
            {
                "--- Select ---",
                "East",
                "Central",
                "West"
            };
            CBO.SelectedItem = "--- Select ---";
            CBO.ItemsSource = filterConstraints;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string path = ofd.FileName.ToString();
            ExcelFileReader(path);
        }

        public void ExcelFileReader(string path)
        {
            if(path != "")
            {
                var stream = File.Open(path, FileMode.Open, FileAccess.Read);
                var reader = ExcelReaderFactory.CreateReader(stream);
                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                IEnumerable<DataTable> tables = result.Tables.Cast<DataTable>();
                foreach (DataTable table in tables)
                {
                    DataGrid.ItemsSource = table.DefaultView;
                }
            }
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataView dv = DataGrid.ItemsSource as DataView;
                if (dv != null)
                {
                    if(TextSearch.Text != "")
                    {
                        dv.RowFilter = string.Format("{0}='{1}'", "Rep", TextSearch.Text);
                    }
                    else
                    {
                        dv.RowFilter = "";
                        DataGrid.ItemsSource = dv;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView dv = DataGrid.ItemsSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = string.Format("{0}='{1}'", "Rep", TextSearch.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView dv = DataGrid.ItemsSource as DataView;
            String selectedItem = CBO.SelectedItem.ToString();
            if(dv != null)
            {
                if(selectedItem != "--- Select ---")
                {
                    dv.RowFilter = string.Format("Region Like '%{0}%'", selectedItem);
                    DataGrid.ItemsSource = dv;
                }
                else
                {
                    dv.RowFilter = "";
                    DataGrid.ItemsSource = dv;
                }
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = DataGrid.ItemsSource as DataView;
            if(dv != null)
            {
                if(DatePickerFrom.SelectedDate != null || DatePickerTo.SelectedDate != null)
                {
                    DateTime Date1 = DatePickerFrom.SelectedDate.Value.Date;
                    DateTime Date2 = DatePickerTo.SelectedDate.Value.Date;
                    dv.RowFilter = string.Format("OrderDate >= '{0:MM/dd/yyyy}' AND OrderDate <= '{1:MM/dd/yyyy}'", Date1, Date2);
                    DataGrid.ItemsSource = dv;
                }
                else
                {
                    dv.RowFilter = "";
                    DataGrid.ItemsSource = dv;
                }
            }
        }
    }
}