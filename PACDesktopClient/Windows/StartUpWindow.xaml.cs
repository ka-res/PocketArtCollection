using Newtonsoft.Json;
using PACDesktopClient.Windows;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using PACDesktopClient.Connections;

namespace PACDesktopClient.Windows
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBox.Visibility = Visibility.Hidden;
            loginPasswordBox.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Hidden;
            loginLabel.Visibility = Visibility.Hidden;
            passwordLabel.Visibility = Visibility.Hidden;
            registerButton.Visibility = Visibility.Hidden;

            registerTextBox.Visibility = Visibility.Visible;
            registerPassPasswordBox.Visibility = Visibility.Visible;
            registerConfirmPasswordBox.Visibility = Visibility.Visible;
            registerLabel.Visibility = Visibility.Visible;
            registerPasswordLabel.Visibility = Visibility.Visible;
            registerConfirmPasswordLabel.Visibility = Visibility.Visible;
            registerNowButton.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;
        }

        private void registerNowButton_Click(object sender, RoutedEventArgs e)
        {
            var userLogin = registerTextBox.Text;
            var userPassword = registerPassPasswordBox.Password;
            var userConfirmedPassword = registerConfirmPasswordBox.Password;

            Authorization.RegisterUser(userLogin, userPassword, userConfirmedPassword);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var userLogin = loginTextBox.Text;
            var userPassword = loginPasswordBox;

            Authorization.userToken = Authorization.GetToken(userLogin, userPassword.Password);
            Authorization.usersEmail = userLogin;
            
            if (Authorization.userToken != string.Empty)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Logowanie nie powiodło się!\r\nSpróbuj ponownie lub załóż nowe konto.");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBox.Visibility = Visibility.Visible;
            loginPasswordBox.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Visible;
            loginLabel.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            registerButton.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Hidden;

            registerTextBox.Visibility = Visibility.Hidden;
            registerPassPasswordBox.Visibility = Visibility.Hidden;
            registerConfirmPasswordBox.Visibility = Visibility.Hidden;
            registerLabel.Visibility = Visibility.Hidden;
            registerPasswordLabel.Visibility = Visibility.Hidden;
            registerConfirmPasswordLabel.Visibility = Visibility.Hidden;
            registerNowButton.Visibility = Visibility.Hidden;
        }

        private void infoLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("TYPE:\t\t Thesis\r\n" +
                "TITLE:\t\t „Pocket Art Collection” application as an example of \t\t\tREST architecture in .NET\r\n" +
                "ACADEMIC YEAR:\t 2016/2017");
        }

        private void loginPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_Click(null, null);
            }
        }

        private void registerConfirmPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                registerNowButton_Click(null, null);
            }
        }
    }
}
