using System;
using System.IO;
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

namespace ISP11_17_Petrov_Galimova_Autorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();
        bool truecapcha = false;
        int flag = 0;


        string path = @"C:/Users/WSR/source/repos/ISP11_17_Petrov_Galimova_Autorization/auto.txt";

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {

                    string[] mass = streamReader.ReadToEnd().Split(' ');
                    txt_login.Text = mass[0].Trim();
                    txt_password.Text = mass[1].Trim();

                }
            }

            for (int i = 1; i < 6; i++)
            {
                people.Add(new Person
                {
                    ID = i,
                    Name = $"User{i}",
                    Login = $"Login{i}",
                    Password = $"Passw{i}"
                }
                );
            }

        }
                  

        private void btn_signIn_Click(object sender, RoutedEventArgs e)
        {

            var user = people.Where(i => i.Login == txt_login.Text && i.Password == txt_password.Text).FirstOrDefault();

            if (user != null && truecapcha == false)
            {
              
                if (chek_save.IsChecked == true)
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        streamWriter.WriteLine(user.Login + " " + user.Password);
                    }

                }

                HelloWindow helloWindow = new HelloWindow(user);
                helloWindow.ShowDialog();
            }

            else if (user != null && truecapcha == true)
            {
                if (tb_capcha.Text == txt_capcha_enter.Text)
                {
                    HelloWindow helloWindow = new HelloWindow(user);
                    helloWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Неправильно введена Капча");
                    tb_capcha.Text = GetString.CapchaAct();
                }

            }

            else
            {
                if (flag >= 2)
                {
                    truecapcha = true;
                    btn_reload.Visibility = Visibility.Visible;
                    tb_capcha.Visibility = Visibility.Visible;
                    tb_capcha_text.Visibility = Visibility.Visible;
                    txt_capcha_enter.Visibility = Visibility.Visible;
                    tb_capcha.Text = GetString.CapchaAct();
                }
                MessageBox.Show("Пользователь не найден");
                flag += 1;
               
               
            }

        }
               

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {
            tb_capcha.Text = GetString.CapchaAct();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
