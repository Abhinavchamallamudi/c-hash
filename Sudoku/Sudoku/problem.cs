/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-15-2018
 * purpose : The logic behind the sudoku puzzle
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class problem //It's public so we can access it from anywhere
    {
        public char[,] challenge = new char[9, 9];
        public char[,] solution = new char[9, 9];
        public char[,] saved = new char[9, 9];
        public bool cheatUsed;
        public bool isSaved;
        public string puzzleFile;
        public string time;
        public string saveStateTime; //To fix offset when it's saved and when it's checked

        public problem(string _puzzleFile, string toDeserializeProblem, string toDeserializeSolution, bool _cheatUsed, string toDeserializeSaved = "", string _time = "") {
            puzzleFile = _puzzleFile;
            cheatUsed = _cheatUsed;
            deserializeProblem(toDeserializeProblem);
            deserializeSolution(toDeserializeSolution);
            if (toDeserializeSaved != "") {
                deserializeSaved(toDeserializeSaved);
            }
            time = _time;
        }

        public void deserializeProblem(string toDeserialize) {
            for (int i = 0; i < 81; i++) { //Go through string and put every number into our 2D array
                if (toDeserialize[i] == '0')
                {
                    challenge[i / 9, i % 9] = ' '; //If it's 0 we are changing it to blank space so we can handle things easier later
                }
                else {
                    challenge[i / 9, i % 9] = toDeserialize[i];
                }
            }
        }

        public void deserializeSolution(string toDeserialize)
        {
            for (int i = 0; i < 81; i++) //Go through string and put every number into our 2D array
            {
                solution[i / 9, i % 9] = toDeserialize[i];
            }
        }

        public void deserializeSaved(string toDeserialize)
        {
            for (int i = 0; i < 81; i++) //Go through string and put every number into our 2D array
            { 
                saved[i / 9, i % 9] = toDeserialize[i];
            }
        }

        public void saveState(bool clear = false, bool finished = false) {
            string text = "";
            //If clear flag is not true than user didn't want to reset it, just save it
            if (!clear) {
                if (finished)
                {
                    text += "S";
                    text += Environment.NewLine;
                }
                else {
                    text += "E";
                    text += Environment.NewLine;
                }
            }
            for (int i = 0; i < 9; i++) //Serializing it, same way as stored with (maybe) additional fields
            {
                if (i != 0)
                { //Add new line just if it's not first row to print
                    text += Environment.NewLine; //We are using Environment.NewLine because different operating system have different new line separator
                }
                for (int j = 0; j < 9; j++)
                {
                    if (challenge[i, j] == ' ') //We have to revert empty space back to 0
                    {
                        text += "0";
                    }
                    else {
                        text += Convert.ToString(challenge[i, j]);
                    }
                }
            }
            text += Environment.NewLine;
            text += Environment.NewLine;
            for (int i = 0; i < 9; i++)
            {
                if (i != 0)
                { //Add new line just if it's not first row to print
                    text += Environment.NewLine; //We are using Environment.NewLine because different operating system have different new line separator
                }
                for (int j = 0; j < 9; j++)
                {
                    if (solution[i, j] == ' ') //We have to revert empty space back to 0
                    {
                        text += "0";
                    }
                    else
                    {
                        text += Convert.ToString(solution[i, j]);
                    }
                }
            }
            if (!clear)
            {
                text += Environment.NewLine;
                text += Environment.NewLine;
                for (int i = 0; i < 9; i++)
                {
                    if (i != 0)
                    { //Add new line just if it's not first row to print
                        text += Environment.NewLine; //We are using Environment.NewLine because different operating system have different new line separator
                    }
                    for (int j = 0; j < 9; j++)
                    {
                        if (saved[i, j] == ' ') //We have to revert empty space back to 0
                        {
                            text += "0";
                        }
                        else
                        {
                            text += Convert.ToString(saved[i, j]);
                        }
                    }
                }
                text += Environment.NewLine;
                text += Environment.NewLine;
                if (cheatUsed)
                {
                    text += "T";
                }
                else
                {
                    text += "F";
                }
                text += Environment.NewLine;
                text += Environment.NewLine;
                saveStateTime = gameWindow.minutes + ":" + gameWindow.seconds;
                text += saveStateTime;
            }
            File.WriteAllText(puzzleFile, text);
        }
        //to create functionality for time
        public int getMinutes() {
            string[] minutesSeconds = time.Split(':');
            return Convert.ToInt32(minutesSeconds[0]);
        }

        public int getSeconds()
        {
            string[] minutesSeconds = time.Split(':');
            return Convert.ToInt32(minutesSeconds[1]);
        }

        public int getMinutesSaved()
        {
            string[] minutesSeconds = saveStateTime.Split(':');
            return Convert.ToInt32(minutesSeconds[0]);
        }

        public int getSecondsSaved()
        {
            string[] minutesSeconds = saveStateTime.Split(':');
            return Convert.ToInt32(minutesSeconds[1]);
        }
    }
}
