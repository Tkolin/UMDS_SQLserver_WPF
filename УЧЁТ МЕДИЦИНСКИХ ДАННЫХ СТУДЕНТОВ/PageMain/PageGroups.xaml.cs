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
    /// Логика взаимодействия для PageGroups.xaml
    /// </summary>
    public partial class PageGroups : Page
    {
        public PageGroups()
        {
            InitializeComponent();
            DGGroup.ItemsSource = AppConnect.modelOdb.Group.ToList();
            CMBSpec.SelectedValuePath = "SpecialityID";
            CMBSpec.DisplayMemberPath = "SpecialityName";
            CMBSpec.ItemsSource = AppConnect.modelOdb.Speciality.ToList();
            DataGridUpdate();
        }
        private void DataGridUpdate()
        {
            DGGroup.ItemsSource = AppConnect.modelOdb.Group.ToList();
        }

        private void CMBSpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int spec = Convert.ToInt32(CMBSpec.SelectedIndex);
            DGGroup.ItemsSource = AppConnect.modelOdb.Group.Where(x => x.SpecialityID == spec + 1).ToList();
        }

        private void TXBCourse_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TXBCourse.Text.Length > 0)
            {
                int crs = Convert.ToInt32(TXBCourse.Text);
                int spec = Convert.ToInt32(CMBSpec.SelectedIndex);
                DGGroup.ItemsSource = AppConnect.modelOdb.Group.Where(x => x.Course.Equals(crs) && x.SpecialityID == spec + 1).ToList();
            }
            else
            {
                DataGridUpdate();
            }
        }

        private void BTNStud_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageGroupStuds((sender as Button).DataContext as Group));
        }
    }
}
