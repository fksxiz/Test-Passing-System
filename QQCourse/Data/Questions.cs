//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QQCourse.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questions
    {
        public Questions()
        {
            this.Answers = new HashSet<Answers>();
        }
    
        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public Nullable<int> TestId { get; set; }
    
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual Tests Tests { get; set; }
    }
}