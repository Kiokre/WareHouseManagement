
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
    
public partial class OutputInfo
{

    public string Id { get; set; }

    public string IdObject { get; set; }

    public string IdOutputInfo { get; set; }

    public int IdCustomer { get; set; }

    public Nullable<int> Count { get; set; }

    public string Status { get; set; }



    public virtual Customer Customer { get; set; }

    public virtual Object Object { get; set; }

    public virtual Output Output { get; set; }

}

}
