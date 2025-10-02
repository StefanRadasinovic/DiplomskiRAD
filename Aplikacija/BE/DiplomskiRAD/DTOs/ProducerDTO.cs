namespace DiplomskiRAD.DTOs
{
    public class ProducerDTO
    {
        public class ProducerInfo
        {

            public string Name { get; set; }
            public string? Description { get; set; }

            public ProducerInfo(string name, string? description)
            {
                Name = name;
                Description = description;
            }

            public ProducerInfo() { }

            public ProducerInfo(string name)
            {
                Name = name;
            }
        }

        public class CreateProducerDto
        {
            public string Name { get; set; }
            public string? Description { get; set; }
        }

        public class UpdateProducerDto
        {
            public string Name { get; set; }
            public string? Description { get; set; }
        }

    }
}
