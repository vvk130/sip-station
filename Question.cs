using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Question
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int Upvotes { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Course Course { get; set; } = null!;
}
