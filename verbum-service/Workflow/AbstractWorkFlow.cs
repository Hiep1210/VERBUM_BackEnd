namespace verbum_service.Workflow
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

        protected abstract void PreStep(T entity);
        protected abstract void ValidationStep(T entity);
        protected abstract void CommonStep(T entity);
        protected abstract void PostStep(T entity);
    }
}
