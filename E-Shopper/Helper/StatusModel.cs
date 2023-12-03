namespace E_Shopper.Helper
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusModel()
        {

        }
        public StatusModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
