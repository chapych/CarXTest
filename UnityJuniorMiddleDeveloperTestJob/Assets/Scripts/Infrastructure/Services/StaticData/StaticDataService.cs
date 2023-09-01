using System.Collections.Generic;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_PATH = "StaticData/";
        private Dictionary<string, LevelStaticData> levels;

        public void Load()
        {
            //  levels = LoadFromResources<string, LevelStaticData>("Levels", x => x.levelName);
        }

        public LevelStaticData ForLevel(string level) => levels[level];
    }
}


