using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zo_organized.ItemSingular.Domain.Aggregates;
using zo_organized.ItemSingular.Domain.Commands;
using zo_organized.ItemSingular.Domain.Repositories;

namespace zo_organized.ItemSingular.Infrastructure.Repositories
{
    public class ItemSingularRespository : IItemSingularRepository<ItemSingularAggregate>
    {
        private readonly IItemSingularCommands<ItemSingularAggregate> _command;

        public ItemSingularRespository(IItemSingularCommands<ItemSingularAggregate> command)
        {
            _command = command;
        }

        public async Task<ItemSingularAggregate> AddItemSingular(ItemSingularAggregate aggregate)
        {
            return (ItemSingularAggregate)await _command.Insert(aggregate);
        }

        public async Task<ItemSingularAggregate> GetAllItemPlural(ItemSingularAggregate aggregate)
        {
            return (ItemSingularAggregate)await _command.GetAll(aggregate);
        }

        public async Task<ItemSingularAggregate> GetItemSingularById(ItemSingularAggregate aggregate)
        {
            return (ItemSingularAggregate)await _command.GetById(aggregate);
        }
    }
}
