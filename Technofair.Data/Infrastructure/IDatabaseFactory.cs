using System;

namespace TFSMS.Admin.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        TechnofairContext Get();
    }
}
