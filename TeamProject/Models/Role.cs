using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

public partial class Role
{
    public Role()
    {

    }

    public int Id { get; set; }

    [Required]
    public string Description { get; set; }

}
