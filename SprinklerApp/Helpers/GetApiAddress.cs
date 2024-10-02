
namespace SprinklerApp.Helpers
{
    public class GetApiAddress
    {
        public enum ApiType
        {
            Sprinkler,
            Tank
            //TODO: Add more API types if needed
        }

        public static string GetAddress(ApiType apiType, long? id = null) 
        {
            switch (apiType)
            {
                case ApiType.Sprinkler:
                    if (id == null)
                        return $"{ApiSettings.Instance.ApiAddress}/Sprinkler";
                    else
                        return $"{ApiSettings.Instance.ApiAddress}/Sprinkler/{id}";
                case ApiType.Tank:
                    if (id == null) 
                        return $"{ApiSettings.Instance.ApiAddress}/Tank";
                    else
                        return $"{ApiSettings.Instance.ApiAddress}/Tank/{id}";
                //TODO: Add more cases for other API 
                default:
                    throw new ArgumentException("Invalid API type.");
            }
        }
    }

}
