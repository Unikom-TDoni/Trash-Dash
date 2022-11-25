namespace Group8.TrashDash.Level
{
    public struct LevelEntity
    {
        public readonly uint Level;

        public readonly bool IsOpened;

        public readonly float HighScore;

        public LevelEntity(uint level, float highScore, bool isOpened) =>
            (Level, HighScore, IsOpened) = (level, highScore, isOpened);
    }
}
