using System;
using System.Collections.Generic;
using System.Net.Http;
using collage_interview_tasks;
using collage_interview_anwsers.StudioGhibli;
using Newtonsoft.Json;

namespace collage_interview_anwsers
{
    class Program
    {
        public const string GHIBLI_SITE = "https://ghibliapi.herokuapp.com/";
        

        static void Main(string[] args)
        {

            HttpClient client = new HttpClient();

            UI(client);
        }

        static void UI(HttpClient client)
        {
            while (true)
            {
                Console.WriteLine("Type number to choose command:");
                Console.WriteLine("1. Show all films id and title");
                Console.WriteLine("2. Show film by id");
                Console.WriteLine("3. Show all people id and name");
                Console.WriteLine("4. Show people by id");
                Console.WriteLine("5. End program");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        FilmsDeserializer filmsParser = new FilmsDeserializer();
                        FilmsHandler filmsHandler = new FilmsHandler(client, filmsParser);
                        WriteAllFilmsIdAndTitle(filmsHandler.Handle(HttpMethod.Get, GHIBLI_SITE + "films", null, null));
                        break;
                    case "2":
                        Console.WriteLine("Enter film id:");
                        var filmId = Console.ReadLine();
                        FilmDeserializer filmParser = new FilmDeserializer();
                        FilmHandler filmHandler = new FilmHandler(client, filmParser);
                        filmHandler.Handle(HttpMethod.Get, $"{GHIBLI_SITE}films/{filmId}", null, null).WriteFilm(client);
                        break;
                    case "3":
                        AllPeopleDeserializer allPeopleDeserializer = new AllPeopleDeserializer();
                        AllPeopleHandler allPeopleHandler = new AllPeopleHandler(client, allPeopleDeserializer);
                        WriteAllPeopleIdAndName(allPeopleHandler.Handle(HttpMethod.Get, GHIBLI_SITE + "people", null, null));
                        break;
                    case "4":
                        Console.WriteLine("Enter people id:");
                        var peopleId = Console.ReadLine();
                        PeopleDeserializer peopleDeserializer = new PeopleDeserializer();
                        PeopleHandler peopleHandler = new PeopleHandler(client, peopleDeserializer);
                        peopleHandler.Handle(HttpMethod.Get, $"{GHIBLI_SITE}people/{peopleId}", null, null).WritePeople();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Wrong option! Try again.");
                        break;
                }

                Console.WriteLine();

            }
        }

        public static void WriteAllFilmsIdAndTitle(List<Film> films)
        {
            foreach (Film film in films)
            {
                Console.WriteLine($"id: {film.id}, title: {film.title}");
            }
        }

        public static void WriteAllPeopleIdAndName(List<People> allPeople)
        {
            foreach (People people in allPeople)
            {
                Console.WriteLine($"id: {people.id}, title: {people.name}");
            }
        }
    }

    public class ResponseDeserializer<TResponse> : HttpRequestHandler<TResponse>.IHttpResponseDeserializer<TResponse>
    {
        public TResponse Deserialize(HttpResponseMessage response)
        {
            string json = response.Content.ReadAsStringAsync().Result;
            TResponse deserializedObject = JsonConvert.DeserializeObject<TResponse>(json);
            return deserializedObject;
        }
    }
}
