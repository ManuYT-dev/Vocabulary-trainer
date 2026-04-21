using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using Vokabeltrainer;

namespace Vokabeltrainer
{
    internal class VocabularyTrainer
    {
        private Canvas canvas {get; set;}
        private DeckClass _deck;
        private VocabularyEntry current_entry {get; set;}
        private int total_vocabs;
        private int correct;
        private int wrong;
        public bool finished;
        private TextBox textBox;
        private ProgressBar progressBar;
        private DeckClass deck
        {
            get
            {
                return _deck;
            }
            set
            {
                this._deck = value;
                this.total_vocabs = deck.vocabulary.Count;
                this.correct = 0;
                this.wrong = 0;
                this.progressBar.Maximum = total_vocabs;
                this.progressBar.Value = 0;
            }
        }

        public VocabularyTrainer(Canvas canvas, DeckClass deck, TextBox textBox, ProgressBar progressBar)
        {
            this.canvas = canvas;
            this.textBox = textBox;
            this.progressBar = progressBar;
            this.deck = new DeckClass(deck.vocabulary);
            this.current_entry = this.deck.GetRandom();
        }

        public void NextWord()
        {
            if (this.current_entry != null)
            {
                this.deck.Remove(this.current_entry.ID);
            }
            this.current_entry = this.deck.GetRandom();
            if (this.current_entry == null)
            {
                this.DrawText($"You got {this.correct}/{this.total_vocabs} correct!");
            }
        }

        public void ShowGerman()
        {
            if (current_entry != null)
            {
                this.DrawText(current_entry.DE);
            }
        }

        public void ShowEnglish()
        {
            if (current_entry != null)
            {
                this.DrawText(current_entry.EN);
            }
        }
        public void ShowFrench()
        {
            if (current_entry != null)
            {
                this.DrawText(current_entry.FR);
            }
        }

        public void DrawCurrent(string Language)
        {
            this.DrawText(this.current_entry.GetAttr(Language));
        }

        // Chatpgt Start
        public void UpdateProgressColor()
        {
            int answered = correct + wrong;
            if (answered == 0)
                return;

            double progress = (double)correct / answered;
            byte r, g;

            if (progress < 0.5)
            {
                r = 255;
                g = (byte)(progress * 2 * 255);
            }
            else
            {
                r = (byte)((1 - progress) * 2 * 255);
                g = 255;
            }

            progressBar.Foreground = new SolidColorBrush(Color.FromRgb(r, g, 0));
            this.progressBar.Value += 1;
        }
        //Chatgpt Ende

        public bool CheckCorrect(string input, string target_language)
        {
            if (input.Equals(this.current_entry.GetAttr(target_language)))
            {
                this.correct += 1;
                this.DrawText("Correct!", Brushes.LightGreen);
                this.UpdateProgressColor();
                return true;
            }
            this.wrong += 1;
            this.DrawText("Wrong!", Brushes.Red);
            this.UpdateProgressColor();
            return false;
        }

        public void DrawText(string text, Brush color = null)
        {
            this.canvas.Children.Clear();
            Label label = new Label();
            label.Content = text;
            label.FontWeight = FontWeights.Bold;
            label.FontSize = 24;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Height = canvas.Height;
            label.Width = canvas.Width;
            if (color != null)
                label.Foreground = color;
            this.canvas.Children.Add(label);
        }
    }
}
