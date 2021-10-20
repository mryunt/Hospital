namespace AppCore.Entity
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}
