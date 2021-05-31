using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Co_Vid19Tracker.ViewModels.Base;

namespace Co_Vid19Tracker.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок
        private string _Title = "Анализ статистики заболеваемости";
        /// <summary>Заголовок окна</summary>
        public string Title 
        {
            get => _Title;
            //set
            //{
            //    if (Equals(_Title, value)) return;
            //    _Title = value;
            //    OnPropertyChanged();
            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Статус программы
        private string _Status = "Готов!";
        /// <summary>Статус программы</summary>
        public string Status 
        { 
            get => _Status; 
            set => Set(ref _Status,value); 
        }
        #endregion
    }


}
