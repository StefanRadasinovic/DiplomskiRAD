using DiplomskiRAD.Enums;

namespace DiplomskiRAD.DTOs
{
    public class SparePartDTO
    {

        public class SparePartInfo
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public double Amount { get; set; }

            public IsSpartPartUsed IsSpartPartUsed { get; set; }
        }

        public class UsedSparePartDto
        {
            public string Name { get; set; }

            public double Amount { get; set; }

        }
    }
}
