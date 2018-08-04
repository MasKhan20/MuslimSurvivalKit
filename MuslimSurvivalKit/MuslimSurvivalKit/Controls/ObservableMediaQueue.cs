using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.Controls
{
    public class ObservableMediaQueue<T> : ObservableCollection<T>
    {
        public bool IsLoaded { get; set; }
        public bool IsLoading { get; set; }

        public int PlayerIndex { get; private set; }
        public int ExpectedLength { get; set; }

        //private T _selectedMedia;
        //public T SelectedMedia
        //{
        //    get { return _selectedMedia; }
        //    set
        //    {
        //        _selectedMedia = value;
        //
        //        if (SelectedMedia != null)
        //            SetIndex(SelectedMedia);
        //    }
        //}

        public ObservableMediaQueue()
            : base() { }

        public ObservableMediaQueue(ICollection<T> mediaQueue)
            : base(mediaQueue) { }

        public ObservableMediaQueue(IEnumerable<T> mediaQueue) 
            : base(mediaQueue) { }

        public T CurrentMedia()
        {
            if (base.Count != 0 && PlayerIndex < base.Count)
            {
                return base[PlayerIndex];
            }
            return default(T);
        }

        public bool SetIndex(int index)
        {
            if (base.Count != 0 && index >= 0 && index < base.Count)
            {
                PlayerIndex = index;
                return true;
            }
            return false;
        }

        public bool SetIndex(T mediaItem)
        {
            if (base.Count != 0 && base.Contains(mediaItem))
            {
                PlayerIndex = base.IndexOf(mediaItem);
                return true;
            }
            return false;
        }

        public bool HasPrevious()
        {
            return (base.Count != 0 && PlayerIndex > 0);
        }

        public bool SetIndexAsPrevious()
        {
            if (HasPrevious())
            {
                PlayerIndex--;
                return true;
            }
            return false;
        }

        public bool HasNext()
        {
            return (base.Count != 0 && PlayerIndex < base.Count);
        }

        public bool SetIndexAsNext()
        {
            if (HasNext())
            {
                PlayerIndex++;
                return true;
            }
            return false;
        }
    }
}
