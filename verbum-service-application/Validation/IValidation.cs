using verbum_service_domain.Common.ErrorModel;

namespace verbum_service_application.Validation
{
    public interface IValidation<T>
    {
        public List<string> Validate(T request);
    }
}
