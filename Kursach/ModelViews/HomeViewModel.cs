﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.ModelViews
{ 
    public class HomeViewModel
    {
    public HomeView[] Fanfics { get; set; }
    }
public class HomeView
{
    public int Ref { get; set; }
    public string Name { get; set; }
    public object Image { get; set; }
    public DateTime EndDate { get; set; }
    public string requiredCh { get; set; }
    public string endedCh { get; set; }
}
}
