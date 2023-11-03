namespace MauiMultiWindowTest.Common
{
    public static class Extensions
    {
        public static string AddParameterToUri(this string url, string parameterName, string parameterValue)
        {
            if (url.Contains("?"))
            {
                return $"{url}&{parameterName}={parameterValue}";
            }
            else
            {
                return $"{url}?{parameterName}={parameterValue}";
            }
        }

        public static string AddParametersToUri(this string url, Dictionary<string, string> parameters)
        {
            var result = url;

            if (parameters.Any())
            {
                foreach (var p in parameters)
                {
                    result = result.AddParameterToUri(p.Key, p.Value);
                }
            }

            return result;
        }


    }
}
