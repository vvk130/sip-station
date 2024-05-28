using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Course
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? LastModified { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
