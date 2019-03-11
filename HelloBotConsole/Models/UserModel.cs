namespace HelloBotConsole.Models
{
    public class UserModel
    {
        public int? Id {set; get; }
        public string Name { get; set; }
        public int? Age { get; set; }

        public UserModel()
        {
            this.Id = null;
            this.Name = null;
            this.Age = null;
        }
        
    }
}