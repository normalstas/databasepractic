using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class Расходы
{
    public int IdRashod { get; set; }

    public string НазваниеРасхода { get; set; } = null!;

    public int СуммаРасхода { get; set; }

    public DateOnly ДатаРасхода { get; set; }

    public string КатегорияРасхода { get; set; } = null!;

    public virtual ICollection<Info> Infos { get; set; } = new List<Info>();
}
