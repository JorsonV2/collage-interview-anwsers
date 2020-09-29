using System.Collections.Generic;
using collage_interview_tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace collage_interview_anwsers.StudioGhibli
{
    public class Film
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string release_date { get; set; }
        public string rt_score { get; set; }
        public List<string> people { get; set; }
        public List<string> species { get; set; }
        public List<string> locations { get; set; }
        public List<string> vehicles { get; set; }
        public string url { get; set; }

        public void WriteFilm(HttpClient client)
        {
            Console.WriteLine("---");
            Console.WriteLine($"title: {title}, director: {director}, producer: {producer}");
            Console.WriteLine($"description: {description}");
            Console.WriteLine($"release date: {release_date}, score: {rt_score}");
            Console.WriteLine("---");
        }
    }

    public class FilmsHandler : HttpRequestHandler<List<Film>>
    {
        public FilmsHandler(HttpClient httpClientProxy, IHttpResponseDeserializer<List<Film>> parser) : base(httpClientProxy, parser)
        {
        }
    }

    public class FilmsDeserializer : ResponseDeserializer<List<Film>> { }

    public class FilmHandler : HttpRequestHandler<Film>
    {
        public FilmHandler(HttpClient httpClientProxy, IHttpResponseDeserializer<Film> parser) : base(httpClientProxy, parser)
        {
        }
    }

    public class FilmDeserializer : ResponseDeserializer<Film> { }

}
