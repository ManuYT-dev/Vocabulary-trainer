using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vokabeltrainer
{
    public class DeckClass
    {
        public List<VocabularyEntry> vocabulary { get; private set; }

        public DeckClass()
        {
            this.vocabulary = [];
        }

        public DeckClass(List<VocabularyEntry> vocabularyEntries)
        {
            this.vocabulary = vocabularyEntries;
        }

        public void Add(int id, string de, string en, string fr)
        {
            this.vocabulary.Add(new VocabularyEntry(id, de, en, fr));
        }

        public void Remove(int id)
        {
            this.vocabulary.RemoveAll(entry => entry.ID == id);
        }

        public void Load(string path)
        {
            string json_string = File.ReadAllText(path, Encoding.UTF8);
            this.vocabulary = JsonSerializer.Deserialize<List<VocabularyEntry>>(json_string);
        }

        public void Save(string path)
        {
            string serialized = JsonSerializer.Serialize(vocabulary);
            File.WriteAllText(path, serialized);
        }

        public VocabularyEntry? GetRandom()
        {
            if (this.vocabulary.Count == 0)
                return null;
            int choice = Random.Shared.Next(0, this.vocabulary.Count); 
            return this.vocabulary[choice];
        }
    }
}