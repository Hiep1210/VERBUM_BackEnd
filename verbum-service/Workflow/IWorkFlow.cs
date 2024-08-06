namespace verbum_service.Workflow
{
    public interface IWorkFlow<T>
    {
        void process(T entity);
    }
}
