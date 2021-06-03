using Co_Vid19Tracker.Infrastructure.Commands;
using Co_Vid19Tracker.Models;
using Co_Vid19Tracker.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Co_Vid19Tracker.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Индекс номера выбранной вкладки
        private int _SelectedPageIndex;

        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }
        #endregion

        #region Тестовые точки для графика
        private IEnumerable<DataPoint> _TestDataPoint;
        public IEnumerable<DataPoint> TestDataPoint
        {
            get => _TestDataPoint; 
            set => Set(ref _TestDataPoint, value); 
        }
        #endregion

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

        #region Команды
        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecuted(object p) => true;


        public ICommand ChangeTabIndexCommand { get; }

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }
        private bool CanChangeTabIndexCommandExecuted(object p) => true;

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecuted);
            
            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for( var x = 0d; x< 366; x+=0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);
                data_points.Add(new DataPoint { XValue = x, YValue = y }); 
            }
            TestDataPoint = data_points;
        }
    }


}
