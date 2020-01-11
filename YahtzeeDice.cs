using System;
using static System.Console;

namespace Bme121 { 
    public class YahtzeeDice
    {
        private static int[] dice = new int[5];
        Random randNum = new Random();
        static bool[] unrolled = new bool[5]; //true if to be rerolled; false if not to be rerolled

	    public void Roll()
        {
            //give random face value to all unrolled dice (1..6)
            for (int i = 0; i<5; i++)
            {
                if (unrolled[i])
                {
                    dice[i] = randNum.Next(1, 7);
                    unrolled[i] = false;
                }
            }

            /*for (int i=0; i<5; i++)
            {
                dice[i] = 6;
                unrolled[i] = false;
            }*/

            int i = randNum.Next(1, 7);
            dice[0] = i;
            dice[1] = i;
            dice[2] = i;
            dice[3] = i;
            dice[4] = i;
            
        }

        public void Unroll (string faces)
        {
            //returns selected dice to an unrolled state 

            //checks for "all" to reset all dice to an unrolled state 
            if (faces.Equals("all"))
            {
                for (int i =0; i<5; i++)
                {
                    unrolled[i] = true;
                }
            }
            else
            {

                //accepts a string where each character in the string is the face value of one die to unroll
                string[] facesArray = faces.Split(",", 5, StringSplitOptions.RemoveEmptyEntries);
                foreach (string face in facesArray)
                {
                    int iFace = Int32.Parse(face);
                    for(int i = 0; i<5; i++)
                    {
                        if (iFace == dice[i])
                        {
                            unrolled[i] = true; 
                        }
                    }
                }
            }
        }

        public int Sum ()
        {
            //return sum of the face values of all five dice
            //used for chance 
            int sumFaceValues = 0;

            foreach (int die in dice)
            {
                sumFaceValues += die;
            }

            return sumFaceValues;
        }

        public int Sum (int face) //assuming that "face value" means the actual value displayed on each of the dice
        {
            //return the sum of just the dice having the face value passed as an argument
            int sumFaceValues = 0;

            foreach (int die in dice)
            {
                if (die == face)
                {
                    sumFaceValues += die;
                }
            }
            return sumFaceValues;
        }

        public bool IsRunOf (int length)
        {
            //checks for runs
            //i.e. arrange dice to form a sequence of increasing values of at least the given length
            //checks for small and large straights 
            
            bool isRunOfLength = false;
            bool[] numExists = new bool[length];
            int[] tempDice = dice;

            Array.Sort(tempDice);

            
            
            for (int checkingNum = 1; checkingNum <= 6 - length + 1; checkingNum++)
            {
                //initialize array every time
                for (int i = 0; i < length; i++)
                {
                    numExists[i] = false;
                }

                //check that the checkingNum exists in the tempDice array
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j<5; j++)
                    {
                        if (checkingNum + i == tempDice[j])
                        {
                            numExists[i] = true;
                            
                        }
                    }
                }

                foreach (bool num in numExists)
                {
                    if (num)
                    {
                        isRunOfLength = true;
                    }
                    else
                    {
                        isRunOfLength = false;
                        break;
                    }
                }

                if (isRunOfLength)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsSetOf(int size)
        {
            //checks for sets 
            //i.e. that at least 'size" dice have the same face value 
            
            for (int i = 1; i < 7; i++)
            {
                int faceCounter = 0;
                foreach (int die in dice)
                {
                    if (die == i)
                    {
                        faceCounter++;
                    }
                }
                if (faceCounter >= size)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFullHouse()
        {
            //checks for full house 
            //3 dice with one face value and two dice with a different face value 
            int[] tempDice = dice;
            Array.Sort(tempDice);
            if (tempDice[0] != tempDice[4])
            {
                if (tempDice[0] == tempDice[1] && tempDice[1] == tempDice[2] && tempDice[3] == tempDice[4])
                {
                    //first 3 dice are the same and the last two are the same
                    return true;
                }
                else if (tempDice[0] == tempDice[1] && tempDice[2] == tempDice[3] && tempDice[3] == tempDice[4])
                {
                    //first 2 dice are the same and the last 3 are the same 
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string outputDice = " |  ";
            int counter = 1;
            foreach (int die in dice) 
            {
                outputDice += $"Die {counter} : {die}  |  ";
                counter++; 
            }
            return outputDice;
        }
    }
}
