using System;
using System.Collections.Generic;

namespace SipStation;

public partial class Answer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Content { get; set; } = null!;

    public int Upvotes { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual Question Question { get; set; } = null!;
}
