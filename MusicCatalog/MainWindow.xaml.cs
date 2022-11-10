using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace MusicCatalog
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusicCatalogEntities db = new MusicCatalogEntities();
        public MainWindow()
        {
            InitializeComponent();
            db.Albums.Load();
            db.Artists.Load();
            db.Concerts.Load();
            db.Songs.Load();
            db.GroupMembers.Load();
            artistsGrid.ItemsSource = db.Artists.Local.ToBindingList();
            membersGrid.ItemsSource = db.GroupMembers.Local.ToBindingList();
            concertsGrid.ItemsSource = db.Concerts.Local.ToBindingList();
            songsGrid.ItemsSource = db.Songs.Local.ToBindingList();
            albumsGrid.ItemsSource = db.Albums.Local.ToBindingList();
        }

        private void artistsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (artistsGrid.SelectedIndex != -1)
            {
                Artists temp = (Artists)artistsGrid.SelectedItem;
                artistNameBox.Text = temp.ArtistOrGroupName;
                careerEndPicker.SelectedDate = temp.CareerEnd;
                careerStartPicker.SelectedDate = temp.CareerBegin;
                albumsCountBox.Text = temp.AlbumsNumber.ToString();
            }
        }
        private void membersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membersGrid.SelectedIndex != -1)
            {
                GroupMembers temp = (GroupMembers)membersGrid.SelectedItem;
                memberNameBox.Text = temp.MemberName;
                memberGroupBox.Text = temp.GroupOfMemberName;

    }

        }
        private void concertsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (concertsGrid.SelectedIndex != -1)
            {
                Concerts temp = (Concerts)concertsGrid.SelectedItem;
                concertPlaceBox.Text = temp.ConcertPlace;
                concertArtistNameBox.Text = temp.NameOfArtist;
                concertDatePicker.SelectedDate = temp.ConcertDate; 
            }
        }

        private void AddArtist_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Artists temp = new Artists();
                temp.ArtistOrGroupName = artistNameBox.Text;
                temp.CareerBegin = (DateTime)careerStartPicker.SelectedDate;
                temp.CareerEnd = (DateTime)careerEndPicker.SelectedDate;
                temp.AlbumsNumber = Convert.ToInt32(albumsCountBox.Text);
                db.Artists.Add(temp);
                db.SaveChanges();
                artistsGrid.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
        }

        private void DeleteArtist(object sender, RoutedEventArgs e)
        {

            try
            {
                Artists temp = new Artists();
                temp = (Artists)artistsGrid.SelectedItem;
                db.Artists.Remove(temp);
                db.SaveChanges();
                artistsGrid.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }

        }

        private void ChangeArtist(object sender, RoutedEventArgs e)
        {
            try
            {

                Artists result = (from p in db.Artists
                                  where p.ArtistOrGroupName == artistNameBox.Text
                                  select p).SingleOrDefault();

                result.ArtistOrGroupName = artistNameBox.Text;
                result.CareerBegin = (DateTime)careerStartPicker.SelectedDate;
                result.CareerEnd = (DateTime?)careerEndPicker.SelectedDate;
                result.AlbumsNumber = Convert.ToInt32(albumsCountBox.Text);
                db.SaveChanges();
                artistsGrid.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
        }

        private void AddMember(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupMembers temp = new GroupMembers() { GroupOfMemberName = memberGroupBox.Text, MemberName = memberNameBox.Text };
                db.GroupMembers.Add(temp);
                db.SaveChanges();
                membersGrid.Items.Refresh();

            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
        }

        private void ChangeMember(object sender, RoutedEventArgs e)
        {
            try
            {

                GroupMembers result = (from p in db.GroupMembers
                                       where p.MemberName == memberNameBox.Text
                                       select p).SingleOrDefault();

                result.MemberName = memberNameBox.Text;
                result.GroupOfMemberName = memberGroupBox.Text;

                db.SaveChanges();
                membersGrid.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
        }

        private void DeleteMember(object sender, RoutedEventArgs e)
        {
            if (membersGrid.SelectedIndex != -1)
            {
                GroupMembers temp = (GroupMembers)membersGrid.SelectedItem;
                db.GroupMembers.Remove(temp);
                db.SaveChanges();
                membersGrid.Items.Refresh();
            }
        }

        private void AddConcert(object sender, RoutedEventArgs e)
        {
            Concerts temp = new Concerts() { ConcertPlace = concertPlaceBox.Text, ConcertDate = (DateTime)concertDatePicker.SelectedDate, NameOfArtist = concertArtistNameBox.Text };
            db.Concerts.Add(temp);
            db.SaveChanges();
            concertsGrid.Items.Refresh();
        }

        private void ChangeConcert(object sender, RoutedEventArgs e)
        {
            try
            {

                Concerts result = (from p in db.Concerts
                                   where p.ConcertDate == concertDatePicker.SelectedDate
                                   select p).SingleOrDefault();

                result.ConcertPlace = concertPlaceBox.Text;
                result.NameOfArtist = concertArtistNameBox.Text;

                db.SaveChanges();
                concertsGrid.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
        }

        private void DeleteConcert(object sender, RoutedEventArgs e)
        {
            if (concertsGrid.SelectedIndex != -1)
            {
                Concerts temp = (Concerts)concertsGrid.SelectedItem;
                db.Concerts.Remove(temp);
                db.SaveChanges();
                concertsGrid.Items.Refresh();
            }
        }

        private void AddSong(object sender, RoutedEventArgs e)
        {
            Songs temp = new Songs() {ArtistName = songArtistBox.Text,SongName = songNameBox.Text, SongLyrics = songTextBox.Text };
            db.Songs.Add(temp);
            db.SaveChanges();
            songsGrid.Items.Refresh();
        }
    }
}
