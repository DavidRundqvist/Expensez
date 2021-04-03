﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez {
    public static class Constants {
        public const string RootFolder = @"\\nasgul\David\Expenses";

        public static CategoryPresentation DefaultCategory { get; } = new(new Category("Övrigt", "White", new[] { ".*" }));
    }
}
