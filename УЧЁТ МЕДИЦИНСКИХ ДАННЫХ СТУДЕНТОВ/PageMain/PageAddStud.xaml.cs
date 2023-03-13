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
    /// Логика взаимодействия для PageAddStud.xaml
    /// </summary>
    public partial class PageAddStud : Page
    {
        Student studentEdit;
        public PageAddStud(Student sEdit)
        {

            this.studentEdit = sEdit;
            InitializeComponent();
            CMBGroup.SelectedValuePath = "GroupID";
            CMBGroup.DisplayMemberPath = "GroupName";
            CMBGroup.ItemsSource = AppConnect.modelOdb.Group.ToList();
            CMBStat.SelectedValuePath = "StatusID";
            CMBStat.DisplayMemberPath = "StatusName";
            CMBStat.ItemsSource = AppConnect.modelOdb.Status.ToList();
            CMBGend.SelectedValuePath = "GenderID";
            CMBGend.DisplayMemberPath = "GenderName";
            CMBGend.ItemsSource = AppConnect.modelOdb.Gender.ToList();
            CMBContr.SelectedValuePath = "ContractID";
            CMBContr.DisplayMemberPath = "ContractName";
            CMBContr.ItemsSource = AppConnect.modelOdb.Contract.ToList();
            CMBDorm.SelectedValuePath = "DormID";
            CMBDorm.DisplayMemberPath = "DormName";
            CMBDorm.ItemsSource = AppConnect.modelOdb.Dorm.ToList();
            CMBGripp.SelectedValuePath = "GrippStatusID";
            CMBGripp.DisplayMemberPath = "GrippStatusName";
            CMBGripp.ItemsSource = AppConnect.modelOdb.GrippStatus.ToList();
            CMBEduF.SelectedValuePath = "EduFormID";
            CMBEduF.DisplayMemberPath = "EduFormName";
            CMBEduF.ItemsSource = AppConnect.modelOdb.EduForm.ToList();
            CMBVac.SelectedValuePath = "VacCertID";
            CMBVac.DisplayMemberPath = "VacCertName";
            CMBVac.ItemsSource = AppConnect.modelOdb.VacCert.ToList();
            DataContext = this;
            if(sEdit != null)
            {
                TXBSurn.Text = sEdit.StudentSurname;
                TXBName.Text = sEdit.StudentName;
                TXBPatr.Text = sEdit.StudentPatronymic;
                TimeBBirth.SelectedDate = sEdit.BirthDate;
                CMBGroup.SelectedItem = sEdit.Group;
                CMBStat.SelectedItem = sEdit.Status;
                TXBReg.Text = sEdit.StudentRegAddress;
                TXBFact.Text = sEdit.StudentFactAddress;
                CMBGend.SelectedItem = sEdit.Gender;
                CMBContr.SelectedItem = sEdit.Contract;
                TXBPolN.Text = sEdit.PolisNumber;
                TXBPolG.Text = sEdit.PolisGiven;
                CMBVac.SelectedItem = sEdit.VacCert;
                CMBDorm.SelectedItem = sEdit.Dorm;
                TimeBFluor.SelectedDate = sEdit.FluorDate;
                CMBGripp.SelectedItem = sEdit.GrippStatus;
                CMBEduF.SelectedItem = sEdit.EduForm;
            }

        }

        private void BTNback_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void BTNadd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Student studObj = null;
                bool editComput = true;
                if (studentEdit == null)
                {
                    studObj = new Student();
                    editComput = false;
                }
                else
                    studObj = studentEdit;


                studObj.StudentSurname = TXBSurn.Text;
                studObj.StudentName = TXBName.Text;
                studObj.StudentPatronymic = TXBPatr.Text;
                studObj.BirthDate = TimeBBirth.SelectedDate.Value;
                studObj.Group = CMBGroup.SelectedItem as Group;
                studObj.Status = CMBStat.SelectedItem as Status;
                studObj.StudentRegAddress = TXBReg.Text;
                studObj.StudentFactAddress = TXBFact.Text;
                studObj.Gender = CMBGend.SelectedItem as Gender;
                studObj.Contract = CMBContr.SelectedItem as Contract;
                studObj.EduForm = CMBEduF.SelectedItem as EduForm;
                studObj.PolisNumber = TXBPolN.Text;
                studObj.PolisGiven = TXBPolG.Text;
                studObj.VacCert = CMBVac.SelectedItem as VacCert;
                studObj.Dorm = CMBDorm.SelectedItem as Dorm;
                studObj.FluorDate = null;
                if(TimeBFluor.SelectedDate != null)
                studObj.FluorDate = TimeBFluor.SelectedDate.Value;
                studObj.GrippStatus = CMBGripp.SelectedItem as GrippStatus;


                if (!editComput)
                    AppConnect.modelOdb.Student.Add(studObj);
                AppConnect.modelOdb.SaveChanges();
                MessageBox.Show("Студент успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new PageMenuAdmin());
        }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
}
    }
}
