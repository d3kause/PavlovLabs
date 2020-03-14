using System;
using System.Linq;
using PavlovLabs.Models;
using System.Collections.Generic;

namespace PavlovLabs.Storage
{
public class MemCache : IStorage<CarRepairData>
   {
       private object _sync = new object();

       private List<CarRepairData> _memCache = new List<CarRepairData>();

       public CarRepairData this[Guid id]
       {
           get
           {
               lock (_sync)
               {
                   if (!Has(id)) throw new IncorrectCarRepairExeption($"No CarRepairData with id {id}");
                   return _memCache.Single(x => x.Id == id);
               }
           }

           set
           {
               if (id == Guid.Empty) throw new IncorrectCarRepairExeption("Cannot request CarRepairData with an empty id");
               lock (_sync)
               {
                   if (Has(id))
                   {
                       RemoveAt(id);
                   }
                   value.Id = id;
                   _memCache.Add(value);
               }
           }
       }


       public System.Collections.Generic.List<CarRepairData> All => _memCache.Select(x => x).ToList();

       public void Add(CarRepairData value)
       {
           if (value.Id != Guid.Empty) throw new IncorrectCarRepairExeption($"Cannot add value with predefined id {value.Id}");
           value.Id = Guid.NewGuid();
           this[value.Id] = value;
       }

       public bool Has(Guid id)
       {
           return _memCache.Any(x => x.Id == id);
       }

       public void RemoveAt(Guid id)
       {
           lock (_sync)
           {
               _memCache.RemoveAll(x => x.Id == id);
           }
       }
   }
}