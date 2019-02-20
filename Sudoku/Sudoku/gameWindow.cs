/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-15-2018
 * purpose : This is for writing functionality for all the buttons and textboxes available
 */
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

namespace Sudoku
{
    public partial class gameWindow : Form
    {
        private Button[] buttonList = new Button[82]; //It's easier for me to write index = field number so I added one extra so that's index 0
        private Button lastButton; //This will track on what was the last button on board that was clicked (not including pre populated ones)
        public static int seconds = 0;
        public static int minutes = 0;
        bool playing = false;
        bool finishExit = false;
        Timer timer = null;

        public gameWindow()
        {
            InitializeComponent();
            buttonList[1] = field1; buttonList[2] = field2; buttonList[3] = field3; buttonList[4] = field4; buttonList[5] = field5;
            buttonList[6] = field6; buttonList[7] = field7; buttonList[8] = field8; buttonList[9] = field9; buttonList[10] = field10;
            buttonList[11] = field11; buttonList[12] = field12; buttonList[13] = field13; buttonList[14] = field14; buttonList[15] = field15;
            buttonList[16] = field16; buttonList[17] = field17; buttonList[18] = field18; buttonList[19] = field19; buttonList[20] = field20;
            buttonList[21] = field21; buttonList[22] = field22; buttonList[23] = field23; buttonList[24] = field24; buttonList[25] = field25;
            buttonList[26] = field26; buttonList[27] = field27; buttonList[28] = field28; buttonList[29] = field29; buttonList[30] = field30;
            buttonList[31] = field31; buttonList[32] = field32; buttonList[33] = field33; buttonList[34] = field34; buttonList[35] = field35;
            buttonList[36] = field36; buttonList[37] = field37; buttonList[38] = field38; buttonList[39] = field39;
            buttonList[40] = field40; buttonList[41] = field41; buttonList[42] = field42; buttonList[43] = field43; buttonList[44] = field44;
            buttonList[45] = field45; buttonList[46] = field46; buttonList[47] = field47; buttonList[48] = field48; buttonList[49] = field49;
            buttonList[50] = field50; buttonList[51] = field51; buttonList[52] = field52; buttonList[53] = field53; buttonList[54] = field54;
            buttonList[55] = field55; buttonList[56] = field56; buttonList[57] = field57; buttonList[58] = field58; buttonList[59] = field59;
            buttonList[60] = field60; buttonList[61] = field61; buttonList[62] = field62; buttonList[63] = field63; buttonList[64] = field64;
            buttonList[65] = field65; buttonList[66] = field66; buttonList[67] = field67; buttonList[68] = field68; buttonList[69] = field69;
            buttonList[70] = field70; buttonList[71] = field71; buttonList[72] = field72; buttonList[73] = field73; buttonList[74] = field74;
            buttonList[75] = field75; buttonList[76] = field76; buttonList[77] = field77; buttonList[78] = field78; buttonList[79] = field79;
            buttonList[80] = field80; buttonList[81] = field81;

            for (var i = 0; i < 81; i++) {
                buttonList[i + 1].Click += (sender, e) => sudokuButtonClick(sender, e, i + 1); //Adding click event with custom parameter for each button
                buttonList[i + 1].KeyPress += (sender, e) => sudokuKeyPressed(sender, e); //When user inputs something on keyboard after clicking the button
            }

        }

        private void sudokuKeyPressed(object sender, KeyPressEventArgs e)
        {
            int i;
            if (int.TryParse(e.KeyChar.ToString(), out i)) //Checking if it's a number that was clicked
            {
                (sender as Button).Text = Convert.ToString(i); //Populate button with number
                clearErrors(); //Every time when user enters new number we are clearing all of the errors
                if (i == 0) { //If 0 was entered clear the cell
                    (sender as Button).Text = " ";
                }
                checkFinish(); //Check if user finished the game
                pauseButton.Focus(); //Change focus to pause button
                pauseButton.Enabled = true; //Enable pause button (in case game was just resetted) 
            }
        }

        private void sudokuButtonClick(object sender, EventArgs e, int buttonNumber) {
            clearErrors(); //Reset coloring after every click
            (sender as Button).BackColor = Color.Blue;
        }

