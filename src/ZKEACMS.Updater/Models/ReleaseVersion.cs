﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKEACMS.Updater.Models
{
    public class ReleaseVersion
    {
        public string Version { get; set; }
        public VersionInfo[] Versions { get; set; }
    }
}
