using Newtonsoft.Json;

namespace Assets._Source.CodeBase.Meta.Services.JsonManager
{
    public class JsonConvector : IJsonConvector
    {
        public GameConfigData Get(string json)
        {
            GameConfigData gameConfigData =
                JsonConvert.DeserializeObject<GameConfigData>(json);

            return gameConfigData;
        }
    }
}