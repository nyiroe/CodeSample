using System.Data.Entity;
using WpfMvvmEf6Example.Interfaces;

namespace WpfMvvmEf6Example
{
    public static class Extensions
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
            {
                ItemState state = entry.Entity.State;
                entry.State = state.ConvertState();
            }
        }

        public static EntityState ConvertState(this ItemState state)
        {
            switch (state)
            {
                case ItemState.Added:
                    return EntityState.Added;
                case ItemState.Modified:
                    return EntityState.Modified;
                case ItemState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
