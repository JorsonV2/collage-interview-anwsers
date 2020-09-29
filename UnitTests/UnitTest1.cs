using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using collage_interview_anwsers.StudioGhibli;
using System.Net.Http;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class HandlerTests
    {
        [TestMethod]
        public void GetFilmsTest()
        {
            HttpClient client = new HttpClient();
            FilmsDeserializer filmsDeserializer = new FilmsDeserializer();
            FilmsHandler filmsHandler = new FilmsHandler(client, filmsDeserializer);
            List<Film> films = filmsHandler.Handle(HttpMethod.Get, "https://ghibliapi.herokuapp.com/films", null, null);

            Assert.AreEqual(20, films.Count, "Wrong number of films");
        }

        [TestMethod]
        public void GetRightFilmTitle()
        {
            HttpClient client = new HttpClient();
            FilmDeserializer filmDeserializer = new FilmDeserializer();
            FilmHandler filmHandler = new FilmHandler(client, filmDeserializer);
            Film film = filmHandler.Handle(HttpMethod.Get, "https://ghibliapi.herokuapp.com/films/578ae244-7750-4d9f-867b-f3cd3d6fecf4", null, null);

            Assert.AreEqual("The Tale of the Princess Kaguya", film.title, "Wrong title");
        }

        [TestMethod]
        public void GetRightEyeColor()
        {
            HttpClient client = new HttpClient();
            PeopleDeserializer peopleDeserializer = new PeopleDeserializer();
            PeopleHandler peopleHandler = new PeopleHandler(client, peopleDeserializer);
            People people = peopleHandler.Handle(HttpMethod.Get, "https://ghibliapi.herokuapp.com/people/fcb4a2ac-5e41-4d54-9bba-33068db083ca", null, null);

            Assert.AreEqual("Dark brown", people.eye_color, "Wrong eye color");
        }
    }
}
