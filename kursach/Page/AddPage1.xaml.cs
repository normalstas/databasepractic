using kursach.Models;
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

namespace kursach.Page
{
    /// <summary>
    /// Логика взаимодействия для AddPage1.xaml
    /// </summary>
    public partial class AddPage1 : Window
    {
        public AddPage1()
        {
            InitializeComponent();
        }
		Сбережения _sber;
		KursachContext _db = new KursachContext();
		private void btnotm_click(object sender, RoutedEventArgs e)
		{
			Data.sbernew = false;
			Close();
		}

		private void btnadd_click(object sender, RoutedEventArgs e)
		{

			StringBuilder error = new StringBuilder();
			if (string.IsNullOrEmpty(tbnamedohod.Text))
			{
				error.Append("Введите Тип Сбережения");

			}
			if (string.IsNullOrEmpty(tbsumdohod.Text))
			{

				error.Append("Введите Свои Сбережения");

			}
			
			if (string.IsNullOrEmpty(tbkategdohod.Text))
			{
				error.Append("Введите На Что");

			}

			if (error.Length > 0)
			{
				MessageBox.Show(error.ToString());
				return;
			}


			try
			{
				if (Data.sber == null)
				{
					_sber.ТипСбережения = tbnamedohod.Text;
					_sber.Сбережения1 = int.Parse(tbsumdohod.Text);
					_sber.НаЧто = tbkategdohod.Text;
					_db.Сбереженияs.Add(_sber);
					_db.SaveChanges();
					Data.sbernew = true;
				}
				else
				{
					_db.SaveChanges();
					Data.sbernew = true;
				}
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());

			}





		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Data.dohod == null)
			{
				Title = "Добавить";
				btnadd.Content = "Добавить";
				_sber = new Сбережения();

			}
			else
			{
				this.Title = "Изменить";
				btnadd.Content = "Изменить";
				_sber = _db.Сбереженияs.Find(Data.sber.IdSberezh);
			}
			this.DataContext = Data.dohod;

		}
	}
}
