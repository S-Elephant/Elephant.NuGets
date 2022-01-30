using System.Diagnostics.CodeAnalysis;
using Elephant.Common;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database
{
    /// <summary>
    /// A generic CRUD repository base class that also includes retrieving an entity by id.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileNameMustMatchTypeName", Justification = "Local interface.")]
    public interface IGenericCrudRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all.
        /// </summary>
        Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken);

        /// <summary>
        /// Get by id.
        /// </summary>
        Task<IResultStatus<TEntity>> ById(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete by id.
        /// </summary>
        void Delete(object id);

        /// <summary>
        /// Delete by id and save.
        /// </summary>
        Task<IResultStatus<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Insert.
        /// </summary>
        Task Insert(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Insert and save.
        /// </summary>
        Task<IResultStatus<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Save.
        /// </summary>
        Task<IResultStatus<int>> Save(CancellationToken cancellationToken);

        /// <summary>
        /// Update.
        /// </summary>
        void Update(TEntity obj);

        /// <summary>
        /// UpdateAndSave
        /// </summary>
        Task<IResultStatus<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);
    }

    /// <summary>
    /// A generic CRUD repository base class that also includes retrieving an entity by id.
    /// </summary>
    public abstract class GenericCrudRepository<TEntity, TContext> : IGenericCrudRepository<TEntity>
        where TEntity : class
        where TContext : IContext
    {
        /// <summary>
        /// Main database context.
        /// </summary>
        protected TContext Context { get; private set; }

        /// <summary>
        /// The table/DbSet to use for this repository.
        /// </summary>
        protected DbSet<TEntity> Table { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudRepository{T}"/> class.
        /// </summary>
        public GenericCrudRepository(TContext mainContext)
        {
            Context = mainContext;
            Table = mainContext.Set<TEntity>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken) => await Table.ToListAsync(cancellationToken);

        /// <summary>
        /// Note that the object[] array is required because otherwise it will consider the cancellationToken as the second id field in the params list. See: https://stackoverflow.com/questions/55758059/get-error-entity-type-course-is-defined-with-a-single-key-property-but-2-va
        /// </summary>
        public async Task<IResultStatus<TEntity>> ById(object id, CancellationToken cancellationToken)
        {
            TEntity? entityFound = await Table.FindAsync(new object[] { id }, cancellationToken);

            if (entityFound == null)
                return new ResultStatus<TEntity>(404, $"Entity {typeof(TEntity).FullName} not found in database. Supplied Id was: {id}");

            return new ResultStatus<TEntity>(entityFound);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task Insert(TEntity obj, CancellationToken cancellationToken) => await Table.AddAsync(obj, cancellationToken);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IResultStatus<int>> Save(CancellationToken cancellationToken)
        {
            int recordsAffectedCount = await Context.SaveChangesAsync(cancellationToken);
            return new ResultStatus<int>(recordsAffectedCount);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            Table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Delete(object id)
        {
            TEntity? existing = Table.Find(id);
            if (existing != null)
                Table.Remove(existing);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IResultStatus<int>> DeleteAndSave(object id, CancellationToken cancellationToken)
        {
            Delete(id);
            return await Save(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IResultStatus<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken)
        {
            await Insert(obj, cancellationToken);
            return await Save(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<IResultStatus<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken)
        {
            Update(obj);
            return await Save(cancellationToken);
        }
    }
}