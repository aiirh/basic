using Aiirh.DatabaseTools.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DataTable = Aiirh.Basic.Tables.DataTable;

namespace Aiirh.DatabaseTools
{
    public static class DbContextExtensions
    {
        public static async Task<List<T>> FromRawSqlAsync<T>(this DbContext context, string query, Func<DbDataReader, T> map)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            await context.Database.OpenConnectionAsync();
            var result = await command.ExecuteReaderAsync();
            var entities = new List<T>();
            while (await result.ReadAsync())
            {
                entities.Add(map(result));
            }

            return entities;
        }

        public static async Task<DataTable> FromRawSqlAsync(this DbContext context, string sql)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            context.Database.OpenConnection();

            using var reader = command.ExecuteReader();

            var headers = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                headers.Add(reader.GetName(i));
            }

            var rows = new List<object[]>();
            while (reader.Read())
            {
                var row = new object[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader.GetValue(i);
                }

                rows.Add(row);
            }

            var result = new DataTable
            {
                Headers = headers,
                Rows = rows
            };

            return result;
        }

        public static void UpdateAuditEntities<TContext>(this TContext context) where TContext : DbContext, IHasCurrentUserId
        {
            var modifiedEntries = context.ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = context.CurrentUserId;
                }
                else
                {
                    context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = context.CurrentUserId;
            }
        }

        public static void UpdateGuidKeyEntities(this DbContext context)
        {
            var modifiedEntries = context.ChangeTracker.Entries().Where(x => x.Entity is IHasGuidId && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in modifiedEntries)
            {
                var entity = (IHasGuidId)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.Id = Guid.NewGuid();
                }
                else
                {
                    context.Entry(entity).Property(x => x.Id).IsModified = false;
                }
            }
        }
    }
}
