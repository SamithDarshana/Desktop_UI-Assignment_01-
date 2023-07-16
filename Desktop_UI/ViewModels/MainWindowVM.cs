using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Desktop_UI.Views;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Desktop_UI.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Desktop_UI.ViewModels
{
    public partial class MainWindowVM : ObservableObject 
    {
        [ObservableProperty]
        public ObservableCollection<Student> students;

        [ObservableProperty]
        public Student selectedStudent;

        public MainWindowVM()
        {
            Students = new ObservableCollection<Student>();
        }

        [RelayCommand]
        public void AddStudent()
        {
            var viewModel = new AddStudentWindowVM();
            var window = new AddStudentWindow
            {
                Title = "Add Student",
                DataContext = viewModel,
            };

            if (window.ShowDialog() == true)
            {
                var student = new Student
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Image = viewModel.Image,
                    DateofBirth = viewModel.DateOfBirth,
                    GPA = viewModel.GPA
                };

                if (student.FirstName != null && student.LastName != null)
                {
                    Students.Add(student);
                }
            }
        }

        [RelayCommand]
        public void EditStudent()
        {
            if (SelectedStudent == null)
            {
                return;
            }
            else
            {
                var viewModel = new AddStudentWindowVM
                {
                    FirstName = SelectedStudent.FirstName,
                    LastName = SelectedStudent.LastName,
                    Image = SelectedStudent.Image,
                    DateOfBirth = SelectedStudent.DateofBirth,
                    GPA = SelectedStudent.GPA
                };
                var window = new AddStudentWindow
                {
                    Title = "Edit Student",
                    DataContext = viewModel,
                };
                if (window.ShowDialog() == true)
                {
                    SelectedStudent.FirstName = viewModel.FirstName;
                    SelectedStudent.LastName = viewModel.LastName;
                    SelectedStudent.Image = viewModel.Image;
                    SelectedStudent.DateofBirth = viewModel.DateOfBirth;
                    SelectedStudent.GPA = viewModel.GPA;

                    Students = new ObservableCollection<Student>(Students);
                }
            }
        }

        [RelayCommand]
        public void DeleteStudent()
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Select the student.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {

                var dialogResult = MessageBox.Show($"Do you want to delete {SelectedStudent.FirstName} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (dialogResult == MessageBoxResult.Yes)
                    Students.Remove(SelectedStudent);
            }
        }
    }
}
