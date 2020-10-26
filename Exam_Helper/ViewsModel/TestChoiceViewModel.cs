﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Exam_Helper.ViewsModel
{
    public class TestChoiceViewModel
    {
        public TestChoiceViewModel()
        {
            
        }

        public string [] TestMethodsNames { get; set; }
        public int[] TestsMethodsIds { get; set; }
        
        [Range(1,100000,ErrorMessage ="choose smth blyat")]
        public int SelectedId { get; set; }
    }





}
