using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class Info
{
    public int Id { get; set; }

    public int IdDohod { get; set; }

    public int IdRashod { get; set; }

    public int Raznisa { get; set; }

    public virtual Доходы IdDohodNavigation { get; set; } = null!;

    public virtual Расходы IdRashodNavigation { get; set; } = null!;

	
}

