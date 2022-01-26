using System;

namespace Program360
{
    public class Soccer
    {
        public static string[] getRankings(string[] matches)
        {
            // Your program begins here!

            // Declare useful arrays, and values
            int uniqueTeam = uniqueTeams(matches);
            string matchName = matches[0];
            string[] teams = new string[uniqueTeam];
            int[][] scores = new int[uniqueTeam][];

            // initialize jagged array to contain score data
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



            string[] finalAwnswer = new string[teams.Length + 1];
            finalAwnswer[0] = matchName;

            string[] newScores = new string[teams.Length];
            newScores = sortTeamArray(teams, scores, matchName);

            for (int i = 0;i < newScores.Length; i++)
            {
                finalAwnswer[i+1] = newScores[i];
            }


            for (int i = 0; i < finalAwnswer.Length; i++)
            {
                System.Console.WriteLine(finalAwnswer[i]);
            }
            return finalAwnswer;
        }


        public static int uniqueTeams(string[] matches)
        {
            List<string> teams = new List<string>(2);

            for (int i = 0; i < matches.Length; i++)
            {
                if (i != 0)
                {
                    string tempMatch = matches[i];
                    string[] splitTeams = tempMatch.Split('#');
                    string team1 = splitTeams[0];
                    string team2 = splitTeams[2];

                    if (!teams.Contains(team1))
                    {
                        teams.Add(team1);
                    }
                    if (!teams.Contains(team2))
                    {
                        teams.Add((team2));
                    }

                }
            }

            return teams.Count;


        }


        // Takes teams[], scores[], and the current match we are looking at in the original array
        public static void sortMatch(string[] teams, int[][] scores, string testMatch)
        {
            // Splits into an array with both teams and the scores, assigns both teams reference variable
            string[] splitTeams = testMatch.Split('#');
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
        // This function needs to sort everything into a proper string[] and send it back using previously acquired data
        public static string[] sortTeamArray(string[] teams, int[][] scores, string matchName)
        {
            string[] tempGame = new string[teams.Length];


            string[] tempName = new string[teams.Length];
            int[][] tempScores = new int[teams.Length][];

            for (int i = 0; i < teams.Length; i++)
            {
                tempScores[i] = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            }


            int lastScore = 0;
            for (int i = 0; i < teams.Length; i++)
            {
                int index = 0;
                int score = 0;

                for (int j = 0; j < teams.Length; j++)
                {
                    // sort by points
                    if (scores[j][6] >= score && !tempName.Contains(teams[j]))
                    {
                        lastScore = scores[j][6];
                        score = scores[j][6];
                        index = j;                                                                   
                    }
                }


                if (!tempName.Contains(teams[index]))
                {
                    tempScores[i] = scores[index];
                    tempName[i] = teams[index];

                }
            }
           


            // Puts each string in almost proper format
            for (int i = 0; i < teams.Length; i++)
            {
                string tempString = String.Format("{9}) {0} {1}p, {2}g ({3}-{4}-{5}), {6}gd ({7}-{8})", 
                    tempName[i], tempScores[i][6], tempScores[i][0], tempScores[i][1], tempScores[i][2], tempScores[i][3],
                    tempScores[i][4] - tempScores[i][5], tempScores[i][4],tempScores[i][5], i+1);
                tempGame[i] = tempString;
            }
            return tempGame;
        }
        static void Main(string[] args)
        {
            string[] input = new string[] {
                "Pewee League",
                "Two#0@1#Three",
                "Two#11@11#One",
                "One#31@31#Two",
                "Two#0@0#One"
            };
            Program360.Soccer.getRankings(input);
        }
    }

}
