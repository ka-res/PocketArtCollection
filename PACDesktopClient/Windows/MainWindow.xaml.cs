using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using Microsoft.Win32;
using PACDesktopClient.Models;
using PACDesktopClient.Connections;
using PACDesktopClient.Tools;
using System.Collections.Generic;
using System.Configuration;

namespace PACDesktopClient.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PieceOfArt nwPoa = new PieceOfArt();
        PieceOfArt edtPoa = new PieceOfArt();
        PieceOfArt slctdPoa = new PieceOfArt();

        int option = -1;

        public MainWindow()
        {
            InitializeComponent();

            GetPieces.GetAllPieces(collectionListView);

            Style style = new Style();
            style.Setters.Add(new Setter(BackgroundProperty, Brushes.LightGray));
            style.Setters.Add(new Setter(BorderBrushProperty, Brushes.LightGray));
            gvTmp.SetValue(GridViewColumn.HeaderContainerStyleProperty, style);

            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchImage.MouseDown += SearchImage_MouseDown;
        }

        private void SearchImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchTextBox.Text = "";
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTextBox.Text == null || searchTextBox.Text == "")
            {
                GetPieces.GetAllPieces(collectionListView);
            }

            var searchedCollection = new List<PieceOfArt>();
            var lol = collectionListView.ItemsSource.GetEnumerator();

            foreach (var item in collectionListView.ItemsSource)
            {
                if ((item as PieceOfArt).Title.ToLower().Contains(searchTextBox.Text))
                {
                    searchedCollection.Add(item as PieceOfArt);
                }
                lol.MoveNext();
            }

            collectionListView.ItemsSource = searchedCollection;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Authorization.userToken = null;
            StartUpWindow StratUpWindow = new StartUpWindow();
            StratUpWindow.Show();
            this.Close();
        }

        private void InitializeMainWindow(PieceOfArt poa)
        {
            pieceLabel.Tag = poa;
            artistLabel.Tag = poa;
            periodLabel.Tag = poa;
            descriptionLabel.Tag = poa;
            techniqueLabel.Tag = poa;
            dateLabel.Tag = poa;
            pieceImage.Tag = poa;
            editToggleButton.Tag = edtPoa;
            addPieceToggleButton.Tag = nwPoa;

            if (poa == null)
            {
                poa = new PieceOfArt()
                {
                    Id = Guid.NewGuid(),
                    Picture = null,
                    Artist = "",
                    DateOfCreation = 2017,
                    Description = "",
                    Period = "",
                    Techinque = "",
                    Title = "",
                    UsersEmail = ConfigurationManager.AppSettings["adminEmail"]
                };
            }

            if (poa.Picture != null && poa.Picture.Length > 0)
            {
                pieceImage.Visibility = Visibility.Visible;
                tipTextBlock.Visibility = Visibility.Hidden;
                pieceImage.Source = PictureTools.ImageToImageSource(PictureTools.Base64ToImage(poa.Picture));
            }
            else if (!editToggleButton.IsChecked == true && !addPieceToggleButton.IsChecked == true)
            {
                pieceImage.Visibility = Visibility.Hidden;
                tipTextBlock.Visibility = Visibility.Visible;
            }

            pieceLabel.MouseDown += PieceLabel_MouseDown;
            artistLabel.MouseDown += ArtistLabel_MouseDown;
            periodLabel.MouseDown += PeriodLabel_MouseDown;
            descriptionLabel.MouseDown += DescriptionLabel_MouseDown;
            techniqueLabel.MouseDown += TechniqueLabel_MouseDown;
            dateLabel.MouseDown += DateLabel_MouseDown;

            displayTextBlock.Text = poa.Title;
        }

        private void DateLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;
            displayTextBlock.Text = poa.DateOfCreation.ToString();

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");

            option = 6;

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.DateOfCreation == (dateLabel.Tag as PieceOfArt).DateOfCreation)
                {
                    editModeTextBox.Text = (dateLabel.Tag as PieceOfArt).DateOfCreation.ToString();
                }
                else editModeTextBox.Text = edtPoa.DateOfCreation.ToString();
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.DateOfCreation == (dateLabel.Tag as PieceOfArt).DateOfCreation)
                {
                    editModeTextBox.Text = (dateLabel.Tag as PieceOfArt).DateOfCreation.ToString();
                }
                else editModeTextBox.Text = nwPoa.DateOfCreation.ToString();
            }
        }

        private void TechniqueLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;
            displayTextBlock.Text = poa.Techinque;

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");

            option = 5;

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.Techinque == null || edtPoa.Techinque == "" || edtPoa.Techinque == (techniqueLabel.Tag as PieceOfArt).Techinque)
                {
                    editModeTextBox.Text = (techniqueLabel.Tag as PieceOfArt).Techinque;
                }
                else editModeTextBox.Text = edtPoa.Techinque;
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.Techinque == null || nwPoa.Techinque == "" || nwPoa.Techinque == (techniqueLabel.Tag as PieceOfArt).Techinque)
                {
                    editModeTextBox.Text = (techniqueLabel.Tag as PieceOfArt).Techinque;
                }
                else editModeTextBox.Text = nwPoa.Techinque;
            }
        }

        private void DescriptionLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;
            displayTextBlock.Text = poa.Description;

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");

            option = 4;

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.Description == null || edtPoa.Description == "" || edtPoa.Description == (descriptionLabel.Tag as PieceOfArt).Description)
                {
                    editModeTextBox.Text = (descriptionLabel.Tag as PieceOfArt).Description;
                }
                else editModeTextBox.Text = edtPoa.Description;
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.Description == null || nwPoa.Description == "" || nwPoa.Description == (descriptionLabel.Tag as PieceOfArt).Description)
                {
                    editModeTextBox.Text = (descriptionLabel.Tag as PieceOfArt).Description;
                }
                else editModeTextBox.Text = nwPoa.Description;
            }
        }

        private void PeriodLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;
            displayTextBlock.Text = poa.Period.ToString();

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");

            option = 3;

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.Period == null || edtPoa.Period == "" || edtPoa.Period == (periodLabel.Tag as PieceOfArt).Period)
                {
                    editModeTextBox.Text = (periodLabel.Tag as PieceOfArt).Period;
                }
                else editModeTextBox.Text = edtPoa.Period;
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.Period == null || nwPoa.Period == "" || nwPoa.Period == (periodLabel.Tag as PieceOfArt).Period)
                {
                    editModeTextBox.Text = (periodLabel.Tag as PieceOfArt).Period;
                }
                else editModeTextBox.Text = nwPoa.Period;
            }
        }

        private void ArtistLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;
            if (poa != null)
            {
                displayTextBlock.Text = poa.Artist;
            }
            else
            {
                displayTextBlock.Text = "";
            }

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");

            option = 2;

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.Artist == null || edtPoa.Artist == "" || edtPoa.Artist == (artistLabel.Tag as PieceOfArt).Artist)
                {
                    editModeTextBox.Text = (artistLabel.Tag as PieceOfArt).Artist;
                }
                else editModeTextBox.Text = edtPoa.Artist;
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.Artist == null || nwPoa.Artist == "" || nwPoa.Artist == (artistLabel.Tag as PieceOfArt).Artist)
                {
                    editModeTextBox.Text = (artistLabel.Tag as PieceOfArt).Artist;
                }
                else editModeTextBox.Text = nwPoa.Artist;
            }
        }

        private void PieceLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceOfArt poa = (sender as Label).Tag as PieceOfArt;

            if (poa != null)
            {
                displayTextBlock.Text = poa.Title;
            }
            else
            {
                displayTextBlock.Text = "";
            }

            pieceLabel.Foreground = PictureTools.ConvertRGBToBrush("#333333");
            artistLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            periodLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            descriptionLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            techniqueLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");
            dateLabel.Foreground = PictureTools.ConvertRGBToBrush("#aaaaaa");

            var collectionItems = collectionListView.ItemsSource;
            int counterResult = 0;
            bool thatsRight = false;

            option = 1;

            if (editToggleButton.IsChecked == true)
            {
                foreach (PieceOfArt pieceItem in collectionItems)
                {
                    if (edtPoa.Title == pieceItem.Title)
                    {
                        counterResult++;
                    }
                    else
                    {
                        Debug.WriteLine("It's doubled, dude!");
                    }
                }

                if (counterResult > 0) thatsRight = true;
                else thatsRight = true;

                if (thatsRight)
                {
                    if (edtPoa.Title == null || edtPoa.Title == "" || edtPoa.Title == (pieceLabel.Tag as PieceOfArt).Title)
                    {
                        editModeTextBox.Text = (pieceLabel.Tag as PieceOfArt).Title;
                    }
                    else editModeTextBox.Text = edtPoa.Title;
                }
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                foreach (PieceOfArt pieceItem in collectionItems)
                {
                    if (nwPoa.Title == pieceItem.Title)
                    {
                        counterResult++;
                    }
                    else
                    {
                        Debug.WriteLine("It's doubled, dude!");
                    }
                }

                if (counterResult > 0) thatsRight = true;
                else thatsRight = true;

                if (thatsRight)
                {
                    if (nwPoa.Title == null || nwPoa.Title == "" || nwPoa.Title == (pieceLabel.Tag as PieceOfArt).Title)
                    {
                        editModeTextBox.Text = (pieceLabel.Tag as PieceOfArt).Title;
                    }
                    else editModeTextBox.Text = nwPoa.Title;
                }
            }
        }

        private void collectionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PieceOfArt currentPiece = (sender as ListView).SelectedItem as PieceOfArt;
            InitializeMainWindow(currentPiece);
            slctdPoa = currentPiece;
            tipTextBlock.Visibility = Visibility.Hidden;
        }

        private void EditModeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var collectionItems = collectionListView.Items;
            int counterResult = 0;
            bool thatsRight = false;

            switch (option)
            {
                case 1:
                    foreach (PieceOfArt pieceItem in collectionItems)
                    {
                        if (editModeTextBox.Text == pieceItem.Title)
                        {
                            counterResult++;
                        }
                        else
                        {
                            Debug.WriteLine("Something went wrong, dude...");
                        }
                    }

                    if (counterResult > 0) thatsRight = false;
                    else thatsRight = true;

                    if (thatsRight)
                    {
                        if (editToggleButton.IsChecked == true)
                        {
                            edtPoa.Title = editModeTextBox.Text;
                        }
                        else if (addPieceToggleButton.IsChecked == true)
                        {
                            nwPoa.Title = editModeTextBox.Text;
                        }
                    }
                    break;
                case 2:
                    if (editToggleButton.IsChecked == true)
                    {
                        edtPoa.Artist = editModeTextBox.Text;
                    }
                    else if (addPieceToggleButton.IsChecked == true)
                    {
                        nwPoa.Artist = editModeTextBox.Text;
                    }
                    break;
                case 3:
                    if (editToggleButton.IsChecked == true)
                    {
                        edtPoa.Period = editModeTextBox.Text;
                    }
                    else if (addPieceToggleButton.IsChecked == true)
                    {
                        nwPoa.Period = editModeTextBox.Text;
                    }
                    break;
                case 4:
                    if (editToggleButton.IsChecked == true)
                    {
                        edtPoa.Description = editModeTextBox.Text;
                    }
                    else if (addPieceToggleButton.IsChecked == true)
                    {
                        nwPoa.Description = editModeTextBox.Text;
                    }
                    break;
                case 5:
                    if (editToggleButton.IsChecked == true)
                    {
                        edtPoa.Techinque = editModeTextBox.Text;
                    }
                    else if (addPieceToggleButton.IsChecked == true)
                    {
                        nwPoa.Techinque = editModeTextBox.Text;
                    }
                    break;
                case 6:
                    try
                    {
                        if (editToggleButton.IsChecked == true)
                        {
                            edtPoa.DateOfCreation = Convert.ToInt32(editModeTextBox.Text);
                        }
                        else if (addPieceToggleButton.IsChecked == true)
                        {
                            nwPoa.DateOfCreation = Convert.ToInt32(editModeTextBox.Text);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie podano prawidłowej wartości liczbowej!");
                    }
                    break;

                default:
                    break;
            }
        }

        private void editToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            addPieceToggleButton.IsEnabled = false;
            removePieceToggleButton.IsEnabled = false;

            editToggleButton.Background = Brushes.Gold;
            editToggleButton.BorderBrush = Brushes.Gold;

            displayTextBlock.Visibility = Visibility.Hidden;
            editModeTextBox.Visibility = Visibility.Visible;
            acceptButton.Visibility = Visibility.Visible;

            editModeTextBox.TextChanged += EditModeTextBox_TextChanged;

            edtPoa = slctdPoa;
        }

        private void editToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            editToggleButton.Background = Brushes.DarkGray;
            editToggleButton.BorderBrush = Brushes.DarkGray;

            addPieceToggleButton.IsEnabled = true;
            removePieceToggleButton.IsEnabled = true;

            displayTextBlock.Visibility = Visibility.Visible;
            editModeTextBox.Visibility = Visibility.Hidden;
            acceptButton.Visibility = Visibility.Hidden;

            edtPoa = null;
            InitializeMainWindow(slctdPoa);
        }

        private void pieceImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string lol = "";

            if (editToggleButton.IsChecked == true)
            {
                if (edtPoa.Picture != null && edtPoa.Picture.Length > 0)
                {
                    lol = "1";
                }
                else
                {
                    lol = "0";
                }

                MessageBox.Show("Tytul: " + edtPoa.Title + ", Artysta: " + edtPoa.Artist + ", Opis:" + edtPoa.Description +
                    ", Okres: " + edtPoa.Period + ", Technika: " + edtPoa.Techinque + ", Data: " + edtPoa.DateOfCreation +
                    ", ID: " + edtPoa.Id + ", Zdjecie: " + lol);
            }
            else if (addPieceToggleButton.IsChecked == true)
            {
                if (nwPoa.Picture != null && nwPoa.Picture.Length > 0)
                {
                    lol = "1";
                }
                else
                {
                    lol = "0";
                }

                MessageBox.Show("Tytul: " + nwPoa.Title + ", Artysta: " + nwPoa.Artist + ", Opis:" + nwPoa.Description +
                    ", Okres: " + nwPoa.Period + ", Technika: " + nwPoa.Techinque + ", Data: " + nwPoa.DateOfCreation +
                    ", ID: " + nwPoa.Id + ", Zdjecie: " + lol);
            }
        }

        private void addPieceToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            editToggleButton.IsEnabled = false;
            removePieceToggleButton.IsEnabled = false;

            addPieceToggleButton.Background = Brushes.Gold;
            addPieceToggleButton.BorderBrush = Brushes.Gold;

            pieceImage.Visibility = Visibility.Hidden;
            tipTextBlock.Visibility = Visibility.Hidden;
            addPictureButton.Visibility = Visibility.Visible;

            displayTextBlock.Visibility = Visibility.Hidden;
            editModeTextBox.Visibility = Visibility.Visible;

            acceptButton.Visibility = Visibility.Visible;

            editModeTextBox.TextChanged += EditModeTextBox_TextChanged;

            nwPoa = new PieceOfArt()
            {
                Id = Guid.NewGuid(),
                Picture = null,
                Artist = "",
                DateOfCreation = 2017,
                Description = "",
                Period = "",
                Techinque = "",
                Title = ""
            };
            InitializeMainWindow(nwPoa);
        }

        private void addPieceToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            addPieceToggleButton.Background = Brushes.LightGray;
            addPieceToggleButton.BorderBrush = Brushes.LightGray;

            editToggleButton.IsEnabled = true;
            removePieceToggleButton.IsEnabled = true;

            displayTextBlock.Visibility = Visibility.Visible;
            tipTextBlock.Visibility = Visibility.Hidden;
            editModeTextBox.Visibility = Visibility.Hidden;
            acceptButton.Visibility = Visibility.Hidden;
            addPictureButton.Visibility = Visibility.Hidden;
            pieceImage.Visibility = Visibility.Visible;

            nwPoa = null;
            InitializeMainWindow(slctdPoa);
        }

        private void addPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog PictureOpenFileDialog = new OpenFileDialog();
            PictureOpenFileDialog.Title = "Wybierz grafikę...";
            PictureOpenFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            if (PictureOpenFileDialog.ShowDialog() == true)
            {
                var newImage = new BitmapImage(new Uri(PictureOpenFileDialog.FileName));
                var lol = PictureTools.ImageToByte(newImage);
                var lol2 = Convert.ToBase64String(lol);
                pieceImage.Source = newImage;
                nwPoa.Picture = lol2;

                addPictureButton.Visibility = Visibility.Hidden;
                pieceImage.Visibility = Visibility.Visible;
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (addPieceToggleButton.IsChecked == true)
            {
                nwPoa.UsersEmail = Authorization.usersEmail;
                PostPiece.PostThePiece(nwPoa);
                addPieceToggleButton.IsChecked = false;
            }
            else if (editToggleButton.IsChecked == true)
            {
                nwPoa.UsersEmail = Authorization.usersEmail;
                PutPiece.PutThePiece(edtPoa);
                editToggleButton.IsChecked = false;
            }

            GetPieces.GetAllPieces(collectionListView);
        }

        private void pieceImage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (editToggleButton.IsChecked == true)
            {
                OpenFileDialog PictureOpenFileDialog = new OpenFileDialog();
                PictureOpenFileDialog.Title = "Wybierz grafikę...";
                PictureOpenFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

                if (PictureOpenFileDialog.ShowDialog() == true)
                {
                    var newImage = new BitmapImage(new Uri(PictureOpenFileDialog.FileName));
                    var lol = PictureTools.ImageToByte(newImage);
                    var lol2 = Convert.ToBase64String(lol);
                    pieceImage.Source = newImage;
                    edtPoa.Picture = lol2;
                }

                addPictureButton.Visibility = Visibility.Hidden;
                pieceImage.Visibility = Visibility.Visible;
            }
        }

        private void removePieceToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            addPieceToggleButton.IsEnabled = false;
            editToggleButton.IsEnabled = false;

            removePieceToggleButton.Background = Brushes.Gold;
            removePieceToggleButton.BorderBrush = Brushes.Gold;

            if (MessageBox.Show("Czy na pewno usunąć dzieło?", "Potwierdź wybór",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DeletePiece.RemoveThePiece(slctdPoa);
                GetPieces.GetAllPieces(collectionListView);
                tipTextBlock.Visibility = Visibility.Visible;
            }

            removePieceToggleButton.IsChecked = false;
        }

        private void removePieceToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            addPieceToggleButton.IsEnabled = true;
            editToggleButton.IsEnabled = true;

            removePieceToggleButton.Background = Brushes.Gray;
            removePieceToggleButton.BorderBrush = Brushes.Gray;
        }
    }
}