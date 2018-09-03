using NetCoreFramework.Domain.Interfaces;


namespace NetCoreFramework.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
