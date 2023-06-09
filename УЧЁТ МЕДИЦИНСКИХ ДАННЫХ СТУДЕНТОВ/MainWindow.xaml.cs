﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ.AppData;
using УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ.PageMain;

namespace УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new УМДСEntities();
            AppFrame.frameMain = FrmMain;

            FrmMain.Navigate(new PageGroupStuds());
        }
    }
}
