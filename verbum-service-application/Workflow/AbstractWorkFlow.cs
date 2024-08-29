namespace verbum_service_application.Workflow
{
    public abstract class AbstractWorkFlow<T> : IWorkFlow<T>
    {
        public async Task process(T entity)
        {
            await PreStep(entity);
            await ValidationStep(entity);
            await CommonStep(entity);
            await PostStep(entity);
        }

        protected abstract Task PreStep(T request);
        protected abstract Task ValidationStep(T request);
        protected abstract Task CommonStep(T request);
        protected abstract Task PostStep(T request);
    }
}
