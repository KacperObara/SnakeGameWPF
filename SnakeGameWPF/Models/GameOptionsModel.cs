using System.Windows.Media;

namespace SnakeGameWPF.Models
{
    public enum Difficulty
    {
        Low = 200000,
        Medium = 150000,
        Hard = 50000,
        Year2020 = 10000
    }

    public class GameOptionsModel
    {
        public Difficulty Difficulty { get; set; }
        public int SnakeLength { get; set; }
        public int AppleCount { get; set; }
        public bool BackgroundsEnabled { get; set; }
        public Brush SnakeColor { get; set; }
    }
}
