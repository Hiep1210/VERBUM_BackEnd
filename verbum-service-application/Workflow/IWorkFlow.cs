namespace verbum_service_application.Workflow
{
    public interface IWorkFlow<T>
    {
        void process(T entity);
    }
}
