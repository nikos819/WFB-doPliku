# Ankiety – aplikacja do przeprowadzania ankiet

## Opis projektu
Projekt jest aplikacją okienkową napisaną w języku C# z wykorzystaniem technologii WPF (Windows Presentation Foundation). Aplikacja umożliwia zbieranie odpowiedzi ankietowych oraz automatyczne generowanie statystyk na podstawie zapisanych danych.

## Funkcjonalności
- Wprowadzanie wieku użytkownika.
- Wybór płci: mężczyzna, kobieta lub inny.
- Wybór ulubionego koloru z listy.
- Walidacja wieku w zakresie od 1 do 100 lat.
- Blokowanie wprowadzania znaków innych niż cyfry w polu wieku.
- Wymaganie zaakceptowania zgody przed zapisaniem ankiety.
- Zapisywanie odpowiedzi do pliku `ankiety.txt`.
- Automatyczne obliczanie statystyk po zapisaniu ankiety.
- Wyświetlanie wyników w interfejsie aplikacji.
- Zapisywanie podsumowania do pliku `wyniki.txt`.
- Obsługa błędów podczas zapisywania danych.

## Obliczane statystyki
- Łączna liczba ankiet.
- Średni wiek respondentów.
- Najczęściej wybierany kolor.
- Liczba mężczyzn.
- Liczba kobiet.
- Liczba osób wybierających opcję „Inny”.

## Wykorzystane technologie
- C# – język programowania.
- WPF – tworzenie interfejsu graficznego.
- .NET – środowisko uruchomieniowe.
- LINQ – grupowanie i analiza danych.
- Regex – walidacja wprowadzanych znaków.
- System.IO – odczyt i zapis plików tekstowych.

## Pliki projektu
- `MainWindow.xaml` – interfejs graficzny aplikacji.
- `MainWindow.xaml.cs` – logika aplikacji, walidacja danych i obliczanie statystyk.
- `ankiety.txt` – plik zawierający zapisane odpowiedzi.
- `wyniki.txt` – plik zawierający podsumowanie wyników.

Pliki tekstowe są zapisywane w katalogu uruchomieniowym aplikacji.

## Format zapisu danych
Każda ankieta jest zapisywana w osobnym wierszu pliku `ankiety.txt` w formacie:

`Płeć|Wiek|Kolor`

Przykład:

`Mężczyzna|25|Niebieski`

`Kobieta|32|Zielony`

`Inny|19|Czerwony`

## Uruchomienie projektu
1. Otwórz projekt w programie Visual Studio.
2. Skompiluj projekt.
3. Uruchom aplikację.
4. Wypełnij formularz ankiety.
5. Wybierz kolor i zaakceptuj zgodę.
6. Kliknij przycisk zapisu, aby zapisać ankietę i zaktualizować statystyki.

## Cel projektu
Celem projektu jest stworzenie prostej aplikacji do przeprowadzania ankiet, zapisywania odpowiedzi w plikach tekstowych oraz automatycznej analizy zebranych danych. Projekt pozwala wykorzystać podstawowe zagadnienia programowania w języku C#, obsługi plików, walidacji danych i przetwarzania kolekcji.

## Autor
Projekt edukacyjny wykonany w języku C# z wykorzystaniem technologii WPF.
