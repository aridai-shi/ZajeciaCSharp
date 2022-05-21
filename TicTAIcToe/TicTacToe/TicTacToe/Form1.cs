using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private bool player = true;
        private int[,] gameState = new int[3, 3];
        private Button[,] buttonMap = new Button[3, 3];
        private readonly Tuple<int,int> notFoundTuple = new Tuple<int,int>(4,4);
        private Tuple<int,int> winCounter = new Tuple<int, int>(0,0);
        private int moveCounter = 0;
        private int streak = 0;
        private bool lastWinner = false;
        private bool firstGame = true;
        public Form1()
        {
            InitializeComponent();
            InitGame();
        }
        private void InitGame()
        {
            if (lastWinner == player)
            {
                streak++;
            }
            else
            {
                streak = 0;
                lastWinner = player;
            }
            if (firstGame) lastWinner = player;
            string streakString = (streak > 0 && !firstGame) ? $"(STREAK:{streak})" : "";
            if (player)
            {
                winLabel.Text = $"WYGRANE:\nO:{winCounter.Item1} {streakString}  X:{winCounter.Item2}";
            }
            else
            {
                winLabel.Text = $"WYGRANE:\nO:{winCounter.Item1}  X:{winCounter.Item2} {streakString}";
            }
            historyLabel.Text = "";
            moveCounter = 0;
            resetButton.Enabled = false;
            resetButton.Visible = false;
            player = !player;
            gameState = new int[3, 3];
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    gameState[x, y] = 0;
                }
            }
            buttonMap = new Button[3, 3];
            int i = 0;
            foreach (Button button in Input.Controls)
            {
                buttonMap[i % 3, i / 3] = button;
                button.Tag = i;
                button.Text = "";
                i++;
            }
            label1.Text = "Teraz kolej gracza: " + (player ? "O" : "X");
            if (player) makeAIMove();
        }
        private void inputButtonClicked(object sender, EventArgs e)
        {
            if (player) return;
            makeMove(sender as Button);
        }
        private void makeMove(Button button)
        {
            if (endGame() || (moveCounter >= 10)) return;
            Tuple<int, int> coords = MapButton(button);
            if (coords != notFoundTuple)
            {
                if (gameState[coords.Item1, coords.Item2] != 0) return;
                gameState[coords.Item1, coords.Item2] = playerInt();
                button.Text = playerString();
            }
            historyLabel.Text += $"{playerString()}: ({coords.Item1},{coords.Item2}) \n";
            moveCounter++;
            if (endGame())
            {
                MessageBox.Show("Wygrał gracz " + playerString());
                historyLabel.Text += "Wygrał gracz " + playerString() + "\n";
                winCounter = new Tuple<int, int>(winCounter.Item1 + playerBonus(true), winCounter.Item2 + playerBonus(false));
                resetButton.Enabled = true;
                resetButton.Visible = true;
                return;
            }
            else if (moveCounter >= 9)
            {
                MessageBox.Show("REMIS");
                historyLabel.Text += "REMIS\n";
                resetButton.Enabled = true;
                resetButton.Visible = true;
                winCounter = new Tuple<int, int>(0, 0);
                return;
            }
            player = !player;
            label1.Text = "Teraz kolej gracza: " + playerString();
            if (player) makeAIMove(); // jeśli kolej komputera, daj mu zagrać
        }

        private void makeAIMove()
        {
            if (endGame() || (moveCounter >= 10)) return; // nie wykonuj ruchów po remisie
            Tuple<int, int> aiMove = AICheckMoves(); // sprawdź gdzie masz ruch do wykonania
            if (aiMove != notFoundTuple) // jeśli masz jakiś
            {
                makeMove(buttonMap[aiMove.Item1, aiMove.Item2]); // wykonaj go jako gracz 2
            }
            else // jeśli nie masz żadnego, zakończ turę bez wykonywania ruchu
            {
                if (moveCounter >= 9)
                {
                    MessageBox.Show("REMIS");
                    historyLabel.Text += "REMIS\n";
                    resetButton.Enabled = true;
                    resetButton.Visible = true;
                    return;
                }
                player = !player;
                label1.Text = "Teraz kolej gracza: " + playerString();
            }
        }

        private int playerBonus(bool check)
        {
            return Convert.ToInt32(check == player);
        }
        private string playerString()
        {
            return (player ? "O" : "X");
        }
        private int playerInt()
        {
            return player ? 1 : -1;
        }
        private bool endGame()
        {
            return (LookForSums() != notFoundTuple); // szuka pełnych linii, jak nie dostanie notFoundTuple, to znaczy że ktoś wygrał
        }
        private Tuple<int, int> AICheckMoves()
        {
            Tuple<int, int> look = LookForSums(true); // szuka prawie dokończonych linii
           if (look != notFoundTuple) // jeśli jakaś linia jest prawie dokończona (tj. albo zaraz wygra albo zaraz przegra)
           {
                return look; // zwraca kratkę gdzie jeszcze niczego nie ma
           }
           else if (gameState[1, 1] == 0) // jak nie, to sprawdza czy jest środek
            {
                return new Tuple<int, int>(1, 1); // jak jest to daje
            }
            else // jak nie, bierze pierwszą lepszą kratkę
            {
                for (int x = 0; x <= 2; x++)
                {
                    for (int y = 0; y <= 2; y++)
                    {
                        if (gameState[x, y] == 0)
                        {
                            Debug.WriteLine($"RANDOM: {x},{y}");
                            return new Tuple<int, int>(x, y);
                        }
                    }
                }
            }
            return notFoundTuple; // jak wystąpi jakiś bug gdzie nie ma żadnego ruchu do wykonania, zwraca notFoundTuple
        }
        private Tuple<int,int> LookForSums(bool ai=false) // sprawdza sumę danej linii - żeby się nie powtarzać pomiędzy funkcjami endGame() i AICheckMoves()
        {
            int sum = ai ? 2 : 3; // jeśli szukamy pod ai, szukamy linii z sumą 2, jeśli pod wygraną - 3
            int sumRow = 0;
            int sumCol = 0;
            int sumDiagL = 0;
            int sumDiagR = 0;
            Tuple<int,int> def = new Tuple<int, int>(0, 0); // defaultowa wartość dla funkcji endGame()
            for (int i = 0; i <= 2; i++)
            {
                sumRow = 0;
                sumCol = 0;
                for (int j = 0; j <= 2; j++)
                {
                    sumRow += gameState[i, j]; // sumuje rzędy i kolumny w tym samym czasie
                    sumCol += gameState[j, i];
                }
                if (Math.Abs(sumRow) == sum)
                {
                    if (!ai) return def; // jeśli nie obchodzi nas czy mamy puste kratki w tym rzędzie - np. w funkcji endGame - zwracamy wartość defaultową by cokolwiek zwrócić
                    for (int j = 0; j <= 2; j++)
                    {
                        if (gameState[i, j] == 0) // jeśli znajdzie pustą kratkę
                        {
                            return new Tuple<int, int>(i, j); // zwraca ją
                        }
                    }
                }
                if (Math.Abs(sumCol) == sum)
                {
                    if (!ai) return def;
                    for (int j = 0; j <= 2; j++)
                    {
                        if (gameState[j, i] == 0)
                        {
                            return new Tuple<int, int>(j, i);
                        }
                    }
                }
            }
            for (int i = 0; i <= 2; i++)
            {
                sumDiagL += gameState[i, 2 - i];
                sumDiagR += gameState[i, i];
            }
            if (Math.Abs(sumDiagR) == sum)
            {
                if (!ai) return def;
                for (int j = 0; j <= 2; j++)
                {
                    if (gameState[j, j] == 0)
                    {
                        return new Tuple<int, int>(j, j);
                    }
                }
            }
            else if (Math.Abs(sumDiagL) == sum)
            {
                if (!ai) return def;
                for (int j = 0; j <= 2; j++)
                {
                    if (gameState[j, 2 - j] == 0)
                    {
                        return new Tuple<int, int>(j, 2 - j);
                    }
                }
            }
            return notFoundTuple;
        }
        private void MessageGameState()
        {
            string output = "";
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    output += gameState[j,i].ToString();

                }
                output += "-";
            }
            MessageBox.Show(output);
        }
        private Tuple<int, int> MapButton(Button get)
        {
            Tuple<int, int> output = notFoundTuple;
            for (int i = 0; i <=2; i++) {
                for (int j = 0; j <= 2; j++)
                {
                    if (buttonMap[i, j] != null)
                    {
                        if (buttonMap[i, j].Tag == get.Tag)
                        {
                            output = Tuple.Create(i, j);
                        }
                    }
                }
            }
            return output;
        }

        private void resetGame(object sender, EventArgs e)
        {
            firstGame = false;
            InitGame();
        }
    }

}
