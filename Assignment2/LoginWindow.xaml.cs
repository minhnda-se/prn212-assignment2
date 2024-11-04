using BusinessObjects;
using Repositories;
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
using System.Windows.Shapes;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountRepository accountRepository;
        public LoginWindow()
        {
            InitializeComponent();
            accountRepository = new AccountRepository();
            btnLogin.IsEnabled = false;
        }

        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AccountMember account = accountRepository.GetAccountById(txtAccountId.Text);
          
            if (account != null && !string.IsNullOrEmpty(txtPassword.Password) && account.MemberPassword.Equals(txtPassword.Password))
            {
                ProductWindow productWindow = new ProductWindow();
                productWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Id or Password!!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Check if the PasswordBox has a password
            var passwordBox = sender as PasswordBox;
            btnLogin.IsEnabled = !string.IsNullOrEmpty(txtPassword.Password);
        }
    }
}
