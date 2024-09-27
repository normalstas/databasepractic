using kursach.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Threading;

namespace kursach.Page
{
	/// <summary>
	/// Логика взаимодействия для Logiin.xaml
	/// </summary>
	public partial class Logiin : Window
	{
		public Logiin()
		{
			InitializeComponent();
		}
		
		private void Window_Activated(object sender, EventArgs e)
		{
			tbLogin.Focus();
			Log.Login = false;
			DispatcherTimer _timer = new DispatcherTimer();
			_timer.Interval = new TimeSpan(0, 0, 10);
			_timer.Tick += new EventHandler(timer_Tick);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			stackPanel.IsEnabled = true;
		}
		private void btnEnter_click(object sender, RoutedEventArgs e)
		{
			using (KursachContext _db = new KursachContext())
			{
				var user = _db.Users.Where(user => user.UserLogin ==
				tbLogin.Text && user.UserPassword == tbPas.Password);
				if (user.Count() == 1 && txtCaprcha.Text == tbCaptcha.Text)
				{
					Log.Login = true;
					Log.UserSurName = user.First().UserSurname;
					Log.UserName = user.First().UserName;
					//Log.UserPatronymic = user.First().UserPathronymic;
					_db.Roles.Load();
					Log.Right = user.First().UserRoleNavigation.RoleName;
					Close();
				}
				else
				{
					if (user.Count() == 1)
					{
						MessageBox.Show("Капча неверна!");
					}
					else
					{
						MessageBox.Show("Логин и пароль не совпадают");
					}
					GetCaptcha();

					tbLogin.Focus();
				}
			}
		}

		private void btnEsc_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btnGuest_Click(object sender, RoutedEventArgs e)
		{
			Log.Login = true;
			Log.UserSurName = "Гость";
			Log.UserName = "";
			Log.UserPatronymic = "";
			Log.Right = "Клиент";
			Close();
		}

		void GetCaptcha()
		{
			string masChar = "QWERTYUIOPLKJHGFDSAZXCVBNMmnbvcxz" +
				"lkjhgfdsa1234567890";
			string captcha = "";
			Random rnd = new Random();
			for (int i = 0; i <= 6; i++)
			{
				captcha += masChar[rnd.Next(0, masChar.Length)];
			}
			grid.Visibility = Visibility.Visible;
			txtCaprcha.Text = captcha;
			tbCaptcha.Text = null;
			txtCaprcha.LayoutTransform = new RotateTransform(rnd.Next(-15, 15));
			line.X1 = 10;
			line.Y1 = rnd.Next(10, 40);
			line.X2 = 280;
			line.Y2 = rnd.Next(10, 40);
		}
	}
}
