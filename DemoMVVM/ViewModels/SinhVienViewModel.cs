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
        public void txtID_TextChanged(EventArgs e)
        {


        }

        //Sự kiện change Text
        public void txtName_TextChanged( EventArgs e)
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
