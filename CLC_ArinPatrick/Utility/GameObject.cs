namespace Minesweeper_ArinPatrick.Utility
{
    public class GameObject
    {
        public int Id { get; set; }
        public string JSONString { get; set; }
        public string Username { get; set; }
        public GameObject(int id, string jSONString, string username)
        {
            Id = id;
            JSONString = jSONString;
            Username = username;
        }
    }
}