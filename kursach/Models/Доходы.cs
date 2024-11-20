using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class Доходы
{
    public int IdDohod { get; set; }

    public string НазваниеДохода { get; set; } = null!;

    public int СуммаДохода { get; set; }

    public DateOnly ДатаДохода { get; set; }

    public string? КатегорияДохода { get; set; }

    public virtual ICollection<Info> Infos { get; set; } = new List<Info>();
}
