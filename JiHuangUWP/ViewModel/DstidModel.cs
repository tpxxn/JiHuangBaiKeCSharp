using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using JiHuangUWP.Model;

namespace JiHuangUWP.ViewModel
{
    public class DstidModel : ViewModelBase
    {
        public DstidModel()
        {

        }

        public ObservableCollection<DstidAbigail> DstidAbigail
        {
            set;
            get;
        } = new ObservableCollection<DstidAbigail>();

        private List<DstidAbigail> _dstidAbigail;

        public string DebugStr
        {
            set
            {
                _debugStr = value;
                OnPropertyChanged();
            }
            get
            {
                return _debugStr;
            }
        }



        private string _debugStr;

        public void Screen()
        {
            DstidAbigail.Clear();
            foreach (var temp in _dstidAbigail)
            {
                if (string.IsNullOrEmpty(DebugStr) || Bdt(temp.Name, DebugStr) || Bdt(temp.Chinese, DebugStr))
                {
                    DstidAbigail.Add(temp);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">数据</param>
        /// <param name="str">匹配</param>
        public static bool Bdt(string text, string str)
        {
            int i = 0;
            bool reu = false;
            foreach (var temp in str)
            {
                reu = false;
                for (; i < text.Length; i++)
                {
                    if (temp == text[i])
                    {
                        reu = true;
                        i++;
                        break;
                    }
                }
            }
            return reu;
        }

        public override void OnNavigatedFrom(object obj)
        {
            DstidAbigail.Clear();
        }

        public override async void OnNavigatedTo(object obj)
        {
            _dstidAbigail = obj as List<DstidAbigail>;
            if (_dstidAbigail != null)
            {
                DstidAbigail.Clear();
                foreach (var temp in _dstidAbigail)
                {
                    DstidAbigail.Add(temp);
                }

                foreach (var temp in _dstidAbigail)
                {
                    await temp.Read();
                }
            }
        }
    }
}
