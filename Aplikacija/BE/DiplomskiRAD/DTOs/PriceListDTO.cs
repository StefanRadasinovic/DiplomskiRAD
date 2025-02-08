using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.DTOs
{
    public class PriceListDTO
    {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        public class PriceListInfo
        {
            public Guid Id { get; set; }

            public double Price { get; set; }

            public string StartingDate { get; set; } // Change to string

            public string EndingDate { get; set; }   // Change to string

            public List<MotorcycleInfo> Motorcycles { get; set; }

<<<<<<< Updated upstream
            public PriceListInfo(Guid id ,double price, string startingDate, string endingDate, List<MotorcycleInfo> motorcycles)
            {
                Id = id;
=======
            public PriceListInfo(double price, string startingDate, string endingDate, List<MotorcycleInfo> motorcycles)
            {
>>>>>>> Stashed changes
                Price = price;
                StartingDate = startingDate;
                EndingDate = endingDate;
                Motorcycles = motorcycles;
            }

<<<<<<< Updated upstream
            public PriceListInfo(Guid id, double price, string startingDate, string endingDate)
            {
                Id = id;
=======
            public PriceListInfo(double price, string startingDate, string endingDate)
            {
>>>>>>> Stashed changes
                Price = price;
                StartingDate = startingDate;
                EndingDate = endingDate;
            }

<<<<<<< Updated upstream
            public PriceListInfo( double price)
            {
                Price = price;
         
            }


        }

=======
            public PriceListInfo(double price)
            {
                Price = price;
            }

            public PriceListInfo() { }
        }



>>>>>>> Stashed changes
        public class CreatePriceListDto
        {
            public double Price { get; set; }

            public string StartingDate { get; set; } 

            public string EndingDate { get; set; } 
        }

<<<<<<< Updated upstream
        public class UpdatePriceListDto
        {
            public Guid Id { get; set; }
            public double Price { get; set; }

            public DateTime StartingDate { get; set; }

            public DateTime EndingDate { get; set; }

        }

        public class PriceOnly
        {
          
            public double Price { get; set; }
           
            public PriceOnly( double price)
            {
                
                Price = price;
                
            }
        }

=======
>>>>>>> Stashed changes
    }
}
