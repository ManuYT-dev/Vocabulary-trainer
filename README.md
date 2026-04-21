# Vocabulary trainer 🌍

**Vocabulary trainer** is a modern, interactive desktop vocabulary trainer built with C# and Windows Presentation Foundation (WPF). Whether for school, university, or personal language goals—this app allows you to create custom vocabulary lists, manage them, and test your knowledge through various quiz modes.

---

## 📸 Screenshots

### Main Menu
*(The clean dashboard showing your vocabulary sets and learning statistics)*
<img width="1167" height="659" alt="image" src="https://github.com/user-attachments/assets/0533a725-0584-48e9-8ba7-44f00292a72d" />

### Add Vocabulary
*(The editor for adding new words, translations, and categories)*
<img width="1180" height="699" alt="image" src="https://github.com/user-attachments/assets/39ad8a1e-5481-4455-aeed-cacb3938f19d" />

### Results
*(Your performance summary after completing a learning session)*
<img width="1167" height="656" alt="image" src="https://github.com/user-attachments/assets/f51be9db-c06e-49d2-b271-ff2060f09712" />

---

## 🌟 Features

* **Vocabulary Management:** Easily create, edit, and organize your own custom vocabulary lists.
* **Multiple Learning Modes:** Test yourself using classic flashcards or strict text-input modes.
* **Progress Tracking:** Keep an eye on which words you have mastered and which ones need more practice.
* **Modern UI:** A clean, responsive, and user-friendly interface built with native WPF.

---

## 🚀 Getting Started

### Prerequisites
To run or modify this project, you will need:
* **Windows OS** (WPF is a Windows-only framework)
* **.NET Desktop Runtime** (Version 6.0 or newer recommended)
* **Visual Studio 2022** (For developers)

### Installation & Running

#### Option 1: Running the App (No Code)
1. Go to the [Releases](../../releases) tab on GitHub.
2. Download the latest `.zip` file.
3. Extract the folder and launch `Vokabeltrainer.exe`.

#### Option 2: Building from Source
1. Clone the repository to your local machine:
   ```bash
   git clone [https://github.com/ManuYT-dev/VocabFlow.git](https://github.com/ManuYT-dev/VocabFlow.git)
   ```
2. Open the `.sln` (Solution) file in Visual Studio.
3. Set the build configuration to `Release` or `Debug`.
4. Click **Start** (or press `F5`) to compile and launch the application.

---

## ⚙️ Developer Notes
When making adjustments to the save files (e.g., the JSON files for the vocabulary sets): always add a backslash before quotation marks when dealing with fields like `\"Count\"` or `\"Anzahl\"` to prevent escaping issues during parsing. 

Example of correct formatting: `{"Vocabulary_\"Count\"": 150}`

---

## 📄 License
This project is licensed under the **MIT License**. You are free to use, modify, and distribute the code for personal or commercial projects. See the `LICENSE` file for more details.
