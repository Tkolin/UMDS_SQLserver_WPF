using Microsoft.Office.Interop.Excel;
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
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
//using System.Windows.Media.Animation;
//using Page = System.Windows.Controls.Page;

namespace УЧЁТ_МЕДИЦИНСКИХ_ДАННЫХ_СТУДЕНТОВ.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageGroupStuds.xaml
    /// </summary>
    public partial class PageGroupStuds : System.Windows.Controls.Page
    {
        private object xlManual;
        Group group;
        public PageGroupStuds(Group group)
        {
            InitializeComponent();
            DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x=>x.GroupID==group.GroupID).ToList();
            this.group = group;
            LBLGr.Content = group.GroupName;
            CBOXall.IsChecked = true;
        }
        public PageGroupStuds()
        {

            InitializeComponent();
            DGGroup.ItemsSource = AppConnect.modelOdb.Student.ToList();
            this.group = null;
            LBLGr.Content = null;
            CBOXall.IsChecked = true;
        }

            private void BTNStud_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageStud((sender as System.Windows.Controls.Button).DataContext as Student));
        }

        private void DGGroup_LoadingRow(object sender, DataGridRowEventArgs e)
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


        private void BTNFluor_Click(object sender, RoutedEventArgs e)
        {
            //подключение таблиц
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            //создание страницы
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];


            //форматирование текста
            ws.Range["I1:K1"].Merge(); ws.Range["I2:K2"].Merge(); ws.Range["A3:K3"].Merge();

            ws.Range["I1"].Value = "Приложение  3		";
            ws.Range["I2"].Value = "к  приказу №__________ от ________________ " + DateTime.Now.Year.ToString() + "г.					";
            ws.Range["A3"].Value = "Отчет о прохождении ежегодного профилоктического " +
                                    "флюрогрофического осмотра студентов 18 лет и старше" +
                                    "очной формы обучения в " + (DateTime.Now.Year - 1) + "г. - " + (DateTime.Now.Year) + "г.";
            ws.Range["A6:L7"].Merge();
            ws.Range["A6"].Value = "Институт непрерывного педагогического образования" +
                                    "Колледж педагогического образования, информатики и права";
            ws.Range["A9:A10"].Merge(); ws.Range["B9:B10"].Merge(); ws.Range["C9:C10"].Merge(); ws.Range["D9:G9"].Merge();
            ws.Range["H9:H10"].Merge(); ws.Range["I9:I10"].Merge(); ws.Range["J9:J10"].Merge(); ws.Range["K9:K10"].Merge();

            ws.Range["A9"].Value = "№ акад. группы";
            ws.Range["B9"].Value = "Всего студентов в группе, по состоянию на 01.09.2022г. чел. ";
            ws.Range["C9"].Value = "Кол-во несовершеннолетних в группе, чел.";
            ws.Range["D9"].Value = "Кол-во студентов из чила студентов 18 лет и страше, чел.";

            ws.Range["D10"].Value = "академ. отпуск";
            ws.Range["E10"].Value = "отчислены/переведены на ЗФО";
            ws.Range["G10"].Value = "прошли ранее (вне графика)";
            
            
            //ws.Range["F10"].Value = "Беременные";
            ws.Range["F10"].ColumnWidth = 0;

            //ws.Range["G10"].ColumnWidth = 0;
            ws.Range["H9"].Value = "Кол-во студентов подлежащих ПФО, всего  чел.";
            ws.Range["I9"].Value = "Прошли ПФО по графику, всего чел.";
            ws.Range["J9"].Value = "Всего не прошли ПФО, чел.";
            ws.Range["K9"].Value = "Указать Ф.И.О. (полность)";
            ws.Range["A1:L10"].WrapText = true;
            ws.Range["A1:L10"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Range["A1:L10"].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            ws.StandardWidth = 13;
            ws.Range["K1"].ColumnWidth = 44.57;

            //Строки - начала и конца таблицы данных
            int startR = 11;
            int sR = 11;
            
            //Список студентов и дата по графику проходения
            List<Student> students = AppConnect.modelOdb.Student.ToList();
            DateTime dateFlurGraf = new DateTime(2015, 7, 20);//Если надо то задать в програме указание даты

            //Заполнение данных
            foreach (Group gr in AppConnect.modelOdb.Group.ToList())
            {
                List<Student> studentsForGroup = students.Where(s => s.Group == gr).ToList();

                ws.Range["A" + sR].Value = gr.GroupName;
                ws.Range["B" + sR].Value = studentsForGroup.Count();
                ws.Range["C"+ sR].Value = studentsForGroup.Where(c => c.Age < 18 ).Count();
                studentsForGroup = students.Where(s => s.Group == gr && s.Age >= 18).ToList();

                ws.Range["D" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 1).Count();
                studentsForGroup = studentsForGroup.Where(c => c.GrippStatusID != 1).ToList();
                ws.Range["E" + sR].Value = studentsForGroup.Where(c => c.EduFormID == 2).Count();
                studentsForGroup = studentsForGroup.Where(c => c.EduFormID != 2).ToList();
                ws.Range["G" +sR].Value = studentsForGroup.Where(c => c.FluorDiff == 0).Count();
                studentsForGroup = studentsForGroup.Where(c => c.FluorDiff != 0).ToList();



                ws.Range["H" + sR].Formula = "=B" + sR + "-C" + sR + "-D" + sR + "-E" + sR + "-G" + sR + "";

                foreach (Student stud in studentsForGroup)
                {
                    int count = 0;
                    if(stud.FluorDate != null)
                        if(stud.FluorDate == dateFlurGraf)
                            count++;
                    ws.Range["I" + sR].Value = count;
                }

                ws.Range["J" + sR].Formula = "=H"+sR+"-I"+sR;

                foreach (Student studentNonFlur in studentsForGroup.Where(c => c.FluorDiff > 0 && c.EduFormID == 1 && c.GrippStatusID != 1 ).ToList())
                    ws.Range["K" + sR].Value += studentNonFlur.StudentSurname + " " +
                        studentNonFlur.StudentName.ToUpper()[0] + "." + studentNonFlur.StudentPatronymic.ToUpper()[0] + ". ,";
              
                ws.Calculate();
                sR++;
            }
            //Подведение итогов
            ws.Range["A" + sR].Value = "Итог:";
            ws.Range["B" + sR].Formula = "=СУММ(B" + startR + ":" + "B" + (sR - 1) + ")";
            ws.Range["C" + sR].Formula = "=СУММ(C" + startR + ":" + "C" + (sR - 1) + ")";
            ws.Range["D" + sR].Formula = "=СУММ(D" + startR + ":" + "D" + (sR - 1) + ")";
            ws.Range["E" + sR].Formula = "=СУММ(E" + startR + ":" + "E" + (sR - 1) + ")";
            ws.Range["G" + sR].Formula = "=СУММ(G" + startR + ":" + "G" + (sR - 1) + ")";
            ws.Range["H" + sR].Formula = "=СУММ(H" + startR + ":" + "H" + (sR - 1) + ")";
            ws.Range["I" + sR].Formula = "=СУММ(I" + startR + ":" + "I" + (sR - 1) + ")";
            ws.Range["J" + sR].Formula = "=СУММ(J" + startR + ":" + "J" + (sR - 1) + ")";
            sR += 2;
            ws.Range["A" + sR + ":J" + sR].Merge(); ws.Range["A" + sR].Value = "Зам.директора по НО _________________________________Когумбаева О.П.";
            sR += 2;
            ws.Range["A" + sR + ":J" + sR].Merge(); ws.Range["A" + sR].Value = "Исполнитель _____________________________ Топоева О.Г.";
            ws.Calculate();
        }
        bool adults = true;
        private void BTNGripp_Click(object sender, RoutedEventArgs e)
        {
            //подключение таблиц
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            //создание страницы
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];


            //форматирование текста
            ws.Range["I1:l1"].Merge(); ws.Range["I2:l2"].Merge(); ws.Range["A3:L3"].Merge();

            ws.Range["I1"].Value = "Приложение  2		";
            ws.Range["I2"].Value = "к  приказу №__________ от ________________ " + DateTime.Now.Year.ToString() + "г.";
            ws.Range["A3"].Value = "Отчет о проведении иммунизации против гриппа"+
                "студентов 18лет и старше очной формы обучения в "                                                                       											
                        + (DateTime.Now.Year - 1) + "г. - " + (DateTime.Now.Year) + "г.";
            ws.Range["A6:L7"].Merge();
            ws.Range["A6"].Value = "Институт непрерывного педагогического образования " +
                                    "Колледж педагогического образования, информатики и права";
            ws.Range["A9:A10"].Merge(); ws.Range["B9:B10"].Merge(); ws.Range["C9:C10"].Merge(); ws.Range["D9:F9"].Merge();
            ws.Range["H9:H10"].Merge(); ws.Range["I9:I10"].Merge(); ws.Range["J9:J10"].Merge(); ws.Range["K9:K10"].Merge();
            ws.Range["L9:L10"].Merge(); ws.Range["G9:G10"].Merge();

            ws.Range["A9"].Value = "№ акад. группы";
            ws.Range["B9"].Value = "Всего студентов в группе, по состоянию на 01.09.2022г. чел. ";
            ws.Range["C9"].Value = "Кол-во несовершеннолетних в группе, чел.";

            ws.Range["D9"].Value = "Не подлежат вакцинации против гриппа ";
            if (adults)
                ws.Range["D9"].Value += "Старше 18";
            else
                ws.Range["D9"].Value += "Младше 18";

            ws.Range["D10"].Value = "академ. отпуск";
            ws.Range["E10"].Value = "отчислены/переведены на ЗФО";
            ws.Range["F10"].Value = "Предоставившие справку об отводе по мед. показаниям, кол-во чел.";
            //ws.Range["G10"].ColumnWidth = 0;

            ws.Range["G9"].Value = "Подлежит вакцинации кол-во чел.";
            if(adults)
                ws.Range["H9"].Value = "Кол-во студентов проставивших вакцинацию в здравпункте чел.";
            else
                ws.Range["H9"].Value = "Кол-во студентов проставивших вакцинацию в детской бол., чел.";
            ws.Range["I9"].Value = "Кол-во студентов проставивших вакцинацию по месту жительства чел.";
            ws.Range["J9"].Value = "Кол-во студентов заболевших на период вакцинации, чел.";
            ws.Range["K9"].Value = "Кол-во студентов написавших отказ от иммунизации, чел.";
            ws.Range["L9"].Value = "Всего не прошедших иммунизацию, чел";
            ws.Range["A1:L10"].WrapText = true;
            ws.Range["A1:L10"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ws.Range["A1:L10"].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            ws.StandardWidth = 13;
            ws.Range["D9"].RowHeight = 30; 
            //Строки - начала и конца таблицы данных
            int startR = 11;
            int sR = 11;

            //Список студентов и дата по графику проходения
            List<Student> students = AppConnect.modelOdb.Student.ToList();
            DateTime dateFlurGraf = new DateTime(2015, 7, 20);//Если надо то задать в програме указание даты

            //Заполнение данных
            foreach (Group gr in AppConnect.modelOdb.Group.ToList())
            {
                List<Student> studentsForGroup = students.Where(s => s.Group == gr).ToList();

                ws.Range["A" + sR].Value = gr.GroupName;
                ws.Range["B" + sR].Value = studentsForGroup.Count();
                ws.Range["C" + sR].Value = studentsForGroup.Where(c => c.Age < 18).Count();
                if(adults)
                    studentsForGroup = students.Where(s => s.Group == gr && s.Age >= 18).ToList();
                else
                    studentsForGroup = students.Where(s => s.Group == gr && s.Age < 18).ToList();

                ws.Range["D" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 1).Count();
                studentsForGroup = studentsForGroup.Where(c => c.GrippStatusID != 1).ToList();

                ws.Range["F" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 6).Count();
                studentsForGroup = studentsForGroup.Where(c => c.GrippStatusID != 6).ToList();

                ws.Range["E" + sR].Value = studentsForGroup.Where(c => c.EduFormID == 2).Count();
                studentsForGroup = studentsForGroup.Where(c => c.EduFormID != 2).ToList();
                // ws.Range["G" + sR].Value = studentsForGroup.Where(c => c.FluorDiff == 0).Count();
                if(adults)
                ws.Range["H" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 3).Count();
                else
                    ws.Range["H" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 2).Count();
                ws.Range["I" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 4).Count();
                ws.Range["J" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 7).Count();
                ws.Range["K" + sR].Value = studentsForGroup.Where(c => c.GrippStatusID == 5).Count();
                if (adults)
                    ws.Range["G" + sR].Formula = "= B" + sR + " - C" + sR + " - D" + sR + " - E" + sR + " - F" + sR;
                else
                    ws.Range["G" + sR].Formula = "= C" + sR + " - D" + sR + " - E" + sR + " - F" + sR;
               ws.Range["L" + sR].Formula = "= G" + sR + " - H" + sR + " - I" + sR;               
                ws.Calculate();
                sR++;
            }
            //Подведение итогов
            ws.Range["A" + sR].Value = "Итог:";
            ws.Range["B" + sR].Formula = "=СУММ(B" + startR + ":" + "B" + (sR -1) + ")";
            ws.Range["C" + sR].Formula = "=СУММ(C" + startR + ":" + "C" + (sR - 1) + ")";
            ws.Range["D" + sR].Formula = "=СУММ(D" + startR + ":" + "D" + (sR - 1) + ")";
            ws.Range["F" + sR].Formula = "=СУММ(F" + startR + ":" + "F" + (sR - 1) + ")";
            ws.Range["E" + sR].Formula = "=СУММ(E" + startR + ":" + "E" + (sR - 1) + ")";
            ws.Range["G" + sR].Formula = "=СУММ(G" + startR + ":" + "G" + (sR - 1) + ")";
            ws.Range["H" + sR].Formula = "=СУММ(H" + startR + ":" + "H" + (sR - 1) + ")";
            ws.Range["I" + sR].Formula = "=СУММ(I" + startR + ":" + "I" + (sR - 1) + ")";
            ws.Range["G" + sR].Formula = "=СУММ(G" + startR + ":" + "G" + (sR - 1) + ")";
            ws.Range["J" + sR].Formula = "=СУММ(J" + startR + ":" + "J" + (sR - 1) + ")";
            ws.Range["K" + sR].Formula = "=СУММ(K" + startR + ":" + "K" + (sR - 1) + ")";
            ws.Range["L" + sR].Formula = "=СУММ(L" + startR + ":" + "L" + (sR - 1) + ")";
            sR += 2;
            ws.Range["A" + sR + ":J" + sR].Merge(); ws.Range["A" + sR].Value = "Зам.директора по НО _________________________________Когумбаева О.П.";
            sR += 2;
            ws.Range["A" + sR + ":J" + sR].Merge(); ws.Range["A" + sR].Value = "Исполнитель _____________________________ Топоева О.Г.";
            ws.Calculate();
        }

        private void RBTNAbult_Checked(object sender, RoutedEventArgs e)
        {
            if (CBOXall == null)
                return;
            //Совершеннолетие    
            if (RBTNAbult.IsChecked == true)
                adults = true;
            else
                adults = false;

            if (CBOXall.IsChecked == true)
            {
                if (group == null)
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.ToList();
                else
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.GroupID == group.GroupID).ToList();
                return;
            }
            if (adults)
            {
                if (group == null)
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.Age >= 18).ToList();
                else
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.GroupID == group.GroupID).ToList();
            }
            else
            {
                if (group == null)
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.Age < 18).ToList();
                else
                    DGGroup.ItemsSource = AppConnect.modelOdb.Student.Where(x => x.GroupID == group.GroupID).ToList();
            }



        }


    }
}
