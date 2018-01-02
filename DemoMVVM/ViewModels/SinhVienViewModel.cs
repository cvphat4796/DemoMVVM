using Caliburn.Micro;
using DemoMVVM.Helper;
using DemoMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Media;

namespace DemoMVVM.ViewModels
{
    public class SinhVienViewModel : Screen
    {
        #region Fields
        private SinhVienModel db = new SinhVienModel();
        private SinhVien _SelectedSV;
        private int _Index;
        private Lop _SelectedLop;
        private ObservableCollection<SinhVien> _ListSinhVien;
        private ObservableCollection<Lop> _ListLop;
        private ObservableCollection<int> _ListDay;
        private ObservableCollection<int> _ListMonth;
        private ObservableCollection<int> _ListYear;
        private int _Day;
        private int _Month;
        private int _Year;
        private bool _SexNam;
        private bool _SexNu;
        #endregion

        #region Properties Binding
        public ObservableCollection<int> ListDay
        {
            get { return _ListDay; }
            set
            {
                _ListDay = value;
                NotifyOfPropertyChange(() => ListDay);
            }
        }

        public ObservableCollection<int> ListMonth
        {
            get { return _ListMonth; }
            set
            {
                _ListMonth = value;
                NotifyOfPropertyChange(() => ListMonth);
            }
        }

        public ObservableCollection<int> ListYear
        {
            get { return _ListYear; }
            set
            {
                _ListYear = value;
                NotifyOfPropertyChange(() => ListYear);
            }
        }

        public ObservableCollection<SinhVien> ListSinhVien
        {
            get { return _ListSinhVien; }
            set
            {
                _ListSinhVien = value;
                NotifyOfPropertyChange(() => ListSinhVien);
            }
        }

        public ObservableCollection<Lop> ListLop
        {
            get { return _ListLop; }
            set
            {
                _ListLop = value;
                NotifyOfPropertyChange(() => ListLop);
            }
        }

        public SinhVien SelectedSV
        {
            get { return _SelectedSV; }
            set
            {
                _SelectedSV = value;
                if (_SelectedSV != null)
                {
                    Index = ListLop.IndexOf(_SelectedSV.Lop);
                    SelectedDay = _SelectedSV.Birthday.Value.Day;
                    SelectedMonth = _SelectedSV.Birthday.Value.Month;
                    SelectedYear = _SelectedSV.Birthday.Value.Year;
                    switch (_SelectedSV.Sex)
                    {
                        case "Nam":
                            SexNam = true;
                            SexNu = false;
                            break;
                        case "Nữ":
                            SexNam = false;
                            SexNu = true;
                            break;
                    }
                }
                else
                {
                    Index = -1;
                    SelectedDay = SelectedMonth = 1;
                    SelectedYear = 1930;
                    SexNam = SexNu = false;
                }

                NotifyOfPropertyChange(() => SelectedSV);
            }
        }

        public bool SexNam
        {
            get{ return _SexNam; }
            set
            {
                _SexNam = value;
                NotifyOfPropertyChange(() => SexNam);
            }
        }

        public bool SexNu
        {
            get { return _SexNu; }
            set
            {
                _SexNu = value;
                NotifyOfPropertyChange(() => SexNu);
            }
        }

        public int SelectedDay
        {
            get { return _Day; }
            set
            {
                _Day = value;
                NotifyOfPropertyChange(() => SelectedDay);
            }
        }

        public int SelectedMonth
        {
            get { return _Month; }
            set
            {
                _Month = value;
                NotifyOfPropertyChange(() => SelectedMonth);
            }
        }

        public int SelectedYear
        {
            get { return _Year; }
            set
            {
                _Year = value;
                NotifyOfPropertyChange(() => SelectedYear);
            }
        }

        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }

        public Lop SelectedLop
        {
            get { return _SelectedLop; }
            set
            {
                _SelectedLop = value;
                NotifyOfPropertyChange(() => SelectedLop);
            }
        }

        #endregion

