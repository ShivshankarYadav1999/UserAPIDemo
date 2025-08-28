using System;
using System.Collections.Generic;

namespace UserAPIDemo.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public int? ModifiedUserId { get; set; }
}
