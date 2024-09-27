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

namespace kursach
{
	/// <summary>
	/// Логика взаимодействия для AddPage.xaml
	/// </summary>
	public partial class AddPage : Window
	{
		public AddPage()
		{
			InitializeComponent();


		}
		Расходы _rashod;
		KursachContext _db = new KursachContext();
		

		private void btnotm_click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btnadd_click(object sender, RoutedEventArgs e)
		{
			StringBuilder error = new StringBuilder();
			if (string.IsNullOrEmpty(tbnamerashod.Text))
			{
				error.Append("Введите название");

			}
			if (string.IsNullOrEmpty(tbsumrashod.Text))
			{
				error.Append("Введите корректные данные");

			}
			if (daterashod.SelectedDate == null)
			{
				error.Append("Выберите дату");

			}
			if (string.IsNullOrEmpty(tbkategrashod.Text))
			{
				error.Append("Введите название категории");

			}

			if (error.Length > 0)
			{
				MessageBox.Show(error.ToString());
				return;
			}


			try
			{
				if (Data.dohod == null)
				{
					_rashod.ДатаРасхода = DateOnly.FromDateTime(daterashod.SelectedDate.Value);
					_rashod.НазваниеРасхода = tbnamerashod.Text;
					_rashod.СуммаРасхода = int.Parse(tbsumrashod.Text);
					_rashod.КатегорияРасхода = tbkategrashod.Text;
					_db.Расходыs.Add(_rashod);
					_db.SaveChanges();
				}
				else
				{
					_db.SaveChanges();
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
			if (Data.rashod == null)
			{
				Title = "Добавить";
				btnadd.Content = "Добавить";
				_rashod = new Расходы();
				

			}
			else
			{
				this.Title = "Изменить";
				btnadd.Content = "Изменить";
				_rashod = _db.Расходыs.Find(Data.rashod.IdRashod);
			}
			this.DataContext = Data.rashod;
		}
	}
}
