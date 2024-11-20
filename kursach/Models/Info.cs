using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class Info
{
    public int Id { get; set; }

    public int? IdDohod { get; set; }

    public int? IdRashod { get; set; }

    public int? IdSberezh { get; set; }

    public int? СуммаДохода { get; set; }

    public int? СуммаРасходов { get; set; }

    public int? СуммаСбережений { get; set; }

    public virtual Доходы? IdDohodNavigation { get; set; }

    public virtual Сбережения? IdSberezhNavigation { get; set; }
}
