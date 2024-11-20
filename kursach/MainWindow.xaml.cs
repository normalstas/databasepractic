using kursach.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
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

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadDBInDataGrid();
		}

		KursachContext _db = new KursachContext();
		Info inf = new Info();
		int sumsberezh;
		int sumdohod;
		int sumrash;
		void LoadDBInDataGrid()
		{


			_db.SaveChanges();
			_db.Доходыs.Load();
			_db.Расходыs.Load();
			_db.Infos.Load();
			_db.Сбереженияs.Load();
			dg3.ItemsSource = _db.Infos.ToList();
			dg1.ItemsSource = _db.Доходыs.ToList();
			dg2.ItemsSource = _db.Расходыs.ToList();
			dg4.ItemsSource = _db.Сбереженияs.ToList();
		}

		private void add_click(object sender, RoutedEventArgs e)
		{
			Data.dohod = null;

			Page.AddPage2 f = new Page.AddPage2();
			f.Owner = this;
			f.ShowDialog();
			if (Data.dohnew)
			{
				if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
				{

					sumdohod = _db.Доходыs.Sum(x => x.СуммаДохода);
					inf.СуммаДохода = sumdohod;
					if (_db.Infos != null)
					{

						if (inf.СуммаРасходов == null)
						{
							sumrash = _db.Расходыs.Sum(x => x.СуммаРасхода);
							inf.СуммаРасходов = sumrash;
							_db.Infos.Add(inf);

						}

						if (inf.СуммаСбережений == null)
						{
							sumsberezh = _db.Сбереженияs.Sum(x => x.Сбережения1);
							inf.СуммаСбережений = sumsberezh;
							_db.Infos.Add(inf);

						}
					}
					_db.Infos.Add(inf);
					_db.SaveChanges();
					if (dg3.Items.Count != 0)
					{

						var entity = _db.Infos.Find(inf.Id - 1);
						_db.Infos.Remove(entity);
					}


				}

			}
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
				if (Data.dohnew)
				{

					if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
					{

						sumdohod = _db.Доходыs.Sum(x => x.СуммаДохода);
						inf.СуммаДохода = sumdohod;
						if (_db.Infos != null)
						{

							foreach (var inf in _db.Infos.ToList())
							{
								if (inf.СуммаРасходов != null)
								{
									inf.СуммаРасходов = sumrash;
								}
								if (inf.СуммаСбережений != null)
								{
									inf.СуммаСбережений = sumsberezh;
								}
							}
						}
						_db.Infos.Add(inf);
						_db.SaveChanges();
						if (dg3.Items.Count != 0)
						{

							var entity = _db.Infos.Find(inf.Id - 1);
							_db.Infos.Remove(entity);
						}

					}
				}

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
			if (Data.razhnew)
			{
				if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
				{

					sumrash = _db.Расходыs.Sum(x => x.СуммаРасхода);

					inf.СуммаРасходов = sumrash;
					if (_db.Infos != null)
					{


						if (inf.СуммаДохода == null)
						{
							sumdohod = _db.Доходыs.Sum(x => x.СуммаДохода);
							inf.СуммаДохода = sumdohod;
							_db.Infos.Add(inf);
							
						}

						if (inf.СуммаСбережений == null)
						{
							sumsberezh = _db.Сбереженияs.Sum(x => x.Сбережения1);
							inf.СуммаСбережений = sumsberezh;
							_db.Infos.Add(inf);
							
						}


					}
					_db.Infos.Add(inf);
					_db.SaveChanges();
					if (dg3.Items.Count != 0)
					{

						var entity = _db.Infos.Find(inf.Id - 1);
						_db.Infos.Remove(entity);
					}

				}

			}
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
				if (Data.razhnew)
				{
					if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
					{

						sumrash = _db.Расходыs.Sum(x => x.СуммаРасхода);
						inf.СуммаРасходов = sumrash;
						if (_db.Infos != null)
						{

							foreach (var inf in _db.Infos.ToList())
							{
								if (inf.СуммаДохода != null)
								{
									inf.СуммаДохода = sumdohod;
								}
								if (inf.СуммаСбережений != null)
								{
									inf.СуммаСбережений = sumsberezh;
								}
							}

						}
						_db.Infos.Add(inf);
						_db.SaveChanges();
						if (dg3.Items.Count != 0)
						{

							var entity = _db.Infos.Find(inf.Id - 1);
							_db.Infos.Remove(entity);
						}
					}


				}
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


		private void add4_click(object sender, RoutedEventArgs e)
		{
			Data.rashod = null;
			Page.AddPage1 f = new Page.AddPage1();
			f.Owner = this;

			f.ShowDialog();
			if (Data.sbernew)
			{
				if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
				{

					
					sumsberezh = _db.Сбереженияs.Sum(x => x.Сбережения1);
					inf.СуммаСбережений = sumsberezh;


					if (inf.СуммаДохода == null)
					{
						sumdohod = _db.Доходыs.Sum(x => x.СуммаДохода);
						inf.СуммаДохода = sumdohod;
						_db.Infos.Add(inf);

					}

					if (inf.СуммаРасходов == null)
					{
						sumrash = _db.Расходыs.Sum(x => x.СуммаРасхода);
						inf.СуммаРасходов = sumrash;
						_db.Infos.Add(inf);

					}


					_db.Infos.Add(inf);
					_db.SaveChanges();
					if (dg3.Items.Count != 0)
					{
						var entity = _db.Infos.Find(inf.Id - 1);
						_db.Infos.Remove(entity);
					}

				}
			}
			LoadDBInDataGrid();
		}
		private void izm4_click(object sender, RoutedEventArgs e)
		{
			if (dg4.SelectedItem != null)
			{
				Data.sber = (Сбережения)dg4.SelectedItem;
				Page.AddPage1 f = new Page.AddPage1();
				f.Owner = this;
				f.ShowDialog();
				if (Data.sbernew)
				{
					if (dg1.Items.Count != 0 && dg2.Items.Count != 0 && dg4.Items.Count != 0)
					{

						sumsberezh = _db.Сбереженияs.Sum(x => x.Сбережения1);
						inf.СуммаСбережений = sumsberezh;
						if (_db.Infos != null)
						{

							foreach (var inf in _db.Infos.ToList())
							{
								if (inf.СуммаДохода != null)
								{
									inf.СуммаДохода = sumdohod;
								}
								if (inf.СуммаРасходов != null)
								{
									inf.СуммаРасходов = sumrash;
								}
							}

						}
						_db.Infos.Add(inf);
						_db.SaveChanges();
						if (dg3.Items.Count != 0)
						{
							var entity = _db.Infos.Find(inf.Id - 1);
							_db.Infos.Remove(entity);
						}

					}

				}
				LoadDBInDataGrid();
			}
		}

		private void delete4_click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;
			result = MessageBox.Show("Точно удалить?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (result == MessageBoxResult.Yes)
			{
				try
				{
					Сбережения row = (Сбережения)dg4.SelectedItem;
					if (row != null)
					{
						using (KursachContext _db = new KursachContext())
						{
							_db.Сбереженияs.Remove(row);
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
				dg4.Focus();
			}

			LoadDBInDataGrid();
		}

		private void filtr4_click(object sender, RoutedEventArgs e)
		{
			if (filtrtb4.Text.IsNullOrEmpty() == false)
			{

				var filtered = _db.Сбереженияs.Where(p => p.ТипСбережения ==
				filtrtb4.Text);
				dg4.ItemsSource = filtered.ToList();

			}
		}

		private void poisk4_click(object sender, RoutedEventArgs e)
		{
			List<Сбережения> list = (List<Сбережения>)dg4.ItemsSource;
			var filtered = list.Where(p => p.НаЧто.Contains(poisktb4.Text));
			if (filtered.Count() > 0)
			{
				var item = filtered.First();
				dg4.SelectedItem = item;
				dg4.ScrollIntoView(item);
			}
		}




		private void filtr3_click(object sender, RoutedEventArgs e)
		{
			if (filtrtb3.Text.IsNullOrEmpty() == false)
			{

				var filtered = _db.Infos.Where(p => p.IdDohod ==
				int.Parse(filtrtb3.Text));
				dg3.ItemsSource = filtered.ToList();

			}
		}



		private void Window_Initialized(object sender, EventArgs e)
		{
			Page.Logiin f = new Page.Logiin();
			f.ShowDialog();
			if (Log.Login == false) Close();
			if (Log.Right == "Польз     ")
			{
				delete.IsEnabled = false;
				delete.Visibility = Visibility.Collapsed;
				izm.IsEnabled = false;
				izm.Visibility = Visibility.Collapsed;
				delete2.IsEnabled = false;
				delete2.Visibility = Visibility.Collapsed;
				izm2.IsEnabled = false;
				izm2.Visibility = Visibility.Collapsed;
				delete4.IsEnabled = false;
				delete4.Visibility = Visibility.Collapsed;
				delete3.IsEnabled = false;
				delete3.Visibility = Visibility.Collapsed;
				izm4.IsEnabled = false;
				izm4.Visibility = Visibility.Collapsed;
			}
			if (Log.Right == "Клиент")
			{
				delete.IsEnabled = false;
				delete.Visibility = Visibility.Collapsed;
				izm.IsEnabled = false;
				izm.Visibility = Visibility.Collapsed;
				add.IsEnabled = false;
				add.Visibility = Visibility.Collapsed;
				delete2.IsEnabled = false;
				delete2.Visibility = Visibility.Collapsed;
				izm2.IsEnabled = false;
				izm2.Visibility = Visibility.Collapsed;
				add2.IsEnabled = false;
				add2.Visibility = Visibility.Collapsed;
				delete3.IsEnabled = false;
				delete3.Visibility = Visibility.Collapsed;
				delete4.IsEnabled = false;
				delete4.Visibility = Visibility.Collapsed;
				izm4.IsEnabled = false;
				izm4.Visibility = Visibility.Collapsed;
				add4.IsEnabled = false;
				add4.Visibility = Visibility.Collapsed;
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