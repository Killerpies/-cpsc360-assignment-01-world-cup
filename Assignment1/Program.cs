using System;

namespace Program360
{
    public class Soccer
    {
        /// <summary>
        /// Takes String array loops through it to organize and sort team data
        /// </summary>
        /// <param name="matches"></param>
        /// <returns>FinalAnswer - String Array</returns>
        public static string[] getRankings(string[] matches)
        {
            // Your program begins here!

            // Declare useful arrays, and values
            int uniqueTeam = uniqueTeams(matches);
            string matchName = matches[0];
            string[] teams = new string[uniqueTeam];
            int[][] scores = new int[uniqueTeam][];

            // initialize 2D array to contain score data
            for (int i = 0; i < uniqueTeam; i++)
            {
                scores[i] = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            }

            // loop through matches
            for (int i = 0; i < matches.Length; i++)
            {
                // if not index 0, send our string to the match sorting function
                if (i != 0)
                {
                    string tempMatch = matches[i];
                    sortMatch(teams, scores, tempMatch);
                }
            }

            // Declare final answer array, set the first index == name of the match. Example: "Pewee League"
            string[] finalAnswer = new string[teams.Length + 1];
            finalAnswer[0] = matchName;

            // Determine order of the array
            string[] newScores = new string[teams.Length];
            newScores = sortTeamArray(teams, scores, matchName);


            // Input the properly ordered array into finalAnswer array
            for (int i = 0;i < newScores.Length; i++)
            {
                finalAnswer[i+1] = newScores[i];
            }
            // return properly formatted final answer
            return finalAnswer;
        }
        /// <summary>
        /// Determines the number of Unique teams in the match
        /// </summary>
        /// <param name="matches"></param>
        /// <returns>teams.Count - int variable representing number of teams</returns>
        public static int uniqueTeams(string[] matches)
        {
            // Create arraylist <string>
            List<string> teams = new List<string>(2);

            // Loops through and adds each team to the arraylist
            for (int i = 0; i < matches.Length; i++)
            {
                if (i != 0)
                {
                    // Splits into an array with both teams and the scores, assigns both teams reference variable
                    string tempMatch = matches[i];
                    string[] splitTeams = tempMatch.Split('#');
                    string team1 = splitTeams[0];
                    string team2 = splitTeams[2];

                    // Check if the team1 is inside our teams array
                    // if not we add it to the array
                    if (!teams.Contains(team1))
                    {
                        teams.Add(team1);
                    }
                    // Check if the team2 is inside our teams array
                    // if not we add it to the array
                    if (!teams.Contains(team2))
                    {
                        teams.Add((team2));
                    }
                }
            }
            // returns number of teams in the match
            return teams.Count;
        }



        /// <summary>
        /// takes teams[], scores[][] and, currentMatch as param's 
        /// lookes at each team and decides each teams: goals, points, wins, and games played
        /// stores those values as int's in scores
        /// also stores the name of each team in teams[] under the same element as their scores
        /// </summary>
        /// <param name="teams"> empty string array we fill with team names</param>
        /// <param name="scores"> emty int 2d array we fill with each teams scores</param>
        /// <param name="currentMatch">a single match given to us that we get scores and team names from</param>
        public static void sortMatch(string[] teams, int[][] scores, string currentMatch)
        {
            //This functions code is a bit redundent because I accomplished the same thing with uniqueTeams function
            //This could be done better, but I spent a lot more time on this than I meant too.

            // Splits into an array with both teams and the scores, assigns both teams reference variable
            string[] splitTeams = currentMatch.Split('#');
            string team1 = splitTeams[0];
            string team2 = splitTeams[2];

            // Same thing here except we target the scores inside the array and we split them and assign variables to them
            string[] splitScores = splitTeams[1].Split('@');
            int team1Score = Int16.Parse(splitScores[0]);
            int team2Score = Int16.Parse(splitScores[1]);

            // Declare winner and loser reference
            string winner = null;
            string loser = null;

            // Determine who won and who lost
            if (team1Score > team2Score)
            {
                winner = team1;
                loser = team2;

            }
            if (team1Score < team2Score)
            {
                winner = team2;
                loser = team1;
            }


            // Check if the team1 is inside our teams array
            // if not we add it to the array
            if (!teams.Contains(team1))
            {
                // loop through teams and check for empty spot
                for (int i = 0; i < teams.Length; i++)
                {
                    // if spot is empty and our team was not contained in array previously, we add our team to this location
                    if(teams[i] == null){
                        teams[i] = team1;
                        break;
                    }
                }
            }
            // Check if the team2 is inside our teams array
            // if not we add it to the array
            if (!teams.Contains(team2))
            {
                // loop through teams and check for empty spot
                for (int i = 0; i < teams.Length; i++)
                {
                    // if spot is empty and our team was not contained in array previously, we add our team to this location
                    if (teams[i] == null){
                        teams[i] = team2;
                        break;
                    }
                }
            }


            // Loop through the teams array and check if team1 or team2 matches one of the teams inside
            for (int i = 0;i < teams.Length; i++)
            {
                if (team1 == teams[i])
                {
                    // increment games, goals, enemygoals
                    scores[i][0] += 1;
                    scores[i][4] += team1Score;
                    scores[i][5] += team2Score;

                    //If no winner, increment tie's and total points earned
                    if (winner == null)
                    {
                        scores[i][2] += 1;
                        scores[i][6] += 1;
                    }
                    // if the winner is team1 increment victorys and total points
                    if (winner == team1)
                    {
                        scores[i][1] += 1;
                        scores[i][6] += 3;
                    }
                    // if the winner is team2 incriment losses
                    if (winner == team2)
                    {
                        scores[i][3] += 1;
                    }
                }
                if (team2 == teams[i])
                {
                    // increment games, goals, enemygoals
                    scores[i][0] += 1;
                    scores[i][4] += team2Score;
                    scores[i][5] += team1Score;
                    //If no winner, increment tie's and total points earned
                    if (winner == null)
                    {
                        scores[i][2] += 1;
                        scores[i][6] += 1;
                    }
                    // if the winner is team2 increment victorys and total points
                    if (winner == team2)
                    {
                        scores[i][1] += 1;
                        scores[i][6] += 3;
                    }
                    // if the winner is team1 incriment losses
                    if (winner == team1)
                    {
                        scores[i][3] += 1;
                    }
                }

            }

 
        }