        private void gameWindow_Load(object sender, EventArgs e)
        {
            //Resetting values
            bool finishExit = false;
            minutes = 0;
            seconds = 0;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += secondPassed;
            timer.Start();
            playing = true;
            //Setting up the board
            for (int i = 0; i < Form1.currentPuzzle.challenge.GetLength(0); i++) {
                for (var j = 0; j < Form1.currentPuzzle.challenge.GetLength(1); j++) {
                    char number = Form1.currentPuzzle.challenge[i, j];
                    if (number == ' ') {
                        buttonList[i * 9 + j + 1].ForeColor = Color.Blue; //We are changing font color of buttons that user is supposed to enter
                        buttonList[i * 9 + j + 1].Text = " ";
                        continue;
                    }
                    //If it's button that user is not supposed to enter
                    buttonList[i * 9 + j + 1].Text = Convert.ToString(number); //We are changing button's text and disabling it from clicking
                    buttonList[i * 9 + j + 1].Enabled = false; //We are disabling that button of being clicked
                }
            } //If our games was saved one
            if (Form1.currentPuzzle.isSaved) {
                minutes = Form1.currentPuzzle.getMinutes();
                seconds = Form1.currentPuzzle.getSeconds();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //If button is empty
                        if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) == ' ')
                        { //Check if it populated in the saved version
                            if (Form1.currentPuzzle.challenge[i, j] == ' ' && Form1.currentPuzzle.saved[i, j] != '0')
                            { //If it's also 0 in the challenge and in the saved one we don't need to populate that cell
                                buttonList[i * 9 + j + 1].Text = Convert.ToString(Form1.currentPuzzle.saved[i, j]);
                            }
                        }
                    }
                }
                playing = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int remains = 0; //Keeping track how many fields needs to be populated
            bool errors = false; //Keeping track if we had any errors
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) != ' ')
                    {
                        if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) != Form1.currentPuzzle.solution[i, j])
                        { //When we find problem enter this
                            if (checkRow(i)) {
                                markIncorrectRow(i); //If that row has problems mark it
                                errors = true;
                            }
                            if (checkColumn(j)) {
                                markIncorrectColumn(j); //If that column has problems mark it
                                errors = true;
                            }
                            if (checkBox(i, j)) {
                                errors = true; //This does not markInncorectBox function because we color incorrect cells directly inside checkBox() function
                            }
                            if (!errors) { //If there were no duplicated numbers but number is still not correct
                                buttonList[i * 9 + j + 1].BackColor = Color.Yellow;
                                errors = true;
                            }
                        }
                    }
                    else {
                        remains++;
                    }
                }
            }
            if (!errors) {
                MessageBox.Show("You are doing great! You need to populate " + remains + " more fields!");
            }
        }

        private void checkFinish() {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) != Form1.currentPuzzle.solution[i, j]) {
                        return;
                    }
                }
            }
            //If we arrive here we are displaying message to user and we are saving current board state
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Form1.currentPuzzle.saved[i, j] = Convert.ToChar(buttonList[i * 9 + j + 1].Text);
                }
            }
            Form1.currentPuzzle.saveState(false, true);
            int[] times = new int[2];
            times = Form1.readTimes(Form1.currentDifficulty);
            string bestToPrint = "none";
            string averageToPrint = "none";
            if (times[0] != 0) {
                bestToPrint = fixTime(times[0] / 60, times[0] % 60);
                averageToPrint = fixTime(times[1] / 60, times[1] % 60);
            }
            MessageBox.Show("Good job! You successfully finished the game in: " + fixTime(Form1.currentPuzzle.getMinutesSaved(), Form1.currentPuzzle.getSecondsSaved()) + 
                "\nYour best time (without cheating) for " + Form1.currentDifficulty +" difficulty is: " + bestToPrint +
                "\nYour average time for (without cheating) " + Form1.currentDifficulty +" difficulty is: " + averageToPrint);

            finishExit = true;
            this.Close();
        }

        //Check if there are multiple numbers in same row
        private bool checkRow(int row) {
            int[] numbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //Array which will contain how many times each number appeared
            for (int i = 0; i < 9; i++) {
                switch (buttonList[row * 9 + 1 + i].Text) {
                    case "1": numbers[1]++; break;
                    case "2": numbers[2]++; break;
                    case "3": numbers[3]++; break;
                    case "4": numbers[4]++; break;
                    case "5": numbers[5]++; break;
                    case "6": numbers[6]++; break;
                    case "7": numbers[7]++; break;
                    case "8": numbers[8]++; break;
                    case "9": numbers[9]++; break;
                    default: break;
                }
            }
            for (int i = 1; i <= 9; i++) {
                if (numbers[i] > 1) {
                    return true;
                }
            }
            return false;
        }

        //Check if there are multiple numbers in same column
        private bool checkColumn(int column)
        {
            int[] numbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //Array which will contain how many times each number appeared
            for (int i = 0; i < 9; i++)
            {
                switch (buttonList[i * 9 + 1 + column].Text)
                {
                    case "1": numbers[1]++; break;
                    case "2": numbers[2]++; break;
                    case "3": numbers[3]++; break;
                    case "4": numbers[4]++; break;
                    case "5": numbers[5]++; break;
                    case "6": numbers[6]++; break;
                    case "7": numbers[7]++; break;
                    case "8": numbers[8]++; break;
                    case "9": numbers[9]++; break;
                    default: break;
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                if (numbers[i] > 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkBox(int row, int column) {
            bool error = false;
            if (row >= 0 && row <= 2) { //This else if block will determine what mini box we need to check based on row and column passed
                if (column >= 0 && column <= 2) {
                    error = checkBoxDetermined(1, 1); //We are passing starting point of block where that number is
                } else if (column >= 3 && column <= 5) {
                    error = checkBoxDetermined(1, 4);
                }
                else if (column >= 6 && column <= 8) {
                    error = checkBoxDetermined(1, 7);
                }
            } else if (row >= 3 && row <= 5) {
                if (column >= 0 && column <= 2) {
                    error = checkBoxDetermined(4, 1);
                } else if (column >= 3 && column <= 5) {
                    error = checkBoxDetermined(4, 4);
                } else if (column >= 6 && column <= 8) {
                    error = checkBoxDetermined(4, 7);
                }
            }
            else if (row >= 6 && row <= 8) {
                if (column >= 0 && column <= 2) {
                    error = checkBoxDetermined(7, 1);
                } else if (column >= 3 && column <= 5) {
                    error = checkBoxDetermined(7, 4);
                } else if (column >= 6 && column <= 8) {
                    error = checkBoxDetermined(7, 7);
                }
            }
            return error;
        }

        //This is the function that actually checks mini boxes for repeating numbers
        private bool checkBoxDetermined(int row, int column) {
            int[] numbers = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //Array which will contain how many times each number appeared
            row--; //Fixing row counter to be 0 so we can do row*9 (0*9) meaning first row
            int oldRow = row;
            for (int i = 0; i < 3; i++, row++) {
                for (int j = 0; j < 3; j++) {
                    switch (buttonList[row * 9 + column + j].Text)
                    {
                        case "1": numbers[1]++; break;
                        case "2": numbers[2]++; break;
                        case "3": numbers[3]++; break;
                        case "4": numbers[4]++; break;
                        case "5": numbers[5]++; break;
                        case "6": numbers[6]++; break;
                        case "7": numbers[7]++; break;
                        case "8": numbers[8]++; break;
                        case "9": numbers[9]++; break;
                        default: break;
                    }
                }
            }
            for (int k = 1; k <= 9; k++)
            {
                if (numbers[k] > 1)
                {
                    for (int i = 0; i < 3; i++, oldRow++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            buttonList[oldRow * 9 + column + j].BackColor = Color.Red;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private void markIncorrectRow(int row) {
            for (int i = 0; i < 9; i++) {
                buttonList[row * 9 + 1 + i].BackColor = Color.Red;
            }
        }

        private void markIncorrectColumn(int column) {
            for (int i = 0; i < 9; i++) {
                buttonList[i * 9 + 1 + column].BackColor = Color.Red;
            }
        }

        private void clearErrors() {
            for (int i = 0; i < 81; i++) {
                buttonList[i + 1].BackColor = Color.Transparent;
            }
        }

        //This function is going to help player
        private void helpButton_Click(object sender, EventArgs e)
        {
            List<Button> emptyButtons = new List<Button>(); //This list will store all of the buttons that are not populated
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++) {
                    if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) == ' ') //Check if button is empty
                    {
                        emptyButtons.Add(buttonList[i * 9 + j + 1]);
                    }
                }
            }
            if (emptyButtons.Count != 0) //If we have empty cells choose random and populate it
            {
                Random random = new Random();
                int cell = random.Next(0, emptyButtons.Count); //Generate random number from 0 to how many buttons are empty
                for (int i = 0; i < 9; i++) {
                    for (int j = 0; j < 9; j++) {
                        if (buttonList[i * 9 + j + 1].Name == emptyButtons[cell].Name) { //When we arrive on button that we need to change
                            clearErrors();
                            buttonList[i * 9 + j + 1].Text = Convert.ToString(Form1.currentPuzzle.solution[i, j]);
                            buttonList[i * 9 + j + 1].BackColor = Color.Green; //We are highlighting cell that we help him populate
                            Form1.currentPuzzle.cheatUsed = true;
                            checkFinish(); //Check if user finished the game
                            return;
                        }
                    }
                }

            }
            else { //Populate first field that is not correct
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) != Form1.currentPuzzle.solution[i, j]) //If cell has wrong value populate it with correct value
                        {
                            clearErrors();
                            buttonList[i * 9 + j + 1].Text = Convert.ToString(Form1.currentPuzzle.solution[i, j]);
                            buttonList[i * 9 + j + 1].BackColor = Color.Green; //We are highlighting cell that we help him populate
                            Form1.currentPuzzle.cheatUsed = true;
                            checkFinish(); //Check if user finished the game
                            return;
                        }
                    }
                }
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (playing) //If user is playing and wants to pause
            {
                playing = false;
                pauseButton.Text = "Play";
                timer.Stop();
                hideBoard.Visible = true; //Hiding board so user cannot cheat
            }
            else {
                playing = true;
                pauseButton.Text = "Pause";
                timer.Start();
                hideBoard.Visible = false; //Unhiding board
            }
        }

        private void secondPassed(object sender, EventArgs e) {
            if (playing) {
                seconds++; //Adding second
                if (seconds == 60) {
                    minutes++; //Adding minute
                    seconds = 0;
                }
                currentTime.Text = fixTime(minutes, seconds);
            }
        }

        private void gameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finishExit) { //If user wanted to exit (before game is finished)
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Form1.currentPuzzle.saved[i, j] = Convert.ToChar(buttonList[i * 9 + j + 1].Text);
                    }
                }
                if (MessageBox.Show("Do you want to save your progress?", "Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Form1.currentPuzzle.saveState();
                }          
            }
            timer.Tick -= secondPassed; //Deleting timer event handler so we can create it again if form needs to be loaded again
        }

        //This function is going to make string for time (minutes:seconds) look nicer
        private string fixTime(int minutes, int seconds) {
            string time = "";
            if (minutes < 10)
            {
                time += "0"; //If our time is under 10 minute adding additional 0
            }
            time += minutes + ":";
            if (seconds < 10)
            {
                time += "0";
            }
            time += seconds;

            return time;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //If we arrive here we are displaying message to user and we are saving current board state
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Form1.currentPuzzle.saved[i, j] = Convert.ToChar(buttonList[i * 9 + j + 1].Text);
                }
            }
            Form1.currentPuzzle.saveState();
            MessageBox.Show("Progress succesfully saved!");
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            playing = false;
            seconds = 0;
            minutes = 0;
            Form1.currentPuzzle.cheatUsed = false;
            Form1.currentPuzzle.isSaved = false;

            //Setting up the board
            for (int i = 0; i < Form1.currentPuzzle.challenge.GetLength(0); i++)
            {
                for (var j = 0; j < Form1.currentPuzzle.challenge.GetLength(1); j++)
                {
                    char number = Form1.currentPuzzle.challenge[i, j];
                    if (number == ' ')
                    {
                        buttonList[i * 9 + j + 1].ForeColor = Color.Blue; //We are changing font color of buttons that user is supposed to enter
                        buttonList[i * 9 + j + 1].Text = " ";
                        continue;
                    }
                    //If it's button that user is not supposed to enter
                    buttonList[i * 9 + j + 1].Text = Convert.ToString(number); //We are changing button's text and disabling it from clicking
                    buttonList[i * 9 + j + 1].Enabled = false; //We are disabling that button of being clicked
                }
            } //If our games was saved one
            if (Form1.currentPuzzle.isSaved)
            {
                minutes = Form1.currentPuzzle.getMinutes();
                seconds = Form1.currentPuzzle.getSeconds();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //If button is empty
                        if (Convert.ToChar(buttonList[i * 9 + j + 1].Text) == ' ')
                        { //Check if it populated in the saved version
                            if (Form1.currentPuzzle.challenge[i, j] == ' ' && Form1.currentPuzzle.saved[i, j] != '0')
                            { //If it's also 0 in the challenge and in the saved one we don't need to populate that cell
                                buttonList[i * 9 + j + 1].Text = Convert.ToString(Form1.currentPuzzle.saved[i, j]);
                            }
                        }
                    }
                }
            }
            clearErrors();
            Form1.currentPuzzle.saveState(true);
            MessageBox.Show("You successfully restarted progress!");
            playing = true;
        }
    }
}
