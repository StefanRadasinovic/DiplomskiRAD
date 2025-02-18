using DiplomskiRAD.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiRAD.DTOs
{
    public class ReviewDTO
    {
        public class ReviewInfo 
        { 
            public string Comment { get; set; }

            public int Grade { get; set; }
        }

        public class CreateReviewDto
        {
            public string Comment { get; set; }

            public int Grade { get; set; }

        }
    }
}
