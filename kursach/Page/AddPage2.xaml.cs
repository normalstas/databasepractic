using kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
	
	public partial class AddPage2 : Window
	{
		public AddPage2()
		{
			InitializeComponent();
		}
		Доходы _dohod;
		KursachContext _db = new KursachContext();
		DateOnly dateOnly = new DateOnly();

		private void btnotm_click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btnadd_click(object sender, RoutedEventArgs e)
		{
			
				StringBuilder error = new StringBuilder();
				if (string.IsNullOrEmpty(tbnamedohod.Text))
				{
					error.Append("Введите название");
					
				}
				if (string.IsNullOrEmpty(tbsumdohod.Text))
				{
					
					error.Append("Введите корректные данные");
					
				}
				if (datedohod.SelectedDate == null)
				{
					error.Append("Выберите дату");
					
				}
				if (string.IsNullOrEmpty(tbkategdohod.Text))
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
					_dohod.ДатаДохода = DateOnly.FromDateTime(datedohod.SelectedDate.Value);
					_dohod.НазваниеДохода = tbnamedohod.Text;
					_dohod.СуммаДохода = int.Parse(tbsumdohod.Text);
					_dohod.КатегорияДохода = tbkategdohod.Text;
					_db.Доходыs.Add(_dohod);
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
			if (Data.dohod == null)
			{
				Title = "Добавить";
				btnadd.Content = "Добавить";
				_dohod = new Доходы();

			}
			else
			{
				this.Title = "Изменить";
				btnadd.Content = "Изменить";
				_dohod = _db.Доходыs.Find(Data.dohod.IdDohod);
			}
			this.DataContext = Data.dohod;

		}
	}
}
