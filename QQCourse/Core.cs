using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class Core
    {
        public static int VOID = -666;
        public static int NONE = -1;

        public static Data.Users CurrentUser;

        private static Data.mi_TestPassingSystemEntities _Database;

        public static Data.mi_TestPassingSystemEntities Database
        {
            get
            {
                if (_Database == null)
                {
                    _Database = new Data.mi_TestPassingSystemEntities();
                }
                return _Database;
            }
        }

        public static MainWindow AppMainWindow;

        public static string VereficationCode = null;

        public static void CancelAllChanges()
        {
            var ChangedEntries = Database.ChangeTracker.Entries()
                .Where(E=>E.State!=System.Data.EntityState.Unchanged)
                .ToList();
            foreach (DbEntityEntry item in ChangedEntries)
            {
                CancelChanges(item);
            }
        }

        public static void CancelChanges(object Entity)
        {
            DbEntityEntry Entry = Database.Entry(Entity);
            try
            {
                switch(Entry.State)
                {
                    case System.Data.EntityState.Added:
                        Entry.State = System.Data.EntityState.Detached;
                        break;
                    case System.Data.EntityState.Modified:
                        Entry.CurrentValues.SetValues(Entry.OriginalValues);
                        Entry.State = System.Data.EntityState.Unchanged;
                        break;
                    case System.Data.EntityState.Deleted:
                        Entry.State = System.Data.EntityState.Unchanged;
                        break;
                }
            }catch
            {
                Entry.Reload();
            }
        }
    }
}
