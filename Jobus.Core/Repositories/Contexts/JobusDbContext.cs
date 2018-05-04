using Jobus.Common.Extensions;
using Jobus.Core.Repositories.Contexts.TypeConfigurations;
using Jobus.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using System;
using System.Text.RegularExpressions;

namespace Jobus.Core.Repositories.Contexts
{
    public class JobusDbContext : DbContext
    {
        public JobusDbContext(DbContextOptions<JobusDbContext> options) : base(options)
        {

        }

        public DbSet<WsClient> WsClients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new WsClientConfiguration());
            //FixSnakeCaseNames(modelBuilder);
            SetSnakeCase(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        private void SetSnakeCase(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names
                foreach (var property in entity.GetProperties())
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach (var key in entity.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }
        }

        #region Snake Case longer version

        private static readonly Regex _keysRegex = new Regex("^(PK|FK|IX)_", RegexOptions.Compiled);

        private void FixSnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                ConvertToSnake(mapper, table);
                foreach (var property in table.GetProperties())
                {
                    ConvertToSnake(mapper, property);
                }

                foreach (var primaryKey in table.GetKeys())
                {
                    ConvertToSnake(mapper, primaryKey);
                }

                foreach (var foreignKey in table.GetForeignKeys())
                {
                    ConvertToSnake(mapper, foreignKey);
                }

                foreach (var indexKey in table.GetIndexes())
                {
                    ConvertToSnake(mapper, indexKey);
                }
            }
        }

        private void ConvertToSnake(INpgsqlNameTranslator mapper, object entity)
        {
            switch (entity)
            {
                case IMutableEntityType table:
                    var relationalTable = table.Relational();
                    relationalTable.TableName = ConvertGeneralToSnake(mapper, relationalTable.TableName);
                    if (relationalTable.TableName.StartsWith("asp_net_"))
                    {
                        relationalTable.TableName = relationalTable.TableName.Replace("asp_net_", string.Empty);
                        relationalTable.Schema = "identity";
                    }

                    break;
                case IMutableProperty property:
                    property.Relational().ColumnName = ConvertGeneralToSnake(mapper, property.Relational().ColumnName);
                    break;
                case IMutableKey primaryKey:
                    primaryKey.Relational().Name = ConvertKeyToSnake(mapper, primaryKey.Relational().Name);
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.Relational().Name = ConvertKeyToSnake(mapper, foreignKey.Relational().Name);
                    break;
                case IMutableIndex indexKey:
                    indexKey.Relational().Name = ConvertKeyToSnake(mapper, indexKey.Relational().Name);
                    break;
                default:
                    throw new NotImplementedException("Unexpected type was provided to snake case converter");
            }
        }

        private string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName) =>
            ConvertGeneralToSnake(mapper, _keysRegex.Replace(keyName, match => match.Value.ToLower()));

        private string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName) =>
            mapper.TranslateMemberName(ModifyNameBeforeConvertion(mapper, entityName));

        protected virtual string ModifyNameBeforeConvertion(INpgsqlNameTranslator mapper, string entityName) => entityName;

        #endregion
    }
}
