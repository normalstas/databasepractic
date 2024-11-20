using System;
using System.Collections.Generic;

namespace kursach.Models;

public partial class Сбережения
{
    public int IdSberezh { get; set; }

    public string ТипСбережения { get; set; } = null!;

    public int Сбережения1 { get; set; }

    public string НаЧто { get; set; } = null!;

    public virtual ICollection<Info> Infos { get; set; } = new List<Info>();
}
