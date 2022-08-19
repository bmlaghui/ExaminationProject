using System;
using System.Data.Entity;
using System.Linq;

namespace WebApplicationRestApiPizza_1.Models
{
    public class Question
    {
        public string questionText { get; set; }
        public string[] options { get; set; }
    }
}