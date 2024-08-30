namespace verbum_service_application.Workflow
{
    public interface IWorkFlow<T>
    {
        Task process(T entity);
    }
}
