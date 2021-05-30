using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Co_Vid19Tracker.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _Disposed) return;
            _Disposed = true;
            //Освобождение управляемых ресурсов.
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
