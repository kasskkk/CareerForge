# CARRERFORGE

## Opis projektu

CarrerForge to aplikacja internetowa umożliwiająca użytkownikom przeglądanie ofert pracy, aplikowanie na wybrane stanowiska oraz zarządzanie swoimi aplikacjami. Aplikacja jest zbudowana przy użyciu technologii - ASP.NET Core MVC oraz Entity Framework Core.

## Funkcjonalności

- Przeglądanie zatwierdzonych ofert pracy
- Reportowanie zgłoszeń
- Tworzenie nowych ofert pracy (dla administratorów i pracodawców)
- Usuwanie ofert pracy (dla administratorów)
- Przeglądanie własnych ofert pracy (dla pracodawców)
- Aplikowanie na oferty pracy (dla poszukujących pracy)
- Zarządzanie aplikacjami użytkowników (dla administratorów i pracodawców) wyświetlanie aplikantów

## Technologie

- ASP.NET Core MVC
- Entity Framework Core
- Microsoft Identity
- MS SQL

## Instalacja

1. Sklonuj repozytorium:
https://github.com/kasskkk/CareerForge
2. Skonfiguruj swoje połączenie w `appsettings.json`
3. Zaktualizuj bazę danych:
database update 
4. Uruchom aplikację:
Dostępni seedowani użytkownicy:
- Rola - Admin | Login - admin@test.com Passw - Admin!123
- Rola - Employer | Login - employer@test.com Passw - Employer!123
- Rola - Jobseeker | Login - jobseeker@test.com Passw - Jobseeker!123

## Konfiguracja

### Plik `appsettings.json`

Upewnij się, że plik `appsettings.json` zawiera poprawne ustawienia połączenia z bazą danych:

## Struktura projektu

- `Application/` - Logika aplikacji i serwisy
- `Domain/` - Modele domenowe, interfejsy
- `Infrastructure/` - Dostęp do danych i konfiguracja bazy danych
- `UI/` - Warstwa prezentacji (kontrolery, widoki, viewModele)

## Przykładowe scenariusze użycia

### Tworzenie nowej oferty pracy

1. Zaloguj się jako administrator lub pracodawca.
2. Login- employer@test.com Passw- Employer!123
3. Przejdź do sekcji "Create Job Post".
4. Wypełnij formularz i kliknij "Save".

### Aplikowanie na ofertę pracy

1. Zaloguj się jako poszukujący pracy.
2. Login- jobseeker@test.com Passw- Jobseeker!123
3. Wybierz ofertę pracy i kliknij "Apply".
4. Jeśli nie masz jeszcze dodanych informacji o sobie, zostaniesz przekierowany do strony, gdzie możesz je dodać (zdjęcie CV).

### Odczytywanie zgłoszen postów

1. Zaloguj się jako administrator.
2. Login- admin@test.com Passw- Admin!123
3. Przejdź do sekcji "Reported Posts".
4. Przeglądaj zgłoszenia (jeśli jobseeker je dodał).
   
## Autorzy

- https://github.com/kasskkk

## Licencja

Ten projekt jest licencjonowany na warunkach licencji MIT.
