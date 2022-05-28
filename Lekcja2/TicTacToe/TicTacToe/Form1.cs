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
        private int[,] gameState = new int[gridSize, gridSize];
        private Button[,] buttonMap = new Button[gridSize, gridSize];
        private readonly Tuple<int,int> notFoundTuple = new Tuple<int,int>(gridSize+1, gridSize+1);
        private Tuple<int,int> winCounter = new Tuple<int, int>(0,0);
        private int moveCounter = 0;
        private int streak = 0;
        private bool lastWinner = false;
        private bool firstGame = true;
        private const int gridSize = 3;
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
        }
        private void inputButtonClicked(object sender, EventArgs e)
        {
            if (endGame()|| (moveCounter >= 10)) return;
            Button button = (sender as Button);
            Tuple<int, int> coords = MapButton(button);
            if (coords != notFoundTuple)
            {
                if (gameState[coords.Item1, coords.Item2] != 0) return;
                gameState[coords.Item1, coords.Item2] = playerInt();
                button.Text = playerString();
            }
            historyLabel.Text += $"{playerString()}: ({coords.Item1},{coords.Item2}) \n";
            moveCounter++;
            if (endGame()){
                MessageBox.Show("Wygrał gracz " + (player ? "1" :"2"));
                historyLabel.Text += "Wygrał gracz " + (player ? "1" : "2") + "\n";
                winCounter = new Tuple<int, int>(winCounter.Item1 + playerBonus(true), winCounter.Item2 + playerBonus(false));
                resetButton.Enabled = true;
                resetButton.Visible = true;
                return;
            }else if(moveCounter >= 9){
                MessageBox.Show("REMIS");
                historyLabel.Text += "REMIS\n";
                resetButton.Enabled = true;
                resetButton.Visible = true;
                return;
            }
            player = !player;
            label1.Text = "Teraz kolej gracza: " + playerString();

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
            return (player ? 1 : -1);
        }
        private bool endGame()
        {
            /// <summary>
            /// Jako, że stan każdej komórki jest zapisywany jako liczba od -1 do 1 (0: nic, 1: kółko, -1: krzyżyk)
            /// Kiedy zsumujemy wartości danej linii (tj. rzędu, kolumny lub skosu), suma zwycięskiej linii zawsze będzie miała albo minimalną albo maksymalną możliwą wartość
            /// Np. Suma rzędu OXO = 2, ale XXX = -3 i OOO=3
            /// Zatem wystarczy przeiterować przez wszystkie możliwe linie, aby odnaleźć sumę
            /// To rozwiązanie jest prostsze w odczycie niż ify, a także może skalować, jeśli wprowadzilibyśmy inne tryby gry (np. 4x4, 5x5, itd.)
            /// </summary>
            
            int sumDiagL = 0; // sumowanie wartości w lewym skosie
            int sumDiagR = 0; // sumowanie wartości w prawym skosie
            for (int i = 0; i <= gridSize-1; i++)
            {
                int sumCol = 0; // sumowanie wartości kolumn
                int sumRow = 0; // sumowanie wartości rzędów
                for (int j = 0; j <= gridSize - 1; j++)
                {
                    sumCol += gameState[i, j]; 
                    sumRow += gameState[j,i];
                }
                if (Math.Abs(sumCol) == gridSize)
                {
                    return true;
                }
                if (Math.Abs(sumRow) == gridSize)
                {
                    return true;
                }
                sumDiagL += gameState[gridSize - i - 1, i];
                sumDiagR += gameState[i, i];
            }
            if (Math.Abs(sumDiagL) == gridSize)
            {
                return true;
            }
            if (Math.Abs(sumDiagR) == gridSize)
            {
                return true;
            }
            return false;
        }
        private void MessageGameState()
        {
            string output = "";
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    output += gameState[j,i].ToString().Replace("-1","-").Replace("1","+");

                }
                output += "/";
            }
            Debug.WriteLine(output);
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
