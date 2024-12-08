
namespace SmartTravel.Shared.Helper
{
    public interface IBaseMapping<TEntity, TModel>
    {
        TEntity ToEntity(TModel model);
        TEntity ToEntity(TEntity entity, TModel model);
        TModel ToModel(TEntity entity);
        TModel ToReferedModel(TModel existedModel, TEntity entity);
        TModel ToReferedModel(TModel existedModel, IEnumerable<TEntity> entities);
        TModel ToReferedModel(TModel existedModel, IEnumerable<IEnumerable<TEntity>> collectionOfEntities);
        IEnumerable<TModel> ToListModels(IEnumerable<TEntity> entities);
        IEnumerable<TModel> ToListReferedModels(IEnumerable<TModel> existedModels, IEnumerable<TEntity> entities);
        IEnumerable<TModel> ToListReferedModels(IEnumerable<TModel> existedModels, IEnumerable<IEnumerable<TEntity>> collectionOfEntities);
    }
}
