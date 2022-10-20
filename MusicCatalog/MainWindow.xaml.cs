using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}
