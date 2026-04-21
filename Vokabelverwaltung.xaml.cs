using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Vokabeltrainer
{
    /// <summary>
    /// Interaktionslogik für Vokabelverwaltung.xaml
    /// </summary>
    public partial class Vokabelverwaltung : Window
    {
        DeckClass deck;
        string path;

        public Vokabelverwaltung()
        {
            InitializeComponent();
            this.deck = null;
            this.path = "vokabeln.json";
        }

        public Vokabelverwaltung(DeckClass deck): this()
        {
            this.deck = deck;
            this.Load();
            DataGrid_MainGrid.Items.Refresh();
        }

        private void Button_Add_Vocab_Click(object sender, RoutedEventArgs e)
        {
            if (this.deck == null)
            {
                return;
            }

            if (this.LastVocabEmpty())
            {
                return;
            }

            int new_id = 1;
            if (this.deck.vocabulary.Count > 0)
                new_id = this.deck.vocabulary.Last().ID + 1;
            this.deck.Add(new_id, "", "", "");
            DataGrid_MainGrid.Items.Refresh();
        }

        private void Button_Remove_Vocab_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_MainGrid.SelectedIndex >= 0)
            {
                foreach (VocabularyEntry entry in DataGrid_MainGrid.SelectedItems)
                {
                    this.deck.Remove(entry.ID);
                }
                DataGrid_MainGrid.Items.Refresh();
            }
        }

        private bool LastVocabEmpty()
        {
            // True if last Vocab is empty
            VocabularyEntry last_entry = this.deck.vocabulary.Last();
            if (string.IsNullOrEmpty(last_entry.ID.ToString()) || string.IsNullOrEmpty(last_entry.DE.ToString()) ||
                string.IsNullOrEmpty(last_entry.EN.ToString()) || string.IsNullOrEmpty(last_entry.FR.ToString()))
            {
                return true;
            }
            return false;
        }

        private void Load()
        {
            if (!File.Exists(this.path))
                File.WriteAllText(this.path, "[\r\n { \"ID\": 1, \"DE\": \"Hallo\", \"EN\": \"Hello\", \"FR\": \"Bonjour\" },\r\n { \"ID\": 2, \"DE\": \"Tschüss\", \"EN\": \"Goodbye\", \"FR\": \"Au revoir\" },\r\n { \"ID\": 3, \"DE\": \"Danke\", \"EN\": \"Thank you\", \"FR\": \"Merci\" },\r\n { \"ID\": 4, \"DE\": \"Bitte\", \"EN\": \"Please\", \"FR\": \"S'il te plaît\" },\r\n { \"ID\": 5, \"DE\": \"Ja\", \"EN\": \"Yes\", \"FR\": \"Oui\" },\r\n { \"ID\": 6, \"DE\": \"Nein\", \"EN\": \"No\", \"FR\": \"Non\" },\r\n { \"ID\": 7, \"DE\": \"Mann\", \"EN\": \"Man\", \"FR\": \"Homme\" },\r\n { \"ID\": 8, \"DE\": \"FRau\", \"EN\": \"Woman\", \"FR\": \"Femme\" },\r\n { \"ID\": 9, \"DE\": \"Kind\", \"EN\": \"Child\", \"FR\": \"ENfant\" },\r\n { \"ID\": 10, \"DE\": \"Haus\", \"EN\": \"House\", \"FR\": \"Maison\" },\r\n { \"ID\": 11, \"DE\": \"Auto\", \"EN\": \"Car\", \"FR\": \"Voiture\" },\r\n { \"ID\": 12, \"DE\": \"Buch\", \"EN\": \"Book\", \"FR\": \"Livre\" },\r\n { \"ID\": 13, \"DE\": \"Schule\", \"EN\": \"School\", \"FR\": \"École\" },\r\n { \"ID\": 14, \"DE\": \"Lehrer\", \"EN\": \"Teacher\", \"FR\": \"Professeur\" },\r\n { \"ID\": 15, \"DE\": \"Schüler\", \"EN\": \"StuDENt\", \"FR\": \"Élève\" },\r\n { \"ID\": 16, \"DE\": \"FReund\", \"EN\": \"FRiENd\", \"FR\": \"Ami\" },\r\n { \"ID\": 17, \"DE\": \"Familie\", \"EN\": \"Family\", \"FR\": \"Famille\" },\r\n { \"ID\": 18, \"DE\": \"EssEN\", \"EN\": \"Food\", \"FR\": \"Nourriture\" },\r\n { \"ID\": 19, \"DE\": \"Wasser\", \"EN\": \"Water\", \"FR\": \"Eau\" },\r\n { \"ID\": 20, \"DE\": \"Zeit\", \"EN\": \"Time\", \"FR\": \"Temps\" }\r\n]\r\n");
            
            this.deck.Load(this.path);
            this.DataGrid_MainGrid.ItemsSource = this.deck.vocabulary;
            DataGrid_MainGrid.Items.Refresh();
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            this.Load();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            this.deck.Save(this.path);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.LastVocabEmpty())
            {
                this.deck.Save(this.path);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to exit? \nLast Vocab won´t be saved",
                    "Confirm Exit",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (this.deck.vocabulary.Count > 0)
                this.deck.Remove(this.deck.vocabulary.Last().ID);
            this.deck.Save(this.path);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_MainGrid.Columns[0].Width = 100;
            DataGrid_MainGrid.Columns[1].Width = 220;
            DataGrid_MainGrid.Columns[2].Width = 220;
            DataGrid_MainGrid.Columns[3].Width = 220;
        }
    }
}
