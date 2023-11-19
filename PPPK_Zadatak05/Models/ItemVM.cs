namespace PPPK_Zadatak05.Models
{
    public class ItemVM
    {
        // Properties from Item
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public string PersonId { get; set; }

        // Properties from Person
        public string FirstName { get; set; }
        public string LastName { get; set; }

       
    }
}
