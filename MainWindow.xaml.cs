using System.Runtime.CompilerServices;
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

namespace Vokabeltrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Vokabelverwaltung vokabelverwaltung;
        private VocabularyTrainer trainer;
        public DeckClass Deck;
        public bool current_revealed;
        public bool sleeping = false;
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.Deck = new();
            this.Deck.Load("vokabeln.json");
        }

        private void Button_Verwalten_Click(object sender, RoutedEventArgs e)
        {
            this.vokabelverwaltung = new(Deck);
            this.vokabelverwaltung.ShowDialog();
            this.trainer = new VocabularyTrainer(Canvas_Text, this.Deck, textBox, progressBar);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsActive)
                return;

            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null) return;

            ComboBox mainComboBox = ComboBox_Ausgabe;
            ComboBox secondaryComboBox = ComboBox_Eingabe;

            if (comboBox.Name == ComboBox_Eingabe.Name)
            {
                mainComboBox = ComboBox_Eingabe;
                secondaryComboBox = ComboBox_Ausgabe;
            }

            if (mainComboBox.SelectedItem != null && secondaryComboBox.SelectedItem != null)
            {
                string mainVal = mainComboBox.SelectedItem.ToString().Split(":")[1];
                string secondaryVal = secondaryComboBox.SelectedItem.ToString().Split(":")[1];

                if (mainVal.Equals(secondaryVal) && e.RemovedItems.Count > 0)
                {
                    secondaryComboBox.SelectionChanged -= ComboBox_SelectionChanged;

                    int i = 0;
                    string? comboBoxString = ((ComboBoxItem)e.RemovedItems[0]).Content.ToString();

                    foreach (ComboBoxItem item in secondaryComboBox.Items)
                    {
                        if (item.Content.ToString() == comboBoxString)
                        {
                            secondaryComboBox.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }

                    secondaryComboBox.SelectionChanged += ComboBox_SelectionChanged;
                }
                this.Deck.Load("vokabeln.json");
                this.trainer = new VocabularyTrainer(Canvas_Text, this.Deck, textBox, progressBar);
                this.trainer.DrawCurrent(ComboBox_Ausgabe.Text);
            }
        }

        private async Task Reveal()
        {
            this.sleeping = true;
            this.trainer.CheckCorrect(textBox.Text, ComboBox_Eingabe.Text);
            await Task.Delay(1000);
            this.sleeping = false;
        }
        private async void Button_Nächste_Click(object sender, RoutedEventArgs e)
        {
            // Chatgpt hat mir gesagt async anstatt thread zu benutzen.
            if (sleeping)
                return;

            if (!current_revealed)
            {
                await this.Reveal();
            }
            this.trainer.NextWord();
            if (this.trainer.finished)
                return;

            Dictionary<string, Action> dict_reveals = new() {
                { "DE", this.trainer.ShowGerman },
                {"EN", this.trainer.ShowEnglish },
                {"FR", this.trainer.ShowFrench },
            };
            dict_reveals[ComboBox_Ausgabe.Text]();
            this.current_revealed = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.trainer = new VocabularyTrainer(Canvas_Text, this.Deck, textBox, progressBar);
            this.trainer.DrawCurrent(ComboBox_Ausgabe.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.sleeping || this.current_revealed)
                return;
            await this.Reveal();
            this.current_revealed = true;
        }
    }
}