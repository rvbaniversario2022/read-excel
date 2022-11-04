using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp2.Model
{
    public class Excel : INotifyPropertyChanged
    {
        private int _OrderID;
        private string _OrderDate;
        private string _Region;
        private string _Rep;
        private string _Item;
        private int _Units;
        private double _UnitCost;
        private double _Total;

        public int OrderID
        {
            get => _OrderID;
            set 
            {
                _OrderID = value;
                onPropertyChanged("OrderID");
            }
        }
        public string OrderDate
        {
            get => _OrderDate;
            set
            {
                _OrderDate = value;
                onPropertyChanged("OrderDate");
            }
        }
        public string Region
        {
            get => _Region;
            set
            {
                _Region = value;
                onPropertyChanged("Region");
            }
        }
        public string Rep { 
            get => _Rep;
            set
            {
                _Rep = value;
                onPropertyChanged("Rep");
            }
        }
        public string Item { 
            get => _Item;
            set
            {
                _Item = value;
                onPropertyChanged("Item");
            }
        }
        public int Units { 
            get => _Units;
            set
            {
                _Units = value;
                onPropertyChanged("Units");
            }
        }
        public double UnitCost { 
            get => _UnitCost;
            set
            {
                _UnitCost = value;
                onPropertyChanged("UnitCost");
            } 
        }
        public double Total { 
            get => _Total;
            set
            {
                _Total = value;
                onPropertyChanged("Total");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
