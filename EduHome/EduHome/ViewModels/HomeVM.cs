﻿using EduHome.Models;
using System.Collections.Generic;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public Header Headers { get; set; }
        public Footer Footers { get; set; }
       
       
    }
    
}
