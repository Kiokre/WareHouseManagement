
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WareHouseManagement.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Output
{

    public string Id { get; set; }

    public Nullable<System.DateTime> DateInput { get; set; }



    public virtual OutputInfo OutputInfo { get; set; }

}

}