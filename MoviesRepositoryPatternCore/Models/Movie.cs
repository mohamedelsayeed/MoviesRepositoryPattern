using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryPattern.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(250)]
        public string StoryLine { get; set; }
        public string Poster { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
