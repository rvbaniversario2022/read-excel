using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> filterConstraints;

        public MainWindow()
        {
            InitializeComponent();

            PopulateCombo();
        }

        private void PopulateCombo()
        {
            filterConstraints = new List<string>
            {
                "--- Select ---",
                "East",
                "Central",
                "West"
            };
            CBO.SelectedItem = filterConstraints[0];
            CBO.ItemsSource = filterConstraints;
        }

        //private void BtnOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    _ = ofd.ShowDialog();
        //    string path = ofd.FileName.ToString();
        //    ExcelFileReader(path);
        //}

        //public void ExcelFileReader(string path)
        //{
        //    if (path != "")
        //    {
        //        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        //        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
        //        DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
        //        {
        //            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //            {
        //                UseHeaderRow = true
        //            }
        //        });
        //        IEnumerable<DataTable> tables = result.Tables.Cast<DataTable>();
        //        foreach (DataTable table in tables)
        //        {
        //            DataGrid.ItemsSource = table.DefaultView;
        //        }
        //    }
        //}

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DG.ItemsSource is DataView dv)
                {
                    if (TextSearch.Text != "")
                    {
                        dv.RowFilter = string.Format("{0}='{1}'", "Rep", TextSearch.Text);
                    }
                    else
                    {
                        dv.RowFilter = "";
                        DG.ItemsSource = dv;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView dv = DG.ItemsSource as DataView;
                if (dv != null)
                {
                    if (TextSearch.Text != "")
                    {
                        dv.RowFilter = string.Format("{0}='{1}'", "Rep", TextSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView dv = DG.ItemsSource as DataView;
            String selectedItem = CBO.SelectedItem.ToString();
            if (dv != null)
            {
                if (selectedItem != "--- Select ---")
                {
                    dv.RowFilter = string.Format("Region Like '%{0}%'", selectedItem);
                    DG.ItemsSource = dv;
                }
                else
                {
                    dv.RowFilter = "";
                    DG.ItemsSource = dv;
                }
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = DG.ItemsSource as DataView;
            if (dv != null)
            {
                if (DatePickerFrom.SelectedDate != null || DatePickerTo.SelectedDate != null)
                {
                    DateTime Date1 = DatePickerFrom.SelectedDate.Value.Date;
                    DateTime Date2 = DatePickerTo.SelectedDate.Value.Date;
                    dv.RowFilter = string.Format("OrderDate >= '{0:MM/dd/yyyy}' AND OrderDate <= '{1:MM/dd/yyyy}'", Date1, Date2);
                    DG.ItemsSource = dv;
                }
                else
                {
                    dv.RowFilter = "";
                    DG.ItemsSource = dv;
                }
            }
        }
    }
}