        public SinhVienViewModel()
        {
            //Title cho Window
            base.DisplayName = "Thông tin Sinh Viên - CEOS Technology";
            //Lấy danh sách Sinh Viên
            ListSinhVien = new ObservableCollection<SinhVien>(db.SinhVien.ToList());
            //Lấy danh sách lớp
            ListLop = new ObservableCollection<Lop>(db.Lop.ToList());
            //Lấy danh sách ngày tháng năm
            ListDay = GetDateTime.GetListDay();
            ListMonth = GetDateTime.GetListMonth();
            ListYear = GetDateTime.GetListYear();
            Button n = new Button();
        }


        public bool CanInsert(string txtID, string txtName, bool radNam, bool radNu)
        {

            if(String.IsNullOrEmpty(txtID) || String.IsNullOrEmpty(txtName) || (!radNam && !radNu)  || SelectedLop == null)
                return false;
            return true;
        }

        //Sự kiên Insert
        public void Insert(string txtID, string txtName, bool radNam, bool radNu)
        {
            try
            {
                SinhVien SV = new SinhVien
                {
                    Id = txtID,
                    Birthday = new DateTime(SelectedYear, SelectedMonth, SelectedDay),
                    Name = txtName,
                    IdLop = SelectedLop.IdLop,
                    Sex = radNam ? "Nam" : "Nữ"
                };

                db.SinhVien.Add(SV);
                db.SaveChanges();
                ListSinhVien.Add(SV);
            }
            catch
            {
                MessageBox.Show("Lỗi Thêm Sinh Viên");
                SelectedSV = null;
            }
          
        }

        public bool CanUpdate(string txtID, string txtName, bool radNam, bool radNu)
        {

            if (String.IsNullOrEmpty(txtID) || String.IsNullOrEmpty(txtName) || (!radNam && !radNu) || SelectedLop == null)
                return false;
            return true;
        }

        //Sự kiên Sửa
        public void Update(string txtID, string txtName, bool radNam, bool radNu)
        {
            try
            {
                SinhVien SV = db.SinhVien.FirstOrDefault(n => n.Id == txtID);
                SV.IdLop = SelectedLop.IdLop;
                SV.Name = txtName;
                SV.Sex = radNam ? "Nam" : "Nữ";
                SV.Birthday = new DateTime(SelectedYear, SelectedMonth, SelectedDay);
                db.Entry(SV).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ListSinhVien = new ObservableCollection<SinhVien>(db.SinhVien.ToList());
            }
            catch
            {
                MessageBox.Show("Lỗi Sửa Sinh Viên");
                SelectedSV = null;
            }

        }

        //Sự kiện change Text
        public void txtID_TextChanged(object s, EventArgs e)
        {


        }

        //Sự kiện change Text
        public void txtName_TextChanged(object s, EventArgs e)
        {


        }

        public void Grid_Initialized(object s, EventArgs e)
        {
            Grid st = s as Grid;
            //Type t =typeof(SinhVien);
            //MemberInfo[] memberArray = t.GetProperties();
            //for(int col =0;col < memberArray.Count(); col++)
            //{
            //    ColumnDefinition colTemp = new ColumnDefinition();
            //    st.ColumnDefinitions.Add(colTemp);
            //    RowDefinition rowTemp = new RowDefinition();
            //    st.RowDefinitions.Add(rowTemp);
            //    TextBlock headTemp = new TextBlock();
            //  //  headTemp.Text = attributes[col].ToString();
            //}
            //ColumnDefinition col1 = new ColumnDefinition();
            //col1.Width = new GridLength(45);
            //ColumnDefinition col2 = new ColumnDefinition();
            //col1.Width = new GridLength(105);
            //RowDefinition row1 = new RowDefinition();
            //row1.Height = new GridLength(45);
            //RowDefinition row2 = new RowDefinition();
            //row2.Height = new GridLength(45);
            //st.ColumnDefinitions.Add(col1);
            //st.ColumnDefinitions.Add(col2);
            //st.RowDefinitions.Add(row1);
            //st.RowDefinitions.Add(row2);
            //TextBlock txtID = new TextBlock();
            //txtID.Text = "ID";

            //TextBlock txtName = new TextBlock();
            //txtName.Text = "Name";
            //st.Children.Add(txtID);
            //st.Children.Add(txtName);
            //for (int row = 0; row < ListSinhVien.Count; row++)
            //{

            //}

            #region add gird
            // Create the Grid
            Grid DynamicGrid = s as Grid;
            DynamicGrid.Width = 400;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;

            // Create Columns
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);

