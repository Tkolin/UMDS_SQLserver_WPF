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

namespace УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageWarning.xaml
    /// </summary>
    public partial class PageWarning : Page
    {
        public PageWarning()
        {
            InitializeComponent();
            DGStudent.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.FluorDiff > 0 || x.FluorDiff == null).ToList();

            DataGridUpdate();
        }
        private void DataGridUpdate()
        {
            DGStudent.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.FluorDiff > 0 || x.FluorDiff == null).ToList();
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

        private void BTNBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
    }
}
