using System;
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

//Библиотека для работы с Excel
using Microsoft.Office.Interop.Excel;

namespace УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageMenuAdmin.xaml
    /// </summary>
    public partial class PageMenuAdmin : System.Windows.Controls.Page
    {
        public PageMenuAdmin()
        {
            InitializeComponent();
            DGStudent.ItemsSource = AppConnect.modelOdb.Student.ToList();

            DataGridUpdate();
        }
        private void DataGridUpdate()
        {
            DGStudent.ItemsSource = AppConnect.modelOdb.Student.ToList();
        }

        private void BTNGr_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageGroups());
        }

        private void TXBStud_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXBStud.Text.Length > 0)
            {
                string str = TXBStud.Text;
                DGStudent.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.StudentSurname.StartsWith(str) || x.StudentName.StartsWith(str) || x.StudentPatronymic.StartsWith(str)).ToList();
            }
            else
            {
                DataGridUpdate();
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageAddStud(null));
        }

        private void DGStudent_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                Student student = (Student)e.Row.DataContext;
                if (student.FluorDiff > 0 || student.FluorDiff == null)
                {
                    e.Row.Background = new SolidColorBrush(Colors.LightGray);
                }
                else
                {
                    e.Row.Background = new SolidColorBrush(Colors.White);
                }
            }
            catch
            {

            }
        }

        private void BTNFlu_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageWarning());
        }

        private void BTNDel_Click(object sender, RoutedEventArgs e)
        {
            if (DGStudent.SelectedValue == null)
            {
                MessageBox.Show($"Студент не выбран!", "Внимание");
                return;
            }
        

            if (MessageBox.Show($"Вы уверены?", "Внимание",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try { 
                    AppConnect.modelOdb.Student.Remove(DGStudent.SelectedValue as Student);
                    AppConnect.modelOdb.SaveChanges();
                    DataGridUpdate();
                }
                catch {
                    MessageBox.Show("Ошибка", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }

        }

        private void BTNEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGStudent.SelectedValue == null)
            {
                MessageBox.Show($"Студент не выбран!", "Внимание");
                return;
            }
            NavigationService.Navigate(new PageAddStud(DGStudent.SelectedValue as Student));
        }

    }
}
