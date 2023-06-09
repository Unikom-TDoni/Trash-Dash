namespace Group8.TrashDash.Level
{
    public struct LevelEntity
    {
        public readonly int Level;

        public readonly float HighScore;

        public LevelEntity(int level, float highScore) =>
            (Level, HighScore) = (level, highScore);
    }
}