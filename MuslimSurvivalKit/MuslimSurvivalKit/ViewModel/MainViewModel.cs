using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Binding Properties
        private string _adUnitId;
        public string AdUnitId
        {
            get { return _adUnitId; }
            set
            {
                _adUnitId = value;
                RaisePropertyChanged(nameof(AdUnitId));
            }
        }
        #endregion

        public MainViewModel()
        {
            AdUnitId =
#if DEBUG
                  "ca-app-pub-3940256099942544/6300978111";
#else
                "ca-app-pub-4025243320631804/8161981113";
#endif
        }
    }
}
