using kursach.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadDBInDataGrid();
		}

		KursachContext _db = new KursachContext();
		Info inf;
		

		void LoadDBInDataGrid()
		{


			//foreach (var дохід in _db.Доходыs)
			//{
			//	foreach (var расход in _db.Расходыs.Where(r => r.ДатаРасхода == дохід.ДатаДохода))
			//	{
			//		inf = new Info
			//		{
						
			//			IdDohod = дохід.IdDohod,
			//			IdRashod = расход.IdRashod,
			//			IdDohodNavigation = дохід,
			//			IdRashodNavigation = расход,
			//			Raznisa = дохід.СуммаДохода - расход.СуммаРасхода

			//		};
			//	}

			//}
			//if (inf != null)
			//{

			//	_db.Infos.Add(inf);
			//	_db.SaveChanges();

			//}
			_db.Доходыs.Load();
			_db.Расходыs.Load();
			_db.Infos.Load();
			dg3.ItemsSource = _db.Infos.ToList();
			dg1.ItemsSource = _db.Доходыs.ToList();
			dg2.ItemsSource = _db.Расходыs.ToList();

		}

		private void add_click(object sender, RoutedEventArgs e)
		{
			Data.dohod = null;

			Page.AddPage2 f = new Page.AddPage2();
			f.Owner = this;
			f.ShowDialog();
			LoadDBInDataGrid();
		}

		private void izm_click(object sender, RoutedEventArgs e)
		{
			if (dg1.SelectedItem != null)
			{
				Data.dohod = (Доходы)dg1.SelectedItem;
				Page.AddPage2 f = new Page.AddPage2();
				f.Owner = this;
				f.ShowDialog();
				LoadDBInDataGrid();
			}
		}

		private void delete_click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;
			result = MessageBox.Show("Точно удалить?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				try
				{
					Доходы row = (Доходы)dg1.SelectedItem;
					if (row != null)
					{
						using (KursachContext _db = new KursachContext())
						{
							_db.Доходыs.Remove(row);
							_db.SaveChanges();
						}
						LoadDBInDataGrid();
					}
				}
				catch
				{

					MessageBox.Show("Ошибка удаления");
				}
			}
			else
			{
				dg1.Focus();
			}

			LoadDBInDataGrid();

		}

		private void filtr_click(object sender, RoutedEventArgs e)
		{
			if (filtrtb.Text.IsNullOrEmpty() == false)
			{

				var filtered = _db.Доходыs.Where(p => p.НазваниеДохода ==
				filtrtb.Text);
				dg1.ItemsSource = filtered.ToList();

			}
		}

		private void poisk_click(object sender, RoutedEventArgs e)
		{
			List<Доходы> list = (List<Доходы>)dg1.ItemsSource;
			var filtered = list.Where(p => p.КатегорияДохода.Contains(poisktb.Text));
			if (filtered.Count() > 0)
			{
				var item = filtered.First();
				dg1.SelectedItem = item;
				dg1.ScrollIntoView(item);
			}
		}

		private void add2_click(object sender, RoutedEventArgs e)
		{
			Data.rashod = null;
			AddPage f = new AddPage();
			f.Owner = this;
			f.ShowDialog();
			LoadDBInDataGrid();
		}
		private void izm2_click(object sender, RoutedEventArgs e)
		{
			if (dg2.SelectedItem != null)
			{
				Data.rashod = (Расходы)dg2.SelectedItem;
				AddPage f = new AddPage();
				f.Owner = this;
				f.ShowDialog();
				LoadDBInDataGrid();
			}
		}

		private void delete2_click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;
			result = MessageBox.Show("Точно удалить?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				try
				{
					Расходы row = (Расходы)dg2.SelectedItem;
					if (row != null)
					{
						using (KursachContext _db = new KursachContext())
						{
							_db.Расходыs.Remove(row);
							_db.SaveChanges();
						}
						LoadDBInDataGrid();
					}
				}
				catch
				{

					MessageBox.Show("Ошибка удаления");
				}
			}
			else
			{
				dg2.Focus();
			}

			LoadDBInDataGrid();
		}

		private void filtr2_click(object sender, RoutedEventArgs e)
		{
			if (filtrtb2.Text.IsNullOrEmpty() == false)
			{

				var filtered = _db.Расходыs.Where(p => p.НазваниеРасхода ==
				filtrtb2.Text);
				dg2.ItemsSource = filtered.ToList();

			}
		}

		private void poisk2_click(object sender, RoutedEventArgs e)
		{
			List<Расходы> list = (List<Расходы>)dg2.ItemsSource;
			var filtered = list.Where(p => p.КатегорияРасхода.Contains(poisktb2.Text));
			if (filtered.Count() > 0)
			{
				var item = filtered.First();
				dg2.SelectedItem = item;
				dg2.ScrollIntoView(item);
			}
		}

		private void filtr3_click(object sender, RoutedEventArgs e)
		{
			if (filtrtb3.Text.IsNullOrEmpty() == false)
			{

				var filtered = _db.Infos.Where(p => p.Raznisa ==
				int.Parse(filtrtb3.Text));
				dg3.ItemsSource = filtered.ToList();

			}
		}

		private void poisk3_click(object sender, RoutedEventArgs e)
		{
			//List<Info> list = (List<Info>)dg3.ItemsSource;
			//var filtered = list.Where(p => p.Raznisa.Contains(poisktb2.Text));
			//if (filtered.Count() > 0)
			//{
			//	var item = filtered.First();
			//	dg2.SelectedItem = item;
			//	dg2.ScrollIntoView(item);
			//}
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			Page.Logiin f = new Page.Logiin();
			f.ShowDialog();
			if (Log.Login == false) Close();
			if (Log.Right == "Польз     ")
			{

			}
			if (Log.Right == "Клиент")
			{

			}
			Title = Log.UserSurName + " " + Log.UserName + "(" +
				Log.Right + ")";
		}

		private void delete3_click(object sender, RoutedEventArgs e)
		{
			//MessageBoxResult result;
			//result = MessageBox.Show("Точно удалить?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			
				try
				{
					Info row = (Info)dg3.SelectedItem;
					if (row != null)
					{
						using (KursachContext _db = new KursachContext())
						{
							_db.Infos.Remove(row);
							_db.SaveChanges();
						}
						LoadDBInDataGrid();
					}
				}
				catch
				{

					MessageBox.Show("Ошибка удаления");
				}
			LoadDBInDataGrid();
		}
	}
}