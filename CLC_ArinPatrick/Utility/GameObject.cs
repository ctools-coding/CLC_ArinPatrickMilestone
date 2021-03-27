namespace Minesweeper_ArinPatrick.Utility
{
    public class GameObject
    {
        public int Id { get; set; }
        public string JSONString { get; set; }

        public GameObject(int id, string jSONString)
        {
            Id = id;
            JSONString = jSONString;
        }
    }
}