            // Create Rows
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(45);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(45);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(45);
            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);

            // Add first column header
            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "ID";
            txtBlock1.FontSize = 14;
            txtBlock1.FontWeight = FontWeights.Bold;
            txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock1.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlock1, 0);
            Grid.SetColumn(txtBlock1, 0);

            // Add second column header
            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Name";
            txtBlock2.FontSize = 14;
            txtBlock2.FontWeight = FontWeights.Bold;
            txtBlock2.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock2.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlock2, 0);
            Grid.SetColumn(txtBlock2, 1);

            // Add third column header
            TextBlock txtBlock3 = new TextBlock();
            txtBlock3.Text = "Lop";
            txtBlock3.FontSize = 14;
            txtBlock3.FontWeight = FontWeights.Bold;
            txtBlock3.Foreground = new SolidColorBrush(Colors.Green);
            txtBlock3.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlock3, 0);
            Grid.SetColumn(txtBlock3, 2);

            //// Add column headers to the Grid
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);

            for(int row = 0; row < ListSinhVien.Count; row++)
            {
                TextBlock authorText = new TextBlock();
                authorText.Text = ListSinhVien[row].Id;
                authorText.FontSize = 12;
                authorText.FontWeight = FontWeights.Bold;
                Grid.SetRow(authorText, 1);
                Grid.SetColumn(authorText, 0);



                TextBlock ageText = new TextBlock();
                ageText.Text = ListSinhVien[row].Name;
                ageText.FontSize = 12;
                ageText.FontWeight = FontWeights.Bold;
                Grid.SetRow(ageText, 1);
                Grid.SetColumn(ageText, 1);

                Button btnLop = new Button();
                btnLop.Content = ListSinhVien[row].Lop.Name;
                btnLop.FontSize = 12;
                btnLop.FontWeight = FontWeights.Bold;
                Grid.SetRow(btnLop, 1);
                Grid.SetColumn(btnLop, 2);
                TextBox txtLop = new TextBox();
                txtLop.Text = ListSinhVien[row].Name;
                txtLop.FontSize = 12;
                txtLop.FontWeight = FontWeights.Bold;
                txtLop.Visibility = Visibility.Hidden;

                btnLop.Click += btn1_Click;
                Grid.SetRow(txtLop, 1);
                Grid.SetColumn(txtLop, 2);
                
                // Add first row to Grid
                DynamicGrid.Children.Add(authorText);
                DynamicGrid.Children.Add(ageText);
                DynamicGrid.Children.Add(btnLop);

            }

            

           
            #endregion

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            

        }

        public bool CanDelete(string txtID)
        {

            if (String.IsNullOrEmpty(txtID) || SelectedSV == null)
                return false;
            return true;
        }
        //Sự kiên xóa
        public void Delete(string txtID)
        {
            //Gọi Dialog hỏi
            WindowManager manager = new WindowManager();
            bool Dialog = (bool)manager.ShowDialog(new DialogYesNoViewModel("Bạn muốn Xóa???", "Thông Báo"));
            if (Dialog)
            {
                try
                {
                    SinhVien SV = db.SinhVien.FirstOrDefault(n => n.Id == txtID);
                    db.Entry(SV).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ListSinhVien.Remove(SV);
                }
                catch
                {
                    MessageBox.Show("Lỗi Xóa Sinh Viên");
                    SelectedSV = null;
                }
            }
        
        }
    }
}
