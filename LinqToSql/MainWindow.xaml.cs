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
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace LinqToSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LinqToSqlDataClassesDataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager
                .ConnectionStrings["LinqToSql.Properties.Settings.PanjutorialsDBConnectionString"].ConnectionString;

            dataContext = new LinqToSqlDataClassesDataContext(connectionString);

            //InsertUniversites();
            //RemoveUniversities();
            //InsertStudent();
            //InsertLectures();
            //InsertStudentLectureAssociations();
            //GetUniversityOfJohn();







        }

        private void RemoveUniversities()
        {
            University yale = dataContext.Universities.First(university => university.Name == "Yale");
            dataContext.Universities.DeleteOnSubmit(yale);
            dataContext.SubmitChanges();
        }

        public void InsertUniversites()
        {
            
            University yale = new University();
            yale.Name = "Yale";
            dataContext.Universities.InsertOnSubmit(yale);
            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Universities;
        }

        public void InsertStudent()
        {
            University yale = dataContext.Universities.First(university => university.Name == "Yale");
            University oxford = dataContext.Universities.First(university => university.Name == "Oxford");

            List<Student> students = new List<Student>();
            students.Add(new Student { Name = "John", Gender = "Female", UniversityId = yale.Id });
            students.Add(new Student { Name = "Peter", Gender = "Male", University = yale});
            students.Add(new Student {Name = "Sarah", Gender = "Female", University = oxford});
            students.Add(new Student { Name = "James", Gender = "Male", University = oxford });

            //dataContext.Students.InsertAllOnSubmit(students);
           // dataContext.SubmitChanges();

            //MainDataGrid.ItemsSource = dataContext.Students;

        }

        public void InsertLectures()
        {
            Lecture clinicalBiochemistry = new Lecture()
            {
                Name = "Clinical Biochemistry",
            };
            Lecture molecularBiology = new Lecture()
            {
                Name = "Molecular Biology",
            };

            Lecture organicChemistry = new Lecture()
            {
                Name = "Organic Chemistry",
            };

            Lecture quantumPhysics = new Lecture()
            {
                Name = "Quantum Physics",
            };

            List<Lecture> lectures = new List<Lecture>
            {
                clinicalBiochemistry,
                molecularBiology,
                organicChemistry,
                quantumPhysics
            };
            //dataContext.Lectures.InsertAllOnSubmit(lectures);
           // dataContext.SubmitChanges();
            //MainDataGrid.ItemsSource = dataContext.Lectures;

        }

        public void InsertStudentLectureAssociations()
        {
           Student john = dataContext.Students.First(student => student.Name == "John");
           Student peter = dataContext.Students.First(student => student.Name == "Peter");
           Student sarah = dataContext.Students.First(student => student.Name == "Sarah");
           Student james = dataContext.Students.First(student => student.Name == "James");

            Lecture clinicalBiochemistry = dataContext.Lectures.First(lecture => lecture.Name == "Clinical Biochemistry");
            Lecture molecularBiology = dataContext.Lectures.First(lecture => lecture.Name == "Molecular Biology");

            StudentLecture studentLectureCarla = new StudentLecture()
            {
                Student = john,
                Lecture = clinicalBiochemistry
            };
            StudentLecture studentLectureToni = new StudentLecture()
            {
                Student = peter,
                Lecture = molecularBiology
            };
            dataContext.StudentLectures.InsertOnSubmit(studentLectureToni);
            dataContext.StudentLectures.InsertOnSubmit(studentLectureCarla);
            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.StudentLectures;
        }

        public void GetUniversityOfJohn()
        {
            Student john = dataContext.Students.First(student => student.Name == "John");
            University johnUniversity = john.University;
            List<University> johnUniversityList = new List<University>();
            johnUniversityList.Add(johnUniversity);
            MainDataGrid.ItemsSource = johnUniversityList;
        }
    }
}


