using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zo_organized.ItemSingular.Domain.Aggregates;
using zo_organized.ItemSingular.Domain.Commands;
using zo_organized.ItemSingular.Domain.ValueObjects;
using zo_organized.Shared;
using zo_organized.Shared.Interfaces;

namespace zo_organized.ItemSingular.Infrastructure.CommandHandlers
{
    public class ItemSingularCommands : IItemSingularCommands<ItemSingularAggregate>
    {
        private readonly ISqlDataAccess<ItemSingularAggregate> _db;

        public ItemSingularCommands(ISqlDataAccess<ItemSingularAggregate> db)
        {
            _db = db;
        }

        public Task<IAggregateRoot> Delete(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<IAggregateRoot> GetAll(IAggregateRoot aggregate)
        {
            var result = await _db.GetListDataValueObject<ItemSingularDetail>((ItemSingularAggregate)aggregate, aggregate.StoredProcedureName, aggregate.DbConnectionName);
            if (result?.ItemSingularList?.Count() > 0)
            {
                result.AddGetAllItemPluralSuccessEvent();
                await new DomainEventsDispatcher().Dispatch(result);
            }
            return result;
        }

        public async Task<IAggregateRoot> GetById(IAggregateRoot aggregate)
        {
            var result = await _db.GetDataValueObject<ItemSingularDetail, dynamic>((ItemSingularAggregate)aggregate, aggregate.StoredProcedureName, new { ItemSingularId = aggregate.UniqueId }, aggregate.DbConnectionName);
            if (!string.IsNullOrEmpty(result?.ItemSingularInformation?.ItemSingularName))
            {
                result.AddGetItemSingularByIdSuccessEvent();
                await new DomainEventsDispatcher().Dispatch(result);
            }
            return result;
        }

        public async Task<IAggregateRoot> Insert(IAggregateRoot aggregate)
        {
            var ItemSingularAggregate = (ItemSingularAggregate)aggregate;
            var result = await _db.GetDataValueObject<ItemSingularDetail, dynamic>(ItemSingularAggregate, ItemSingularAggregate.StoredProcedureName,
                new { Name = ItemSingularAggregate.ItemSingularInformation.ItemSingularName, Description = ItemSingularAggregate.ItemSingularInformation.Description, Nickname = ItemSingularAggregate.ItemSingularInformation.ItemSingularNickname, ItemSingularAggregate.ItemSingularInformation.IsActive }, 
                ItemSingularAggregate.DbConnectionName);
            if (result?.ItemSingularInformation?.ItemSingularId != Guid.Empty)
            {
                result?.AddItemSingularSuccessEvent();
                await new DomainEventsDispatcher().Dispatch(result);
            }
            return result;
        }

        public Task<IAggregateRoot> Update(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
