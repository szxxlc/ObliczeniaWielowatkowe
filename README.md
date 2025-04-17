# Obliczenia wielowątkowe

## Wstęp

Projekt zawiera porównanie dwóch podejść do równoległego mnożenia macierzy:
- z wykorzystaniem klasy `Parallel.For`
- z użyciem klasycznych wątków (`Thread`)

Aplikacja została przygotowana w ramach kursu **Platformy Programistyczne .NET i Java** na Politechnice Wrocławskiej.

---

## Opis zadania

Zadanie polegało na:
- stworzeniu klasy reprezentującej macierze i operację ich mnożenia,
- implementacji dwóch wariantów mnożenia macierzy:
  - równoległe z użyciem `Parallel.For`
  - równoległe z użyciem `Thread`
- przetestowaniu wydajności obu podejść przy różnych rozmiarach macierzy i liczbach wątków
- pomiarze czasu wykonania oraz przygotowaniu wykresów porównawczych

---

## Struktura projektu

- **Matrix.cs** – Klasa reprezentująca macierz, zawierająca metody do inicjalizacji, odczytu wartości oraz wypisywania na konsolę.
- **MatrixMultiplier.cs** – Klasa zawierająca dwie statyczne metody:
  - `MultiplyParallel` – implementacja z wykorzystaniem `Parallel.For`
  - `MultiplyThread` – implementacja z wykorzystaniem klasy `Thread`
- **Program.cs** – Aplikacja konsolowa służąca do:
  - przeprowadzania testów czasowych,
  - zapisu wyników do pliku `.csv`,
    
---

## Metodologia pomiaru

W celu uzyskania wiarygodnych wyników zastosowano następujące podejście:
- Różne rozmiary macierzy (250×250, 500×500, 1000×1000),
- Różne liczby wątków (od 1 do 12),
- Powtarzanie pomiaru dla danych parametrów pięciokrotnie,
- Obliczananie średniego czasu działania.

---

## Wyniki badań

### 📊 Wykres 1 – Porównanie `Parallel` vs `Thread` dla rozmiaru macierzy 250x250

<img src="https://github.com/user-attachments/assets/c5d93857-0c8d-4eda-a2f4-e31941b92977" width="650">

### 📊 Wykres 2 – Porównanie `Parallel` vs `Thread` dla rozmiaru macierzy 500x500

<img src="https://github.com/user-attachments/assets/42f5de0d-e2bc-4cd2-997d-076b68c69ecf" width="650">
<!-- ![image](https://github.com/user-attachments/assets/42f5de0d-e2bc-4cd2-997d-076b68c69ecf) -->

### 📊 Wykres 3 – Porównanie `Parallel` vs `Thread` dla rozmiaru macierzy 1000x1000

<img src="https://github.com/user-attachments/assets/a75426d1-e3e8-43d0-9dd4-42273a957b7b" width="650">
<!-- ![image](https://github.com/user-attachments/assets/a75426d1-e3e8-43d0-9dd4-42273a957b7b) -->

---

## Wnioski

- Dla mniejszych macierzy różnice między `Parallel.For` i `Thread` są nieznaczne.
- `Parallel.For` skaluje się lepiej przy większej liczbie wątków oraz większych rozmiarach macierzy.
- Klasa `Thread` daje pełną kontrolę, ale wymaga ręcznego zarządzania i synchronizacji, co zwiększa złożoność implementacji.
- `Parallel.For` zapewnia prostotę i efektywność przy standardowych operacjach równoległych.

---

## Wykorzystane technologie

- .NET 8.0
- C#
