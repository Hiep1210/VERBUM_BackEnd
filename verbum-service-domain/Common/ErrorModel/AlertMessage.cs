namespace verbum_service_domain.Common.ErrorModel
{
    public class AlertMessage
    {
        public static string Alert(string alertCode, params string[] parameter)
        {
            return string.Format(alertCode, parameter);
        }
    }
}
