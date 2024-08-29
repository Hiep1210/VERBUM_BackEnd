namespace verbum_service_application.Workflow
{
    public abstract class AbstractWorkFlow<T> : IWorkFlow<T>
    {
        public void process(T entity)
        {
            PreStep(entity);
            ValidationStep(entity);
            CommonStep(entity);
            PostStep(entity);
        }

        protected abstract void PreStep(T request);
        protected abstract void ValidationStep(T request);
        protected abstract void CommonStep(T request);
        protected abstract void PostStep(T request);
    }
}
