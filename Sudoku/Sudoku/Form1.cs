/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-15-2018
 * purpose : form for creating the sudoku puzzle
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        public static string puzzleFolder;
        public static problem currentPuzzle; //Storing puzzle that we are currently "working" on
        public static string currentDifficulty = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            puzzleFolder = Path.Combine(projectFolder, "puzzles");
        }

        //Reading directory.txt folder, checking if the next puzzle is one that we are looking for, if yes we are returning it
        private problem readPuzzles(string difficulty, string needed)
        {
            string line;
            problem currentLoaded = null;

            //Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(puzzleFolder + "/directory.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split('/'); //Splitting string by / to see it's difficulty and puzzle file name
                if (words[0] == difficulty)
                {
                    if (needed == "unsolved")
                    {
                        currentLoaded = loadUnsolvedPuzzle(Path.Combine(puzzleFolder, line));
                        if (currentLoaded != null) { //If we loaded what we need return it
                            currentDifficulty = difficulty;
                            file.Close();
                            return currentLoaded;
                        }
                    }
                    else if (needed == "saved")
                    {
                        currentLoaded = loadSavedPuzzle(Path.Combine(puzzleFolder, line));
                        if (currentLoaded != null) //If we loaded what we need return it
                        {
                            currentDifficulty = difficulty;
                            file.Close();
                            return currentLoaded;
                        }
                    }
                }

            }
            file.Close();
            return currentLoaded;
        }

        //Reading file and checking if it's one that we need
        private problem loadUnsolvedPuzzle(string puzzleLocation)
        {
            int i = 0; //Counter what line we are currently reading
            int emptyLine = 0; //Keeping track of what part of the puzzle we are reading
            string line;
            string challengeString = "";
            string solutionString = "";

            //Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(puzzleLocation);
            while ((line = file.ReadLine()) != null)
            {
                if (i == 0)
                {
                    if (line[0] == 'S' || line[0] == 'A') //S stands for solved, E stands for saved
                    {
                        file.Close();
                        return null; //If this puzzle is not unsolved one return null
                    }
                }
                if (line == "")
                {
                    emptyLine++;
                    i++;
                    continue;
                }
                if (emptyLine == 0)
                {
                    challengeString += line;
                }
                else if (emptyLine == 1)
                {
                    solutionString += line;
                }

                i++;
            }
            file.Close();

            return new problem(puzzleLocation, challengeString, solutionString, false);
        }

        //Reading file and checking if it's one that we need
        private problem loadSavedPuzzle(string puzzleLocation)
        {
            int i = 0; //Counter what line we are currently reading
            int emptyLine = 0; //Keeping track of what part of the puzzle we are reading
            string line;
            string challengeString = "";
            string solutionString = "";
            string savedString = "";
            bool cheates = false;
            string time = "";

            //Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(puzzleLocation);
            while ((line = file.ReadLine()) != null)
            {
                if (i == 0)
                {
                    if (line[0] != 'E') //S stands for solved, E stands for saved
                    {
                        file.Close();
                        return null; //If this puzzle is not saved one return null
                    }
                    i++;
                    continue;
                }

                if (line == "")
                {
                    emptyLine++;
                    i++;
                    continue;
                }

                if (emptyLine == 0)
                {
                    challengeString += line;
                }
                else if (emptyLine == 1)
                {
                    solutionString += line;
                }
                else if (emptyLine == 2)
                {
                    savedString += line;
                }
                else if (emptyLine == 3)
                {
                    if (line == "T") //If user used cheates before
                    {
                        cheates = true;
                    }
                }
                else if (emptyLine == 4)
                {
                    time = line;
                }
            }
            file.Close();
            return new problem(puzzleLocation, challengeString, solutionString, cheates, savedString, time);
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            problem saved = readPuzzles("easy", "saved"); //Try fetching saved puzzle first (to check if there is saved one first)
            if (saved != null) //If there is saved puzzle
            {
                MessageBox.Show("Saved puzzle detected! You will continue playing it.");
                currentPuzzle = saved;
                currentPuzzle.isSaved = true;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            saved = readPuzzles("easy", "unsolved");
            if (saved != null) //If there is unsolved puzzle for that difficulty
            {
                currentPuzzle = saved;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            else
            {
                MessageBox.Show("There is no more unsolved puzzles for easy difficulty, please choose another difficulty.");
            }
        }

        private void mediumButton_Click(object sender, EventArgs e)
        {
            problem saved = readPuzzles("medium", "saved"); //Try fetching saved puzzle first (to check if there is saved one first)
            if (saved != null) //If there is saved puzzle
            {
                MessageBox.Show("Saved puzzle detected! You will continue playing it.");
                currentPuzzle = saved;
                currentPuzzle.isSaved = true;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            saved = readPuzzles("medium", "unsolved");
            if (saved != null) //If there is unsolved puzzle for that difficulty
            {
                currentPuzzle = saved;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            else
            {
                MessageBox.Show("There is no more unsolved puzzles for medium difficulty, please choose another difficulty.");
            }
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            problem saved = readPuzzles("hard", "saved"); //Try fetching saved puzzle first (to check if there is saved one first)
            if (saved != null) //If there is saved puzzle
            {
                MessageBox.Show("Saved puzzle detected! You will continue playing it.");
                currentPuzzle = saved;
                currentPuzzle.isSaved = true;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            saved = readPuzzles("hard", "unsolved");
            if (saved != null) //If there is unsolved puzzle for that difficulty
            {
                currentPuzzle = saved;
                gameWindow gw = new gameWindow();
                gw.ShowDialog();
                return;
            }
            else {
                MessageBox.Show("There is no more unsolved puzzles for hard difficulty, please choose another difficulty.");
            }
        }

        //Reading directory.txt folder, checking if the puzzle is one that is solved, if yes do some additional checks
        static public int[] readTimes(string difficulty)
        {
            string line;
            string currentLoaded = null;
            int bestTime = int.MinValue;
            int totalTime = 0;
            int puzzleForAverage = 0;

            //Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(puzzleFolder + "/directory.txt");
            while ((line = file.ReadLine()) != null)
            {
                currentLoaded = null;
                string[] words = line.Split('/'); //Splitting string by / to see it's difficulty and puzzle file name
                if (words[0] == difficulty)
                {
                    currentLoaded = loadPuzzleForTime(Path.Combine(puzzleFolder, line));
                    if (currentLoaded != null)
                    {
                        string[] mAndS = currentLoaded.Split(':'); //We are splitting seconds and minutes
                        int[] splitted = new int[2] { Convert.ToInt32(mAndS[0]), Convert.ToInt32(mAndS[1])}; //Converting our splitted array into integer array so we can calculate with those values
                        if (bestTime < splitted[0] * 60 + splitted[1]) {
                            bestTime = splitted[0] * 60 + splitted[1];
                        }
                        totalTime += splitted[0] * 60 + splitted[1];
                        puzzleForAverage++;
                    }
                }

            }
            file.Close();
            //If there are no puzzle that were finished without cheating
            int[] forReturn = new int[2] { 0, 0 };
            if (puzzleForAverage == 0) {
                return forReturn;
            }
            forReturn[0] = bestTime;
            forReturn[1] = totalTime / puzzleForAverage;
            return forReturn;
        }

        //Same function as loading saved puzzles but deleted things what we don't need
        private static string loadPuzzleForTime(string puzzleLocation) {
            int i = 0; //Counter what line we are currently reading
            int emptyLine = 0; //Keeping track of what part of the puzzle we are reading
            string line;

            //Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(puzzleLocation);
            while ((line = file.ReadLine()) != null)
            {
                if (i == 0)
                {
                    if (line[0] != 'S') //S stands for solved, E stands for saved
                    {
                        file.Close();
                        return null; //If this puzzle is not saved one return null
                    }
                    i++;
                    continue;
                }

                if (line == "")
                {
                    emptyLine++;
                    i++;
                    continue;
                }else if (emptyLine == 3){
                    if (line == "T") //If user used cheates before
                    {
                        file.Close(); //Closing file before returning
                        return null; //Because flag that he cheated is true
                    }
                }else if (emptyLine == 4){
                    file.Close(); //Closing file before returning
                    return line;
                }
            }
            file.Close();
            return null;
        }
    }
}
