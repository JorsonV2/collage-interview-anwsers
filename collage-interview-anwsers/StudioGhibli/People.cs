using System.Collections.Generic;
using collage_interview_tasks;
using System.Net.Http;
using System;

namespace collage_interview_anwsers.StudioGhibli
{
    public class People
    {
        public string id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string eye_color { get; set; }
        public string hair_color { get; set; }
        public List<string> films { get; set; }
        public string species { get; set; }
        public string url { get; set; }

        public void WritePeople()
        {
            Console.WriteLine("---");
            Console.WriteLine($"name: {name}, gender: {gender}, age: {age}");
            Console.WriteLine($"eye color: {eye_color}, hair color: {hair_color}");
            Console.WriteLine("---");
        }
    }
    public class AllPeopleHandler : HttpRequestHandler<List<People>>
    {
        public AllPeopleHandler(HttpClient httpClientProxy, IHttpResponseDeserializer<List<People>> parser) : base(httpClientProxy, parser)
        {
        }
    }

    public class AllPeopleDeserializer : ResponseDeserializer<List<People>> { }

    public class PeopleHandler : HttpRequestHandler<People>
    {
        public PeopleHandler(HttpClient httpClientProxy, IHttpResponseDeserializer<People> parser) : base(httpClientProxy, parser)
        {
        }
    }

    public class PeopleDeserializer : ResponseDeserializer<People> { }
}
