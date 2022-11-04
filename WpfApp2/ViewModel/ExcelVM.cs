using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using WpfApp2.Model;
using System.Data;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using ExcelDataReader;

namespace WpfApp2.ViewModel
{
    public class ExcelVM : INotifyPropertyChanged
    {
        private readonly Excel _excel = new Excel();
        public DataTable DataTableMerger => _excel.DataTableMerger;
        public ICommand AddRowsButtonClickCommand => new DelegateCommand(o => ReturnDataTableForGridView());

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReturnDataTableForGridView()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            _ = ofd.ShowDialog();
            string path = ofd.FileName.ToString();
            ExcelFileReader(path);
        }

        public void ExcelFileReader(string path)
        {
            if (path != "")
            {
                FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
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
                    _excel.DataTableMerger = table;
                    OnPropertyChanged(nameof(DataTableMerger));
                }
            }
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExcecute;
        private readonly Action<object> _execute;
        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action<object> execute) : this(execute, null) { }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExcecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExcecute is null)
                return true;
            return _canExcecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
