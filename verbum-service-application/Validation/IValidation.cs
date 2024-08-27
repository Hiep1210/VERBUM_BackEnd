using verbum_service_domain.Models.ErrorModel;

namespace verbum_service_application.Validation
{
    internal interface IValidation<T>
    {
        public List<AlertMessage> Validate(T entity);
    }
}
