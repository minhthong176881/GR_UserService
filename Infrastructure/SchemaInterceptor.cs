using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace UserService.Infrastructure
{
    public class SchemaInterceptor : DbCommandInterceptor
    {
        private readonly TenantInfo _tenantInfo;

        public SchemaInterceptor(TenantInfo tenantInfo)
        {
            _tenantInfo = tenantInfo;
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            command.CommandText = $"USE user_service_db {command.CommandText}";
            command.CommandText = command.CommandText
                .Replace("FROM ", $" FROM {_tenantInfo.Name}.")
                .Replace("JOIN ", $" JOIN {_tenantInfo.Name}.");

            return base.ReaderExecuting(command, eventData, result);
        }
    }
}