using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            if (endGame() || (moveCounter >= 10)) return;
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
                MessageBox.Show("Wygrał gracz" + playerInt().ToString() );
                historyLabel.Text += "Wygrał gracz" + playerInt().ToString() + "\n";
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
            return (1 + Convert.ToInt32(player));
        }
        private bool endGame()
        {
            for (int i = 0; i <= 2; i++)
            {
                if((gameState[i,0] == gameState[i,1])&&(gameState[i, 1] == gameState[i, 2]) && gameState[i, 0] != 0)
                {
                    return true;
                }
                if ((gameState[0,i] == gameState[1,i]) && (gameState[1,i] == gameState[2,i]) && gameState[0,i] != 0)
                {
                    return true;
                }
                
            }
            if ((gameState[0, 0] == gameState[1, 1]) && (gameState[1, 1] == gameState[2, 2]) && gameState[0, 0] != 0)
            {
                return true;
            }
            if ((gameState[2, 0] == gameState[1, 1]) && (gameState[1, 1] == gameState[0, 2]) && gameState[2, 0] != 0)
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
