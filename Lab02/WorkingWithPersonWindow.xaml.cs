using System;
using Lab02.ViewModel;
using System.Windows;
using Lab02.Model;

namespace Lab02
{
    public partial class WorkingWithPersonWindow : Window
    {
        public WorkingWithPersonWindow()
        {
            WorkingWithPerson w = new WorkingWithPerson();
            DataContext = w;
            if(w.CloseWindow==null)
                w.CloseWindow = new Action(this.Close);
            InitializeComponent();
        }
        public WorkingWithPersonWindow(Person  p)
        {
            InitializeComponent();
            WorkingWithPerson w = new WorkingWithPerson(p);
            if(w.CloseWindow==null)
                w.CloseWindow = new Action(this.Close);
            DataContext = w;
        }
    }
}