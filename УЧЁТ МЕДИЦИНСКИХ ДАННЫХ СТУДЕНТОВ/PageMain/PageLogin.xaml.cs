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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void BTNlogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppConnect.modelOdb.User.FirstOrDefault(x => x.UserPasscode == PSBpass.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Ошибка входа");
                }
                else
                {
                    AccountHelpClass.Id = userObj.RoleID;
                    switch (userObj.RoleID)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать, администратор " + userObj.UserName + " " + userObj.UserPatronymic
                                + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new PageMenuAdmin());
                            break;
                        //case 2:
                            //MessageBox.Show("Добро пожаловать, " + userObj.UserName + " " + userObj.UserPatronymic
                                //+ "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                           //AppFrame.frameMain.Navigate(new PageUserProduct());
                           //break;
                        default:
                            MessageBox.Show("Ошибка входа", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка" + Ex.Message.ToString() + "Критическая ошибка приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
