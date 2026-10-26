
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace doPliku
{
    public partial class MainWindow : Window
    {
        private readonly string plikAnkiet =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ankiety.txt");

        private readonly string plikWynikow =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wyniki.txt");

        public MainWindow()
        {
            InitializeComponent();
        }

        // Tylko cyfry w polu wieku
        private void AgeTextBox_PreviewTextInput(
            object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]+$");
        }

        // Blokowanie wklejania znaków innych niż cyfry
        private void AgeTextBox_Pasting(
            object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text =
                    (string)e.DataObject.GetData(typeof(string));

                if (!Regex.IsMatch(text, @"^[0-9]+$"))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        // Zapis odpowiedzi i aktualizacja statystyk
        private void SaveButton_Click(
            object sender, RoutedEventArgs e)
        {
            // Sprawdzenie wieku
            if (!int.TryParse(AgeTextBox.Text, out int age)
                || age < 1 || age > 100)
            {
                MessageBox.Show(
                    "Wiek musi być liczbą od 1 do 100.");
                return;
            }

            // Sprawdzenie koloru
            if (ColorComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz ulubiony kolor.");
                return;
            }

            // Sprawdzenie zgody
            if (ConsentCheckBox.IsChecked != true)
            {
                MessageBox.Show("Musisz zaakceptować zgodę.");
                return;
            }

            // Pobranie płci
            string gender;

            if (MaleRadio.IsChecked == true)
                gender = "Mężczyzna";
            else if (FemaleRadio.IsChecked == true)
                gender = "Kobieta";
            else
                gender = "Inny";

            // Pobranie koloru
            string color =
                ((ComboBoxItem)ColorComboBox.SelectedItem)
                .Content.ToString();

            try
            {
                // Zapis jednej odpowiedzi do pliku
                string odpowiedz = gender + "|" + age + "|" + color;

                File.AppendAllText(
                    plikAnkiet,
                    odpowiedz + Environment.NewLine,
                    Encoding.UTF8);

                // Aktualizacja podsumowania
                AktualizujWyniki();

                MessageBox.Show(
                    "Ankieta została zapisana!",
                    "Sukces",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Nie udało się zapisać danych:\n" + ex.Message);
            }
        }

        // Obliczanie statystyk ze wszystkich odpowiedzi
        private void AktualizujWyniki()
        {
            if (!File.Exists(plikAnkiet))
                return;

            string[] linie = File.ReadAllLines(
                plikAnkiet, Encoding.UTF8);

            var ankiety = new List<(string Gender, int Age, string Color)>();

            foreach (string linia in linie)
            {
                string[] dane = linia.Split('|');

                if (dane.Length != 3)
                    continue;

                if (!int.TryParse(dane[1], out int age)
                    || age < 1 || age > 100)
                    continue;

                ankiety.Add((
                    dane[0],
                    age,
                    dane[2]
                ));
            }

            if (ankiety.Count == 0)
                return;

            // Średni wiek
            double sredniWiek = ankiety.Average(a => a.Age);

            // Najczęściej wybierany kolor
            var kolory = ankiety
                .GroupBy(a => a.Color)
                .Select(g => new
                {
                    Kolor = g.Key,
                    Liczba = g.Count()
                })
                .ToList();

            int maxWyborow = kolory.Max(k => k.Liczba);

            string najczestszeKolory = string.Join(
                ", ",
                kolory
                    .Where(k => k.Liczba == maxWyborow)
                    .Select(k => k.Kolor));

            // Liczba osób według płci
            int mezczyzni = ankiety.Count(
                a => a.Gender == "Mężczyzna");

            int kobiety = ankiety.Count(
                a => a.Gender == "Kobieta");

            int inne = ankiety.Count(
                a => a.Gender == "Inny");

            // Tworzenie podsumowania
            string wynik =
                "PODSUMOWANIE ANKIET\n" +
                "====================\n\n" +

                "Liczba ankiet: " + ankiety.Count + "\n" +

                "Średni wiek: " +
                sredniWiek.ToString("0.00",
                    CultureInfo.GetCultureInfo("pl-PL")) +
                "\n\n" +

                "Najczęściej wybierany kolor: " +
                najczestszeKolory + "\n" +

                "Liczba wyborów tego koloru: " +
                maxWyborow + "\n\n" +

                "LICZBA OSÓB\n" +
                "Mężczyźni: " + mezczyzni + "\n" +
                "Kobiety: " + kobiety + "\n" +
                "Inne: " + inne + "\n";

            // Zapis wyników od nowa po każdej ankiecie
            File.WriteAllText(
                plikWynikow, wynik, Encoding.UTF8);
        }
    }
}
