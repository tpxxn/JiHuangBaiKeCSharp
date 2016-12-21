using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiHuangUWP.Model;

namespace JiHuangUWP.ViewModel
{
    public class DstidModel:ViewModelBase
    {
        public DstidModel()
        {

        }

        public ObservableCollection<DstidAbigail> DstidAbigail
        {
            set;
            get;
        }=new ObservableCollection<DstidAbigail>();

        private List<DstidAbigail> _dstidAbigail;


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
