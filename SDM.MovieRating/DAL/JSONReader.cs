using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.DAL
{
    public class JSONReader
    {
        private const string FILE_NAME = @"C:\Users\kenne\Downloads\ratings.json"; // Change the path

        public List<MovieReview> ReadJSON()
        {
            List<MovieReview> reviews = new List<MovieReview>();

            Console.Write("Converting Json file to objects... ");

            Stopwatch sw = Stopwatch.StartNew();

            using (StreamReader streamReader = new StreamReader(FILE_NAME))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                try
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            MovieReview mr = ReadOneMovieReview(reader);
                            reviews.Add(mr);
                        }
                    }
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            sw.Stop();
            Console.WriteLine("Done. Time = {0:f4} sec.", sw.ElapsedMilliseconds / 1000d);

            return reviews;
        }

        private MovieReview ReadOneMovieReview(JsonTextReader reader)
        {
            MovieReview m = new MovieReview();
            for (int i = 0; i < 4; i++)
            {
                reader.Read();
                switch (reader.Value)
                {
                    case "Reviewer":
                        m.ReviewerId = (int) reader.ReadAsInt32();
                        break;
                    case "Movie":
                        m.MovieId = (int) reader.ReadAsInt32();
                        break;
                    case "Grade":
                        m.Rating = (int) reader.ReadAsInt32();
                        break;
                    case "Date":
                        m.Date = (DateTime) reader.ReadAsDateTime();
                        break;
                    default: throw new InvalidDataException("no such token: " + reader.Value);
                }
            }

            return m;
        }
    }
}