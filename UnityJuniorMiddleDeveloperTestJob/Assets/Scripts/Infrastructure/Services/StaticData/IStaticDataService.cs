namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LevelStaticData ForLevel(string level);
    }
}