namespace Model.DTO.Base
{
    public interface EnableAllValue<TEntity> where TEntity : BaseModel
    {
        TEntity GetAllValuedEntity();
    }
}