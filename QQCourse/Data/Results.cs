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
    
    public partial class Results
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> TestId { get; set; }
        public double Score { get; set; }
        public System.TimeSpan Time { get; set; }
    
        public virtual Tests Tests { get; set; }
        public virtual Users Users { get; set; }
    }
}
