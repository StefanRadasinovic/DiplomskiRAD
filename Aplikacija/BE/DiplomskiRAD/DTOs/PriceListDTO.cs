using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.DTOs
{
    public class PriceListDTO
    {

        public class PriceListInfo
        {
            public Guid Id { get; set; }

            public double Price { get; set; }

            public string StartingDate { get; set; } // Change to string

            public string EndingDate { get; set; }   // Change to string

            public List<JustMotorcycleName> JustMotorcycleNames { get; set; }


            public PriceListInfo(Guid id, double price, string startingDate, string endingDate, List<JustMotorcycleName> justMotorcycleNames)
            {
                Id = id;
                Price = price;
                StartingDate = startingDate;
                EndingDate = endingDate;
                JustMotorcycleNames = justMotorcycleNames;
            }


            public PriceListInfo(Guid id, double price, string startingDate, string endingDate)
            {
                Id = id;
                Price = price;
                StartingDate = startingDate;
                EndingDate = endingDate;
            }
        }

        public class CreatePriceListDto
        {
            public double Price { get; set; }

            public string StartingDate { get; set; } 

            public string EndingDate { get; set; } 
        }

        public class UpdatePriceListDto
        {
            public Guid Id { get; set; }
            public double Price { get; set; }

            public DateTime StartingDate { get; set; }

            public DateTime EndingDate { get; set; }

        }

        public class DisplayPriceOnly
        {
          
            public double Price { get; set; }
           
            public DisplayPriceOnly( double price)
            {
                
                Price = price;
                
            }
        }

        public class PriceListInfo22
        {
            public Guid Id { get; set; }

            public double Price { get; set; }

            public string StartingDate { get; set; } // Change to string

            public string EndingDate { get; set; }   // Change to string

            public List<JustEquipmentName> JustEquipmentName { get; set; }


            public PriceListInfo22(Guid id, double price, string startingDate, string endingDate, List<JustEquipmentName> justEquipmentNames)
            {
                Id = id;
                Price = price;
                StartingDate = startingDate;
                EndingDate = endingDate;
                JustEquipmentName = justEquipmentNames;
            }
        }

    }
}
