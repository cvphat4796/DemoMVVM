using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DemoMVVM.Models
{
    public static class GetDateTime
    {
        //List Ngày
        public static ObservableCollection<int> GetListDay()
        {
            ObservableCollection<int> List = new ObservableCollection<int>();
            for(int i=1; i<=31; i++)
            {
                List.Add(i);
            }
            return List;

        }

        //List Tháng
        public static ObservableCollection<int> GetListMonth()
        {
            ObservableCollection<int> List =  new ObservableCollection<int>();
            for (int i = 1; i <= 12; i++)
            {
                List.Add(i);
            }
            return List;

        }

        //List Năm
        public static ObservableCollection<int> GetListYear()
        {
            return new ObservableCollection<int>(Enumerable.Range(1930, DateTime.Now.Year - 1930 + 1).ToList());
           

        }
    }
}
