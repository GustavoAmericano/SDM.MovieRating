using System;
using System.Security.Cryptography.X509Certificates;
using SDM.MovieRating.BLL.Implementation;
using SDM.MovieRating.DAL;

namespace MovieReveiwerConsole
{
    public class PrintClass
    {
        DataContext _ctx = new DataContext();
        MovieLogic ml;
        ReviewerLogic rl;
        
        public PrintClass()
        {
            ml = new MovieLogic(_ctx);
            rl = new ReviewerLogic(_ctx);
        }

        public void Start()
        {
            var selection = "";
            while (true)
            {
                Console.WriteLine($"Please enter the number of the thing you want to see\n" +
                                  $"1. Movies\n" +
                                  $"2. Reviewers");
                selection = Console.ReadLine();

                switch (selection)
                {
                        case "1":
                            OpenMovieMenu();
                            break;
                        case "2":
                            OpenReviewerMenu();
                            break;
                        default:
                            Console.WriteLine("Enter a vaild option");
                            break;
                }
            }
        }

        private void OpenReviewerMenu()
        {
            throw new NotImplementedException();
        }

        private void OpenMovieMenu()
        {
            var selection = "";
            Console.WriteLine($"Enter the number of the option you want to see\n" +
                              $"1. Get reviews\n" +
                              $"2. Get average rating\n" +
                              $"3. Get the number of time a rating has been given to a movie\n" +
                              $"4. Get the top rated movie(s)\n" +
                              $"5. Get the top movies\n" +
                              $"6. Get a list of reviewers for a movie\n");
            selection = Console.ReadLine();

            switch (selection)
            {
                    case "1":
                        printGetReviews();
                        break;
                    case "2":
                        printGetAveragerating();
                        break;
                    case "3":
                        printNumberOfTimesRatingGiven();
                        break;
                    case "4":
                        printGetTopRatedMovies();
                        break;
                    case "5":
                        printGetTopMovies();
                        break;
                    case "6":
                        printGetListOfReviewrs();
                        break;
                    
            }
        }

        private void printGetListOfReviewrs()
        {
            throw new NotImplementedException();
        }

        private void printGetTopMovies()
        {
            throw new NotImplementedException();
        }

        private void printGetTopRatedMovies()
        {
            var selection = "";
            var selection2 = "";
            Console.WriteLine($"Enter the id of the movie you want the reviews for");
            selection = Console.ReadLine();
            
            Console.ReadLine();
        }

        private void printNumberOfTimesRatingGiven()
        {
            var selection = "";
            var selection2 = "";
            Console.WriteLine($"Enter the id of the movie");
            selection = Console.ReadLine();
            Console.WriteLine($"Enter the rating");
            selection2 = Console.ReadLine();
            Console.WriteLine($"Id: {selection} times the rating {selection2} has been given {ml.GetTimesRatingGiven(int.Parse(selection), int.Parse(selection2))}");
            Console.ReadLine();
        }

        private void printGetAveragerating()
        {
            var selection = "";
            Console.WriteLine($"Enter the id of the movie you want the reviews for");
            selection = Console.ReadLine();
            Console.WriteLine($"Id: {selection} Rating {ml.GetAverageRating(int.Parse(selection))}");
            Console.ReadLine();
        }

        private void printGetReviews()
        {
            var selection = "";
            Console.WriteLine($"Enter the id of the movie you want the reviews for");
            selection = Console.ReadLine();
            ml.GetReviews(int.Parse(selection)).ForEach(x => Console.WriteLine($"Movie Id: {x.MovieId} Reviewer Id: {x.ReviewerId} Date of review: {x.Date} Rating: {x.Rating}"));
            Console.ReadLine();
        }
    }
}