using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Player> allPl = new List<Player>();
        private List<Player> selectedPl = new List<Player>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 2; i++)
            {
                allPl.Add(new Player(Position.Goalkeeper));
            }
            for(int i = 0; i < 6; i++)
            {
                allPl.Add(new Player(Position.Defender));
            }
            for(int i = 0; i < 6; i++)
            {
                allPl.Add(new Player(Position.Midfielder));
            }
            for(int i = 0; i < 4; i++)
            {
                allPl.Add(new Player(Position.Forward));
            }

            allPl.Sort();

            formations.ItemsSource = new string[] { "4-4-2", "4-3-3", "4-5-1" };
            players.ItemsSource = allPl;
            selectedPlayers.ItemsSource = selectedPl;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (players.SelectedItem == null)
            {
                return;
            }
            if (selectedPl.Count >= 11)
            {
                errorLbl.Text = "Error: You have exceeded the maximum amount of space";
                return;
            }

            if(!PositionValidator((Player)players.SelectedItem))
            {
                return;
            }
            errorLbl.Text = "";
            selectedPl.Add((Player)players.SelectedItem);
            allPl.Remove((Player)players.SelectedItem);

            selectedPl.Sort();

            players.ItemsSource = null;
            players.ItemsSource = allPl;

            selectedPlayers.ItemsSource = null;
            selectedPlayers.ItemsSource = selectedPl;

            
            spacesLbl.Text = $"Spaces Available: {11 - selectedPl.Count}";
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(selectedPlayers.SelectedItem == null)
            {
                return;
            }
            errorLbl.Text = "";
            allPl.Add((Player)selectedPlayers.SelectedItem);
            selectedPl.Remove((Player)selectedPlayers.SelectedItem);
            spacesLbl.Text = $"Spaces Available: {11 - selectedPl.Count}";

            allPl.Sort();

            players.ItemsSource = null;
            players.ItemsSource = allPl;

            selectedPlayers.ItemsSource = null;
            selectedPlayers.ItemsSource = selectedPl;
        }

        private bool PositionValidator(Player player)
        {
            if(formations.SelectedItem == null)
            {
                errorLbl.Text = "Error: You must choose a formation";
                return false;
            }

            string[] positions = formations.SelectedItem.ToString().Split('-');

            int maxInPos = 0;
            Position pos = player.PreferredPosition;
            switch(pos)
            {
                case Position.Goalkeeper:
                    maxInPos = 1;
                    break;
                case Position.Defender:
                    maxInPos = Int32.Parse(positions[0]);
                    break;
                case Position.Midfielder:
                    maxInPos = Int32.Parse(positions[1]);
                    break;
                case Position.Forward:
                    maxInPos = Int32.Parse(positions[2]);
                    break;
                default:
                    return false;
            }

            int counter = 0;
            foreach(Player pl in selectedPl)
            {
                if(pl.PreferredPosition == pos)
                {
                    counter++;
                }
            }

            if(counter >= maxInPos)
            {
                errorLbl.Text = "Error: Too many people in that position";
                return false;
            }

            return true;
        }
    }
}
