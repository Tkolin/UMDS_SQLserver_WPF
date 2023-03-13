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
    /// Логика взаимодействия для PageStud.xaml
    /// </summary>
    public partial class PageStud : Page
    {
        public PageStud(Student student)
        {
            InitializeComponent();
            TXBSurn.Text = student.StudentSurname;
            TXBName.Text = student.StudentName;
            TXBPatr.Text = student.StudentPatronymic;
            TXBBirth.Text = Convert.ToString(student.BirthDate);
            CMBGroup.SelectedValuePath = "GroupID";
            CMBGroup.DisplayMemberPath = "GroupName";
            CMBGroup.ItemsSource = AppConnect.modelOdb.Group.ToList();
            CMBGroup.SelectedIndex = student.GroupID-1;
            CMBStat.SelectedValuePath = "StatusID";
            CMBStat.DisplayMemberPath = "StatusName";
            CMBStat.ItemsSource = AppConnect.modelOdb.Status.ToList();
            CMBStat.SelectedIndex = student.StatusID-1;
            TXBReg.Text = student.StudentRegAddress;
            TXBFact.Text = student.StudentFactAddress;
            CMBGend.SelectedValuePath = "GenderID";
            CMBGend.DisplayMemberPath = "GenderName";
            CMBGend.ItemsSource = AppConnect.modelOdb.Gender.ToList();
            CMBGend.SelectedIndex = student.GenderID-1;
            CMBContr.SelectedValuePath = "ContractID";
            CMBContr.DisplayMemberPath = "ContractName";
            CMBContr.ItemsSource = AppConnect.modelOdb.Contract.ToList();
            CMBContr.SelectedIndex = student.ContractID-1;
            TXBPolN.Text = student.PolisNumber;
            TXBPolG.Text = student.PolisGiven;
            CMBVac.SelectedValuePath = "VacCertID";
            CMBVac.DisplayMemberPath = "VacCertName";
            CMBVac.ItemsSource = AppConnect.modelOdb.VacCert.ToList();
            CMBVac.SelectedIndex = student.VacCertID;
            CMBDorm.SelectedValuePath = "DormID";
            CMBDorm.DisplayMemberPath = "DormName";
            CMBDorm.ItemsSource = AppConnect.modelOdb.Dorm.ToList();
            CMBDorm.SelectedIndex = student.DormID-1;
            TXBFluor.Text = Convert.ToString(student.FluorDate);
            CMBGripp.SelectedValuePath = "GrippStatusID";
            CMBGripp.DisplayMemberPath = "GrippStatusName";
            CMBGripp.ItemsSource = AppConnect.modelOdb.GrippStatus.ToList();
            CMBGripp.SelectedIndex = student.GrippStatusID-1;
            CMBEduF.SelectedValuePath = "EduFormID";
            CMBEduF.DisplayMemberPath = "EduFormName";
            CMBEduF.ItemsSource = AppConnect.modelOdb.EduForm.ToList();
            CMBEduF.SelectedIndex = student.EduFormID - 1;
        }
        private void BTNback_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void BTNsave_Click(object sender, RoutedEventArgs e)
        {
            AppConnect.modelOdb.SaveChanges();
            MessageBox.Show("Данные успешно сохранены!");
            AppFrame.frameMain.GoBack();
        }
    }
}