        /// <summary>
        /// This function sorts everything into a properly formated string[] and send it back using previously acquired data
        /// </summary>
        /// <param name="teams">String array filled with each teams name</param>
        /// <param name="scores" 2D int array filled with each teams scores></param>
        /// <param name="matchName">The name of the stage in the game. Example: "Pewee League" </param>
        /// <returns></returns>
        public static string[] sortTeamArray(string[] teams, int[][] scores, string matchName)
        {
            // declare each array we will be using
            // tempGame will store the final string results formatted
            string[] tempGame = new string[teams.Length];
            // tempname is used as a new place to store team Names in the proper order before entering tempGame
            string[] tempName = new string[teams.Length];
            // temscore is used as a new place to store team Scores in the proper order before entering tempGame
            int[][] tempScores = new int[teams.Length][];


            // Loop through our score array to initialize 2d values as all 0's 
            for (int i = 0; i < teams.Length; i++)
            {
                tempScores[i] = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            }


            // Loop through until we reach the length of teams[] (Which is the number of teams we have)
            for (int i = 0; i < teams.Length; i++)
            {
                // Initialize variables to 0
                // entryIndex will hold the element of whatever value we would like to enter into new array
                int entryIndex = 0;
                int score = 0;

                // Nested loop for the length of teams array
                for (int j = 0; j < teams.Length; j++)
                {
                    // sort by points
                    // If statement checks if int Score variable is less than the score of the team inside nested loop
                    // and also checks if the name has already been added to our new array (To avoid putting a team in twice)
                    if (scores[j][6] >= score && !tempName.Contains(teams[j]))
                    {
                        // Update int Score variable
                        // Set index variable to the index of our newly added var
                        score = scores[j][6];
                        entryIndex = j;                                                                   
                    }
                }

                // Double check that we have not already added the team to the list
                if (!tempName.Contains(teams[entryIndex]))
                {
                    // Use index we got earlier to add our scores to the new temporary lists in the proper order
                    // That is, Highest point team is at tempScores[0], next highest is at tempScores[1], etc...
                    tempScores[i] = scores[entryIndex];
                    tempName[i] = teams[entryIndex];
                }
            }
            // Loops through sorted array and formates scores and name into a string before storing in tempGame
            for (int i = 0; i < teams.Length; i++)
            {
                string tempString = String.Format("{9}) {0} {1}p, {2}g ({3}-{4}-{5}), {6}gd ({7}-{8})", 
                    tempName[i], tempScores[i][6], tempScores[i][0], tempScores[i][1], tempScores[i][2], tempScores[i][3],
                    tempScores[i][4] - tempScores[i][5], tempScores[i][4],tempScores[i][5], i+1);
                tempGame[i] = tempString;
            }
            // Returns formatted list
            return tempGame;
        }
        static void Main(string[] args)
        {

        }
    }

}
